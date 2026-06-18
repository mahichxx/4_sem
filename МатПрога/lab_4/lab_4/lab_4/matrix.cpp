#include "matrix.h"
#include <iostream>
#include <climits>
#include <cstring>

#define INFINITY INT_MAX

// рекурсивное решение
int OptimalM(int i, int j, int n, const int c[], int* s) {
#define OPTIMALM_S(x1,x2) (s[(x1-1)*n + (x2-1)])

    if (i == j) return 0;

    int best = INFINITY;
    for (int k = i; k < j; ++k) {
        int q = OptimalM(i, k, n, c, s)
            + OptimalM(k + 1, j, n, c, s)
            + c[i - 1] * c[k] * c[j];
        if (q < best) {
            best = q;
            OPTIMALM_S(i, j) = k;
        }
    }
    return best;

#undef OPTIMALM_S
}

// динамическое программирование
int OptimalMD(int n, const int c[], int* s) {
#define OPTIMALM_S(x1,x2) (s[(x1-1)*n + (x2-1)])
#define OPTIMALM_M(x1,x2) (M[(x1-1)*n + (x2-1)])

    int* M = new int[n * n];

    for (int i = 1; i <= n; ++i)
        OPTIMALM_M(i, i) = 0;

    for (int l = 2; l <= n; ++l) {
        for (int i = 1; i <= n - l + 1; ++i) {
            int j = i + l - 1;
            OPTIMALM_M(i, j) = INFINITY;
            for (int k = i; k <= j - 1; ++k) {
                int q = OPTIMALM_M(i, k)
                    + OPTIMALM_M(k + 1, j)
                    + c[i - 1] * c[k] * c[j];
                if (q < OPTIMALM_M(i, j)) {
                    OPTIMALM_M(i, j) = q;
                    OPTIMALM_S(i, j) = k;
                }
            }
        }
    }

    int res = OPTIMALM_M(1, n);
    delete[] M;
    return res;

#undef OPTIMALM_M
#undef OPTIMALM_S
}

// восстановление скобок по матрице S
void PrintOptimalParens(int i, int j, int n, const int* s) {
#define OPTIMALM_S(x1,x2) (s[(x1-1)*n + (x2-1)])

    if (i == j) {
        std::cout << "A" << i;
        return;
    }
    std::cout << "(";
    int k = OPTIMALM_S(i, j);
    PrintOptimalParens(i, k, n, s);
    PrintOptimalParens(k + 1, j, n, s);
    std::cout << ")";

#undef OPTIMALM_S
}
