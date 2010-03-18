package com.sqs.projects.HelloWorldProject.bootstrap;

import javax.ejb.Local;

@Local
public interface ApplicationBootstrap {
  String NAME = "applicationBootstrap";

  void init();
}