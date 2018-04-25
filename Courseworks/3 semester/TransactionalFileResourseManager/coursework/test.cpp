//
// Created by dedalexij on 22.04.18.
//

#include "TransactionManager.h"
#include <iostream>

int main() {
	string str("/home/dedalexij/lod/ItHappened");
	TransactionManager transactionManager(str);

	string str1("README.md");

	transactionManager.lock(str1);

	cout << transactionManager.locks();

	transactionManager.unlock(str1);



}