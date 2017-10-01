CREATE TABLE [dbo].[Product]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(500) NULL, 
    [Price] DECIMAL(18, 2) NOT NULL, 
    [DeliveryPrice] DECIMAL(18, 2) NOT NULL
)
