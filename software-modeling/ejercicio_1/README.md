# Ejercicios Primer parcial Simulación y Modelado

Este proyecto contiene una serie de funciones implementadas en C++ para resolver distintos problemas, incluyendo la conversión de números entre sistemas numéricos, la búsqueda de números faltantes y repetidos en una matriz, y la extracción del prefijo común más largo en un conjunto de cadenas. A continuación, se proporciona una descripción general de las funciones y cómo ejecutar el proyecto.
Funcionalidades

    Búsqueda de número faltante y repetido en una matriz:
    La función get_repeated_missing_number permite encontrar el número faltante en una matriz n x n en el rango de [1, n], así como el número que se encuentra repetido en la misma.

    Conversión de número decimal a romano:
    La función integer_to_roman convierte un número entero a su representación en números romanos.

    Conversión de número romano a entero:
    La función roman_to_integer convierte un número romano a su equivalente en el sistema decimal.

    Encontrar el prefijo común más largo:
    La función get_biggest_prefix toma un conjunto de cadenas y devuelve el prefijo común más largo entre ellas.

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
    get_repeated_missing_number
    
    std::vector<int> result = solution.get_repeated_missing_number(matrix);
    std::cout << "Número repetido: " << result[0] << ", Número faltante: " << result[1] << std::endl;
    
    integer_to_roman
    
    std::string roman = solution.integer_to_roman(1999);
    std::cout << "Número romano: " << roman << std::endl;
    
    roman_to_integer
    
    int number = solution.roman_to_integer("MCMXCIV");
    std::cout << "Número en decimal: " << number << std::endl;
    
    get_biggest_prefix
    
    std::string prefix = solution.get_biggest_prefix({"flower", "flow", "flight"});
    std::cout << "Prefijo común más largo: " << prefix << std::endl;

### Contribuciones

Si deseas contribuir al proyecto, por favor sigue estos pasos:

    Haz un fork del repositorio.
    Crea una nueva rama (git checkout -b nueva-funcionalidad).
    Realiza tus cambios y haz commit de ellos (git commit -am 'Agregando nueva funcionalidad').
    Haz push a tu rama (git push origin nueva-funcionalidad).
    Abre un Pull Request para que tus cambios sean revisados.

Licencia

Este proyecto está bajo la licencia MIT. Para más detalles, consulta el archivo LICENSE.