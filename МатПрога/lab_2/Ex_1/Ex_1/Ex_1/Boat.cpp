#include "pch.h"
#include "Boat.h"

#define NINF 0x80000000

static int calcv(const combi::xcombination& xc, const int v[]) {
    int sum = 0;
    for (int i = 0; i < xc.m; i++) sum += v[xc.ntx(i)];
    return sum;
}

// Доход текущего набора сочетаний
static int calcc(const combi::xcombination& xc, const int c[]) {
    int sum = 0;
    for (int i = 0; i < xc.m; i++) sum += c[xc.ntx(i)];
    return sum;
}

int boat_s(int V, short m, short n, const int v[], const int c[], short res[]) {
    combi::xcombination xc(n, m);
    int maxProfit = NINF;

    short status = xc.getfirst();
    while (status >= 0) {
        if (calcv(xc, v) <= V) {
            int currentProfit = calcc(xc, c);
            if (currentProfit > maxProfit) {
                maxProfit = currentProfit;
                // Сохраняем индексы контейнеров
                for (int i = 0; i < m; i++) res[i] = xc.ntx(i);
            }
        }
        status = xc.getnext();
    }
    return maxProfit;
}