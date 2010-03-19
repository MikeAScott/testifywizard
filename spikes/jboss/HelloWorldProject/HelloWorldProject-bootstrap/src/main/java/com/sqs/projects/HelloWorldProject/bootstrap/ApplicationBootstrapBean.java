package com.sqs.projects.HelloWorldProject.bootstrap;

import javax.ejb.EJB;
import javax.ejb.Stateless;

import org.jboss.seam.annotations.Logger;
import org.jboss.seam.annotations.Name;
import org.jboss.seam.annotations.Observer;
import org.jboss.seam.log.Log;

import com.sqs.projects.HelloWorldProject.bootstrap.testdata.UserTestdata;

@Name(ApplicationBootstrap.NAME)
@Stateless
public class ApplicationBootstrapBean implements ApplicationBootstrap {

	@Logger
	private Log log;
	
	@EJB
	private UserTestdata userTestdata;

	@Observer("org.jboss.seam.postInitialization")
	public final void init() {
		log.debug("received org.jboss.seam.postInitialization event");
		
		userTestdata.insert();
		
		log.info("insert testdata complete");
	}
}