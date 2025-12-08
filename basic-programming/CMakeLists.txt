cmake_minimum_required(3.5)

project(
    SerialTest
    VERSION 1.0
    LANGUAGES CXX
)

find_package(SerialPort REQUIRED)
find_package(SerialStream REQUIRED)

add_executable(SerialTest src/SerialTest.cpp)

target_link_libraries(SerialTest PUBLIC SerialPort SerialStream)
