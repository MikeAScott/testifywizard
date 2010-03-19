package com.sqs.projects.HelloWorldProject.bootstrap.testdata;

import java.util.Calendar;

import javax.ejb.Stateless;
import javax.persistence.EntityManager;

import org.jboss.seam.annotations.In;
import org.jboss.seam.annotations.Name;

import com.sqs.projects.HelloWorldProject.model.Gender;
import com.sqs.projects.HelloWorldProject.model.User;

@Stateless
@Name(UserTestdata.NAME)
public class UserTestdataBean implements UserTestdata {

	@In
	private EntityManager entityManager;

	public void insert() {
		for (int i = 1; i <= NUMBER; i++) {
			entityManager.persist(createUser(i));
		}
	}

	private User createUser(final int number) {
		final Calendar calendar = Calendar.getInstance();
		calendar.set(Calendar.YEAR, calendar.get(Calendar.YEAR ) - number);
		final Gender[] gender = Gender.values();
		final User user = new User("User" + number, "User" + number + "@nowhere.com","password",
				calendar.getTime(), gender[number % 2]);

		return user;
	}

}
