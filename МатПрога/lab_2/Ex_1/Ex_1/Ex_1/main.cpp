#include "pch.h"
#include "Combi.h"
#include "Boat.h"
#include <ctime>

#define NINF 0x80000000

// Демонстрация заданий 1-4
void demo_subset()
{
    //2^n
    std::cout << "--- Генератор подмножеств (множество {A,B,C,D}) ---\n";
    char AA[] = { 'A', 'B', 'C', 'D' };
    const int n = sizeof(AA) / sizeof(AA[0]);
    combi::subset s(n);
    int k = s.getfirst();
    while (k >= 0)
    {
        std::cout << "{ ";
        for (int i = 0; i < k; ++i)
            std::cout << AA[s.ntx(i)] << (i < k - 1 ? ", " : " ");
        std::cout << "}\n";
        k = s.getnext();
    }
    std::cout << "Всего: " << s.count() << "\n\n";
}

void demo_combination()
{
    //C
    std::cout << "--- Генератор сочетаний C(5,3) (множество {A,B,C,D,E}) ---\n";
    char AA[] = { 'A', 'B', 'C', 'D', 'E' };
    const int n = sizeof(AA) / sizeof(AA[0]);
    const int m = 3;
    combi::xcombination xc(n, m);
    int k = xc.getfirst();
    while (k >= 0)
    {
        std::cout << xc.nc << ": { ";
        for (int i = 0; i < k; ++i)
            std::cout << AA[xc.ntx(i)] << (i < k - 1 ? ", " : " ");
        std::cout << "}\n";
        k = xc.getnext();
    }
    std::cout << "Всего: " << xc.count() << "\n\n";
}

void demo_permutation()
{
    //n!
    std::cout << "--- Генератор перестановок (множество {A,B,C,D}) ---\n";
    char AA[] = { 'A', 'B', 'C', 'D' };
    const int n = sizeof(AA) / sizeof(AA[0]);
    combi::permutation p(n);
    std::int64_t k = p.getfirst();
    while (k >= 0)
    {
        std::cout << p.np << ": { ";
        for (int i = 0; i < n; ++i)
            std::cout << AA[p.ntx(i)] << (i < n - 1 ? ", " : " ");
        std::cout << "}\n";
        k = p.getnext();
    }
    std::cout << "Всего: " << p.count() << "\n\n";
}

void demo_accomodation()
{
    std::cout << "--- Генератор размещений A(4,3) (множество {A,B,C,D}) ---\n";
    char AA[] = { 'A', 'B', 'C', 'D' };
    const int n = sizeof(AA) / sizeof(AA[0]);
    const int m = 3;
    combi::accomodation a(n, m);
    int k = a.getfirst();
    while (k >= 0)
    {
        std::cout << a.na << ": { ";
        for (int i = 0; i < k; ++i)
            std::cout << AA[a.ntx(i)] << (i < k - 1 ? ", " : " ");
        std::cout << "}\n";
        k = a.getnext();
    }
    std::cout << "Всего: " << a.count() << "\n\n";
}

// Оптимальная загрузка судна 
void task5_boat() {
    const int n = 25;      // контейнеров всего
    const int m_slots = 5; // мест на судне
    const int V_max = 1500; // лимит веса

    int v[n] = { 0 };
    int c[n] = { 0 };
    short res_indices[m_slots] = { 0 };

    srand((unsigned)time(NULL));
    for (int i = 0; i < n; i++) {
        v[i] = 100 + rand() % 801; // 100 - 900 кг
        c[i] = 10 + rand() % 141;  // 10 - 150 у.е.
    }

    int maxProfit = boat_s(V_max, m_slots, n, v, c, res_indices);

    std::cout << "Ограничение веса: " << V_max << ", Мест: " << m_slots << ", Контейнеров: " << n << "\n";

    if (maxProfit != NINF) {
        std::cout << "Выбраны контейнеры (индексы): ";
        int totalWeight = 0;
        for (int i = 0; i < m_slots; i++) {
            std::cout << res_indices[i] << " ";
            totalWeight += v[res_indices[i]];
        }
        std::cout << "\n";

        std::cout << "Общая прибыль: " << maxProfit << "\n";

        std::cout << "Общий вес выбранных: " << totalWeight << " кг\n";
    }
    std::cout << std::endl;
}

void task6_boat_research() {
    const int m_slots = 6;
    const int V_max = 2000;

    std::cout << "\nМест на судне: " << m_slots << std::endl;
    std::cout << std::left << std::setw(12) << "Кол-во конт" << " | " << "Время (мс)" << std::endl;

    for (int n = 25; n <= 35; n++) {
        int* v = new int[n];
        int* c = new int[n];
        short res[m_slots];

        for (int i = 0; i < n; i++) {
            v[i] = 100 + rand() % 801;
            c[i] = 10 + rand() % 141;
        }

        clock_t t1 = clock();
        boat_s(V_max, m_slots, n, v, c, res);
        clock_t t2 = clock();

        std::cout << std::right << std::setw(11) << n << " | " << (t2 - t1) << std::endl;

        delete[] v;
        delete[] c;
    }
    std::cout << std::endl;
}


int main()
{
    setlocale(LC_ALL, "rus");

    demo_subset();
    demo_combination();
    demo_permutation();
    demo_accomodation();

    task5_boat();
    task6_boat_research();

    system("pause");
    return 0;
}
