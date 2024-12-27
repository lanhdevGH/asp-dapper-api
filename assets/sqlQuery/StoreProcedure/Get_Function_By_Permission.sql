USE RestApiDapper2
GO
/****** Object:  StoredProcedure [dbo].[Get_Function_ByPermission]    Script Date: 1/12/2019 12:18:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Get_Function_ByPermission]
	@userId varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select 
		f.Id,
		f.Name,
		f.Url,
		f.ParentId,
		f.IsActive,
		f.SortOrder,
		f.CssClass
	from Functions f
	join Permissions p on f.Id = p.FunctionId
	join AspNetRoles r on p.RoleId = r.Id
	join Actions a on p.ActionId = a.Id
	join AspNetUserRoles ur on r.Id = ur.RoleId
	where ur.UserId = @userId and a.Id ='VIEW'
END