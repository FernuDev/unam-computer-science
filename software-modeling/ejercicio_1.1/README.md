# Ejercicios Primer parcial 1.1 Simulación y Modelado

Este proyecto contiene una serie de funciones implementadas en C++ para resolver distintos problemas, incluyendo la conversión de números entre sistemas numéricos, la búsqueda de números faltantes y repetidos en una matriz, y la extracción del prefijo común más largo en un conjunto de cadenas. A continuación, se proporciona una descripción general de las funciones y cómo ejecutar el proyecto.
Funcionalidades

    Generación de nota a partir de un texto:  
    La función `puedeGenerarNota` determina si una cadena `nota` puede ser generada a partir de los caracteres disponibles en otra cadena `texto`. Cada letra en `texto` solo puede usarse una vez.
    
    Detección de duplicados cercanos:  
    La función `hayDuplicadoCercano` determina si existen dos índices distintos `i` y `j` en el arreglo `A` tales que `A[i] == A[j]` y `|i - j| <= k`.
    
    Detección de números felices:  
    La función `esNumeroFeliz` determina si un número entero positivo es un número feliz, es decir, si al sumar los cuadrados de sus dígitos de manera iterativa el resultado es 1.
    
    Búsqueda de subcadena:  
    La función `buscarSubcadena` implementa el algoritmo Knuth-Morris-Pratt (KMP) para encontrar el índice de la primera ocurrencia de una cadena `s` en otra cadena `t`, o devuelve -1 si no se encuentra.


### Estructura del Proyecto

El proyecto contiene los siguientes archivos y directorios:

Ejercicios_parcial_1/  
│  
├── src/  
│   └── Solution.cpp       // Implementación de las funciones  
│  
├── include/  
│   └── Solution.h         // Declaraciones de las funciones  
│  
├── tests/  
│   └── SolutionTests.cpp  // Pruebas unitarias  
│  
├── CMakeLists.txt         // Configuración para CMake  
└── README.md              // Este archivo  

### Requisitos

Este proyecto utiliza C++ y requiere las siguientes herramientas para su compilación y ejecución:

    C++11 o superior.
    CMake (para generar el proyecto).
    Google Test (para pruebas unitarias).

### Dependencias

Para ejecutar las pruebas unitarias, debes tener instaladas las siguientes dependencias:

    Google Test: El marco de pruebas unitarias utilizado en este proyecto. Puedes instalarlo mediante el siguiente comando:
Para sistemas opetativos basados en Debian se puede instalar la libreria de unit testing de google con

    sudo apt-get install libgtest-dev

CMake: Se necesita para compilar el proyecto y las pruebas. Puedes instalarlo de la siguiente manera:

    sudo apt-get install cmake

### Instrucciones para Ejecutar el Proyecto
Paso 1: Clonar el Repositorio

Clona el repositorio en tu máquina local utilizando Git:

    git clone https://github.com/tu_usuario/solution.git
    cd solution

Paso 2: Compilación del Proyecto

Usa CMake para generar los archivos de compilación y compilar el proyecto. A continuación se muestran los pasos para hacerlo:
Crea un directorio build y navega a él:

    mkdir build
    cd build

Ejecuta CMake para configurar el proyecto:

    cmake ..

Compila el proyecto:

    make

Esto generará los binarios del proyecto, incluyendo el ejecutable principal y los archivos para las pruebas unitarias.
Paso 3: Ejecutar las Pruebas Unitarias

Para ejecutar las pruebas unitarias, puedes hacerlo con el siguiente comando:

    make test

O ejecutando directamente el binario de las pruebas:

    ./tests/SolutionTests

Esto ejecutará todas las pruebas definidas en SolutionTests.cpp, asegurando que las funciones implementadas se comporten como se espera.
Detalles Técnicos

    Funciones Implementadas:
    Las funciones implementadas están en la clase Solution, declaradas en el archivo Solution.h y definidas en Solution.cpp.

    Tests:
    Las pruebas unitarias están ubicadas en el directorio tests/. Se utiliza Google Test como framework para las pruebas.

### Ejemplo de uso
    puedeGenerarNota
    
    std::string nota = "hola";
    std::string texto = "ole";
    bool resultado = solution.puedeGenerarNota(nota, texto);
    std::cout << "¿Se puede generar la nota? " << (resultado ? "Sí" : "No") << std::endl;
    
    hayDuplicadoCercano
    
    std::vector<int> A = {1, 2, 3, 1};
    int k = 3;
    bool duplicado = solution.hayDuplicadoCercano(A, k);
    std::cout << "¿Hay duplicado cercano? " << (duplicado ? "Sí" : "No") << std::endl;
    
    esNumeroFeliz
    
    int numero = 19;
    bool feliz = solution.esNumeroFeliz(numero);
    std::cout << "¿Es número feliz? " << (feliz ? "Sí" : "No") << std::endl;
    
    buscarSubcadena
    
    std::string s = "abc";
    std::string t = "ababc";
    int indice = solution.buscarSubcadena(s, t);
    std::cout << "Índice de subcadena: " << indice << std::endl;

### Contribuciones

Si deseas contribuir al proyecto, por favor sigue estos pasos:

    Haz un fork del repositorio.
    Crea una nueva rama (git checkout -b nueva-funcionalidad).
    Realiza tus cambios y haz commit de ellos (git commit -am 'Agregando nueva funcionalidad').
    Haz push a tu rama (git push origin nueva-funcionalidad).
    Abre un Pull Request para que tus cambios sean revisados.

Licencia

Este proyecto está bajo la licencia MIT. Para más detalles, consulta el archivo LICENSE.