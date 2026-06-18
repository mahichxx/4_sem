#pragma once

#define OPTIMALM_PARM(x) ((int*)x)

int OptimalM( // рекурси€
    int i,
    int j,
    int n,
    const int c[],
    int* s
);

int OptimalMD( // динамическое программирование
    int n,
    const int c[],
    int* s
);

void PrintOptimalParens(int i, int j, int n, const int* s);
