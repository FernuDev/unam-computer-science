#include <iostream>
#include <string>
#include <chrono>

void countToMillion(){
    for(int i=0;i<1000000;++i){}
}

int main(int argc, char *argv[]){
    
    auto start = std::chrono::high_resolution_clock::now();

    countToMillion();

    auto end = std::chrono::high_resolution_clock::now();

    auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(end-start);

    std::cout<< "Duracion: "<<duration.count()<<"ms"<<std::endl;

    return 0;
}
