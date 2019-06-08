USE [Product_Supplier_db]
GO
/****** Object:  StoredProcedure [dbo].[sp_Count_Products_with_SupplierID]    Script Date: 05/06/2019 09:23:57 Õ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[sp_Count_Products_with_SupplierID]
(
	@supplierID int = NULL
)
AS
BEGIN

	SET NOCOUNT OFF

	SELECT COUNT(*) FROM Products WHERE Supplier_ID = @supplierID
END
