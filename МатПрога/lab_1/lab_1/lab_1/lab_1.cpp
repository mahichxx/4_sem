#include <iostream>
#include <ctime>
#include <locale>
#include "Auxil.h"

#define CYCLE 10000000

int main()
{
    setlocale(LC_ALL, "rus");
    double av1 = 0, av2 = 0;
    clock_t t1 = 0, t2 = 0;

    auxil::start(); 
    t1 = clock();   

    for (int i = 0; i < CYCLE; i++)
    {
        av1 += (double)auxil::iget(-100, 100); 
        av2 += auxil::dget(-100, 100);      
    }

    t2 = clock(); 

    std::cout << "Количество циклов: " << CYCLE << std::endl;
    std::cout << "Среднее значение (int): " << av1 / CYCLE << std::endl;
    std::cout << "Среднее значение (double): " << av2 / CYCLE << std::endl;
    std::cout << "Продолжительность (у.е.): " << (t2 - t1) << std::endl;
    std::cout << "Продолжительность (сек): " << (double)(t2 - t1) / CLOCKS_PER_SEC << std::endl;

    std::cout << "\nn\tВремя (сек)\tРезультат" << std::endl;

    int tests[] = {5, 10, 25, 30, 35, 40};

    for (int n : tests) {
        clock_t f1 = clock();
        long long res = auxil::fibonacci(n);
        clock_t f2 = clock();

        double duration = (double)(f2 - f1) / CLOCKS_PER_SEC;
        std::cout << n << "\t" << duration << "\t\t" << res << std::endl;
    }

    system("pause");
    return 0;
}