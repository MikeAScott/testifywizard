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
package com.sqs.AtestProject.manager;

import java.io.Serializable;

import org.jboss.seam.ScopeType;
import org.jboss.seam.annotations.AutoCreate;
import org.jboss.seam.annotations.Name;
import org.jboss.seam.annotations.Observer;
import org.jboss.seam.annotations.Scope;

import com.sqs.AtestProject.domain.User;
import com.sqs.AtestProject.service.Constants;
/**
 * This class represent 'M' in MVC pattern. It is storage to application flow related data such as selectedAlbum, image, mainArea to preview etc..
 *
 * @author Andrey Markhel
 */
@Name("model")
@Scope(ScopeType.CONVERSATION)
@AutoCreate
public class Model implements Serializable{

	private static final long serialVersionUID = -1767281809514660171L;
	
	private User selectedUser;
	
	private NavigationEnum mainArea;

	
	/**
	 * This method invoked after the almost user actions, to prepare properly data to show in the UI.
	 * @param mainArea - next Area to show(determined in controller)
	 * @param selectedUser - user, that was selected(determined in controller)
	 * @param selectedShelf - shelf, that was selected(determined in controller)
	 * @param selectedAlbum - album, that was selected(determined in controller)
	 * @param selectedImage - image, that was selected(determined in controller)
	 * @param images - list of images, to show during slideshow process(determined in controller)
	 */
	public void resetModel(NavigationEnum mainArea, User selectedUser){
		this.setSelectedUser(selectedUser);
		this.setMainArea(mainArea);
	}
	
	/**
	 * This method observes <code> Constants.UPDATE_MAIN_AREA_EVENT </code>event and invoked after the user actions, that not change model, but change area to preview
	 * @param mainArea - next Area to show
	 * 
	 */
	@Observer(Constants.UPDATE_MAIN_AREA_EVENT)
	public void setMainArea(NavigationEnum mainArea) {
		this.mainArea = mainArea;
	}


	public NavigationEnum getMainArea() {
		return mainArea;
	}
	
	public User getSelectedUser() {
		return selectedUser;
	}

	private void setSelectedUser(User selectedUser) {
		this.selectedUser = selectedUser;
	}
}