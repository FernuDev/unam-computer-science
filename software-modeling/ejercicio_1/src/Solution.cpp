//
// Created by fernudev on 3/11/25.
//

#include "Solution.h"

///
/// Función encargada de buscar el número faltante en una matriz de nxn
/// en un rango de [1, n] y también retornar el número que se encuentra repetido.
/// @param matrix std::vector<std::vector<int>> Matriz de números enteros que se analizará.
///
/// @return std::vector<int> Un vector con dos elementos: el primer elemento es el número repetido
/// y el segundo elemento es el número faltante en la matriz.
std::vector<int> Solution::get_repeated_missing_number(const std::vector<std::vector<int>> &matrix) {

    const size_t n = matrix.size();
    // Fórmula de Gauss
    int suma_estimada = ((n * n + 1) * (n * n)) / 2;
    int suma = 0;
    int repetido = 0;
    int faltante = 0;

    std::map<int, int> hashMap;

    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            if (hashMap.contains(matrix[i][j])) {
                repetido = matrix[i][j]; // Encontramos el valor repetido
            }
            hashMap[matrix[i][j]]++;
            suma += matrix[i][j];
        }
    }

    // El valor faltante es la diferencia entre la suma estimada y la suma real, menos el repetido
    faltante = suma_estimada - (suma - repetido);

    return {repetido, faltante};
}

///
/// Función que convierte un número entero del sistema decimal al sistema romano.
/// @param num Número entero a convertir en romano.
/// @return Retorna un std::string que representa a num en el sistema romano.
/// Si el número está fuera del rango válido (0 o mayor a 4000), se retorna una cadena vacía.
std::string Solution::integer_to_roman(int num) {

    if (num <= 0 || num >= 4000) {
        return ""; // Rango válido: 1 a 3999
    }

    std::string number;

    // Mapeamos los números romanos desde 1 hasta 1000 que son los que necesitaremos para construir el numero romano
    std::vector<std::pair<std::string, int>> roman;
    roman.emplace_back("M", 1000);
    roman.emplace_back("CM", 900);
    roman.emplace_back("D", 500);
    roman.emplace_back("CD", 400);
    roman.emplace_back("C", 100);
    roman.emplace_back("XC", 90);
    roman.emplace_back("L", 50);
    roman.emplace_back("XL", 40);
    roman.emplace_back("X", 10);
    roman.emplace_back("IX", 9);
    roman.emplace_back("V", 5);
    roman.emplace_back("IV", 4);
    roman.emplace_back("I", 1);

    for (const auto& [fst, snd] : roman) {
        while (num >= snd) {
            number += fst;
            num -= snd;
        }
    }

    return number;
}

///
/// Función encargada de calcular el equivalente de un número romano en entero en el sistema decimal.
/// @param roman Cadena de caracteres que representa un número romano.
/// @return Retorna la conversión de dicho número en un entero en el sistema decimal.
/// Si la cadena contiene caracteres no válidos, retorna 0.
int Solution::roman_to_integer(const std::string &roman) {

    for (auto c : roman) {
        if (c != 'I' && c != 'V' && c != 'X' &&
            c != 'L' && c != 'C' && c != 'D' && c != 'M') {
            return 0; // Caracter inválido
        }
    }

    std::map<char, int> roman_map;
    int number = 0;

    roman_map['I'] = 1;
    roman_map['V'] = 5;
    roman_map['X'] = 10;
    roman_map['L'] = 50;
    roman_map['C'] = 100;
    roman_map['D'] = 500;
    roman_map['M'] = 1000;

    for (int i = 0; i < roman.size(); i++) {
        if (roman_map[roman[i]] > roman_map[roman[i + 1]] || roman_map[roman[i]] == roman_map[roman[i + 1]]) {
            number += roman_map[roman[i]];
        } else {
            number -= roman_map[roman[i]];
        }
    }
    return number;
}

///
/// Función encargada de encontrar el prefijo común más largo en un conjunto de cadenas.
/// @param words Un vector de cadenas de texto.
/// @return Retorna el prefijo común más largo, si no existe prefijo común, retorna una cadena vacía.
std::string Solution::get_biggest_prefix(const std::vector<std::string> &words) {
    if (words.empty()) return "";

    std::string prefijo = words[0];
    for (int i = 1; i < words.size(); ++i) {
        while (words[i].find(prefijo) != 0) {
            prefijo = prefijo.substr(0, prefijo.size() - 1);
            if (prefijo.empty()) return "";
        }
    }
    return prefijo;
}
