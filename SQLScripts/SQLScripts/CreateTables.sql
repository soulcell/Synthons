USE SYNTHONS
GO

DROP TABLE IF EXISTS SYNTHONS.dbo.SaleProduct
DROP TABLE IF EXISTS SYNTHONS.dbo.SaleService
DROP TABLE IF EXISTS SYNTHONS.dbo.Sale
DROP TABLE IF EXISTS SYNTHONS.dbo.ProductPrice
DROP TABLE IF EXISTS SYNTHONS.dbo.ServicePrice
DROP TABLE IF EXISTS SYNTHONS.dbo.Product
DROP TABLE IF EXISTS SYNTHONS.dbo.Service
DROP TABLE IF EXISTS SYNTHONS.dbo.Customer
DROP TABLE IF EXISTS SYNTHONS.dbo.Employee


CREATE TABLE Customer
(
	CustomerID int IDENTITY(1,1) PRIMARY KEY,
	LastName nvarchar(50) NOT NULL,
	FirstName nvarchar(50) NOT NULL,
	MiddleName nvarchar(50),
	BirthDate date,
	PhoneNumber char(12),
	EmailAddress nvarchar(100)
);


CREATE TABLE Employee
(
	EmployeeID int IDENTITY(1,1) PRIMARY KEY,
	LastName nvarchar(50) NOT NULL,
	FirstName nvarchar(50) NOT NULL,
	MiddleName nvarchar(50),
	BirthDate date
);

CREATE TABLE Sale
(
	SaleID int IDENTITY(1,1) PRIMARY KEY,
	CustomerID int FOREIGN KEY REFERENCES Customer(CustomerID),
	EmployeeID int NOT NULL FOREIGN KEY REFERENCES Employee(EmployeeID),
	OrderDate datetime2(2) NOT NULL,
	PaymentDate datetime2(2),
	TotalDue money
);

CREATE TABLE Product
(
	ProductID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(100) NOT NULL ,
	Description nvarchar(256),
	Manufacturer nvarchar(100)
);

CREATE TABLE Service
(
	ServiceID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(100) NOT NULL ,
	Description nvarchar(256)
);

CREATE TABLE ProductPrice
(
	ProductPriceID int IDENTITY(1,1) PRIMARY KEY,
	ProductID int FOREIGN KEY REFERENCES Product(ProductID),
	Price money NOT NULL,
	AssignmentDate datetime2(2) NOT NULL
);

CREATE TABLE ServicePrice
(
	ServicePriceID int IDENTITY(1,1) PRIMARY KEY,
	ServiceID int FOREIGN KEY REFERENCES Service(ServiceID),
	Price money NOT NULL,
	AssignmentDate datetime2(2) NOT NULL
);

CREATE TABLE SaleProduct
(
	SaleProductID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	SaleID int FOREIGN KEY REFERENCES Sale(SaleID),
	ProductID int NOT NULL FOREIGN KEY REFERENCES Product(ProductID),
	UnitPrice money NOT NULL,
	Qty int NOT NULL,
	TotalPrice AS UnitPrice * Qty
)

CREATE TABLE SaleService
(
	SaleServiceID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	SaleID int FOREIGN KEY REFERENCES Sale(SaleID),
	ServiceID int NOT NULL FOREIGN KEY REFERENCES Service(ServiceID),
	Price money NOT NULL
)