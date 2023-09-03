USE SYNTHONS
GO

DROP FUNCTION IF EXISTS dbo.udf_GetTotalDue
DROP FUNCTION IF EXISTS dbo.udf_ProductCurrentPrice
DROP FUNCTION IF EXISTS dbo.udf_ServiceCurrentPrice
GO

CREATE FUNCTION udf_GetTotalDue (@SaleID int)
RETURNS money
AS
BEGIN
	DECLARE @totalDue money, @productsPrice money, @servicesPrice money

	SELECT @productsPrice = SUM(SP.TotalPrice)
	FROM SaleProduct as SP
	WHERE SP.SaleID = @SaleID

	SELECT @servicesPrice = SUM(SS.Price)
	FROM SaleService as SS
	WHERE SS.SaleID = @SaleID

	SET @totalDue = COALESCE(@productsPrice, 0) + COALESCE(@servicesPrice, 0)

	RETURN @totalDue
END
GO

CREATE FUNCTION udf_ProductCurrentPrice (@ProductID int)
RETURNS money
AS
BEGIN
	DECLARE @price money = (
		SELECT TOP 1 PP.Price
		FROM ProductPrice as PP
		WHERE PP.ProductID = @ProductID
		ORDER BY PP.AssignmentDate DESC
	)
	RETURN @price
END
GO

CREATE FUNCTION udf_ServiceCurrentPrice (@ServiceID int)
RETURNS money
AS
BEGIN
	DECLARE @price money = (
		SELECT TOP 1 SP.Price
		FROM ServicePrice as SP
		WHERE SP.ServiceID = @ServiceID
		ORDER BY SP.AssignmentDate DESC
	)
	RETURN @price
END
GO