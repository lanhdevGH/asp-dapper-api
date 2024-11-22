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
CREATE PROCEDURE [GetAllProducts]
AS
BEGIN
	SET NOCOUNT ON;
	-- Tạo bảng tạm để chứa kết quả
	Declare @ProductTmp Table ( Id int, Name nvarchar(300), Description nvarchar(600), Content ntext, Sku varchar(300), 
								Price decimal(18, 2), DiscountPrice decimal(18, 2), 
								ImageUrl nvarchar(300), ImageList nvarchar(MAX), 
								ViewCount int, CreateDate datetime2(7), UpdateDate datetime2(7), 
								RateTotal int, RateCount int, IsActive bit, CategoryId int,
								ExtendAttributes nvarchar(max));
	-- Lấy thông tin sản phẩm chung - cho vào bảng tạm
	Insert into @ProductTmp (Id, Name, Description, Content, Price, DiscountPrice,
							ImageUrl, ImageList, ViewCount, CreateDate, UpdateDate,
							RateTotal, RateCount, IsActive, CategoryId)
	Select p.Id, p.Name, p.Description, p.Content, p.Price, p.DiscountPrice,
			p.ImageUrl, p.ImageList, p.ViewCount, p.CreateDate, p.UpdateDate,
			p.RateTotal, p.RateCount, p.IsActive, p.CategoryId
	From Products p;

	Update pt
	Set ExtendAttributes = COALESCE(pt.ExtendAttributes + ';', '') + attrDateTime.ExtendAttributes
	From @ProductTmp pt
	Cross apply(SELECT STRING_AGG(CONCAT(a.Code, ':', ps.Value), ';') AS ExtendAttributes
        FROM AttributeValueDateTimes ps
        JOIN Attributes a ON ps.AttributeId = a.Id
        WHERE ps.ProductId = pt.Id) as attrDateTime;
    
	Update pt
	Set ExtendAttributes = COALESCE(pt.ExtendAttributes + ';', '') + attrInt.ExtendAttributes
	From @ProductTmp pt
	Cross apply(SELECT STRING_AGG(CONCAT(a.Code, ':', ps.Value), ';') AS ExtendAttributes
        FROM AttributeValueInts ps
        JOIN Attributes a ON ps.AttributeId = a.Id
        WHERE ps.ProductId = pt.Id) as attrInt;
		
	Update pt
	Set ExtendAttributes = COALESCE(pt.ExtendAttributes + ';', '') + attrString.ExtendAttributes
	From @ProductTmp pt
	Cross apply(SELECT STRING_AGG(CONCAT(a.Code, ':', ps.Value), ';') AS ExtendAttributes
        FROM AttributeValueNVarchars ps
        JOIN Attributes a ON ps.AttributeId = a.Id
        WHERE ps.ProductId = pt.Id) as attrString;
			
	Update pt
	Set ExtendAttributes = COALESCE(pt.ExtendAttributes + ';', '') + attrText.ExtendAttributes
	From @ProductTmp pt
	Cross apply(SELECT STRING_AGG(CONCAT(a.Code, ':', ps.Value), ';') AS ExtendAttributes
        FROM AttributeValueTexts ps
        JOIN Attributes a ON ps.AttributeId = a.Id
        WHERE ps.ProductId = pt.Id) as attrText;

	Update pt
	Set ExtendAttributes = COALESCE(pt.ExtendAttributes + ';', '') + attrDecimal.ExtendAttributes
	From @ProductTmp pt
	Cross apply(SELECT STRING_AGG(CONCAT(a.Code, ':', ps.Value), ';') AS ExtendAttributes
        FROM AttributeValueDecimals ps
        JOIN Attributes a ON ps.AttributeId = a.Id
        WHERE ps.ProductId = pt.Id) as attrDecimal;

	-- Trả về kết quả
    SELECT * FROM @ProductTmp;
END
GO


-- RUN
EXEC GetAllProducts; 
