package com.sqs.projects.HelloWorldProject.model;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

import org.hibernate.validator.Length;
import org.hibernate.validator.NotNull;
import org.jboss.seam.annotations.Name;

import com.sqs.projects.HelloWorldProject.model.Gender;

@Entity
@Name("user")
public class User extends AbstractEntity {

	private static final long serialVersionUID = 1L;
	
	@NotNull
	@Column(nullable = false)
	private String username;
	
	@NotNull
	@Column(nullable = false)
	private String email;

	@NotNull
	@Column(nullable = false)
	@Length(min=5, max=15)
	private String password;
	
	@NotNull
	@Column(nullable = false)
	@Temporal(TemporalType.DATE)
	private Date birthday;
	
	@NotNull
	@Enumerated(EnumType.STRING)
	@Column(nullable = false)
	private Gender gender;
	
	//required for jpa
	public User(){
		super();
	}
	
	public User(String username, String email, String password, Date birthday, Gender gender) {
		this();
		this.username = username;
		this.email = email;
		this.password = password;
		this.birthday = birthday;
		this.gender = gender;
	}
	
	public String getUsername() {
		return username;
	}

	public void setUsername(String username) {
		this.username = username;
	}
	
	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}
	
	public Date getBirthday() {
		return birthday;
	}

	public void setBirthday(Date birthday) {
		this.birthday = birthday;
	}
	
	public Gender getGender() {
		return gender;
	}

	public void setGender(Gender gender) {
		this.gender = gender;
	}

	public String toString() {

		StringBuilder values = new StringBuilder();
		values.append("username = " + username);
		values.append("email = " + email);

		return values.toString();
	}

}
