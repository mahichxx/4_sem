#include <iostream>
#include <string>
#include "Levenshtein.h"

int main() {
    setlocale(LC_ALL, "rus");

    std::string x, y;
    std::cout << "Введите строку X: ";
    std::cin >> x;
    std::cout << "Введите строку Y: ";
    std::cin >> y;

    int lx = static_cast<int>(x.size());
    int ly = static_cast<int>(y.size());

    int d_rec = levenshtein_rec(lx, x.c_str(), ly, y.c_str());
    int d_dp = levenshtein_dp(lx, x.c_str(), ly, y.c_str());

    std::cout << "\nРасстояние Левенштейна (рекурсия): " << d_rec << "\n";
    std::cout << "Расстояние Левенштейна (ДП):        " << d_dp << "\n";

    return 0;
}
