package com.sqs.projects.HelloWorldProject.logic.webapp;

import javax.ejb.Local;

import com.sqs.projects.HelloWorldProject.logic.framework.AbstractEntityListService;
import com.sqs.projects.HelloWorldProject.model.Gender;
import com.sqs.projects.HelloWorldProject.model.Person;

@Local
public interface PersonListService extends AbstractEntityListService<Person> {

	String NAME = "personListService";
	String QUERY_RESULT = "select person from Person person";

	Gender[] getGender();
}
