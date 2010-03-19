package com.sqs.projects.HelloWorldProject.logic.model;

import javax.ejb.Local;

import com.sqs.projects.HelloWorldProject.logic.framework.AbstractEntityService;
import com.sqs.projects.HelloWorldProject.model.User;

@Local
public interface UserService extends AbstractEntityService<User>{
	String NAME = "userService";
}