CREATE TABLE [dbo].[Attributes] (
    [AttributeID]   NVARCHAR (50) NOT NULL,
    [AttributeName] NVARCHAR (50) NULL
);

GO
CREATE TABLE [dbo].[Categories] (
    [CategoryID]   INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (15) NOT NULL,
    [Description]  NTEXT         NULL
);

GO
CREATE TABLE [dbo].[Countries] (
    [CountryName] NVARCHAR (50) NOT NULL
);

GO
CREATE TABLE [dbo].[Customers] (
    [CustomerID]   NCHAR (5)      NOT NULL,
    [CompanyName]  NVARCHAR (100) NULL,
    [ContactName]  NVARCHAR (30)  NULL,
    [ContactTitle] NVARCHAR (30)  NULL,
    [Address]      NVARCHAR (60)  NULL,
    [City]         NVARCHAR (15)  NULL,
    [Country]      NVARCHAR (15)  NULL,
    [Phone]        NVARCHAR (24)  NULL,
    [Fax]          NVARCHAR (24)  NULL
);

GO
CREATE TABLE [dbo].[Employees] (
    [EmployeeID] INT            IDENTITY (1, 1) NOT NULL,
    [LastName]   NVARCHAR (20)  NOT NULL,
    [FirstName]  NVARCHAR (10)  NOT NULL,
    [Title]      NVARCHAR (30)  NULL,
    [BirthDate]  DATETIME       NULL,
    [HireDate]   DATETIME       NULL,
    [Email]      NVARCHAR (100) NULL,
    [Address]    NVARCHAR (60)  NULL,
    [City]       NVARCHAR (15)  NULL,
    [Country]    NVARCHAR (15)  NULL,
    [HomePhone]  NVARCHAR (24)  NULL,
    [Notes]      NTEXT          NULL,
    [PhotoPath]  NVARCHAR (255) NULL,
    [Password]   NVARCHAR (50)  NULL,
    [Roles]      NVARCHAR (255) NULL
);

GO
CREATE TABLE [dbo].[OrderDetails] (
    [OrderID]   INT      NOT NULL,
    [ProductID] INT      NOT NULL,
    [UnitPrice] MONEY    NOT NULL,
    [Quantity]  SMALLINT NOT NULL,
    [Discount]  REAL     NOT NULL
);

GO
CREATE TABLE [dbo].[Orders] (
    [OrderID]      INT           IDENTITY (1, 1) NOT NULL,
    [CustomerID]   NCHAR (5)     NULL,
    [EmployeeID]   INT           NULL,
    [OrderDate]    DATETIME      NULL,
    [RequiredDate] DATETIME      NULL,
    [ShippedDate]  DATETIME      NULL,
    [ShipperID]    INT           NULL,
    [Freight]      MONEY         NULL,
    [ShipAddress]  NVARCHAR (60) NULL,
    [ShipCity]     NVARCHAR (15) NULL,
    [ShipCountry]  NVARCHAR (15) NULL
);

GO
CREATE TABLE [dbo].[ProductAttributes] (
    [AttributeID]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [ProductID]       INT            NOT NULL,
    [AttributeName]   NVARCHAR (255) NOT NULL,
    [AttributeValues] NVARCHAR (MAX) NOT NULL,
    [DisplayOrder]    INT            NOT NULL
);

GO
CREATE TABLE [dbo].[Products] (
    [ProductID]       INT            IDENTITY (1, 1) NOT NULL,
    [ProductName]     NVARCHAR (40)  NOT NULL,
    [SupplierID]      INT            NULL,
    [CategoryID]      INT            NULL,
    [QuantityPerUnit] NVARCHAR (20)  NULL,
    [UnitPrice]       MONEY          NULL,
    [Descriptions]    NVARCHAR (MAX) NULL,
    [PhotoPath]       NVARCHAR (255) NULL
);

