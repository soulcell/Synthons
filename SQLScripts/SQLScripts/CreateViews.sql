USE SYNTHONS
GO

DROP VIEW IF EXISTS dbo.VI_SalesJournal
DROP VIEW IF EXISTS dbo.VI_ProductSoldTotal
DROP VIEW IF EXISTS dbo.VI_ServiceSoldTotal
GO

CREATE VIEW VI_SalesJournal AS
	SELECT 
		S.SaleID,
		C.FirstName + ' ' + C.LastName as Customer,
		E.FirstName + ' ' + E.LastName as Employee,
		S.PaymentDate,
		S.TotalDue
	FROM Sale as S
	INNER JOIN Customer as C
	ON S.CustomerID = C.CustomerID
	INNER JOIN Employee as E
	ON E.EmployeeID = S.EmployeeID
GO

CREATE VIEW VI_ProductSoldTotal AS
	SELECT P.ProductID, Name, TotalSold, TotalPrice
	FROM Product as P
	LEFT JOIN (
		SELECT SP.ProductID, SUM(SP.Qty) as TotalSold, SUM(SP.TotalPrice) as TotalPrice
		FROM SaleProduct as SP
		JOIN Sale as S
		ON SP.SaleID = S.SaleID
		--WHERE S.PaymentDate IS NOT NULL
		GROUP BY SP.ProductID
	) as SQ
	ON SQ.ProductID = P.ProductID
GO

CREATE VIEW VI_ServiceSoldTotal AS
	SELECT S.ServiceID, Name, TotalSold, TotalPrice
	FROM Service as S
	LEFT JOIN (
		SELECT SS.ServiceID, COUNT(SS.ServiceID) as TotalSold, SUM(SS.Price) as TotalPrice
		FROM SaleService as SS
		JOIN Sale as S
		ON SS.SaleID = S.SaleID
		--WHERE S.PaymentDate IS NOT NULL
		GROUP BY SS.ServiceID
	) as SQ
	ON SQ.ServiceID = S.ServiceID
GO