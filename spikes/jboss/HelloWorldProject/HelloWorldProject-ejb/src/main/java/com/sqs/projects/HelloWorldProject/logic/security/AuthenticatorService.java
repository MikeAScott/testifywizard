package com.sqs.projects.HelloWorldProject.logic.security;

import javax.persistence.EntityManager;
import javax.persistence.NoResultException;

import org.jboss.seam.annotations.In;
import org.jboss.seam.annotations.Name;
import org.jboss.seam.annotations.Logger;
import org.jboss.seam.log.Log;
import org.jboss.seam.security.Credentials;
import org.jboss.seam.security.Identity;

import com.sqs.projects.HelloWorldProject.model.User;

@Name("authenticator")
public class AuthenticatorService {

	@Logger
	protected Log log;
	@In
	EntityManager entityManager;
	@In
	Credentials credentials;
	@In
	Identity identity;

	public boolean authenticate() {

		try {

			User user = (User) entityManager
					.createQuery(
							"from User where username = :username and password = :password")
					.setParameter("username", credentials.getUsername())
					.setParameter("password", credentials.getPassword())
					.getSingleResult();

/*			if (user.getRoles() != null) {
				for (UserRole mr : user.getRoles())
					identity.addRole(mr.getName());
			}
*/
			return true;
		} catch (NoResultException ex) {
			return false;
		}
	}
}
