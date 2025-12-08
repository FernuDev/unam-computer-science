#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#ifdef __linux__
// GNU/Linux Headers
#include <fcntl.h>   
#include <errno.h>
#include <termios.h>
#include <unistd.h>

#endif

void SerialReaderLinux(char *argv[]);
void SerialReaderWindows(char *argv[]);

int main(int argc, char *argv []) {
    
    printf("\n====== Serial Reader Antares ======\n");

    #ifdef __linux__
        SerialReaderLinux(argv);
    #elif _WIN32
        SerialReaderWindows(argv);
    #else
        printf("Sorry, this app is not supported for you SO");
    #endif
        return 0;
}

void SerialReaderLinux(char *argv[]){
    printf("\nWorking on Linux system...\n");

    printf("Including Linux libraries...\n");

    float readSerialPort(int serial_port){
        char buffer[8];

        memset(buffer, 0, sizeof(buffer));

        int num_bytes = read(serial_port, &buffer, sizeof(buffer) -1);

        if(num_bytes <= 0) {
            printf("Error %i from read: %s", errno, strerror(errno));
            return -1;
        }

        float value = atof(buffer);

        return value;
    };
    
    size_t port_size = strlen(argv[1]); // sizeof(argv[1][0]) ;

    if(argv[1] && port_size>3){
        // Loading a port 
        printf("The selected port is: %s\n", argv[1]);
        
        // Declaration and concatenation for the asigned port 
        // TODO: Fix the string problem
        printf("\nConnecting to: %s\n", argv[1]);

        // Opening serial port 
        int serial_port = open(argv[1], O_RDWR);

        if(serial_port < 0){
            printf("\nError %i from open %s, returned %s\n", errno, argv[1], strerror(errno));
        }

        // Configuration Setup
        
        // Create a new termios struct called tty by convention
        
        struct termios tty;
        
        // Read in existing settings and handle any error 

        if(tcgetattr(serial_port, &tty) != 0){
            printf("Error in %i from tcgetattr: %s\n", errno, strerror(errno));
        }
        
        // Config the cflags and modes for I/O stream in serial port 
        
        tty.c_cflag &= ~PARENB; // Clear parity bit, disabling parity
        tty.c_cflag &= ~CSTOPB; // Clear stdio field
        tty.c_cflag &= ~CSIZE; // Clear all bits that set the data size
        tty.c_cflag |= CS8;
        tty.c_cflag |= CREAD | CLOCAL;
        
        // Config the lflags 

        tty.c_lflag &= ~ICANON;
        tty.c_lflag &= ~ECHO; // Disable echo
        tty.c_lflag &= ~ECHOE; // Disable erasure
        tty.c_lflag &= ~ECHONL; // Disable new-line echo
        tty.c_lflag &= ~ISIG; // Disable interpolation of INTR, QUIT and SUSP
        tty.c_lflag &= ~(IXON | IXOFF | IXANY ); // Turn off s/w flow ctrl
        tty.c_lflag &= ~(IGNBRK|BRKINT|PARMRK|ISTRIP|INLCR|IGNCR|ICRNL);

        // Config the oflags 
        
        tty.c_oflag &= ~OPOST;
        tty.c_oflag &= ~ONLCR;

        tty.c_cc[VTIME] = 10;
        tty.c_cc[VMIN] = 0;

        // Set the in/out baund rate

        cfsetispeed(&tty, B9600);
        cfsetospeed(&tty, B9600);
        
        // Reading the serial port 

        while(1){
            float value = readSerialPort(serial_port)/1000.0;

            if(value > 0){
                printf("Empuje: %.3f N\n", value);
            }
        }

        printf("Exit the program ...\n");

        close(serial_port);
        

    }else {
        // A port was not selected
        printf("Invalid port, please type a port for the conection\n");
    }

}

void SerialReaderWindows(char *argv[]){
    printf("Working on Windows system...\n");
}
