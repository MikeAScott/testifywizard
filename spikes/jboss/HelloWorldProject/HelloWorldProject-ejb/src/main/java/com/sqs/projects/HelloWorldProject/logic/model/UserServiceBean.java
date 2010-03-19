package com.sqs.projects.HelloWorldProject.logic.model;

import javax.ejb.Stateless;

import org.jboss.seam.annotations.Name;

import com.sqs.projects.HelloWorldProject.logic.framework.AbstractEntityServiceBean;
import com.sqs.projects.HelloWorldProject.model.User;

@Stateless
@Name(UserService.NAME)
public class UserServiceBean extends AbstractEntityServiceBean<User>
		implements UserService {
}
