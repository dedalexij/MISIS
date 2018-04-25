//
// Created by dedalexij on 16.04.18.
//

#include <array>

using namespace std;

#ifndef COURSEWORK_TRANSACTIONMANAGER_H
#define COURSEWORK_TRANSACTIONMANAGER_H


class TransactionManager {

	public:
	TransactionManager() = default;
	explicit TransactionManager(string repoPath);
	~TransactionManager() = default;


	void lock(const string path);
	void unlock(const string path);
	string locks();

	private:
	string _path;

	string exec(const char* cmd);

};


#endif //COURSEWORK_TRANSACTIONMANAGER_H
