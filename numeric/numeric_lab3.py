#!/usr/bin/env python
# coding: utf-8

# In[38]:


import matplotlib.pyplot as plt
import numpy as np
from sympy import *


# In[39]:


def f(x):
  if (x >= -2) & (x <= 0):
    return -x**2 + 2
  if (x >= 0) & (x <= 1):
    return 1 - 8*(x-0.5)**3
  if (x >= 1):
    return x**2 - 2*x + 1  
  return None

def Pol(x, n):
  A = [[0]*(n+1) for i in range(n+1)]
  for i in range(n+1):
    A[i][0] = f(x[i])
    #print(x[i], ' ', A[i][0])
  for j in range(1,n+1):
    for i in range(n+1-j):
      A[i][j] = (A[i+1][j-1] - A[i][j-1])/(x[i+j]-x[i])
  print('P =',end=' ')
  z = Symbol('x')
  poly = 0
  res = Matrix.zeros(n+1,1)
  for i in range(n+1):
    res[i] = A[0][i]
    print(res[i].n(2) ,end='')
    term = res[i]
    for j in range(i):
      term *= z + x[j]
      if x[j] < 0:
        print('( x +',x[j].n(2)*(-1),')',end='')
      else:
        print('( x -',x[j].n(2),')',end='')
    if i != n: 
      print(' +')
    poly += term
    print()
  return res

def p(x, x_0, coefs):
  res = 0
  for i in range(n+1):
    t = 1;
    for j in range(i):
      t *= (x - x_0[j])
    res += t*coefs[i]
  return res


# In[40]:


n = 11
a = -2
b = 3

#рівновіддалені вузли
x1 = Matrix.zeros(n+1, 1)
for i in range(n+1):
  x1[i] = a + i*(b-a)/n

#чебишовські вузли
x2 = Matrix.zeros(n+1, 1)
for i in range(n+1):
  x2[i] = (a+b)/2+(b-a)/2*cos((2*i + 1)*pi/(2*(n+1)))


print("Рівновіддалені вузли")
Matrix(x1).n(2)


# In[41]:


res1 = Pol(x1,n)


# In[42]:


print("Чебишовські вузли")
Matrix(x2).n(2)


# In[43]:


res2 = Pol(x2.n(14),n)


# In[44]:


fig = plt.figure()
ax = fig.add_subplot(1, 1, 1)
ax.spines['left'].set_position('center')
ax.spines['bottom'].set_position('center')
ax.spines['right'].set_color('none')
ax.spines['top'].set_color('none')
ax.xaxis.set_ticks_position('bottom')
ax.yaxis.set_ticks_position('left')

x = np.arange(a,b + 0.01 ,0.1)

ax.plot(x, p(x,x1,res1), 'r', label='P_n_E(x)')
ax.plot(x, p(x,x2,res2), 'b', label='P_n_T(x)')
ax.plot(x, [f(i) for i in x], 'g', label='f(x)')


plt.legend(loc='upper left')
plt.ylim([-5, 5])
ax.set_xlim([-5,5])

plt.show()


# In[45]:


fig = plt.figure()
ax = fig.add_subplot(1, 1, 1)
ax.spines['left'].set_position('center')
ax.spines['bottom'].set_position('center')
ax.spines['right'].set_color('none')
ax.spines['top'].set_color('none')
ax.xaxis.set_ticks_position('bottom')
ax.yaxis.set_ticks_position('left')

x = np.arange(a,b + 0.01 ,0.1)

ax.plot(x, [f(i) for i in x]-p(x,x1,res1), 'r', label='f(x) - P_n_E(x)')
ax.plot(x, [f(i) for i in x]-p(x,x2,res2), 'b', label='f(x) - P_n_T(x)')

plt.legend(loc='upper left')
plt.ylim([-5, 5])
ax.set_xlim([-5,5])

plt.show()


# In[46]:


