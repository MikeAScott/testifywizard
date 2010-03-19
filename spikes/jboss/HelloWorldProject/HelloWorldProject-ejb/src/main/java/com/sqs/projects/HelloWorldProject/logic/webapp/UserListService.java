package com.sqs.projects.HelloWorldProject.logic.webapp;

import javax.ejb.Local;

import com.sqs.projects.HelloWorldProject.logic.framework.AbstractEntityListService;
import com.sqs.projects.HelloWorldProject.model.Gender;
import com.sqs.projects.HelloWorldProject.model.User;

@Local
public interface UserListService extends AbstractEntityListService<User> {

	String NAME = "userListService";
	String QUERY_RESULT = "select user from User user";

	Gender[] getGender();
}
