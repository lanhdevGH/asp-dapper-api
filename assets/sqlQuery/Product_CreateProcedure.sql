-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Product_Create] 
	-- Add the parameters for the stored procedure here
	@name NVARCHAR(MAX),
	@description NVARCHAR(MAX),
	@content NVARCHAR(MAX),
	@sku NVARCHAR(50),
	@price DECIMAL(18, 2),
	@discountprice DECIMAL(18, 2),
	@imageurl NVARCHAR(MAX),
	@imagelist NVARCHAR(MAX),
	@viewcount INT,
	@seoalias NVARCHAR(255),
	@seotitle NVARCHAR(255),
	@seokeyword NVARCHAR(255),
	@seodescription NVARCHAR(MAX),
	@createdat DATETIME,
	@updatedat DATETIME,
	@isactive BIT,
	@ratetotal INT,
	@ratecount INT,
	@categoryids NVARCHAR(MAX),
	@categoryname NVARCHAR(255),
	@Id int out
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET XACT_ABORT ON; -- Tự động Rollback khi gặp lỗi trong transaction
	BEGIN TRAN		-- Bắt đầu một giao dịch - cho đến khi gặp commit mới tiến hành update lên cơ sở dữ liệu
	BEGIN TRY
		INSERT INTO [Products] (Sku, Price, DiscountPrice, ImageUrl, ImageList, ViewCount, CreatedAt, UpdatedAt, IsActive, RateTotal, RateCount)
		VALUES 
			(@sku, @price, @discountprice, @imageurl, @imagelist, @viewcount, GETDATE(), GETDATE(), @isactive, @ratetotal, @ratecount)

		SET @Id = SCOPE_IDENTITY()

		DELETE FROM [AttributeValueDateTimes] WHERE ProductId = @Id
		DELETE FROM [AttributeValueDecimals] WHERE ProductId = @Id
		DELETE FROM [AttributeValueInts] WHERE ProductId = @Id
		DELETE FROM [AttributeValueText] WHERE ProductId = @Id
		DELETE FROM [AttributeValueVarchars] WHERE ProductId = @Id

		INSERT INTO [AttributeValueVarchars] (AttributeId, ProductId, Value, 
		VALUES
			()


		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH
END
GO
