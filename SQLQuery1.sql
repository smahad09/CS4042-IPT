CREATE TABLE [dbo].[orders]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [userId] INT NOT NULL, 
    [productId] INT NOT NULL, 
    [city] VARCHAR(50) NULL, 
    [phone] VARCHAR(50) NULL, 
    [state] VARCHAR(50) NULL, 
    [address] VARCHAR(250) NULL, [orderTotal] FLOAT NULL, 
    CONSTRAINT [FK_Table_ToTable] FOREIGN KEY ([userId]) REFERENCES [users]([userId]), 
    CONSTRAINT [FK_Table_ToTable_1] FOREIGN KEY ([productId]) REFERENCES [products]([productId]),
)
