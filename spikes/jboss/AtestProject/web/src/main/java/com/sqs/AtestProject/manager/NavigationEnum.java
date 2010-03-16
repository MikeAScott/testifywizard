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

/**
 * Class encapsulated all possible states, that can be applied to so called 'mainArea' area on the page.
 * This ensured that properly template will be applied, and user will be redirected to desired page.
 * Next template to show obviously determined in Controller and pushes to Model.
 * @author Andrey Markhel
 */

public enum NavigationEnum {
	ANONYM("includes/public.xhtml"),
	USER_PREFS("includes/userPrefs.xhtml"),
	REGISTER("includes/register.xhtml"),
	SEARCH("includes/search.xhtml"),
	HOME("includes/home.xhtml");
	
	private	NavigationEnum(String t){
		template=t;
	}
	
	private String template;
	
	public String getTemplate() {
		return template;
	}
}