GO
CREATE TABLE [dbo].[Shippers] (
    [ShipperID]   INT           IDENTITY (1, 1) NOT NULL,
    [CompanyName] NVARCHAR (40) NOT NULL,
    [Phone]       NVARCHAR (24) NULL
);

GO
CREATE TABLE [dbo].[Suppliers] (
    [SupplierID]   INT            IDENTITY (1, 1) NOT NULL,
    [CompanyName]  NVARCHAR (40)  NOT NULL,
    [ContactName]  NVARCHAR (30)  NULL,
    [ContactTitle] NVARCHAR (30)  NULL,
    [Address]      NVARCHAR (60)  NULL,
    [City]         NVARCHAR (15)  NULL,
    [Country]      NVARCHAR (15)  NULL,
    [Phone]        NVARCHAR (24)  NULL,
    [Fax]          NVARCHAR (24)  NULL,
    [HomePage]     NVARCHAR (255) NULL
);

GO
ALTER TABLE [dbo].[OrderDetails]
    ADD CONSTRAINT [FK_Order_Details_Orders] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Orders] ([OrderID]);

GO
ALTER TABLE [dbo].[OrderDetails]
    ADD CONSTRAINT [FK_Order_Details_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID]);

GO
ALTER TABLE [dbo].[Orders]
    ADD CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID]);

GO
ALTER TABLE [dbo].[Orders]
    ADD CONSTRAINT [FK_Orders_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID]);

GO
ALTER TABLE [dbo].[Orders]
    ADD CONSTRAINT [FK_Orders_Shippers] FOREIGN KEY ([ShipperID]) REFERENCES [dbo].[Shippers] ([ShipperID]);

GO
ALTER TABLE [dbo].[ProductAttributes]
    ADD CONSTRAINT [FK_ProductAttributes_Products] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID]);

GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID]);

GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Suppliers] ([SupplierID]);

GO
ALTER TABLE [dbo].[OrderDetails]
    ADD CONSTRAINT [DF_Order_Details_Discount] DEFAULT ((0)) FOR [Discount];

GO
ALTER TABLE [dbo].[OrderDetails]
    ADD CONSTRAINT [DF_Order_Details_Quantity] DEFAULT ((1)) FOR [Quantity];

GO
ALTER TABLE [dbo].[OrderDetails]
    ADD CONSTRAINT [DF_Order_Details_UnitPrice] DEFAULT ((0)) FOR [UnitPrice];

GO
ALTER TABLE [dbo].[Orders]
    ADD CONSTRAINT [DF_Orders_Freight] DEFAULT ((0)) FOR [Freight];

GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [DF_Products_UnitPrice] DEFAULT ((0)) FOR [UnitPrice];

GO
ALTER TABLE [dbo].[Categories]
    ADD CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryID] ASC);

GO
ALTER TABLE [dbo].[Customers]
    ADD CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerID] ASC);

GO
ALTER TABLE [dbo].[Employees]
    ADD CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeID] ASC);

GO
ALTER TABLE [dbo].[OrderDetails]
    ADD CONSTRAINT [PK_Order_Details] PRIMARY KEY CLUSTERED ([OrderID] ASC, [ProductID] ASC);

GO
ALTER TABLE [dbo].[Orders]
    ADD CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderID] ASC);

GO
ALTER TABLE [dbo].[ProductAttributes]
    ADD CONSTRAINT [PK_ProductAttributes] PRIMARY KEY CLUSTERED ([AttributeID] ASC);

GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductID] ASC);

GO
ALTER TABLE [dbo].[Shippers]
    ADD CONSTRAINT [PK_Shippers] PRIMARY KEY CLUSTERED ([ShipperID] ASC);

GO
ALTER TABLE [dbo].[Suppliers]
    ADD CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED ([SupplierID] ASC);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Street or post-office box.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Suppliers', @level2type = N'COLUMN', @level2name = N'Address';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number automatically assigned to new supplier.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Suppliers', @level2type = N'COLUMN', @level2name = N'SupplierID';

GO
