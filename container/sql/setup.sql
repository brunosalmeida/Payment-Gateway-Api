USE [master]
GO

/****** Object:  Database [PaymentGateway]    Script Date: 5/28/2021 1:05:41 AM ******/
CREATE DATABASE [PaymentGateway]
GO

USE [PaymentGateway]
GO

/****** Object:  Table [dbo].[Payments]    Script Date: 5/28/2021 1:06:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Payments](
	[Id] [uniqueidentifier] NOT NULL,
	[Amount] [money] NOT NULL,
	[Number] [nvarchar](19) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[CVV] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL
) ON [PRIMARY]
GO


