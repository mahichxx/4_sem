#include <iostream>
#include <string>
#include <ctime>
#include <algorithm>
#include <vector>
#include <iomanip>
#include "Levenshtein.h"

int main() {
    setlocale(LC_ALL, "rus");

    std::string S1(300, 'a'); 
    std::string S2(200, 'b'); 

    // Значения k из методички
    double k_values[] = { 0.04, 0.05, 0.066, 0.1, 0.2, 0.5, 1.0 }; // 1/25, 1/20...

    std::cout << "Сравнение времени вычисления дистанции Левенштейна\n";
    std::cout << "\nДлина S1 | Длина S2 | Рекурсия (мс) | ДП (мс)\n";

    for (double k : k_values) {
        int len1 = static_cast<int>(S1.size() * k);
        int len2 = static_cast<int>(S2.size() * k);

        const char* s1_part = S1.substr(0, len1).c_str();
        const char* s2_part = S2.substr(0, len2).c_str();

        clock_t t3 = clock();
        levenshtein_dp(len1, s1_part, len2, s2_part);
        clock_t t4 = clock();
        double time_dp = static_cast<double>(t4 - t3);

        double time_rec = -1; 
        if (len1 <= 15 && len2 <= 15) {
            clock_t t1 = clock();
            levenshtein_rec(len1, s1_part, len2, s2_part);
            clock_t t2 = clock();
            time_rec = static_cast<double>(t2 - t1);
        }

        std::cout << std::setw(8) << len1 << " | "
            << std::setw(8) << len2 << " | ";

        if (time_rec >= 0) std::cout << std::setw(13) << time_rec;
        else std::cout << std::setw(13) << "long";

        std::cout << " | " << std::setw(7) << time_dp << "\n";
    }

    return 0;
}