CREATE TABLE Customers (
	CustomerID int IDENTITY(1,1),
	FullName varchar(max),
	StreetAddress varchar(max),
	PhoneNumber varchar(max),
	Email varchar(max),
	City varchar(max),
	State_ varchar(max),
	Postcode int,
	PRIMARY KEY (CustomerID))

CREATE TABLE Staff (
	StaffID int IDENTITY (1,1),
	FullName varchar(max),
	PasswordHash varchar(max),
	Privelege varchar(max),
	PRIMARY KEY (StaffID))

CREATE TABLE Products (
	ProductID bigint IDENTITY(1,1),
	ProductIDNumber varchar(max),
	Description_ varchar(max),
	Quantity varchar(max),
	Price float,
	PRIMARY KEY (ProductID))

CREATE TABLE Transactions (
	TransactionID int IDENTITY (1,1),
	Timestamp_ datetime2,
	CustomerID int,
	StaffID int,
	ProductID bigint,
	PRIMARY KEY (TransactionID),
	FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
	FOREIGN KEY (StaffID) REFERENCES Staff(StaffID),
	FOREIGN KEY (ProductID) REFERENCES Products(ProductID))