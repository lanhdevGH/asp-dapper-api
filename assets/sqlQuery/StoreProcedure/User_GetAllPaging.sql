USE [RestApiDapper2]
GO
/****** Object:  StoredProcedure [dbo].[Role_GetAllPaging]    Script Date: 12/25/2024 6:09:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[User_GetAllPaging]
	@condition nvarchar(50),
	@pageIndex int,
	@pageSize int,
	@totalRow int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT @totalRow = COUNT(*)
	FROM AspNetUsers U
	WHERE (@condition IS NULL OR U.FullName LIKE '%' + @condition + '%')

	SELECT *
	FROM AspNetUsers U
	WHERE (@condition IS NULL OR U.FullName LIKE '%' + @condition + '%')
	ORDER BY U.FullName
	OFFSET (@pageIndex - 1) * @pageSize ROW
	FETCH NEXT @pageSize ROW ONLY
	
END
