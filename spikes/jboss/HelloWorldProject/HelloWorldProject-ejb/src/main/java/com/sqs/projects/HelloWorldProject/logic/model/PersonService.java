package com.sqs.projects.HelloWorldProject.logic.model;

import javax.ejb.Local;

import com.sqs.projects.HelloWorldProject.logic.framework.AbstractEntityService;
import com.sqs.projects.HelloWorldProject.model.Person;

@Local
public interface PersonService extends AbstractEntityService<Person>{
	String NAME = "personService";
}