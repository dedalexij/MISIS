//
// Created by dedalexij on 16.04.18.
//
#include <cstring>
#include <cstdio>
#include <iostream>
#include <memory>
#include <stdexcept>
#include <string>
#include <array>

#include "TransactionManager.h"

TransactionManager::TransactionManager(string repoPath)
{
	_path = repoPath;
}

string TransactionManager::locks()
{
	string cd("cd ");
	string cdPath = cd + _path;
	string command(" && git lfs locks r");
	string result = cdPath + command;

	return exec(result.c_str());
}

bool TransactionManager::lock(const string file)
{
	string cd("cd ");
	string cdPath = cd + _path;
	string command(" && git lfs lock ");
	string cdPathComm = cdPath + command;
	string fullCommand = cdPathComm + file;

	string result = exec(fullCommand.c_str());

	return result.find("Loked") != string::npos;

}

bool TransactionManager::unlock(const string file){

	string cd("cd ");
	string cdPath = cd + _path;
	string command(" && git lfs unlock ");
	string cdPathComm = cdPath + command;
	string fullCommand = cdPathComm + file;

	string result = exec(fullCommand.c_str());

	return result.find("Unloked") != string::npos;
}

string TransactionManager::exec(const char *cmd){
	std::array<char, 128> buffer;
	std::string result;
	std::shared_ptr<FILE> pipe(popen(cmd, "r"), pclose);
	if (!pipe) throw std::runtime_error("popen() failed!");
	while (!feof(pipe.get())) {
		if (fgets(buffer.data(), 128, pipe.get()) != nullptr)
			result += buffer.data();
	}
	return result;
}