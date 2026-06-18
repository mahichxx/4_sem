#include "pch.h"
#include "Combi.h"

namespace combi
{
    subset::subset(short n)
    {
        this->n = n;
        sset = new short[n];
        reset();
    }

    void subset::reset()
    {
        sn = 0;
        mask = 0;
    }

    short subset::getfirst()
    {
        std::uint64_t buf = mask;
        sn = 0;
        for (short i = 0; i < n; ++i)
        {
            if (buf & 0x1)
                sset[sn++] = i;
            buf >>= 1;
        }
        return sn;
    }

    short subset::getnext()
    {
        if (++mask < count())
            return getfirst();
        return -1;
    }

    short subset::ntx(short i) const { return sset[i]; }

    std::uint64_t subset::count() const
    {
        return (std::uint64_t(1) << n);
    }

    //  вспомогательная функция факториала 
    static std::uint64_t fact(std::uint64_t x)
    {
        return (x == 0) ? 1 : x * fact(x - 1);
    }

    //  xcombination 
    xcombination::xcombination(short n, short m)
    {
        this->n = n;
        this->m = m;
        sset = new short[m + 2];
        reset();
    }

    void xcombination::reset()
    {
        nc = 0;
        for (short i = 0; i < m; ++i)
            sset[i] = i;
        sset[m] = n;
        sset[m + 1] = 0;
    }

    short xcombination::getfirst()
    {
        return (n >= m) ? m : -1;
    }

    short xcombination::getnext()
    {
        if (n < m) return -1;
        short j;
        for (j = 0; sset[j] + 1 == sset[j + 1]; ++j)
            sset[j] = j;
        if (j >= m) return -1;
        sset[j]++;
        nc++;
        return m;
    }

    short xcombination::ntx(short i) const { return sset[i]; }

    std::uint64_t xcombination::count() const
    {
        if (n < m) return 0;
        return fact(n) / (fact(n - m) * fact(m));
    }

    //  permutation 
    permutation::permutation(short n)
    {
        this->n = n;
        sset = new short[n];
        dart = new bool[n];
        reset();
    }

    void permutation::reset() { getfirst(); }

    std::int64_t permutation::getfirst()
    {
        np = 0;
        for (short i = 0; i < n; ++i)
        {
            sset[i] = i;
            dart[i] = L;
        }
        return (n > 0) ? static_cast<std::int64_t>(np) : -1;
    }

    std::int64_t permutation::getnext()
    {
        short maxm = -1, idx = -1;
        for (short i = 0; i < n; ++i)
        {
            if (i > 0 && dart[i] == L && sset[i] > sset[i - 1] && maxm < sset[i])
            {
                maxm = sset[i];
                idx = i;
            }
            if (i < n - 1 && dart[i] == R && sset[i] > sset[i + 1] && maxm < sset[i])
            {
                maxm = sset[i];
                idx = i;
            }
        }
        if (idx >= 0)
        {
            short dir = (dart[idx] == L) ? -1 : 1;
            std::swap(sset[idx], sset[idx + dir]);
            std::swap(dart[idx], dart[idx + dir]);
            for (short i = 0; i < n; ++i)
                if (sset[i] > maxm)
                    dart[i] = !dart[i];
            return ++np;
        }
        return -1;
    }

    short permutation::ntx(short i) const { return sset[i]; }

    std::uint64_t permutation::count() const { return fact(n); }
    accomodation::accomodation(short n, short m)
    {
        this->n = n;
        this->m = m;
        cgen = new xcombination(n, m);
        pgen = new permutation(m);
        sset = new short[m];
        reset();
    }

    accomodation::~accomodation()
    {
        delete[] sset;
        delete pgen;
        delete cgen;
    }

    void accomodation::reset()
    {
        na = 0;
        cgen->reset();
        pgen->reset();
        cgen->getfirst();
    }

    short accomodation::getfirst()
    {
        if (n < m) return -1;
        for (short i = 0; i < m; ++i)
            sset[i] = cgen->sset[pgen->ntx(i)];
        return m;
    }

    short accomodation::getnext()
    {
        na++;
        if (pgen->getnext() >= 0)
            return getfirst();
        if (cgen->getnext() > 0)
        {
            pgen->reset();
            return getfirst();
        }
        return -1;
    }

    short accomodation::ntx(short i) const { return sset[i]; }

    std::uint64_t accomodation::count() const
    {
        if (n < m) return 0;
        return fact(n) / fact(n - m);
    }
}