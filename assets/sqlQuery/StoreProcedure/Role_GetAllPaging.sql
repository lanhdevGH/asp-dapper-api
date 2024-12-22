USE RestApiDapper2
GO
/****** Object:  StoredProcedure [dbo].[Role_GetAll]    Script Date: 12/22/2024 8:09:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Role_GetAllPaging
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
	FROM AspNetRoles R
	WHERE (@condition IS NULL OR R.Name LIKE @condition + '%')

	SELECT *
	FROM AspNetRoles R
	WHERE (@condition IS NULL OR R.Name LIKE @condition + '%')
	ORDER BY R.Name
	OFFSET (@pageIndex - 1) * @pageSize ROW
	FETCH NEXT @pageSize ROW ONLY
	
END
GO
