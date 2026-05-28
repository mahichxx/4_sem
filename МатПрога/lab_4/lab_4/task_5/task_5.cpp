#include <iostream>
#include <ctime>
#include <iomanip>
#include "LCS.h"

int main() {
    setlocale(LC_ALL, "rus");

    // Данные 7 варианта
    const char* X = "AVTZNHI";
    const char* Y = "AQTRYW";
    char Z[100]; // сюда запишем результат

    std::cout << "Последовательность X: " << X << "\n";
    std::cout << "Последовательность Y: " << Y << "\n\n";

    // 1. Замер рекурсии
    clock_t t1 = clock();
    int len_rec = lcs_rec((int)strlen(X), X, (int)strlen(Y), Y);
    clock_t t2 = clock();

    std::cout << "Рекурсивное решение \n";
    std::cout << "Длина LCS: " << len_rec << "\n";
    std::cout << "Время: " << (double)(t2 - t1) / CLOCKS_PER_SEC << " сек.\n\n";

    // 2. Замер динамики
    clock_t t3 = clock();
    int len_dp = lcs_dp(X, Y, Z);
    clock_t t4 = clock();

    std::cout << "\n Динамическое программирование \n";
    std::cout << "Длина LCS: " << len_dp << "\n";
    std::cout << "Сама LCS:  " << Z << "\n";
    std::cout << "Время: " << (double)(t4 - t3) / CLOCKS_PER_SEC << " сек.\n";

    // Для графиков в отчете (Задание 3 и 5 требует зависимости от k)
    // Тебе нужно будет в цикле вызывать эти функции для подстрок разной длины
    // и записывать время в таблицу.

    return 0;
}