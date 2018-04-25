cmake_minimum_required(VERSION 3.8)
project(coursework)

set(CMAKE_CXX_STANDARD 11)

set(SOURCE_FILES TransactionManager.cpp TransactionManager.h test.cpp)
add_executable(test.cpp ${SOURCE_FILES})
add_library(coursework ${SOURCE_FILES})