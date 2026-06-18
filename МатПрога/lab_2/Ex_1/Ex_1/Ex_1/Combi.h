#pragma once
#include <cstdint>

namespace combi
{
    // Генератор всех подмножеств (битовая маска)
    struct subset
    {
        short n;                    // количество элементов исходного множества
        short sn;                   // размер текущего подмножества
        short* sset;                 // индексы элементов
        std::uint64_t mask;          // битовая маска

        subset(short n = 1);
        void reset();
        short getfirst();            // первое подмножество (пустое)
        short getnext();             // следующее
        short ntx(short i) const;    // i-й индекс
        std::uint64_t count() const; // общее количество
    };

    // Генератор сочетаний из n по m
    struct xcombination
    {
        short n, m;                  // n – всего, m – размер сочетания
        short* sset;                  // индексы текущего сочетания
        std::uint64_t nc;             // номер сочетания (0...count-1)

        xcombination(short n = 1, short m = 1);
        void reset();
        short getfirst();
        short getnext();
        short ntx(short i) const;
        std::uint64_t count() const;
    };

    // Генератор перестановок (алгоритм Джонсона–Троттера)
    struct permutation
    {
        static const bool L = true;   // стрелка влево
        static const bool R = false;  // стрелка вправо
        short n;
        short* sset;                   // текущая перестановка индексов
        bool* dart;                     // направления стрелок
        std::uint64_t np;               // номер перестановки

        permutation(short n = 1);
        void reset();
        std::int64_t getfirst();        // первая перестановка
        std::int64_t getnext();         // следующая
        short ntx(short i) const;
        std::uint64_t count() const;
    };

    // Генератор размещений из n по m (сочетания + перестановки)
    struct accomodation
    {
        short n, m;
        short* sset;                    // индексы текущего размещения
        xcombination* cgen;              // генератор сочетаний
        permutation* pgen;               // генератор перестановок
        std::uint64_t na;                 // номер размещения

        accomodation(short n = 1, short m = 1);
        ~accomodation();
        void reset();
        short getfirst();
        short getnext();
        short ntx(short i) const;
        std::uint64_t count() const;
    };
}