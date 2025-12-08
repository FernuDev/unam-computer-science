# Basic Programming

![C](https://img.shields.io/badge/C-Language-A8B9CC?style=for-the-badge&logo=c&logoColor=white)
![C++](https://img.shields.io/badge/C++-00599C?style=for-the-badge&logo=cplusplus&logoColor=white)
![Arduino](https://img.shields.io/badge/Arduino-00979D?style=for-the-badge&logo=Arduino&logoColor=white)
![CMake](https://img.shields.io/badge/CMake-064F8C?style=for-the-badge&logo=cmake&logoColor=white)

## Descripción

Colección de proyectos de programación básica y sistemas embebidos que incluyen comunicación serial, programación asíncrona, y manejo de sistemas operativos. Desarrollado como parte del curso de Programación Básica enfocado en sistemas de bajo nivel y electrónica.

## Estructura del Proyecto

```
basic-programming/
├── src/
│   ├── Serial.c              # Comunicación serial con Arduino
│   ├── SerialTest.cpp        # Pruebas de comunicación serial
│   ├── OperatingSystem.c     # Llamadas al sistema operativo
│   ├── AsyncProgram.cpp      # Programación asíncrona
│   ├── TimeTest.cpp          # Pruebas de temporización
│   └── ESP32/
│       └── Esp32_Test.ino    # Código para ESP32
├── Build/                    # Ejecutables compilados
├── CMakeLists.txt           # Configuración de CMake
├── main.c                   # Programa principal
└── README.md
```

## Proyectos Incluidos

### 1. Serial Reader

**Descripción:** Sistema de lectura serial para sensor ultrasónico con Arduino Mega

**Características:**
- Comunicación serial bidireccional
- Lectura de sensor ultrasónico HC-SR04
- Post-procesamiento de datos
- Biblioteca en C para facilitar lectura de datos

**Hardware:**
- Arduino Mega 2560
- Sensor ultrasónico HC-SR04
- Conexión USB

### 2. Operating System Interface

**Descripción:** Interfaz de bajo nivel con llamadas al sistema operativo

**Funcionalidades:**
- Manejo de procesos
- Gestión de memoria
- Operaciones de E/S
- Llamadas al sistema

### 3. Async Program

**Descripción:** Implementación de programación asíncrona en C++

**Características:**
- Operaciones no bloqueantes
- Manejo de eventos
- Callbacks y promises
- Multithreading básico

### 4. Time Testing

**Descripción:** Herramientas para medición y análisis de tiempos de ejecución

**Utilidades:**
- Cronómetro de alta precisión
- Benchmarking de funciones
- Análisis de rendimiento

### 5. ESP32 Integration

**Descripción:** Código de prueba para microcontrolador ESP32

**Aplicaciones:**
- WiFi y Bluetooth
- Sensores IoT
- Control de dispositivos

## Instalación y Configuración

### Requisitos Previos

**Compilador C/C++:**
```bash
# Linux
sudo apt-get install build-essential cmake

# macOS
brew install cmake gcc

# Windows
# Instalar MinGW o Visual Studio C++ tools
```

**Permisos de Puerto Serial (Linux):**
```bash
# Agregar usuario al grupo dialout
sudo usermod -a -G dialout $USER

# O dar permisos temporales
sudo chmod a+rw /dev/ttyACM0
```

### Compilación

**Usando CMake:**
```bash
mkdir Build
cd Build
cmake ..
make
```

**Compilación manual:**
```bash
# Serial Reader
gcc -o Build/Serial src/Serial.c -std=c11

# Serial Test
g++ -o Build/SerialTest src/SerialTest.cpp -std=c++11

# Operating System
gcc -o Build/OperatingSystem src/OperatingSystem.c -std=c11

# Async Program
g++ -o Build/AsyncProgram src/AsyncProgram.cpp -std=c++11 -lpthread
```

## Uso

### Serial Reader

**1. Conectar Arduino:**
- Conecta el Arduino Mega vía USB
- Identifica el puerto (Linux: `/dev/ttyACM0`, `/dev/ttyUSB0`)

**2. Subir código Arduino:**
```arduino
const int Echo = 2;
const int Trigger = 4;

void setup() {
  Serial.begin(9600);
  pinMode(Echo, INPUT);
  pinMode(Trigger, OUTPUT);
  digitalWrite(Trigger, LOW);
}

void loop() {
  long t, d;
  
  digitalWrite(Trigger, HIGH);
  delayMicroseconds(10);
  digitalWrite(Trigger, LOW);
  
  t = pulseIn(Echo, HIGH);
  d = t / 59;  // Distancia en cm
  
  Serial.println(d);
  delay(100);
}
```

**3. Ejecutar programa C:**
```bash
./Build/Serial
```

### Operating System Interface

```bash
./Build/OperatingSystem
```

### Async Program

```bash
./Build/AsyncProgram
```

## Sensor Ultrasónico HC-SR04

### Conexiones

| Pin Arduino | Pin HC-SR04 |
|-------------|-------------|
| Pin 4 | Trigger |
| Pin 2 | Echo |
| 5V | VCC |
| GND | GND |

### Principio de Funcionamiento

1. **Trigger:** Se envía un pulso de 10μs
2. **Echo:** El sensor emite ultrasonido
3. **Recepción:** Se mide el tiempo hasta recibir el eco
4. **Cálculo:** Distancia (cm) = tiempo (μs) / 59

**Rango de medición:** 2 cm - 400 cm  
**Precisión:** ±3 mm

## Características Técnicas

### Serial Communication

**Parámetros:**
- Baud rate: 9600 bps
- Data bits: 8
- Stop bits: 1
- Parity: None
- Flow control: None

### Performance

| Operación | Tiempo |
|-----------|--------|
| Lectura serial | ~1ms |
| Medición ultrasónica | ~100ms |
| Procesamiento | <1ms |

## Aplicaciones

### 1. Robótica

- Detección de obstáculos
- Navegación autónoma
- Mapeo de entorno

### 2. IoT (Internet of Things)

- Sensores inteligentes
- Monitoreo remoto
- Automatización del hogar

### 3. Sistemas Embebidos

- Control industrial
- Instrumentación
- Adquisición de datos

### 4. Educación

- Aprendizaje de comunicación serial
- Interfaces hardware-software
- Sistemas de tiempo real

## Conceptos Abordados

### Programación en C

- Punteros y gestión de memoria
- Entrada/Salida de bajo nivel
- Estructuras de datos
- Bibliotecas estáticas

### Programación en C++

- Programación orientada a objetos
- Templates y genéricos
- STL (Standard Template Library)
- Manejo de excepciones

### Sistemas Operativos

- Llamadas al sistema (syscalls)
- Procesos e hilos
- Sincronización
- IPC (Inter-Process Communication)

### Electrónica

- Comunicación serial UART
- Protocolos de comunicación
- Sensores y actuadores
- Microcontroladores

## Debugging

### Verificar Puerto Serial

**Linux:**
```bash
ls -l /dev/ttyACM* /dev/ttyUSB*
dmesg | grep tty
```

**macOS:**
```bash
ls -l /dev/cu.*
```

**Windows:**
```bash
mode  # En CMD
Get-WmiObject Win32_SerialPort  # En PowerShell
```

### Monitor Serial

```bash
# Linux/macOS
screen /dev/ttyACM0 9600

# Alternativa
minicom -D /dev/ttyACM0 -b 9600
```

## Troubleshooting

### Problema: "Permission denied" en puerto serial

**Solución:**
```bash
sudo chmod a+rw /dev/ttyACM0
# o
sudo usermod -a -G dialout $USER
# Luego cerrar sesión y volver a entrar
```

### Problema: Puerto no encontrado

**Solución:**
1. Verificar conexión USB
2. Revisar drivers del microcontrolador
3. Probar con otro cable USB

### Problema: Lecturas erráticas

**Solución:**
1. Verificar conexiones físicas
2. Aumentar delay entre lecturas
3. Implementar filtro de media móvil
4. Verificar alimentación estable

## Extensiones Futuras

1. **Biblioteca .h compartida**
   - API unificada para lectura serial
   - Soporte multiplataforma
   - Documentación con Doxygen

2. **Logging avanzado**
   - Almacenamiento en archivos
   - Timestamps precisos
   - Niveles de log

3. **GUI**
   - Interfaz gráfica con Qt o GTK
   - Gráficas en tiempo real
   - Configuración visual

4. **Networking**
   - Transmisión de datos por red
   - API REST
   - WebSocket para monitoreo web

## Referencias y Recursos

### Documentación
- [Arduino Language Reference](https://www.arduino.cc/reference/en/)
- [C Standard Library](https://en.cppreference.com/w/c)
- [POSIX API](https://pubs.opengroup.org/onlinepubs/9699919799/)

### Hardware
- [HC-SR04 Datasheet](https://cdn.sparkfun.com/datasheets/Sensors/Proximity/HCSR04.pdf)
- [Arduino Mega 2560](https://store.arduino.cc/products/arduino-mega-2560-rev3)
- [ESP32 Documentation](https://docs.espressif.com/projects/esp-idf/en/latest/esp32/)

### Tutoriales
- Serial Communication in C
- CMake Tutorial
- Async Programming Patterns

---

<div align="center">

**Facultad de Ciencias - UNAM**

*Programación Básica y Sistemas Embebidos*

![Status](https://img.shields.io/badge/Estado-Activo-success?style=flat-square)
![Language](https://img.shields.io/badge/Languages-C%2FC++%2FArduino-blue?style=flat-square)

</div>
