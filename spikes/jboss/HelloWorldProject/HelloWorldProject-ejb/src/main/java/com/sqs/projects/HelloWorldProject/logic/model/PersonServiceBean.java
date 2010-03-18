package com.sqs.projects.HelloWorldProject.logic.model;

import javax.ejb.Stateless;

import org.jboss.seam.annotations.Name;

import com.sqs.projects.HelloWorldProject.logic.framework.AbstractEntityServiceBean;
import com.sqs.projects.HelloWorldProject.model.Person;

@Stateless
@Name(PersonService.NAME)
public class PersonServiceBean extends AbstractEntityServiceBean<Person>
		implements PersonService {
}
