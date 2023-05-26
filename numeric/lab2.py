from sympy import sign, Matrix, cos, pi, pprint,init_printing

init_printing(wrap_line=False)

def gauss(A, det=1, eq=0, var=0):
    n, m = len(A), (len(A[0])-1)//2
    big_m = len(A[0])
  
    # дійшли до кінця
    if eq == n or var == m:
        if any(A[k][m] != 0 for k in range(eq, n)):
            return None # немає розв'язку
        
        if n >= m and A[m-1][m-1] != 0:
            print("Детермінант: ", det)
            return [A[k][m] for k in range(m)]
        
        return None # нескінченна к-ть розв'язків
    
    # обираємо головний елемент
    for k in range(eq, n):
        if A[k][var] != 0:
            A[eq], A[k] = A[k], A[eq]
            break
    
    # якщо ненульових немає, переходимо до наступного р-ння
    if A[eq][var] == 0:
        return gauss(A, eq, var+1)
    
    # ділимо на головний елемент
    for i in range(var+1, big_m):
        A[eq][i] /= A[eq][var]
    det *= A[eq][var]
    A[eq][var] = 1
    
    # виключаємо невідомі
    for k in range(n):
        if k == eq or A[k][var] == 0: continue
        coef = A[k][var]
        for i in range(var, big_m):
            A[k][i] -= coef*A[eq][i]
    
    # переходимо до наступного р-ння
    return gauss(A, det, eq+1, var+1)


n = 10
A_b = [[0] * (2*n+1) for i in range(n)]
A_0 = [[0] * (n+1) for i in range(n)]
for i in range(n):
  for j in range(n):
    if j == 0: 
      A_b[i][j] = 1
      A_0[i][j] = 1
    elif j == i: 
      A_b[i][j] = 1 + 0.75*i
      A_0[i][j] = 1 + 0.75*i
    elif i == j+1: 
      A_b[i][j] = 1
      A_0[i][j] = 1
  A_b[i][n] = n*cos(pi/(n-i))
  A_0[i][n] = n*cos(pi/(n-i))
  for j in range(n+1, 2*n+1):
    if i == j-n-1: A_b[i][j]=1
A_b[0][n-1] = 1
A_0[0][n-1] = 1

print("Метод Гаусса:")
print()
x = Matrix(gauss(A_b)).n(10)
print()

A_inv = Matrix.zeros(n,n)
A = Matrix.zeros(n,n)
for i in range(n):
  for j in range(n):
    A_inv[i,j] = A_b[i][j+n+1]
    A[i,j] = A_0[i][j]
print("Обернена матриця: ")
pprint(A_inv.n(2))
print()
print("Добуток оберненої та початкової матриць: ")
pprint((A_inv*A).n(2))
print()
print("Число обумовленості: ", A.norm()*A_inv.norm())
print()
print("Розв'язок системи: ")
pprint(x)

r = Matrix.zeros(n,1)
for i in range(n):
  for j in range(n):
    r[i] += A_0[i][j]*x[j]
  r[i] -= A_0[i][n]
print()
print("Нев'язка:")
pprint(r.n(2))


# returns 0 if A is divergent, else
# returns q which is used to compute number of iterations
def get_q(A):
    n, _ = A.shape
    q = []
    for i in range(n):
        s = 0
        for j in range(n):
            s += abs(A[i, j]) if i != j else 0
        if s > abs(A[i, i]):
            return 0
        else:
            q.append(s / abs(A[i, i]))
    return max(q)
  
# Zeidel method
def zeidel(A, b, eps, iters=100):
    # check that method will converge
    assert get_q(A) != 0
    
    n = len(b)
    x = Matrix.zeros(n, 1)

    for i in range(n):
        b[i] /= A[i, i]
        A[i, :] /= A[i, i]
        A[i, i] = 0
        
    for j in range(iters):
        x_prev = x.copy()
        for i in range(n):
            x[i] = -A[i, :].dot(x) + b[i]
        if ((x-x_prev).norm() < eps): 
          print("Кількість ітерацій: ", j+1)
          break

    return x

n = 10
A = Matrix.zeros(n, n)
b = Matrix.zeros(n,1)

for i in range(n):
  for j in range(n):
    if j == 0: A[i,j] = 1
    elif j == i: A[i,j] = 1 + 0.75*i
    elif i == j+1: A[i,j] = 1
  b[i] = n*cos(pi/(n-i))
A[0,n-1] = 1
A_0 = A.copy()
b_0 = b.copy()
print("Метод Зейделя:")
x = zeidel(A, b, 0.0001).n(10)
pprint(x)
r = Matrix.zeros(n,1)
for i in range(n):
  for j in range(n):
    r[i] += A_0[i,j]*x[j]
  r[i] -= b[i]
print("Нев'язка: ")
pprint(r.n(6))
  