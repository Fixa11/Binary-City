
Create database BinaryCity;
use BinaryCity;



CREATE TABLE Client (
	Id int IDENTITY(1,1) PRIMARY KEY,
 	Name Varchar(max),
	ClientCode Varchar(6) UNIQUE

);


CREATE TABLE Contact (
	Id int IDENTITY(1,1) PRIMARY KEY,
 	Name Varchar(max),
	Surname Varchar(max),
	Email Varchar(64) UNIQUE
);


CREATE TABLE ClientsContact (
	Id int IDENTITY(1,1) PRIMARY KEY,
 	ClientId int,
	Contactid int,
	 FOREIGN KEY (ClientId) REFERENCES  Client(Id),
	 FOREIGN KEY (Contactid) REFERENCES  Contact(Id)
);
