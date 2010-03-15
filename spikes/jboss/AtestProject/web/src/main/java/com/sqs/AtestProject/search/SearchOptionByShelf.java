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


import com.sqs.AtestProject.domain.Shelf;
import com.sqs.AtestProject.search.ISearchAction;
import com.sqs.AtestProject.service.AtestProjectException;

/**
 * Class, that encapsulate functionality related to search by shelf entity.
 * @author Andrey Markavtsov
 *
 */
public class SearchOptionByShelf extends ISearchOption {

	private static final String TEMPLATE = "/includes/search/result/shelfResult.xhtml";
	private static final String SHELF_SEARCH_RESULT = "Shelf search result";
	private static final String SHELVES = "Shelves";

	/* (non-Javadoc)
	 * @see com.sqs.AtestProjectrch.ISearchOption#getName()
	 */
	@Override
	public String getName() {
		return SHELVES;
	}

	/* (non-Javadoc)
	 * @see com.com.sqs.AtestProjectISearchOption#getSearchResultName()
	 */
	@Override
	public String getSearchResultName() {
		return SHELF_SEARCH_RESULT;
	}

	/* (non-Javadoc)
	 * @see com.sqs.com.sqs.AtestProjectrchOption#getSearchResultTemplate()
	 */
	@Override
	public String getSearchResultTemplate() {
		return TEMPLATE;
	}

	/* (non-Javadoc)
	 * @see com.sqs.Atescom.sqs.AtestProjectption#search(com.sqs.AtestProcom.sqs.AtestProjecton, java.lang.String, boolean, boolean)
	 */
	@Override
	public void search(ISearchAction action, String searchQuery,
			boolean searchInMyAlbums, boolean searchInShared) throws AtestProjectException {
		List<Shelf> list = action.searchByShelves(searchQuery, searchInMyAlbums, searchInShared);
		if(list != null){
			setSearchResult(list);
		}else{
			setSearchResult(new ArrayList<Shelf>());
		}
	}
}