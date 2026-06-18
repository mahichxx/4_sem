#pragma once
#include <string>

// Рекурсивное решение (возвращает только длину)
int lcs_rec(int lenx, const char x[], int leny, const char y[]);

// Решение методом динамического программирования (заполняет матрицы)
// Возвращает длину LCS и саму подпоследовательность в массив z
int lcs_dp(const char x[], const char y[], char z[]);