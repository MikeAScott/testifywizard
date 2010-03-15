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
package com.sqs.AtestProject.service;

import java.util.List;

import javax.ejb.Local;

import com.sqs.AtestProject.domain.Album;
import com.sqs.AtestProject.domain.Comment;
import com.sqs.AtestProject.domain.Image;
import com.sqs.AtestProject.domain.MetaTag;
import com.sqs.AtestProject.domain.User;

/**
 * Interface for manipulating with image entity
 *
 * @author Andrey Markhel
 */

@Local
public interface IImageAction {

	public void deleteImage(Image image) throws AtestProjectException;

	public void editImage(Image image, boolean metatagsChanged) throws AtestProjectException;

	public void addImage(Image image) throws AtestProjectException;
	
	public void deleteComment(Comment comment) throws AtestProjectException;
	
	public void addComment(Comment comment) throws AtestProjectException;

	public MetaTag getTagByName(String tag);

	public List<MetaTag> getPopularTags();

	public List<MetaTag> getTagsLikeString(String suggest);

	public boolean isImageWithThisPathExist(Album album, String path);
	
	public Long getCountIdenticalImages(Album album, String path);

	public List<Comment> findAllUserComments(User user);
	
	public void resetImage(Image imageo);

}