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

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.NoResultException;

import org.jboss.seam.annotations.AutoCreate;
import org.jboss.seam.annotations.In;
import org.jboss.seam.annotations.Name;

import com.sqs.AtestProject.domain.Album;
import com.sqs.AtestProject.domain.Comment;
import com.sqs.AtestProject.domain.Image;
import com.sqs.AtestProject.domain.MetaTag;
import com.sqs.AtestProject.domain.User;
/**
 * Class for manipulating with image entity. Analogous to DAO pattern.
 *  EJB3 Bean
 *
 * @author Andrey Markhel
 */
@Name("imageAction")
@Stateless
@AutoCreate
public class ImageAction implements IImageAction {

	@In(value = "entityManager")
    EntityManager em;

    /**
     * Remove image entity from database
     * @param image - image to delete
     * @throws AtestProjectException
     */
    public void deleteImage(Image image) throws AtestProjectException {
    	Album parentAlbum = image.getAlbum();
    	try{
			parentAlbum.removeImage(image);
			image.setImageTags(null);

			em.remove(image);
			em.flush();
    	} catch(Exception e){
    		parentAlbum.addImage(image);
        	throw new AtestProjectException(e.getMessage());
        }
    }

    /**
     * Synchronize state of image entity with database
     * @param image - image to Synchronize
     * @param metatagsChanged - boolean value, that indicates is metatags of this image were changed(add new or delete older)
     * @throws AtestProjectException
     */
    public void editImage(Image image, boolean metatagsChanged) throws AtestProjectException {
    	try{
    	if(metatagsChanged){
    		//Create cash of metatags early associated with image
            final List<MetaTag> removals = new ArrayList<MetaTag>(image.getImageTags());
            
            //Get string representation of current metatgs, associated with image and split them by comma
            final String[] tokens = image.getMetaString().split(Constants.COMMA);
            
            //Populate set of tokens - 'candidates to metatags'
            final Set<String> toks = new HashSet<String>();
            for (String s : tokens) {
            	if(!"".equals(s)){
            		toks.add(s.trim());
            	}
            }
            
            for (String s : toks) {
            	//Find metatag in early associated tags
            	MetaTag t = image.getTagByName(s);
                if (t != null) {
                	//If found - no work needed
                    removals.remove(t);
                } else {
                	//Find metatag in database
                	t = getTagByName(s);
                		if(t != null){
                			//If found simple add reference to it
                			image.addMetaTag(t);
                		}else{
                			//Create new metatag
                			t = new MetaTag();
                            t.setTag(s);
                            image.addMetaTag(t);
                            //Persist to database to prevent concurrent creation of other metatags with given name
                            em.persist(t);
                		}
                	}
                t = null;
            }
            
            for (MetaTag tag : removals) {
            	//If metatag in that collection, we need remove them
                image.removeMetaTag(tag);
            }
            //If this image is covering for album, break the reference
            if (image.isCovering()) {
                if (!image.equals(image.getAlbum().getCoveringImage())) {
                    image.getAlbum().setCoveringImage(image);
                }
            }else{
            	if (image.equals(image.getAlbum().getCoveringImage())) {
                    image.getAlbum().setCoveringImage(image.getAlbum().getImages().get(0));
                }
            }
            
    	}
        em.flush();
    	}
        catch(Exception e){
        	throw new AtestProjectException(e.getMessage());
        }
    }

    /**
     * Persist image entity to database
     * @param image - image to add
     * @throws AtestProjectException
     */
    public void addImage(Image image) throws AtestProjectException {
    	try{
    		 em.persist(image);
    	     image.getAlbum().addImage(image);
    	     em.flush();
    	}
        catch(Exception e){
        	throw new AtestProjectException(e.getMessage());
        }
    }

    /**
     * Remove comment from image
     * @param comment - comment to remove
     * @throws AtestProjectException
     */
    public void deleteComment(Comment comment) throws AtestProjectException {
    	try{
    		Image image = comment.getImage();
    		em.remove(comment);
			image.removeComment(comment);
    		em.flush();
		}
        catch(Exception e){
        	throw new AtestProjectException(e.getMessage());
        }
    }

    /**
     * Add comment from image
     * @param comment - comment to add
     * @throws AtestProjectException
     */
    public void addComment(Comment comment) throws AtestProjectException {
    	try{
    		comment.getImage().addComment(comment);
    		em.persist(comment);
    		em.flush();
    	}
        catch(Exception e){
        	throw new AtestProjectException(e.getMessage());
        }
    }

    /**
     * Find metatag object by its string representation
     * @param tag - string representation of metatag
     * @return metatag object or null
     */
    public MetaTag getTagByName(String tag) {
        final MetaTag t;
        try {
            t = (MetaTag) em.createNamedQuery(Constants.TAG_BY_NAME_QUERY).setParameter(Constants.TAG_PARAMETER, tag).getSingleResult();
        } catch (NoResultException nre) {
        	//If not found
            return null;
        }
        return t;
    }

    /**
     * Find most-popular metatags
     * @return list of most-popular metatags
     */
    @SuppressWarnings("unchecked")
	public List<MetaTag> getPopularTags() {
        return em.createNamedQuery(Constants.TAG_POPULAR_QUERY).setMaxResults(Constants.MAX_RESULTS).getResultList();
    }

    /**
     * Find List of metatags, similar to specified string. Used in autosuggect
     * @param suggest - string to search
     * @return list of most-popular metatags
     */
    @SuppressWarnings("unchecked")
	public List<MetaTag> getTagsLikeString(String suggest) {
        return (List<MetaTag>) em.createNamedQuery(Constants.TAG_SUGGEST_QUERY).setParameter(Constants.TAG_PARAMETER, suggest + Constants.PERCENT).getResultList();
    }

	/**
     * Check if image with specified path already exist in specified album
     * @param album - album to check
     * @param path - path to check
     * @return is image with specified path already exist
     */
	public boolean isImageWithThisPathExist(Album album, String path) {
		return em.createNamedQuery(Constants.IMAGE_PATH_EXIST_QUERY)
		.setParameter(Constants.PATH_PARAMETER, path)
		.setParameter(Constants.ALBUM_PARAMETER, album)
		.getResultList().size() != 0;
	}
	
	/**
     * Return count of images with path, that started from specified path already exist in specified album
     * @param album - album to check
     * @param path - path to check
     * @return count of images
     */
	public Long getCountIdenticalImages(Album album, String path) {
		return (Long)em.createNamedQuery(Constants.IMAGE_IDENTICAL_QUERY)
		.setParameter(Constants.PATH_PARAMETER, path + Constants.PERCENT)
		.setParameter(Constants.ALBUM_PARAMETER, album)
		.getSingleResult();
	}

	/**
     * Retrieve all cooments posted by given user.
     * @return list of comments
     */
	@SuppressWarnings("unchecked")
	public List<Comment> findAllUserComments(User user) {
		return (List<Comment>)em.createNamedQuery(Constants.USER_COMMENTS_QUERY)
		.setParameter(Constants.AUTHOR_PARAMETER, user)
		.getResultList();
	}
	
	/**
     * Refresh state of given image
     * @param image - image to Synchronize
     */
	public void resetImage(Image image) {
		em.refresh(image);
	}
}
