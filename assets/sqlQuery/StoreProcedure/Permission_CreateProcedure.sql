USE [RestApiDapper2]
GO

CREATE TYPE dbo.PermissionsType AS TABLE
(
    RoleId UNIQUEIDENTIFIER,
    FunctionId varchar(50),
    ActionId varchar(50)
);

/****** Object:  StoredProcedure [dbo].[Create_Permission]    Script Date: 1/12/2019 12:17:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TEDU
-- Create date: 11/1/2018
-- Description:	Save permissions for role id
-- =============================================
--create TYPE [dbo].[Permission] AS TABLE(
--    [RoleId] uniqueidentifier,
--    [FunctionId] [varchar](50) NOT NULL,
--    [ActionId] [varchar](50) NOT NULL
--)
--GO

Create PROCEDURE [dbo].[Permission_CreateProcedure]
	@roleId uniqueidentifier,
	@permissions dbo.PermissionsType readonly
AS
BEGIN
	SET NOCOUNT ON;
	set xact_abort on;
	begin tran
	begin try
	   delete from Permissions where RoleId = @roleId

	   insert into Permissions (RoleId,FunctionId,ActionId)
	   select RoleId,FunctionId,ActionId from @permissions

	commit
	end try
	begin catch
		rollback
		  DECLARE @ErrorMessage VARCHAR(2000)
		  SELECT @ErrorMessage = 'Lỗi: ' + ERROR_MESSAGE()
		  RAISERROR(@ErrorMessage, 16, 1)
	end catch

END
