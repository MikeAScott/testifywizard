package com.sqs.projects.HelloWorldProject.model;

import com.bm.datagen.Generator;
import com.bm.datagen.annotations.GeneratorType;
import com.bm.testsuite.BaseEntityFixture;

import com.sqs.projects.HelloWorldProject.model.Gender;
import com.sqs.projects.HelloWorldProject.model.User;

public class UserDBTest extends BaseEntityFixture<User> {

	private static final Generator[] SPECIAL_GENERATORS = { new MyGenderCreator() };

	public UserDBTest() {
		super(User.class, SPECIAL_GENERATORS);
	}
		
	@GeneratorType(className = Gender.class)
	private static final class MyGenderCreator implements Generator<Gender> {

		public Gender getValue() {
			return Gender.MALE;
		}

	}
}