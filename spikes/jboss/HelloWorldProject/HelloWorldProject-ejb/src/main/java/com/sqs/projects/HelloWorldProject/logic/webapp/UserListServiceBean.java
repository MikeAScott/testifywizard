package com.sqs.projects.HelloWorldProject.logic.webapp;

import java.util.List;

import javax.ejb.Remove;
import javax.ejb.Stateful;
import javax.persistence.Query;

import org.jboss.seam.ScopeType;
import org.jboss.seam.annotations.Destroy;
import org.jboss.seam.annotations.Factory;
import org.jboss.seam.annotations.In;
import org.jboss.seam.annotations.Name;
import org.jboss.seam.annotations.Observer;
import org.jboss.seam.annotations.Out;
import org.jboss.seam.annotations.Scope;

import com.sqs.projects.HelloWorldProject.logic.framework.AbstractEntityListServiceBean;
import com.sqs.projects.HelloWorldProject.model.Gender;
import com.sqs.projects.HelloWorldProject.model.User;

@Stateful
@Name(UserListService.NAME)
@Scope(ScopeType.CONVERSATION)
public class UserListServiceBean extends AbstractEntityListServiceBean<User>
		implements UserListService {

	@In(required = false)
	@Out(required = false)
	private User user;

	private List<User> resultList;

	@SuppressWarnings("unchecked")
	@Override
	public List<User> getResultList() {
		if (resultList == null) {
			final Query query = entityManager
					.createQuery(UserListService.QUERY_RESULT);
			resultList = query.getResultList();
		}
		return resultList;
	}

	@Override
	@Observer( { "com.sqs.projects.HelloWorldProject.persited.User",
			"com.sqs.projects.HelloWorldProject.deleted.User" })
	public void refresh() {
		resultList = null;
		log.info("refresh resultList");
	}

	@Override
	public void setSelectedEntity(User user) {
		this.user = user;
	}

	@Factory
	public Gender[] getGender() {
		return Gender.values();
	}

	@Override
	public User getSelectedEntity() {
		return user;
	}

	@Destroy
	@Remove
	public void destroy() {
		log.debug("destry component #0", this);
	}

}