def gauss(A, eq=0, var=0):
    n, m = len(A), len(A[0])-1
    
    if eq == n or var == m:
        if any(A[k][m] != 0 for k in range(eq, n)):
            return None
        
        if n >= m and A[m-1][m-1] != 0:
            return [A[k][m] for k in range(m)]
        
        return None
    
    for k in range(eq, n):
        if A[k][var] != 0:
            A[eq], A[k] = A[k], A[eq]
            break
    
    if A[eq][var] == 0:
        return gauss(A, eq, var+1)
    
    for i in range(var+1, m+1):
        A[eq][i] /= A[eq][var]
    A[eq][var] = 1
    
    for k in range(n):
        if k == eq or A[k][var] == 0: continue
        coef = A[k][var]
        for i in range(var, m+1):
            A[k][i] -= coef*A[eq][i]
      
    return gauss(A, eq+1, var+1)


# In[47]:


h = (b-a)/n
C = [[0] * n for i in range(n-1)]
for i in range(0,n-1):
    if i!=0: C[i][i-1] = h
    C[i][i] = 2*(h+h)
    if i!=n-2: C[i][i+1] = h
    C[i][n-1] = 6*((f(x1[i+2])-f(x1[i+1]))/h - (f(x1[i+1])-f(x1[i]))/h)


# In[48]:


c = [0, *gauss(C), 0]
_a = [f(i) for i in x1]
d = [0] + [(c[i]-c[i-1])/h for i in range(1, n+1)]
_b = [0] + [h*c[i]/2-h**2*d[i]/6+(f(x1[i])-f(x1[i-1]))/h for i in range(1,n+1)]


# In[53]:


def build_spline(a, b, c, d, x_i):
    def spline(x):
        a + b*(x-x_i) + c*(x-x_i)**2/2 + d*(x-x_i)**3/6
    return spline

#s = [build_spline(a[i], b[i], c[i], d[i], x1[i]) for i in range(1,n+1)]

#for spline in s:
#    print(spline(Symbol('x')))
    
def spline(a, b, c, d, x_i, x, draw = 0):
    if (draw == 1):
        print(a, " + ", b, "(x - ", x_i, ") + ", c, "(x - ", x_i, ")^2/2 + ", d,"(x - ", x_i, ")^3/6")
        print()
    return a + b*(x-x_i) + c*(x-x_i)**2/2 + d*(x-x_i)**3/6

fig = plt.figure()
ax = fig.add_subplot(1, 1, 1)
ax.spines['left'].set_position('center')
ax.spines['bottom'].set_position('center')
ax.spines['right'].set_color('none')
ax.spines['top'].set_color('none')
ax.xaxis.set_ticks_position('bottom')
ax.yaxis.set_ticks_position('left')

x = np.arange(a,b + 0.01 ,0.05)
ax.plot(x, [f(i) for i in x], 'g', label='f(x)')

for i in range(1,n+1):
    x = np.arange(x1[i-1],x1[i] + 0.01 ,0.05)
    if i == 1: ax.plot(x, spline(_a[i], _b[i], c[i], d[i], x1[i], x, 1), 'r', label='s(x)')
    else : ax.plot(x, spline(_a[i], _b[i], c[i], d[i], x1[i], x, 1), 'r')
    

plt.legend(loc='upper left')
plt.ylim([-5, 5])
ax.set_xlim([-5,5])

plt.show()


# In[54]:


fig = plt.figure()
ax = fig.add_subplot(1, 1, 1)
ax.spines['left'].set_position('center')
ax.spines['bottom'].set_position('center')
ax.spines['right'].set_color('none')
ax.spines['top'].set_color('none')
ax.xaxis.set_ticks_position('bottom')
ax.yaxis.set_ticks_position('left')

for i in range(1,n+1):
    x = np.arange(x1[i-1],x1[i] + 0.01 ,0.05)
    f_part = [f(i) for i in x]
    if i == 1: ax.plot(x, f_part - spline(_a[i], _b[i], c[i], d[i], x1[i], x), 'r', label='f(x) - s(x)')
    else : ax.plot(x, f_part - spline(_a[i], _b[i], c[i], d[i], x1[i], x), 'r')
    

plt.legend(loc='upper left')
plt.ylim([-5, 5])
ax.set_xlim([-5,5])

plt.show()


# In[ ]:




