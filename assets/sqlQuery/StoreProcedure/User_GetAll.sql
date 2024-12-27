USE [RestApiDapper2]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nguyen Thanh Lanh
-- Create date: 12/23/2024
-- Description:	Procedure get all user
-- =============================================
CREATE PROCEDURE User_GetAll 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRY
		SELECT [Id]
			  ,[UserName]
			  ,[NormalizedUserName]
			  ,[Email]
			  ,[NormalizedEmail]
			  ,[EmailConfirmed]
			  ,[PasswordHash]
			  ,[SecurityStamp]
			  ,[ConcurrencyStamp]
			  ,[PhoneNumber]
			  ,[PhoneNumberConfirmed]
			  ,[TwoFactorEnabled]
			  ,[LockoutEnd]
			  ,[LockoutEnabled]
			  ,[AccessFailedCount]
			  ,[FullName]
			  ,[Address]
		FROM [dbo].[AspNetUsers]
	END TRY
	--
	BEGIN CATCH
		SELECT
		ERROR_MESSAGE() AS ErrorMessage, 
        ERROR_SEVERITY() AS ErrorSeverity, 
        ERROR_STATE() AS ErrorState;
	END CATCH


END
GO
