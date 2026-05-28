#include "LCS.h"
#include <algorithm>
#include <cstring>
#include <iostream>

// Рекурсия (медленно)
int lcs_rec(int lenx, const char x[], int leny, const char y[]) {
    if (lenx == 0 || leny == 0) return 0;

    if (x[lenx - 1] == y[leny - 1])
        return 1 + lcs_rec(lenx - 1, x, leny - 1, y);
    else
        return std::max(lcs_rec(lenx - 1, x, leny, y), lcs_rec(lenx, x, leny - 1, y));
}

// Динамическое программирование (быстро)
int lcs_dp(const char x[], const char y[], char z[]) {
    int nx = (int)strlen(x);
    int ny = (int)strlen(y);

    // C[i][j] - таблица длин
    // Используем одномерный массив для имитации двумерного: C[i*(ny+1) + j]
    int* C = new int[(nx + 1) * (ny + 1)];

    // Инициализация нулями
    for (int i = 0; i <= nx; i++) C[i * (ny + 1) + 0] = 0;
    for (int j = 0; j <= ny; j++) C[0 * (ny + 1) + j] = 0;

    // Заполнение таблицы
    for (int i = 1; i <= nx; i++) {
        for (int j = 1; j <= ny; j++) {
            if (x[i - 1] == y[j - 1]) {
                C[i * (ny + 1) + j] = C[(i - 1) * (ny + 1) + (j - 1)] + 1;
            }
            else {
                C[i * (ny + 1) + j] = std::max(C[(i - 1) * (ny + 1) + j], C[i * (ny + 1) + (j - 1)]);
            }
        }
    }

    // Вывод матрицы C в консоль (как требует задание)
    std::cout << "\nМатрица C (длины):\n  ";
    for (int j = 0; j < ny; j++) std::cout << y[j] << " ";
    std::cout << "\n";
    for (int i = 0; i <= nx; i++) {
        if (i > 0) std::cout << x[i - 1] << " "; else std::cout << "  ";
        for (int j = 0; j <= ny; j++) {
            std::cout << C[i * (ny + 1) + j] << " ";
        }
        std::cout << "\n";
    }

    int length = C[nx * (ny + 1) + ny];

    // Восстановление подпоследовательности (идём с конца таблицы)
    int i = nx, j = ny, k = length;
    z[k] = '\0';
    while (i > 0 && j > 0) {
        if (x[i - 1] == y[j - 1]) {
            z[--k] = x[i - 1];
            i--; j--;
        }
        else if (C[(i - 1) * (ny + 1) + j] >= C[i * (ny + 1) + (j - 1)]) {
            i--;
        }
        else {
            j--;
        }
    }

    delete[] C;
    return length;
}