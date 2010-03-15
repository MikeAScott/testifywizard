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
package com.sqs.AtestProject.search;

import java.util.ArrayList;
import java.util.List;


import com.sqs.AtestProject.domain.Image;
import com.sqs.AtestProject.search.ISearchAction;
import com.sqs.AtestProject.service.AtestProjectException;

/**
 * Class, that encapsulate functionality related to search by image entity.
 * @author Andrey Markavtsov
 *
 */
public class SearchOptionByImage extends ISearchOption {
	
	private static final String TEMPLATE = "/includes/search/result/imageResult.xhtml";
	private static final String IMAGES_SEARCH_RESULT = "Images search result";
	private static final String IMAGES = "Images";
	
	/* (non-Javadoc)
	 * @see com.sqs.AtestProjectrch.ISearchOption#getName()
	 */
	public String getName() {
		return IMAGES;
	}
	
	/* (non-Javadoc)
	 * @see com.com.sqs.AtestProjectISearchOption#getSearchResultName()
	 */
	@Override
	public String getSearchResultName() {
		return IMAGES_SEARCH_RESULT;
	}
	
	/* (non-Javadoc)
	 * @see com.sqs.com.sqs.AtestProjectrchOption#search()
	 */
	@Override
	public void search(ISearchAction action, String q, boolean searchInMyAlbums, boolean searchInShared) throws AtestProjectException {
		List<Image> searchByImage = action.searchByImage(q, searchInMyAlbums, searchInShared);
		if(searchByImage != null){
			setSearchResult(searchByImage);
		}else{
			setSearchResult(new ArrayList<Image>());
		}
	}
	
	/* (non-Javadoc)
	 * @see com.sqs.Atescom.sqs.AtestProjectption#getSearchResultTemplate()
	 */
	@Override
	public String getSearchResultTemplate() {
		return TEMPLATE;
	}
}