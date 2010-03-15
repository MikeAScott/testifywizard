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
package com.sqs.AtestProject.search;

import java.util.List;

import javax.ejb.Local;


import com.sqs.AtestProject.domain.Album;
import com.sqs.AtestProject.domain.Image;
import com.sqs.AtestProject.domain.MetaTag;
import com.sqs.AtestProject.domain.Shelf;
import com.sqs.AtestProject.domain.User;
import com.sqs.AtestProject.service.AtestProjectException;

/**
 * Interface for search actions
 *
 * @author Andrey Markhel
 */
@Local
public interface ISearchAction {
	public List<Image> searchByImage(String query, boolean searchInMyAlbums, boolean searchInShared) throws AtestProjectException;
	
	public List<MetaTag> searchByTags(String query, boolean searchInMyAlbums, boolean searchInShared) throws AtestProjectException;
	
	public List<Album> searchByAlbum(String query, boolean searchInMyAlbums, boolean searchInShared) throws AtestProjectException;
	
	public List<User> searchByUsers(String query, boolean searchInMyAlbums, boolean searchInShared) throws AtestProjectException;
	
	public List<Shelf> searchByShelves(String query,boolean searchInMyAlbums, boolean searchInShared) throws AtestProjectException;
	
}
