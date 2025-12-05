"""
Matrix Determinant Calculator - Python Implementation

This module calculates the determinant of a 3x3 matrix using Sarrus' rule
and validates the result against NumPy's linear algebra library.

Author: Luis Fernando Núñez Rangel
Institution: Facultad de Ciencias, UNAM
Course: Computational Physics - Exam 1

Mathematical Method:
    Sarrus' Rule for 3x3 matrices extends the matrix by repeating the first
    two rows, then sums products along diagonals (positive - negative).
"""

import numpy as np


def determinante(matrix: list) -> float:
    """
    Calculate the determinant of a 3x3 matrix using Sarrus' rule.
    
    Implements the classical Sarrus method for 3x3 determinant calculation
    without using advanced linear algebra libraries for the core computation.

    Args:
        matrix (list[list[float]]): 3x3 matrix represented as list of rows.
                                    Each row is a list of 3 float values.

    Returns:
        float: The determinant value of the input matrix.
        
    Algorithm:
        1. Create extended matrix by appending first two rows
        2. Calculate positive diagonal products:
           - Main diagonal and two wrapping diagonals
        3. Calculate negative diagonal products:
           - Anti-diagonal and two wrapping diagonals
        4. Return difference: positive - negative
        
    Mathematical Formula:
        det(M) = (a₁₁a₂₂a₃₃ + a₁₂a₂₃a₃₁ + a₁₃a₂₁a₃₂) -
                 (a₁₃a₂₂a₃₁ + a₂₃a₃₂a₁₁ + a₃₃a₁₂a₂₁)
                 
    Example:
        >>> M = [[1, 0, 0], [0, 1, 0], [0, 0, 1]]  # Identity matrix
        >>> determinante(M)
        1.0  # det(I) = 1
        
    Note:
        This method is only valid for 3x3 matrices. For larger matrices,
        use LU decomposition or cofactor expansion.
    """
    # Create extended matrix by appending first two rows
    # This facilitates diagonal product calculation
    matriz = matrix.copy()
    matriz.append(matriz[0])
    matriz.append(matriz[1])

    # Calculate positive diagonal products
    # Pattern: top-left to bottom-right wrapping diagonals
    pos = (matriz[0][0] * matriz[1][1] * matriz[2][2] + 
           matriz[1][0] * matriz[2][1] * matriz[3][2] + 
           matriz[2][0] * matriz[3][1] * matriz[4][2])
    
    # Calculate negative diagonal products
    # Pattern: top-right to bottom-left wrapping diagonals
    neg = (matriz[0][2] * matriz[1][1] * matriz[2][0] + 
           matriz[1][2] * matriz[2][1] * matriz[3][0] + 
           matriz[2][2] * matriz[3][1] * matriz[4][0])

    # Return determinant as difference of diagonal sums
    return pos - neg


def main():
    """
    Main program to input matrix and calculate determinant.
    
    Workflow:
        1. Prompt user for 9 matrix elements (3x3)
        2. Calculate determinant using custom implementation
        3. Validate result against NumPy's linalg.det()
        4. Display both results for comparison
    """
    # Initialize empty matrix
    matrix = []

    # Input matrix elements row by row
    print("Enter the matrix values:")
    for i in range(3):
        fila_intermedia = []
        for j in range(3):
            valor = float(input(f"Element [{i+1}][{j+1}]: "))
            fila_intermedia.append(valor)
        matrix.append(fila_intermedia)

    # Calculate determinant using custom implementation
    det_custom = determinante(matrix)
    
    # Calculate determinant using NumPy for validation
    det_numpy = np.linalg.det(matrix)

    # Display results
    print(f"\nDeterminant (custom method): {det_custom}")
    print(f"Determinant (NumPy validation): {det_numpy}")
    
    # Check if results match within floating-point precision
    if abs(det_custom - det_numpy) < 1e-10:
        print("\nValidation: Results match!")
    else:
        print(f"\nWarning: Discrepancy detected: {abs(det_custom - det_numpy)}")


if __name__ == "__main__":
    main()
