#include <iostream>
#include <algorithm>
using namespace std;

const int INF = 1000000000;

void buildMatrix(int d[5][5]) {
    int n = 7;

    d[0][0] = INF;
    d[0][1] = n;          // 7
    d[0][2] = 19 + n;     // 26
    d[0][3] = 3 + n;      // 10
    d[0][4] = 22 - n;     // 15

    d[1][0] = 1 + n;      // 8
    d[1][1] = INF;
    d[1][2] = 12 + n;     // 19
    d[1][3] = 40 - n;     // 33
    d[1][4] = 68 - n;     // 61

    d[2][0] = 7 + n;      // 14
    d[2][1] = 2 * n;      // 14
    d[2][2] = INF;
    d[2][3] = 72;
    d[2][4] = 37 + n;     // 44

    d[3][0] = 18 + n;     // 25
    d[3][1] = 55 - n;     // 48
    d[3][2] = 3 * n;      // 21
    d[3][3] = INF;
    d[3][4] = 2 * n;      // 14

    d[4][0] = 85 - n;     // 78
    d[4][1] = 32 + n;     // 39
    d[4][2] = 45;
    d[4][3] = 12 + n;     // 19
    d[4][4] = INF;
}

// Печать матрицы
void printMatrix(int d[5][5]) {
    cout << "Матрица расстояний:\n";
    for (int i = 0; i < 5; i++) {
        for (int j = 0; j < 5; j++) {
            if (d[i][j] >= INF / 2) cout << "INF\t";
            else cout << d[i][j] << "\t";
        }
        cout << "\n";
    }
    cout << "\n";
}

// Метод ветвей и границ 
int bestCostBB = INF;
int bestPathBB[5];

void branchAndBound(int d[5][5], int path[5], bool used[5],
    int currentCost, int level, int start) {

    if (currentCost >= bestCostBB) return;

    if (level == 5) {
        int last = path[level - 1]; 
        if (d[last][start] >= INF / 2) return;

        int totalCost = currentCost + d[last][start];
        if (totalCost < bestCostBB) {
            bestCostBB = totalCost;
            for (int i = 0; i < 5; i++) bestPathBB[i] = path[i];
        }
        return;
    }

    int last = path[level - 1];

    for (int next = 0; next < 5; next++) {
        if (!used[next] && d[last][next] < INF / 2) {
            used[next] = true;
            path[level] = next;
            branchAndBound(d, path, used, currentCost + d[last][next], level + 1, start);
            used[next] = false;
        }
    }
}

// Полный перебор 
int bruteForceTSP(int d[5][5], int bestPathBrute[5]) {
    int perm[4] = { 1, 2, 3, 4 };
    int bestCost = INF;

    do {
        int cost = 0;
        int cur = 0;
        bool ok = true;

        for (int i = 0; i < 4; i++) {
            int nxt = perm[i];
            if (d[cur][nxt] >= INF / 2) { ok = false; break; }
            cost += d[cur][nxt];
            cur = nxt;
        }

        if (ok && d[cur][0] < INF / 2) {
            cost += d[cur][0];
            if (cost < bestCost) {
                bestCost = cost;
                bestPathBrute[0] = 0;
                for (int i = 0; i < 4; i++) bestPathBrute[i + 1] = perm[i];
            }
        }

    } while (next_permutation(perm, perm + 4));

    return bestCost;
}

//  Печать маршрута 
void printPath(int path[5], int totalCost) {
    cout << "Маршрут: ";
    for (int i = 0; i < 5; i++) cout << path[i] + 1 << " -> ";
    cout << path[0] + 1 << "\n";
    cout << "Длина маршрута: " << totalCost << "\n\n";
}

int main() {
    setlocale(LC_ALL, "Russian");

    int d[5][5];
    buildMatrix(d);
    printMatrix(d);

    cout << "    Метод ветвей и границ    \n";

    int path[5];
    bool used[5] = { false };

    int start = 0;
    path[0] = start;
    used[start] = true;

    branchAndBound(d, path, used, 0, 1, start);

    if (bestCostBB < INF) {
        cout << "Лучший маршрут (ветви и границы):\n";
        printPath(bestPathBB, bestCostBB);
    }

    cout << "    Полный перебор    \n";

    int bestPathBrute[5];
    int bestCostBrute = bruteForceTSP(d, bestPathBrute);

    printPath(bestPathBrute, bestCostBrute);

    cout << "Сравнение:\n";
    cout << "Ветви и границы: " << bestCostBB << "\n";
    cout << "Перебор:          " << bestCostBrute << "\n";

    if (bestCostBB == bestCostBrute)
        cout << "Результаты совпадают.\n";
    else
        cout << "Результаты не совпадают.\n";

    return 0;
}
