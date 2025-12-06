//
// Created by fernudev on 3/11/25.
//

#ifndef SOLUTION_H
#define SOLUTION_H

#include <bits/stdc++.h>

class Solution {
public:
    static std::vector<int> get_repeated_missing_number(const std::vector<std::vector<int> > &matrix);
    static std::string integer_to_roman(int number);
    static int roman_to_integer(const std::string &roman);
    static std::string get_biggest_prefix(const std::vector<std::string> &words);
};

#endif //SOLUTION_H
