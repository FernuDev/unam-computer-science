#include <iostream>
#include <string>
#include <vector>
#include <cstring>  // Agregado para incluir cstring

// Defining the header for each OS

// Linux Headers
#ifdef __linux__

#include <termios.h>
#include <cerrno>
#include <unistd.h>
#include <fcntl.h>

#endif

unsigned int sleepTime = 100;

// Función para configurar la conexión serial
int setupSerial(const std::string &port) {
    int device = open(port.c_str(), O_RDWR | O_NOCTTY | O_SYNC);
  

    if (device < 0) {
        std::cerr << "Error opening serial port " << port << ": " << strerror(errno) << std::endl;
        exit(EXIT_FAILURE);
    }

    struct termios tty{};
    if (tcgetattr(device, &tty) != 0) {
        std::cerr << "Error from tcgetattr: " << strerror(errno) << std::endl;
        close(device);
        exit(EXIT_FAILURE);
    }

    // Configurar opciones del puerto serie
    cfsetospeed(&tty, B9600);  // Velocidad de transmisión: 9600 bps
    cfsetispeed(&tty, B9600);

    tty.c_cflag |= (CLOCAL | CREAD);  // Habilitar lectura y local mode
    tty.c_cflag &= ~PARENB;  // Sin paridad
    tty.c_cflag &= ~CSTOPB;  // 1 stop bit
    tty.c_cflag &= ~CSIZE;   // Limpiar bits de tamaño de carácter
    tty.c_cflag |= CS8;      // 8 bits por byte
    tty.c_lflag &= ~ICANON;  // Modo no canónico
    tty.c_oflag &= ~OPOST;   // Modo raw

    // Aplicar la configuración
    if (tcsetattr(device, TCSANOW, &tty) != 0) {
        std::cerr << "Error from tcsetattr: " << strerror(errno) << std::endl;
        close(device);
        exit(EXIT_FAILURE);
    }

    return device;
}

// Función para leer datos del puerto serie
std::string readFromSerial(int device) {
    char buffer[128];
    memset(buffer, 0, sizeof(buffer));

    int num_bytes = read(device, buffer, sizeof(buffer));

    if (num_bytes < 0) {
        std::cerr << "Error from read: " << strerror(errno) << std::endl;
    } else if (num_bytes > 0) {
        return std::string(buffer);
    }

    return "Empty Buffer";
}

int main(int argc, char **argv) {
    std::cout << "\n===== Serial Reader Antares Aerospace =====" << std::endl;

    std::string port = std::string(argv[1]);
    int device = setupSerial(port);

    std::string response;
    float empuje = 0.0;

    std::vector <float> empujes = {0.0,0.0,0.0,0.0};

    do {
        response = readFromSerial(device);
        
        if (!response.empty()) {
            
            try {
                empuje = std::stof(response);
            }catch (const std::invalid_argument &e) {
                std::cerr << "Invalid argument: " << e.what() << std::endl;
                empuje = empujes.back();

            }catch (const std::out_of_range &e) {
                std::cerr << "Out of range: " << e.what() << std::endl;
                empuje -= 1000;
            }
            if(empuje >= 0.0 && empuje <= 981){
            
                empujes.push_back(empuje);
                std::cout << "Empuje: " << empuje << std::endl;
            } else {
                std::cerr << "Valor fuera de rango " << std::endl;
            }
        }
        usleep(sleepTime * 1000);

        response = "";
        
    } while (empuje > - 10);

    close(device);

    return 0;
}
