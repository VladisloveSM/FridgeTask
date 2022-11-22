CREATE DATABASE FoodShop
GO

USE [FoodShop]
GO

CREATE TABLE [Fridge_Model](
    [Id] uniqueidentifier PRIMARY KEY DEFAULT newid(),
    [Name] nvarchar(50) NOT NULL,
    [Year] smallint
) 

CREATE TABLE [Fridge](
    [Id] uniqueidentifier PRIMARY KEY DEFAULT newid(),
    [Name] nvarchar(50),
    [Owner_Name] nvarchar(50) NOT NULL,
    [Model_id] uniqueidentifier NOT NULL
)

CREATE TABLE [Products](
    [Id] uniqueidentifier PRIMARY KEY DEFAULT newid(),
    [Name] nvarchar(50) NOT NULL,
    [Default_Quantity] int
)

CREATE TABLE [Fridge_Products](
    [Id] uniqueidentifier PRIMARY KEY DEFAULT newid(),
    [Quantity] int,
    [Product_id] uniqueidentifier NOT NULL,
    [Fridge_id] uniqueidentifier NOT NULL
)

ALTER TABLE [Fridge] ADD CONSTRAINT [FK_Fridge_Model] FOREIGN KEY([Model_id])
REFERENCES [Fridge_Model]([Id])

ALTER TABLE [Fridge_Products] ADD CONSTRAINT [FK_FridgeProd_Products] FOREIGN KEY([Product_id])
REFERENCES [Products]([Id])

ALTER TABLE [Fridge_Products] ADD CONSTRAINT [FK_FridgeProd_Fridge] FOREIGN KEY([Fridge_id])
REFERENCES [Fridge]([Id])

----------- INSERT DATA ----------------------

INSERT INTO [Products] ([Name], [Default_Quantity]) VALUES('Banana', 6)
INSERT INTO [Products] ([Name], [Default_Quantity]) VALUES('Grape', 1)
INSERT INTO [Products] ([Name], [Default_Quantity]) VALUES('Apple', 3)
INSERT INTO [Products] ([Name], [Default_Quantity]) VALUES('Cherry', 8)
INSERT INTO [Products] ([Name], [Default_Quantity]) VALUES('Beaf', 1)
INSERT INTO [Products] ([Name], [Default_Quantity]) VALUES('Fish', 4)


INSERT INTO [Fridge_Model] ([Name], [Year]) VALUES('Atlanta', 1998)
INSERT INTO [Fridge_Model] ([Name], [Year]) VALUES('Horizon', 1993)
INSERT INTO [Fridge_Model] ([Name], [Year]) VALUES('LG', 2001)
INSERT INTO [Fridge_Model] ([Name], [Year]) VALUES('Philips', 2000)
INSERT INTO [Fridge_Model] ([Name], [Year]) VALUES('Panasonic', 2003)


DECLARE @idPhil uniqueidentifier
SELECT @idPhil = Id FROM [Fridge_Model] WHERE [Name] = 'Philips'
DECLARE @idLG uniqueidentifier
SELECT @idLG = Id FROM [Fridge_Model] WHERE [Name] = 'LG'
DECLARE @idAtlan uniqueidentifier
SELECT @idAtlan = Id FROM [Fridge_Model] WHERE [Name] = 'Atlanta'
DECLARE @idHoriz uniqueidentifier
SELECT @idHoriz = Id FROM [Fridge_Model] WHERE [Name] = 'Horizon'
DECLARE @idPan uniqueidentifier
SELECT @idPan = Id FROM [Fridge_Model] WHERE [Name] = 'Panasonic'

INSERT INTO [Fridge]([Name],[Owner_Name],[Model_id]) VALUES('Frozen', 'Zlatko', @idLG)
INSERT INTO [Fridge]([Name],[Owner_Name],[Model_id]) VALUES('Subzero', 'Sonya', @idAtlan)
INSERT INTO [Fridge]([Name],[Owner_Name],[Model_id]) VALUES('Colder', 'Shani', @idPhil)
INSERT INTO [Fridge]([Name],[Owner_Name],[Model_id]) VALUES('Brrrrr', 'Neo', @idPan)
INSERT INTO [Fridge]([Name],[Owner_Name],[Model_id]) VALUES('Winter', 'Yana', @idHoriz)
INSERT INTO [Fridge]([Name],[Owner_Name],[Model_id]) VALUES('Odin', 'Sergei', @idHoriz)



DECLARE @idBanana uniqueidentifier
SELECT @idBanana = Id FROM [Products] WHERE [Name] = 'Banana'
DECLARE @idFish uniqueidentifier
SELECT @idFish = Id FROM [Products] WHERE [Name] = 'Fish'
DECLARE @idCherry uniqueidentifier
SELECT @idCherry = Id FROM [Products] WHERE [Name] = 'Cherry'
DECLARE @idApple uniqueidentifier
SELECT @idApple = Id FROM [Products] WHERE [Name] = 'Apple'

DECLARE @idFrozen uniqueidentifier
SELECT @idFrozen = Id FROM [Fridge] WHERE [Name] = 'Frozen'
DECLARE @idColder uniqueidentifier
SELECT @idColder = Id FROM [Fridge] WHERE [Name] = 'Colder'
DECLARE @idWinter uniqueidentifier
SELECT @idWinter = Id FROM [Fridge] WHERE [Name] = 'Winter'
DECLARE @idSubzero uniqueidentifier
SELECT @idSubzero = Id FROM [Fridge] WHERE [Name] = 'Subzero'


INSERT INTO [Fridge_Products]([Quantity],[Product_id],[Fridge_id]) VALUES(25, @idBanana, @idFrozen)
INSERT INTO [Fridge_Products]([Quantity],[Product_id],[Fridge_id]) VALUES(2, @idFish, @idFrozen)
INSERT INTO [Fridge_Products]([Quantity],[Product_id],[Fridge_id]) VALUES(35, @idCherry, @idColder)
INSERT INTO [Fridge_Products]([Quantity],[Product_id],[Fridge_id]) VALUES(4, @idBanana, @idColder)
INSERT INTO [Fridge_Products]([Quantity],[Product_id],[Fridge_id]) VALUES(21, @idCherry, @idWinter)
INSERT INTO [Fridge_Products]([Quantity],[Product_id],[Fridge_id]) VALUES(5, @idApple, @idWinter)
INSERT INTO [Fridge_Products]([Quantity],[Product_id],[Fridge_id]) VALUES(11, @idBanana, @idSubzero)
INSERT INTO [Fridge_Products]([Quantity],[Product_id],[Fridge_id]) VALUES(17, @idCherry, @idSubzero)
INSERT INTO [Fridge_Products]([Quantity],[Product_id],[Fridge_id]) VALUES(1, @idFish, @idSubzero)

GO
CREATE PROCEDURE FindEmptyProducts
    @idFridge uniqueidentifier
AS
	SELECT * FROM Products WHERE NOT EXISTS 
	(SELECT * FROM Fridge_Products 
		WHERE Fridge_Products.Product_id = Products.Id AND Fridge_Products.Fridge_id = @idFridge)

GO