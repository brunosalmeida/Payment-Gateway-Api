USE [master]
GO

IF EXISTS (SELECT name FROM dbo.sysdatabases WHERE name = N'PaymentGateway')
    BEGIN
        PRINT 'Database PaymentGateway already Exists'
    END
ELSE
    BEGIN
        CREATE DATABASE [PaymentGateway]
        PRINT 'PaymentGateway is Created'
END

GO
USE [PaymentGateway]
GO

IF OBJECT_ID('dbo.Payments', 'U') IS NOT NULL
    BEGIN
        PRINT 'Payments table already Exists'
    END
ELSE
    BEGIN

        CREATE TABLE [dbo].[Payments](
                                         [Id] [uniqueidentifier] NOT NULL,
                                         [Amount] [money] NOT NULL,
                                         [Number] [nvarchar](19) NOT NULL,
                                         [Name] [nvarchar](50) NOT NULL,
                                         [Month] [int] NOT NULL,
                                         [Year] [int] NOT NULL,
                                         [CVV] [varchar](3) NOT NULL,
                                         [Status] [int] NOT NULL,
                                         [CreatedDate] [datetime] NOT NULL
        ) ON [PRIMARY]
    END





