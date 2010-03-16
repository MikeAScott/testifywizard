/**
 * License Agreement.
 *
 * Rich Faces - Natural Ajax for Java Server Faces (JSF)
 *
 * Copyright (C) 2007 Exadel, Inc.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License version 2.1 as published by the Free Software Foundation.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
 */
package com.sqs.AtestProject.util;
/**
 * This class is session listener that observes <code>"org.jboss.seam.sessionExpired"</code> event to delete in production systems users when it's session is expired
 * to prevent flood. Used only on livedemo server. If you don't want this functionality simply delete this class from distributive.
 * 
 * @author Andrey Markhel
 */

import javax.persistence.EntityManager;

import org.jboss.seam.ScopeType;
import org.jboss.seam.annotations.Destroy;
import org.jboss.seam.annotations.In;
import org.jboss.seam.annotations.Name;
import org.jboss.seam.annotations.Observer;
import org.jboss.seam.annotations.Scope;
import org.jboss.seam.annotations.Startup;
import org.jboss.seam.annotations.Transactional;
import org.jboss.seam.core.Events;

import com.sqs.AtestProject.domain.User;
import com.sqs.AtestProject.manager.LoggedUserTracker;
import com.sqs.AtestProject.service.Constants;

@Scope(ScopeType.SESSION)
@Name("sessionListener")
@Startup
public class SessionListener {
	
	@In(required=false)
	private User user;

	@In(value="entityManager")
	private EntityManager em;
	@In LoggedUserTracker userTracker;
	@Destroy
	@Transactional
	@Observer("org.jboss.seam.sessionExpired")
	public void onDestroy(){
		if (!Environment.isInProduction()) {
			return;
		}

		if(user.getId() != null && !user.isPreDefined() && !userTracker.containsUserId(user.getId())){
			user = em.merge(user);
			em.remove(user);
			em.flush();
			
			Events.instance().raiseEvent(Constants.USER_DELETED_EVENT, user);
		}
	}

	
}
