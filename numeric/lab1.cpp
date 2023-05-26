#include <bits/stdc++.h>

using namespace std;

double fi(double x) {
  return -pow(x+4,1.0/7);
}

double f(double x) {
  return pow(x, 7) + x + 4;
}

double f1(double x) {
  return 7*pow(x,6) + 1;
}

void pi_method(double x, int n, double q, double eps) {
  double x_prev = x;
  printf(" k %15s%15s%15s\n", "x_k", "|x_k-x_k-1|", "f(x_k)");
  printf(" %d %15.10f%15c%15.10f\n", 0, x,'-', f(x));
  for (int i = 1; i <= n; i++) {
    x_prev = x;
    x = fi(x);
    if (abs(x - x_prev) < eps*(1-q)/q) {
      cout << "*";
    } else {
      cout << " ";
    }
    printf("%d %15.10f%15.10f%15.10f\n", 
      i,x,abs(x - x_prev), f(x));
  }
}

void n_method(double x, int n, double eps) {
  double x_prev;
  printf(" k %15s%15s%15s\n", "x_k", "|x_k-x_k-1|", "f(x_k)");
  printf(" %d %15.10f%15c%15.10f\n", 0, x,'-', f(x));
  for (int i = 1; i <= n; i++) {
    x_prev = x;
    x = x_prev - f(x)/f1(x);
    if (abs(x - x_prev) < eps && abs(f(x)) < eps) {
      cout << "*";
    } else {
      cout << " ";
    }
    printf("%d %15.10f%15.10f%15.10f\n", 
      i,x,abs(x - x_prev), f(x));
  }
}
void mn_method(double x, double eps) {
  double x_prev = MAXFLOAT, f1_x0 = f1(x);
  int i = 0;
  printf(" k %15s%15s%15s\n", "x_k", "|x_k-x_k-1|", "f(x_k)");
  printf(" %d %15.10f%15c%15.10f\n", 0, x,'-', f(x));
  while (abs(x - x_prev) > eps || abs(f(x)) > eps) {
    i++;
    x_prev = x;
    x = x_prev - f(x)/f1_x0;
    printf("%2d %15.10f%15.10f%15.10f\n", 
      i,x,abs(x - x_prev), f(x));
  }
}

int main() {
  double x0 = -1.5, q = 0.07886, eps = 1e-5;
  cout << "Метод простої ітерації\n";
  pi_method(x0, 5, q, eps);
  cout << "Метод Ньютона\n";
  x0 = -1.25;
  n_method(x0, 5, eps);
  cout << "Модифікований метод Ньютона\n";
  mn_method(x0, eps);
  return 0;
}