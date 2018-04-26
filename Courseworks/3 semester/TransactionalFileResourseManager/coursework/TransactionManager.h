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

	bool lock(const string path);//Блокирует файл
	//string path — путь к файлу или папке. Возвращает true, если успешно.

	bool unlock(const string path);//Разблокирует файл
	//string path — путь к файлу или папке. Возвращает true, если успешно.

	string locks();

	private:
	string _path;

	string exec(const char* cmd);
};


#endif //COURSEWORK_TRANSACTIONMANAGER_H
