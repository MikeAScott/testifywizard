package com.sqs.projects.HelloWorldProject.bootstrap.testdata;

import javax.ejb.Local;

@Local
public interface UserTestdata {
	String NAME = "UserTestdata";
	int NUMBER = 20;

	void insert();
}
