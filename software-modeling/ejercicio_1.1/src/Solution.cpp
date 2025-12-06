#include "Solution.h"
#include <unordered_set>
#include <vector>
#include <cmath>

// ------------------------
// 1. puedeGenerarNota
// ------------------------
bool Solution::puedeGenerarNota(const std::string &nota, const std::string &texto) {
    int frecuencia[26] = {0}; // 26 letras latinas en minúscula
    
    for (char c : texto) {
        frecuencia[c - 'a']++;
    }

    for (char c : nota) {
        if (frecuencia[c - 'a'] == 0) {
            return false;
        }
        frecuencia[c - 'a']--;
    }

    return true;
}

// ------------------------
// 2. hayDuplicadoCercano
// ------------------------
bool Solution::hayDuplicadoCercano(const std::vector<int> &A, int k) {
    if (k <= 0) return false; // Si k es 0 o negativo, no hay duplicado cercano
    
    std::unordered_map<int, int> mapa;

    for (int i = 0; i < A.size(); ++i) {
        if (mapa.count(A[i]) && (i - mapa[A[i]]) <= k) {
            return true;
        }
        mapa[A[i]] = i;

        // Eliminar el valor que exceda la ventana de k
        if (i >= k) {
            mapa.erase(A[i - k]);
        }
    }

    return false;
}

// ------------------------
// 3. esNumeroFeliz
// ------------------------
bool Solution::esNumeroFeliz(int n) {
    std::unordered_set<int> visitado;
    
    while (n != 1 && !visitado.count(n)) {
        visitado.insert(n);
        int suma = 0;
        while (n > 0) {
            int digito = n % 10;
            suma += digito * digito;
            n /= 10;
        }
        n = suma;
    }

    return n == 1;
}

// ------------------------
// 4. buscarSubcadena
// ------------------------
std::vector<int> construirLPS(const std::string &patron) {
    int n = patron.size();
    std::vector<int> lps(n, 0);
    int len = 0;
    int i = 1;

    while (i < n) {
        if (patron[i] == patron[len]) {
            len++;
            lps[i] = len;
            i++;
        } else {
            if (len != 0) {
                len = lps[len - 1];
            } else {
                lps[i] = 0;
                i++;
            }
        }
    }
    return lps;
}

int Solution::buscarSubcadena(const std::string &s, const std::string &t) {
    if (s.empty() || t.empty() || s.size() > t.size()) return -1;

    std::vector<int> lps = construirLPS(s);
    int i = 0; // índice en t
    int j = 0; // índice en s

    while (i < t.size()) {
        if (s[j] == t[i]) {
            i++;
            j++;
        }
        if (j == s.size()) {
            return i - j; // Se encontró la coincidencia
        } else if (i < t.size() && s[j] != t[i]) {
            if (j != 0) {
                j = lps[j - 1];
            } else {
                i++;
            }
        }
    }

    return -1;
}
