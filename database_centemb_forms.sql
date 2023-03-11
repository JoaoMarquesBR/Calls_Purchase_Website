
-- DROP tables in DATABASE
-- Needs to run at least three time since Some of It might fail at first due to their relationships
drop table Form
drop table Purchase
drop table Account



--creation of the tables.

create table Account(
 accountID int identity(1,1) primary key,
 groupPermission varchar(20)not null,
 email varchar(50),
 accountName varchar(30)not null,
 username varchar(20)not null,
 password varchar(20)not null,
)

create table Form(
	formID int identity(1,1) primary key,
	accountID int,
	companyName varchar(20),
	repName varchar(20),
	callDate date,
	timeLength varchar(20),
	callDesc varchar(200),
	issueSolved varchar(20),
	followUp varchar(20),	
	constraint forms_accountID_FK foreign key(accountID)  references Account(accountID)
)


create table Purchase(
	Purchase_ID int identity(1,1) primary key ,
	accountID int not null,
	purchaseDate date not null,
	supplier nvarchar(20)not null,
	quantity int not null,
	productPrice money not null,
	tax money,
	net money,
	totalAfterTax money,
	reference nvarchar(20) not null,
	status varchar(20),
	accountID_approver int,
	constraint Purchase_Buyer_ID foreign key(accountID)  references Account(accountID)
)



-- Insert original ADMIN Account 
insert into Account(groupPermission,email,accountName,username,password) values ('Administrator','johnmarques.br2002@gmail.com','admin','admin','123')

select * from Account

--
