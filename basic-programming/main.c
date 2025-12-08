// C headers
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
// GNU/Linux header
#include <termios.h> // Contains POSIX terminal control definitions
#include <errno.h> // Error int and stderror() function
#include <unistd.h> // write(), read(), close() for the serial port
#include <fcntl.h> //Contains file controls 

float readSerialPort(int serial_port) {
    char buffer[256];

    memset(buffer,0,sizeof(buffer));

    int num_bytes = read(serial_port, &buffer, sizeof(buffer)-1);

    if(num_bytes <= 0){
        //Catch read error
        printf("Error %i from read: %s", errno, strerror(errno));
        return -1;
    }
    float value = atof(buffer);

    return value;
}

int main(int argc, char* argv[]){
    
    // Open the serial port
    int serial_port = open("/dev/ttyUSB0", O_RDWR);

    // First we have to check for any error open the serial port 

    if(serial_port < 0) {
        printf("Error %i from open %s\n", errno, strerror(errno));
    }

    // Config the port 

    struct termios tty;

    if(tcgetattr(serial_port, &tty) != 0) {
        printf("Error %i from tcgetattr: %s", errno, strerror(errno));
    }

    tty.c_cflag &= ~PARENB; // Clear parity bit, disabling parity
    tty.c_cflag &= ~CSTOPB; // Clear stio field
    tty.c_cflag &= ~CSIZE; //Clear all bits that set the data size
    tty.c_cflag |= CS8;
    tty.c_cflag |= CREAD | CLOCAL;

    tty.c_lflag &= ~ICANON;
    tty.c_lflag &= ~ECHO; // Disable echo
    tty.c_lflag &= ~ECHOE; // Disable erasure
    tty.c_lflag &= ~ECHONL; // Disable new-line echo
    tty.c_lflag &= ~ISIG; // Disable interpretation of INTR, QUIT and SUSP
    tty.c_iflag &= ~(IXON | IXOFF | IXANY); // Turn off s/w flow ctrl
    tty.c_iflag &= ~(IGNBRK|BRKINT|PARMRK|ISTRIP|INLCR|IGNCR|ICRNL);

    tty.c_oflag &= ~OPOST;
    tty.c_oflag &= ~ONLCR;

    tty.c_cc[VTIME] = 10;
    tty.c_cc[VMIN] = 0;

    // Set the in/out baund rate
    cfsetispeed(&tty, B9600);
    cfsetospeed(&tty, B9600);

    // printf("Press enter to read serial port and q to exit\n");

    while(1) {

        float value = readSerialPort(serial_port);

        if(value > 0){
            printf("Empuje: %.3f N\n", value);
        }
    }

    printf("Exit the program ...\n");

    close(serial_port);

    return 0;
}
