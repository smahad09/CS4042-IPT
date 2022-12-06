CREATE TABLE [dbo].[orders]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [userId] INT Foreign key references users, 
    [productId] INT Foreign key references products, 
    [city] VARCHAR(50) NULL, 
    [phone] VARCHAR(50) NULL, 
    [state] VARCHAR(50) NULL, 
    [address] VARCHAR(250) NULL, [orderTotal] FLOAT NULL, 
)
