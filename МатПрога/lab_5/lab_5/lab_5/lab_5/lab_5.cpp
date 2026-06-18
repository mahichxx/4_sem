#include <iostream>
#include <iomanip>
using namespace std;

int main() {
    setlocale(LC_ALL, "RU");
    int N = 7;

    // Матрица стоимостей 6×6 (с запасом под фиктивного поставщика)
    int C[6][6] = {
        {N + 12, N + 2,  N + 6,  N + 3,  N + 11, N + 1},
        {N + 10, N,    N + 8,  N + 5,  N + 7,  N + 13},
        {N + 1,  N + 5,  N + 11, N + 8,  N + 2,  N + 11},
        {N + 4,  N + 10, N + 10, N + 3,  N + 13, N + 2},
        {N + 3,  N + 11, N + 9,  N,    N + 10, N + 4},
        {0, 0, 0, 0, 0, 0} // 6-я строка для фиктивного поставщика (стоимость 0)
    };

    // Запасы поставщиков (до 6)
    int A[6] = { 168 + N, 113 + N, 150 + N, 159 + N, 100 + N, 0 };

    // Потребности потребителей (6)
    int B[6] = { 143 + N, 107 + N, 131 + N, 193 + N, 95 + N, 163 + N };

    cout << "Матрица стоимостей (C):\n";
    for (int i = 0; i < 5; i++) {
        for (int j = 0; j < 6; j++)
            cout << setw(6) << C[i][j];
        cout << endl;
    }

    // Проверка открытая/закрытая
    int sumA = 0, sumB = 0;
    for (int i = 0; i < 5; i++) sumA += A[i];
    for (int j = 0; j < 6; j++) sumB += B[j];

    cout << "\nСумма запасов = " << sumA;
    cout << "\nСумма потребностей = " << sumB << "\n";

    bool open = false;
    int fakeSupply = 0;

    if (sumA < sumB) {
        open = true;
        fakeSupply = sumB - sumA;
        A[5] = fakeSupply; // фиктивный поставщик в 6-ю ячейку
        cout << "\nЗадача открытая. Добавляем фиктивного поставщика с запасом = "
            << fakeSupply << "\n";
    }

    int m = open ? 6 : 5; // количество поставщиков
    int n = 6;            // количество потребителей

    // План перевозок
    int X[6][6] = { 0 };

    // Копии запасов и потребностей для метода СЗ-угла
    int a_copy[6], b_copy[6];
    for (int i = 0; i < m; i++) a_copy[i] = A[i];
    for (int j = 0; j < n; j++) b_copy[j] = B[j];

    // Метод северо-западного угла
    int row = 0, col = 0;
    while (row < m && col < n) {
        int v = (a_copy[row] < b_copy[col]) ? a_copy[row] : b_copy[col];
        X[row][col] = v;
        a_copy[row] -= v;
        b_copy[col] -= v;

        if (a_copy[row] == 0) row++;
        else col++;
    }

    cout << "\nОпорный план X (Северо-западный угол):\n";
    for (int r = 0; r < m; r++) {
        for (int c = 0; c < n; c++)
            cout << setw(6) << X[r][c];
        cout << endl;
    }

    // Стоимость плана
    long long cost = 0;
    for (int r = 0; r < m; r++)
        for (int c = 0; c < n; c++)
            cost += 1LL * X[r][c] * C[r][c];

    cout << "\nСтоимость опорного плана = " << cost << "\n";

    int u[6], v[6];
    bool u_defined[6] = { false }, v_defined[6] = { false };

    // По правилу: u1 = 0
    u[0] = 0;
    u_defined[0] = true;

    // Находим потенциалы из уравнения u[i] + v[j] = C[i][j] для занятых клеток (X > 0)
    for (int iter = 0; iter < 12; iter++) { // Макс кол-во проходов
        for (int r = 0; r < m; r++) {
            for (int c = 0; c < n; c++) {
                if (X[r][c] > 0) {
                    if (u_defined[r] && !v_defined[c]) {
                        v[c] = C[r][c] - u[r];
                        v_defined[c] = true;
                    }
                    else if (!u_defined[r] && v_defined[c]) {
                        u[r] = C[r][c] - v[c];
                        u_defined[r] = true;
                    }
                }
            }
        }
    }

    cout << "\nПотенциалы u: ";
    for (int r = 0; r < m; r++) cout << u[r] << " ";
    cout << "\nПотенциалы v: ";
    for (int c = 0; c < n; c++) cout << v[c] << " ";
    cout << endl;

    cout << "\nОценки свободных клеток\n";
    bool optimal = true;
    for (int r = 0; r < m; r++) {
        for (int c = 0; c < n; c++) {
            if (X[r][c] == 0) {
                int delta = u[r] + v[c] - C[r][c];
                cout << setw(6) << delta;
                if (delta > 0) optimal = false;
            }
            else {
                cout << setw(6) << "базис";
            }
        }
        cout << endl;
    }

    if (optimal) cout << "\nРезультат: План оптимален.\n";
    else cout << "\nРезультат: План не оптимален (есть оценки > 0).\n";

    return 0;
}