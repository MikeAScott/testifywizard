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


import com.sqs.AtestProject.domain.Album;
import com.sqs.AtestProject.search.ISearchAction;
import com.sqs.AtestProject.service.AtestProjectException;
/**
 * Class, that encapsulate functionality related to search by album entity.
 * @author Andrey Markavtsov
 *
 */
public class SearchOptionByAlbum extends ISearchOption {
	
	private static final String TEMPLATE = "/includes/search/result/albumsResult.xhtml";
	private static final String ALBUMS_SEARCH_RESULT = "Albums search result";
	private static final String ALBUMS = "Albums";
	
	/* (non-Javadoc)
	 * @see com.sqs.AtestProject.search.ISearchOption#getName()
	 */
	@Override
	public String getName() {
		return ALBUMS;
	}
	
	/* (non-Javadoc)
	 * @see com.sqs.AtestProject.search.ISearchOption#getSearchResultName()
	 */
	@Override
	public String getSearchResultName() {
		return ALBUMS_SEARCH_RESULT;
	}
	
	/* (non-Javadoc)
	 * @see com.sqs.AtestProject.search.ISearchOption#search()
	 */
	@Override
	public void search(ISearchAction action, String q, boolean searchInMyAlbums, boolean searchInShared) throws AtestProjectException {
		List<Album> searchByAlbum = action.searchByAlbum(q, searchInMyAlbums, searchInShared);
		if(searchByAlbum != null){
			setSearchResult(searchByAlbum);
		}else{
			setSearchResult(new ArrayList<Album>());
		}
	}
	
	/* (non-Javadoc)
	 * @see com.sqs.AtestProject.search.ISearchOption#getSearchResultTemplate()
	 */
	@Override
	public String getSearchResultTemplate() {
		return TEMPLATE;
	}
}