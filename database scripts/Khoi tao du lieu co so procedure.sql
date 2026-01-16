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
CREATE PROCEDURE KhoiTaoDuLieuCoSo 
	
AS
BEGIN
	INSERT INTO [dbo].[CD_TuongDuong]
           ([ID]
           ,[TenChucDanhTD])
     VALUES
           (0,N'Sư đoàn trưởng'),
		   (1,N'Phó Sư đoàn trưởng'),
		   (2,N'Trung đoàn trưởng'),
		   (3,N'Phó Trung đoàn trưởng'),
		   (4,N'Tiểu đoàn trưởng'),
		   (5,N'Phó Tiểu đoàn trưởng'),
		   (6,N'Đại đội trưởng'),
		   (7,N'Phó Đại đội trưởng'),
		   (8,N'Trung đội trưởng'),
		   (9,N'Phó Trung đội trưởng'),
		   (10,N'Tiểu đội trưởng')
		INSERT INTO [dbo].[DV_TuongDuong]
           ([ID]
           ,[TenKieuDV])
		VALUES
           (0 ,N'Sư đoàn'),
		   (1 ,N'Trung đoàn'),
		   (2 ,N'Tiểu đoàn'),
		   (3 ,N'Đại đội'),
		   (4 ,N'Trung đội'),
		   (5 ,N'Tiểu đội'),
		   (6 ,N'Tổ')
		   INSERT INTO [dbo].[Nganh] ([ID] ,[TenNganh])
			VALUES
           (0 ,N'Tham mưu'),
		   (1 ,N'Chính trị'),
		   (2 ,N'Hậu cần-Kỹ thuật')
		   INSERT INTO [dbo].[ChuyenNganh]([ID],[NganhID],[TenChuyenNganh],[NhomNganh])
     VALUES
           (0 ,2, N'Quân nhu','HC'),
		   (1 ,2, N'Quân y','HC'),
		   (2 ,2, N'Doanh trại','HC'),
		   (3 ,2, N'Xăng dầu','HC'),
		   (4 ,2, N'Tên lửa','KT'),
		   (5 ,2, N'Ra đa','KT'),
		   (6 ,2, N'PPK-TLTT','KT'),
		   (7 ,2, N'Xe-máy','KT'),
		   (8 ,2, N'TC-ĐL-CL','KT'),
		   (9 ,2, N'Vật tư','KT'),
		   (10 ,2, N'Lái xe','KT')
END
GO
