/*
 *============================================================================
 * Program: Prime Number Tester (C Implementation)
 * Purpose: Determine if a given number is prime using trial division
 * Author:  Luis Fernando Núñez Rangel
 * Date:    2025
 *
 * Description:
 *   Tests primality of an integer using the trial division method.
 *   This is the C99 implementation parallel to the Fortran version.
 *
 * Algorithm:
 *   - Numbers 1, 2, 3 are considered prime (base cases)
 *   - For n > 3: test divisibility by all integers from 2 to n-1
 *   - Return 1 (true) if prime, 0 (false) if composite
 *
 * Compilation:
 *   gcc -std=c99 ejercicio_3_2.c -o ejercicio_3_c
 *
 * Usage:
 *   ./ejercicio_3_c
 *   Enter an integer when prompted
 *============================================================================
 */

#include <stdio.h>

/* Function prototype */
int is_p(int a);

/*
 * Main Program
 * Reads an integer from user and determines if it's prime
 */
int main(int argc, char **argv) {
    int number = 0;

    /* Input: Read number from user */
    printf("Enter a number: ");
    scanf("%d", &number);

    /* Output: Display primality test result */
    /* Ternary operator: condition ? true_value : false_value */
    printf("Is prime number: %s\n", is_p(number) ? "true" : "false");

    return 0;
}

/*
 *============================================================================
 * Function: is_p
 * Purpose:  Test if a number is prime using trial division
 *
 * Parameters:
 *   a - Integer to test for primality
 *
 * Returns:
 *   1 if number is prime
 *   0 if number is composite
 *
 * Algorithm:
 *   1. Base case: numbers 1-3 are prime
 *   2. Trial division: test all divisors from 2 to a-1
 *   3. If any divisor divides evenly (a % i == 0), return 0
 *   4. If no divisors found, return 1
 *
 * Complexity:
 *   Time: O(n) where n is the input number
 *   Space: O(1)
 *
 * Optimization Note:
 *   Could be improved to O(√n) by testing only up to square root of a
 *============================================================================
 */
int is_p(int a) {
    /* Base case: numbers 1, 2, 3 are prime */
    if (a >= 1 && a <= 3) {
        return 1;
    }

    /* Trial division: test all potential divisors */
    for (int i = 2; i < a; i++) {
        if (a % i == 0) {
            /* Found a divisor - number is composite */
            return 0;
        }
    }
    
    /* No divisors found - number is prime */
    return 1;
}
