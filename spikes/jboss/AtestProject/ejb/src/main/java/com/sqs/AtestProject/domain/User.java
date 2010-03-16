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
package com.sqs.AtestProject.domain;

import java.io.File;
import java.io.Serializable;
import java.util.Date;


import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;
import javax.persistence.Transient;
import javax.persistence.UniqueConstraint;

import org.hibernate.validator.Email;
import org.hibernate.validator.Length;
import org.hibernate.validator.NotEmpty;
import org.hibernate.validator.NotNull;
import org.jboss.seam.ScopeType;
import org.jboss.seam.annotations.AutoCreate;
import org.jboss.seam.annotations.Name;
import org.jboss.seam.annotations.Scope;

/**
 * Class for representing User Entity
 * EJB3 Entity Bean
 *
 * @author Andrey Markhel
 */

@NamedQueries({
	@NamedQuery(
			name = "user-login",
			query = "select u from User u where u.login = :username and u.passwordHash = :password"
	),
	@NamedQuery(
			name = "user-comments",
			query = "select c from Comment c where c.author = :author"
	),
	@NamedQuery(
			name = "user-exist",
			query = "select u from User u where u.login = :login"
	),
	@NamedQuery(
			name = "email-exist",
			query = "select u from User u where u.email = :email"
	),
	@NamedQuery(
			name = "user-user",
			query = "select u from User u where u.login = :login"
	)
		})

@Entity
@Scope(ScopeType.SESSION)
@Name("user")
@AutoCreate
@Table(name="User", uniqueConstraints = {
	@UniqueConstraint(columnNames = "login"),
	@UniqueConstraint(columnNames = "email")
		}
)
public class User implements Serializable {

	private static final long serialVersionUID = 1L;

	@Id
	@GeneratedValue
	private Long id;

	@NotNull
	private String passwordHash;

	@NotNull
	@NotEmpty
	@Length(min = 3)
	@Column(length = 255, nullable = false)
	private String firstName;

	@NotNull
	@NotEmpty
	@Length(min = 3)
	@Column(length = 255, nullable = false)
	private String secondName;

	@Column(length = 255, nullable = false)
	@NotNull
	@NotEmpty
	@Email
	private String email;

	@Column(length = 255, nullable = false)
	@NotNull
	@NotEmpty
	@Length(min = 3)
	private String login;

	@Transient
	private String password;

	@Transient
	private String confirmPassword;

	@Temporal(TemporalType.TIMESTAMP)
	private Date birthDate;

	@NotNull
	private Sex sex = Sex.MALE;

	private Boolean hasAvatar;

	private boolean preDefined;

	public boolean isPreDefined() {
		return preDefined;
	}

	public void setPreDefined(boolean preDefined) {
		this.preDefined = preDefined;
	}

	//----------------Getters, Setters
	public String getFirstName() {
		return firstName;
	}

	public void setFirstName(String firstName) {
		this.firstName = firstName;
	}

	public String getSecondName() {
		return secondName;
	}

	public void setSecondName(String secondName) {
		this.secondName = secondName;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getPasswordHash() {
		return passwordHash;
	}

	public void setPasswordHash(String passwordHash) {
		this.passwordHash = passwordHash;
	}

	public String getLogin() {
		return login;
	}

	public void setLogin(String login) {
		this.login = login;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public Date getBirthDate() {
		return birthDate;
	}

	public void setBirthDate(Date birthDate) {
		this.birthDate = birthDate;
	}

	public Long getId() {
		return id;
	}

	public String getConfirmPassword() {
		return confirmPassword;
	}

	public void setConfirmPassword(String confirmPassword) {
		this.confirmPassword = confirmPassword;
	}

	public Sex getSex() {
		return sex;
	}

	public void setSex(Sex sex) {
		this.sex = sex;
	}

	public Boolean getHasAvatar() {
		return hasAvatar;
	}

	public void setHasAvatar(Boolean hasAvatar) {
		this.hasAvatar = hasAvatar;
	}

	//---------------------------Business methods



	/**
	 * Return relative path of folder with user's images in file-system(relative to uploadRoot parameter)
	 */
	public String getPath() {
		if (this.getId() == null) {
			return null;
		}
		return File.separator + this.getLogin() + File.separator;
	}


		
	public boolean equals(final Object o) {
		if (this == o) return true;
		if (o == null || getClass() != o.getClass()) return false;

		final User user = (User) o;

		if (id != null ? !id.equals(user.id) : user.id != null) return false;
		if (login != null ? !login.equals(user.login) : user.login != null)
			return false;

		return true;
	}

	public int hashCode() {
		int result = id != null ? id.hashCode() : 0;
		result = 31 * result + (login != null ? login.hashCode() : 0);
		return result;
	}
}