#include <algorithm>
#include "Levenshtein.h"

#define DD(i,j) d[(i)*(ly+1)+(j)]

static int min3(int x1, int x2, int x3) {
    return std::min(std::min(x1, x2), x3);
}

// Динамическое программирование
int levenshtein_dp(int lx, const char x[], int ly, const char y[]) {
    int* d = new int[(lx + 1) * (ly + 1)];

    for (int i = 0; i <= lx; ++i) DD(i, 0) = i;
    for (int j = 0; j <= ly; ++j) DD(0, j) = j;

    for (int i = 1; i <= lx; ++i) {
        for (int j = 1; j <= ly; ++j) {
            DD(i, j) = min3(
                DD(i - 1, j) + 1,
                DD(i, j - 1) + 1,
                DD(i - 1, j - 1) + (x[i - 1] == y[j - 1] ? 0 : 1)
            );
        }
    }

    int res = DD(lx, ly);
    delete[] d;
    return res;
}

// Рекурсивный вариант
int levenshtein_rec(int lx, const char x[], int ly, const char y[]) {
    if (lx == 0) return ly;
    if (ly == 0) return lx;

    int cost = (x[lx - 1] == y[ly - 1]) ? 0 : 1;

    int a = levenshtein_rec(lx - 1, x, ly, y) + 1;      // удаление
    int b = levenshtein_rec(lx, x, ly - 1, y) + 1;      // вставка
    int c = levenshtein_rec(lx - 1, x, ly - 1, y) + cost; // замена/совпадение

    return min3(a, b, c);
}
