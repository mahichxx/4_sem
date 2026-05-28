#pragma once

int levenshtein_dp(
    int lx,
    const char x[],
    int ly,
    const char y[]
);

int levenshtein_rec(
    int lx,
    const char x[],
    int ly,
    const char y[]
);
