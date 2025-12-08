//#include <ncurses.h>
#include <stdio.h>

int main(int argc, char **argv){
    
    #ifdef _WIN32
        printf("Hey you're working on Windows OS\n");
    #elif __linux__
        printf("Hey you're working on GNU/Linux OS\n");
    #else
        printf("Sorry your SO is not listed\n");
    #endif
        getchar();
        return 0;
}
