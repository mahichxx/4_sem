#include <iostream>
#include <string>
#include <random>
#include <ctime>

std::string randomLatinString(size_t len) {
    static const char alphabet[] = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    static std::mt19937 rng(static_cast<unsigned>(std::time(nullptr)));
    std::uniform_int_distribution<int> dist(0, sizeof(alphabet) - 2);

    std::string s;
    s.reserve(len);
    for (size_t i = 0; i < len; ++i) {
        s.push_back(alphabet[dist(rng)]);
    }
    return s;
}

int main() {
    setlocale(LC_ALL, "rus");

    std::string S1 = randomLatinString(300);
    std::string S2 = randomLatinString(200);

    std::cout << "S1 (300): " << S1 << "\n\n";
    std::cout << "S2 (200): " << S2 << "\n\n";

    return 0;
}