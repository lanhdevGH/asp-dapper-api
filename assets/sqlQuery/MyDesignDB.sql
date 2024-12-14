--use [master]
--go

--Drop database [RestApiDapper2]
--go

CREATE DATABASE [RestApiDapper2]
GO

USE [RestApiDapper2]
GO

CREATE TABLE [Categories]
(
    Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Name NVARCHAR(300) NOT NULL,
    SeoAlias VARCHAR(255),
    SeoTitle NVARCHAR(255),
    SeoKeyword NVARCHAR(255),
    SeoDescription NVARCHAR(255),
    ParentId INT NULL,
    IsActive BIT DEFAULT 1,
    CreateDate DATETIME2 DEFAULT GETDATE(),
    UpdateDate DATETIME2 DEFAULT GETDATE()
);
GO

CREATE TABLE [Products]
(
    Id Int Identity(1,1) PRIMARY KEY,
    Name NVARCHAR(300) NOT NULL,
    Description NVARCHAR(600),
    Content nvarchar(600),
    Sku VARCHAR(300) NOT NULL UNIQUE,
    Price DECIMAL(18,2) NOT NULL,
	DiscountPrice DECIMAL(18, 2),
    ImageUrl NVARCHAR(300) NOT NULL,
    ImageList NVARCHAR(MAX),
    ViewCount INT,
    CreateDate DATETIME2 DEFAULT GETDATE(),
    UpdateDate DATETIME2 DEFAULT GETDATE(),
    RateTotal INT,
    RateCount INT,
    IsActive BIT DEFAULT 1,
    CategoryId INT,

	constraint fk_Products_Categories foreign key (CategoryId) references Categories(Id)
);
GO


--CREATE TABLE [ProductInCategories]
--(
--	ProductId int not null,
--	CategoryId Int NOT NULL,
--	constraint PK_Product_Category PRIMARY KEY (ProductId, CategoryID),

--	constraint fk_ProductInCategories_Categories foreign key (CategoryId) references Categories(Id),
--	constraint fk_ProductInCategories_Products foreign key (ProductId) references Products(Id),
--)
--GO

create table [ExtendAttributes]
(
	Id Int Identity(1,1) primary key not null,
	Code varchar(255) unique not null,
	Name nvarchar(500) not null,
	DataType nvarchar(200) not null,
	IsActive bit default 1,
    CreateDate DATETIME2 DEFAULT GETDATE(),
    UpdateDate DATETIME2 DEFAULT GETDATE(),
)
go

create table [AttributeValueNVarchars]
(
	Id Int Identity(1,1) primary key not null,
	AttributeId int not null,
	ProductId int not null,
	Value nvarchar(600) not null,
    CreateDate DATETIME2 DEFAULT GETDATE(),
    UpdateDate DATETIME2 DEFAULT GETDATE(),

	constraint fk_AttributeValueNVarchars_Attributes foreign key (AttributeId) references ExtendAttributes(Id),
	constraint fk_AttributeValueNVarchars_Products foreign key (ProductId) references Products(Id),
)
go

create table [AttributeValueTexts]
(
	Id Int Identity(1,1) primary key not null,
	AttributeId int not null,
	ProductId int not null,
	Value nvarchar(Max) not null,
    CreateDate DATETIME2 DEFAULT GETDATE(),
    UpdateDate DATETIME2 DEFAULT GETDATE(),
	
	constraint fk_AttributeValueTexts_Attributes foreign key (AttributeId) references ExtendAttributes(Id),
	constraint fk_AttributeValueTexts_Products foreign key (ProductId) references Products(Id),
)
go

create table [AttributeValueInts]
(
	Id Int Identity(1,1) primary key not null,
	AttributeId int not null,
	ProductId int not null,
	Value int not null,
    CreateDate DATETIME2 DEFAULT GETDATE(),
    UpdateDate DATETIME2 DEFAULT GETDATE(),
	
	constraint fk_AttributeValueInts_Attributes foreign key (AttributeId) references ExtendAttributes(Id),
	constraint fk_AttributeValueInts_Products foreign key (ProductId) references Products(Id),
)
go

create table [AttributeValueDecimals]
(
	Id Int Identity(1,1) primary key not null,
	AttributeId int not null,
	ProductId int not null,
	Value Decimal(18, 2) not null,
    CreateDate DATETIME2 DEFAULT GETDATE(),
    UpdateDate DATETIME2 DEFAULT GETDATE(),

	constraint fk_AttributeValueDecimals_Attributes foreign key (AttributeId) references ExtendAttributes(Id),
	constraint fk_AttributeValueDecimals_Products foreign key (ProductId) references Products(Id),
)
go


create table [AttributeValueDateTimes]
(
	Id Int Identity(1,1) primary key not null,
	AttributeId int not null,
	ProductId int not null,
	Value Decimal(18, 2) not null,
    CreateDate DATETIME2 DEFAULT GETDATE(),
    UpdateDate DATETIME2 DEFAULT GETDATE(),
	
	constraint fk_AttributeValueDateTimes_Attributes foreign key (AttributeId) references ExtendAttributes(Id),
	constraint fk_AttributeValueDateTimes_Products foreign key (ProductId) references Products(Id),
)
go

CREATE TABLE [Languages]
(
	LanguageId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Name nvarchar(255) not null,
    CreateDate DATETIME2 DEFAULT GETDATE(),
    UpdateDate DATETIME2 DEFAULT GETDATE(),
)
go

Create table [Orders]
(
	Id Int Identity(1, 1) PRIMARY KEY,
	CustomerId Int not null,
	Status NVARCHAR(50) NOT NULL,
	TotalAmount DECIMAL(18, 2) NOT NULL,
	OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
	CreateDate DATETIME2 DEFAULT GETDATE(),
    UpdateDate DATETIME2 DEFAULT GETDATE(),
	Note nvarchar(500) NOT NULL,
)
go

create table [OrderDetails]
(
	OrderDetailId Int Identity(1,1) PRIMARY KEY not null,
	OrderId int NOT NULL,
	ProductId int NOT NULL,
	Quantity INT NOT NULL,
	UnitPrice DECIMAL(18, 2) NOT NULL,
	Discount DECIMAL(18, 2) DEFAULT 0,
	Total DECIMAL(18, 2) NOT NULL,
    CreateDate DATETIME2 DEFAULT GETDATE(),
    UpdateDate DATETIME2 DEFAULT GETDATE(),

	constraint fk_OrderDetails_Products foreign key (ProductId) references Products(Id),
	constraint fk_OrderDetails_Orders foreign key (OrderId) references Orders(Id),
)
go

------------------------------------------------------------------------------------------------------------------------

alter table [ExtendAttributes]
alter column [DataType] nvarchar(50)
go

RENAME TABLE Categories TO Categorys
go

EXEC sp_rename 'Categories', 'Categorys'
go