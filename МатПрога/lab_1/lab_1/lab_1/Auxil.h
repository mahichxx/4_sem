#pragma once
#include <cstdlib>

namespace auxil
{
    void start();                          // Инициализация генератора случайных чисел
    double dget(double rmin, double rmax); // Случайное вещественное число
    int iget(int rmin, int rmax);          // Случайное целое число
    long long fibonacci(int n);
};