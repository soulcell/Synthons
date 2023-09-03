USE SYNTHONS
GO

DROP TRIGGER IF EXISTS dbo.TR_SaleProduct_TotalPriceUpdate
DROP TRIGGER IF EXISTS dbo.TR_SaleService_TotalPriceUpdate
DROP TRIGGER IF EXISTS dbo.TR_SaleProduct_PreventAddingSalesAfterPaid
DROP TRIGGER IF EXISTS dbo.TR_SaleService_PreventAddingSalesAfterPaid
GO

CREATE TRIGGER TR_SaleProduct_TotalPriceUpdate
ON SaleProduct
AFTER INSERT
AS
	UPDATE Sale
	SET TotalDue = dbo.udf_GetTotalDue(SaleID)
	WHERE SaleID IN (
		SELECT I.SaleID
		FROM INSERTED as I
	)
GO

CREATE TRIGGER TR_SaleService_TotalPriceUpdate
ON SaleService
AFTER INSERT
AS
	UPDATE Sale
	SET TotalDue = dbo.udf_GetTotalDue(SaleID)
	WHERE SaleID IN (
		SELECT I.SaleID
		FROM INSERTED as I
	)
GO

CREATE TRIGGER TR_SaleProduct_PreventModifyingSalesAfterPaid
ON SaleProduct
AFTER INSERT, UPDATE, DELETE
AS
	IF EXISTS (
		SELECT *
		FROM Sale as S
		JOIN inserted as i
		ON S.SaleID = i.SaleID
		WHERE S.PaymentDate IS NOT NULL
		)
	BEGIN
		RAISERROR ('This sale order has already been paid', 16, 1);
		ROLLBACK TRANSACTION;
	RETURN
	END;
GO

CREATE TRIGGER TR_SaleService_PreventModifyingSalesAfterPaid
ON SaleService
AFTER INSERT, UPDATE, DELETE
AS
	IF EXISTS (
		SELECT *
		FROM Sale as S
		JOIN inserted as i
		ON S.SaleID = i.SaleID
		WHERE S.PaymentDate IS NOT NULL
		)
	BEGIN
		RAISERROR ('This sale order has already been paid', 16, 1);
		ROLLBACK TRANSACTION;
	RETURN
	END;
GO