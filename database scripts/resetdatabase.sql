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
CREATE PROCEDURE ResetDatabase
	
AS
BEGIN
	DELETE FROM [dbo].[HC_0] 
	DELETE FROM [dbo].[HC_1] 
	DELETE FROM [dbo].[HC_2] 
	DELETE FROM [dbo].[HC_3] 
	DELETE FROM [dbo].[HC_4] 
	DELETE FROM [dbo].[HC_5] 
	DELETE FROM [dbo].[HC_6] 

	DELETE FROM [dbo].[BCC_0] 
	DELETE FROM [dbo].[BCC_1] 
	DELETE FROM [dbo].[BCC_2] 
	DELETE FROM [dbo].[BCC_3] 
	DELETE FROM [dbo].[BCC_4] 
	DELETE FROM [dbo].[BCC_5] 
	DELETE FROM [dbo].[BCC_6] 

	DELETE FROM [dbo].[DVC_0] 
	DELETE FROM [dbo].[DVC_1] 
	DELETE FROM [dbo].[DVC_2] 
	DELETE FROM [dbo].[DVC_3] 
	DELETE FROM [dbo].[DVC_4] 
	DELETE FROM [dbo].[DVC_5] 
	DELETE FROM [dbo].[DVC_6] 

	DELETE FROM [dbo].[TaiKhoan]
	DELETE FROM [dbo].[CD]
	DELETE FROM [dbo].[ChuyenNganh]
	DELETE FROM [dbo].[Nganh]
	DELETE FROM [dbo].[CD_TuongDuong]
	DELETE FROM [dbo].[DV_TuongDuong]
END
GO
