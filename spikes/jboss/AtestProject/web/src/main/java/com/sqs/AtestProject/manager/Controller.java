/**
 * License Agreement.
 *
 *  JBoss RichFaces - Ajax4jsf Component Library
 *
 * Copyright (C) 2007  Exadel, Inc.
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
import org.jboss.seam.annotations.In;
import org.jboss.seam.annotations.Name;
import org.jboss.seam.annotations.Observer;
import org.jboss.seam.annotations.Out;
import org.jboss.seam.annotations.Scope;
import org.jboss.seam.annotations.security.Restrict;
import org.jboss.seam.contexts.Contexts;
import org.jboss.seam.core.Events;
import org.richfaces.component.UITree;

import com.sqs.AtestProject.domain.User;
import com.sqs.AtestProject.service.Constants;
/**
 * This class represent 'C' in MVC pattern. It is logic that determine what actions invoked and what next page need to be showed.
 * Typically on almost all user actions, this class populates the model and determine new view to show.
 * Also contain utility logic, such as checking is the given shelf belongs to the specified user etc..
 * @author Andrey Markhel
 */
@Name("controller")
@Scope(ScopeType.EVENT)
public class Controller implements Serializable{

	private static final long serialVersionUID = 5656562187249324512L;
	
	@In @Out Model model;

	@In(scope = ScopeType.SESSION) User user;

	/**
	 * This method invoked after the user want to see all predefined shelves, existed in application
	 */
	public void selectPublicShelves(){
		model.resetModel(NavigationEnum.ANONYM, user);
	}
	
		
	/**
	 * This method invoked in cases, when it is need to clear fileUpload component
	 * 
	 */
	public void resetFileUpload(){
		pushEvent(Constants.CLEAR_FILE_UPLOAD_EVENT);
	}
			
	/**
	 * This method invoked after the user want to save just edited user to database.
	 * 
	 */
	@Restrict("#{s:hasRole('admin')}")
	public void editUser(){
		pushEvent(Constants.EDIT_USER_EVENT);
		model.resetModel(NavigationEnum.HOME, user);
	}
	
	/**
	 * This method invoked after the user want to interrupt edit user process
	 * 
	 */
	public void cancelEditUser(){
		pushEvent(Constants.CANCEL_EDIT_USER_EVENT);
		model.resetModel(NavigationEnum.ANONYM, user);
	}
	
	/**
	 * This method observes <code>Constants.AUTHENTICATED_EVENT</code> and invoked after the user successfully authenticate to the system
	 * @param u - authenticated user
	 */
	@Observer(Constants.AUTHENTICATED_EVENT)
	public void onAuthenticate(User u){
		model.resetModel(NavigationEnum.HOME, u);
	}
		
	/**
	 * This method invoked after the user want to see profile of the specified user
	 * @param user - user to see
	 * 
	 */
	public void showUser(User user){
		model.resetModel(NavigationEnum.USER_PREFS, user);
		Contexts.getConversationContext().set(Constants.AVATAR_DATA_COMPONENT, null);
	}
		
	/**
	 * This utility method invoked in case if you want to show to the user specified error in popup
	 * @param error - error to show
	 * 
	 */
	public void showError(String error){
		pushEvent(Constants.ADD_ERROR_EVENT, error);
	}
	
	/**
	 * This method observes <code>Constants.START_REGISTER_EVENT</code> and invoked after the user want to start registration process.
	 * 
	 */
	@Observer(Constants.START_REGISTER_EVENT)
	public void startRegistration(){
		model.resetModel(NavigationEnum.REGISTER, user);
	}
	
	/**
	 * This method invoked after the user want to interrupt registration process
	 * 
	 */
	public void cancelRegistration(){
		model.resetModel(NavigationEnum.ANONYM, user);
	}
	
	/**
	 * This utility method determine if the specified node should be marked as selected.
	 * Used in internal rich:tree mechanism
	 */
	public Boolean adviseNodeSelected(UITree tree) {
		Object currentNode = tree.getRowData();
		if(currentNode.equals(model.getSelectedUser())){
			return true;
		}
		return false;
	}
	
	
	/**
	 * This utility method used to determine if the specified user can be edited
	 * @param user - user to check
	 */
	public boolean isProfileEditable(User selectedUser){
		return selectedUser != null && selectedUser.equals(user);
	}
		
	private void pushEvent(String type, Object... parameters) {
		Events.instance().raiseEvent(type, parameters);
	}

}