USE SYNTHONS
GO

DROP PROC IF EXISTS usp_SeedTestValues
DROP PROC IF EXISTS usp_CleanUpTables
DROP PROC IF EXISTS usp_DeleteUnusedCustomers
DROP PROC IF EXISTS usp_GetEmployeesCount
GO

CREATE PROC usp_SeedTestValues
AS
	INSERT INTO Product (Name, Manufacturer)
	VALUES
		('Macbook Air', 'Apple'),
		('iPhone X', 'Apple'),
		('iPhone XS', 'Apple'),
		('iPhone XR', 'Apple'),
		('iMac', 'Apple'),
		('Playstation 5', 'Sony'),
		('Xbox Series X', 'Microsoft'),
		('Surface Studio 2', 'Microsoft');
	
	INSERT INTO Service (Name)
	VALUES
		(N'Установка Windows'),
		(N'Чистка компьютера'),
		(N'Замена термопасты'),
		(N'Установка антивируса'),
		(N'Расширеная гарантия');

	INSERT INTO ProductPrice (ProductID, Price, AssignmentDate)
	VALUES
		(1, 1049, '2022-09-12 00:00:00'),
		(1, 1069, '2022-09-14 00:00:00'),
		(1, 1099, '2022-11-03 00:00:00'),
		(2, 235, '2017-09-12 00:00:00'),
		(3, 222, '2018-09-12 00:00:00'),
		(4, 226, '2018-09-12 00:00:00'),
		(5, 1248, '2020-09-12 00:00:00'),
		(6, 509, '2022-10-01 00:00:00'),
		(7, 499, '2022-11-05 00:00:00'),
		(8, 4299, '2023-02-15 00:00:00');

	INSERT INTO ServicePrice (ServiceID, Price, AssignmentDate)
	VALUES
		(1, 200, '2022-01-01 00:00:00'),
		(2, 10, '2022-01-01 00:00:00'),
		(3, 35, '2022-01-01 00:00:00'),
		(4, 110, '2022-01-01 00:00:00'),
		(5, 50, '2022-01-01 00:00:00');

	INSERT INTO Customer (LastName, FirstName, MiddleName)
	VALUES
		(N'Куплювсё', N'Иван', N'Всевидович'),
		(N'Неиду', N'Анна', N'Некупиновна'),
		(N'Барышников', N'Петр', N'Перекупович'),
		(N'Торговкина', N'Елена', N'Витальевна'),
		(N'Скидочкин', N'Николай', N'Николаевич');

	INSERT INTO Employee (LastName, FirstName, MiddleName)
	VALUES
		(N'Клавиатуров', N'Алексей', N'Михайлович'),
		(N'Мышкина', N'Елена', N'Александровна'),
		(N'Принтеров', N'Денис', N'Владимирович'),
		(N'Мониторова', N'Анна', N'Степановна'),
		(N'Хардов', N'Игорь', N'Петрович');

	INSERT INTO Sale (CustomerID, EmployeeID, OrderDate)
	VALUES
		(1, 1, '2023-04-01 12:00:00'),
		(2, 1, '2023-04-01 13:00:00'),
		(3, 2, '2023-04-01 14:00:00');

	INSERT INTO SaleProduct (SaleID, ProductID, UnitPrice, Qty)
	VALUES
		(1, 1, dbo.udf_ProductCurrentPrice(1), 5),
		(1, 2, dbo.udf_ProductCurrentPrice(2), 15),
		(2, 1, dbo.udf_ProductCurrentPrice(1), 3);

	INSERT INTO SaleService (SaleID, ServiceID, Price) 
	VALUES
		(1, 2, dbo.udf_ServiceCurrentPrice(2)),
		(1, 3, dbo.udf_ServiceCurrentPrice(3)),
		(3, 1, dbo.udf_ServiceCurrentPrice(1)),
		(3, 1, dbo.udf_ServiceCurrentPrice(1));
GO

CREATE PROC usp_CleanUpTables
AS
	DELETE FROM ProductPrice;
	DELETE FROM ServicePrice;
	DELETE FROM SaleProduct;
	DELETE FROM SaleService;
	DELETE FROM Sale;
	DELETE FROM Customer;
	DELETE FROM Employee;
	DELETE FROM Product;
	DELETE FROM Service;
GO

CREATE PROC usp_DeleteUnusedCustomers
AS
	DELETE FROM Customer
	WHERE NOT EXISTS (
	SELECT *
	FROM Sale
	WHERE Sale.CustomerID = Customer.CustomerID
)
GO
	
CREATE PROC usp_GetEmployeesCount
AS
	SELECT COUNT(*)
	FROM Employee
GO