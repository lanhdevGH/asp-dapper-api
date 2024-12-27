USE RestApiDapper2
GO
/****** Object:  StoredProcedure [dbo].[Get_Permission_ByUserId]    Script Date: 1/12/2019 12:18:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Get_Permission_ByUserId]
	@userId varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select distinct 
		f.Id + '_' + a.Id
	from Permissions p 
	join Functions f on f.Id = p.FunctionId
	join AspNetRoles r on p.RoleId = r.Id
	join Actions a on p.ActionId = a.Id
	join AspNetUserRoles ur on r.Id = ur.RoleId
	where ur.UserId = @userId 
END
