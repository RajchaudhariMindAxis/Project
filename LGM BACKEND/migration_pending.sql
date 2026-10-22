BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE TABLE [Tbl_Companies] (
        [CompanyId] int NOT NULL IDENTITY,
        [CompanyCode] nvarchar(20) NOT NULL,
        [CompanyName] nvarchar(200) NOT NULL,
        [CompanyShortName] nvarchar(100) NULL,
        [LogoPath] nvarchar(max) NULL,
        [PAN] nvarchar(max) NULL,
        [TAN] nvarchar(max) NULL,
        [GSTIN] nvarchar(max) NULL,
        [CIN] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        [CreatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_Tbl_Companies] PRIMARY KEY ([CompanyId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE TABLE [Tbl_Currencies] (
        [Id] int NOT NULL IDENTITY,
        [CurrencyName] nvarchar(max) NOT NULL,
        [CurrencySymbol] nvarchar(max) NOT NULL,
        [CurrencyValue] decimal(18,2) NOT NULL DEFAULT 1.0,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_Tbl_Currencies] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE TABLE [Tbl_Categories] (
        [Id] int NOT NULL IDENTITY,
        [CategoryName] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_Tbl_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE TABLE [Tbl_ProductVariants] (
        [Id] int NOT NULL IDENTITY,
        [VariantName] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_Tbl_ProductVariants] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE TABLE [Tbl_Products] (
        [ProductId] int NOT NULL IDENTITY,
        [ProductCode] nvarchar(max) NOT NULL,
        [ProductName] nvarchar(max) NOT NULL,
        [ProductImage] nvarchar(max) NULL,
        [HSNCode] nvarchar(max) NULL,
        [CategoryId] int NULL,
        [VariantId] int NULL,
        [CurrencyId] int NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_Tbl_Products] PRIMARY KEY ([ProductId]),
        CONSTRAINT [FK_Tbl_Products_Tbl_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Tbl_Categories] ([Id]),
        CONSTRAINT [FK_Tbl_Products_Tbl_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Tbl_Currencies] ([Id]),
        CONSTRAINT [FK_Tbl_Products_Tbl_ProductVariants_VariantId] FOREIGN KEY ([VariantId]) REFERENCES [Tbl_ProductVariants] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE INDEX [IX_Tbl_Products_CategoryId] ON [Tbl_Products] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE INDEX [IX_Tbl_Products_CurrencyId] ON [Tbl_Products] ([CurrencyId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE INDEX [IX_Tbl_Products_VariantId] ON [Tbl_Products] ([VariantId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE TABLE [Tbl_Accessories] (
        [AccessoriesId] int NOT NULL IDENTITY,
        [AccessoriesCode] nvarchar(max) NOT NULL,
        [AccessoriesName] nvarchar(max) NOT NULL,
        [AccessoriesImage] nvarchar(max) NULL,
        [CategoryId] int NULL,
        [VariantId] int NULL,
        [CurrencyId] int NULL,
        [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_Tbl_Accessories] PRIMARY KEY ([AccessoriesId]),
        CONSTRAINT [FK_Tbl_Accessories_Tbl_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Tbl_Categories] ([Id]),
        CONSTRAINT [FK_Tbl_Accessories_Tbl_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Tbl_Currencies] ([Id]),
        CONSTRAINT [FK_Tbl_Accessories_Tbl_ProductVariants_VariantId] FOREIGN KEY ([VariantId]) REFERENCES [Tbl_ProductVariants] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE INDEX [IX_Tbl_Accessories_CategoryId] ON [Tbl_Accessories] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE INDEX [IX_Tbl_Accessories_CurrencyId] ON [Tbl_Accessories] ([CurrencyId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE INDEX [IX_Tbl_Accessories_VariantId] ON [Tbl_Accessories] ([VariantId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE TABLE [Branch] (
        [BranchId] int NOT NULL IDENTITY,
        [CompanyId] int NOT NULL,
        [BranchCode] nvarchar(50) NOT NULL,
        [BranchName] nvarchar(150) NOT NULL,
        [Address] nvarchar(300) NULL,
        [City] nvarchar(100) NULL,
        [State] nvarchar(100) NULL,
        [Phone] nvarchar(20) NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Branch] PRIMARY KEY ([BranchId]),
        CONSTRAINT [FK_Branch_Tbl_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Tbl_Companies] ([CompanyId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE INDEX [IX_Branch_CompanyId] ON [Branch] ([CompanyId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE TABLE [Warehouse] (
        [WarehouseId] int NOT NULL IDENTITY,
        [BranchId] int NOT NULL,
        [WarehouseCode] nvarchar(50) NOT NULL,
        [WarehouseName] nvarchar(150) NOT NULL,
        [WarehouseType] nvarchar(50) NULL,
        [Address] nvarchar(300) NULL,
        [City] nvarchar(100) NULL,
        [State] nvarchar(100) NULL,
        [Phone] nvarchar(20) NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Warehouse] PRIMARY KEY ([WarehouseId]),
        CONSTRAINT [FK_Warehouse_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch] ([BranchId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    CREATE INDEX [IX_Warehouse_BranchId] ON [Warehouse] ([BranchId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008085500_Warehouse'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261008085500_Warehouse', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008105330_InitialCreate'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch]') AND [c].[name] = N'City');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Branch] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Branch] DROP COLUMN [City];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008105330_InitialCreate'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch]') AND [c].[name] = N'Phone');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Branch] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Branch] DROP COLUMN [Phone];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008105330_InitialCreate'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch]') AND [c].[name] = N'State');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Branch] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Branch] DROP COLUMN [State];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008105330_InitialCreate'
)
BEGIN
    ALTER TABLE [Branch] ADD [Contact] nvarchar(50) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008105330_InitialCreate'
)
BEGIN
    CREATE TABLE [Brand] (
        [BrandId] int NOT NULL IDENTITY,
        [Code] nvarchar(50) NOT NULL,
        [Name] nvarchar(150) NOT NULL,
        [Description] nvarchar(500) NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_Brand] PRIMARY KEY ([BrandId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008105330_InitialCreate'
)
BEGIN
    CREATE TABLE [Unit] (
        [UnitId] int NOT NULL IDENTITY,
        [Code] nvarchar(50) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [DecimalAllowed] bit NOT NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_Unit] PRIMARY KEY ([UnitId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008105330_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261008105330_InitialCreate', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008120337_AddTax'
)
BEGIN
    ALTER TABLE [Warehouse] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008120337_AddTax'
)
BEGIN
    EXEC sp_rename N'[Unit].[Name]', N'UnitName', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008120337_AddTax'
)
BEGIN
    EXEC sp_rename N'[Unit].[Code]', N'UnitCode', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008120337_AddTax'
)
BEGIN
    EXEC sp_rename N'[Unit].[Active]', N'IsActive', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008120337_AddTax'
)
BEGIN
    EXEC sp_rename N'[Warehouse].[WarehouseType]', N'Type', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008120337_AddTax'
)
BEGIN
    EXEC sp_rename N'[Brand].[Code]', N'BrandCode', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008120337_AddTax'
)
BEGIN
    EXEC sp_rename N'[Brand].[Name]', N'BrandName', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008120337_AddTax'
)
BEGIN
    EXEC sp_rename N'[Brand].[Active]', N'IsActive', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008120337_AddTax'
)
BEGIN
    CREATE TABLE [Tax] (
        [TaxId] int NOT NULL IDENTITY,
        [TaxCode] nvarchar(50) NOT NULL,
        [TaxName] nvarchar(150) NOT NULL,
        [Rate] decimal(18,2) NOT NULL,
        [TaxType] nvarchar(50) NOT NULL,
        [EffectiveFrom] datetime2 NOT NULL,
        [EffectiveTo] datetime2 NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Tax] PRIMARY KEY ([TaxId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261008120337_AddTax'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261008120337_AddTax', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] DROP CONSTRAINT [FK_Tbl_Accessories_Tbl_Categories_CategoryId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    ALTER TABLE [Tbl_Products] DROP CONSTRAINT [FK_Tbl_Products_Tbl_Categories_CategoryId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    ALTER TABLE [Tbl_Categories] DROP CONSTRAINT [PK_Tbl_Categories];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    EXEC sp_rename N'[Tbl_Categories]', N'Category';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    EXEC sp_rename N'[Category].[Id]', N'CategoryId', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    ALTER TABLE [Category] ADD [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Category]') AND [c].[name] = N'CategoryName');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Category] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Category] ALTER COLUMN [CategoryName] nvarchar(150) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    ALTER TABLE [Category] ADD [Code] nvarchar(50) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    ALTER TABLE [Category] ADD [Description] nvarchar(500) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    ALTER TABLE [Category] ADD [ParentCategoryId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    ALTER TABLE [Category] ADD CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    CREATE INDEX [IX_Category_ParentCategoryId] ON [Category] ([ParentCategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    ALTER TABLE [Category] ADD CONSTRAINT [FK_Category_Category_ParentCategoryId] FOREIGN KEY ([ParentCategoryId]) REFERENCES [Category] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] ADD CONSTRAINT [FK_Tbl_Accessories_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD CONSTRAINT [FK_Tbl_Products_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    CREATE TABLE [PriceLists] (
        [PriceListId] int NOT NULL IDENTITY,
        [Code] nvarchar(50) NOT NULL,
        [Name] nvarchar(150) NOT NULL,
        [CustomerChannelType] nvarchar(50) NOT NULL,
        [CurrencyId] int NOT NULL,
        [EffectiveFrom] datetime2 NOT NULL,
        [EffectiveTo] datetime2 NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_PriceLists] PRIMARY KEY ([PriceListId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009073319_AddPriceListTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261009073319_AddPriceListTable', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009084756_CreateBranchAndWarehouseTables'
)
BEGIN
    CREATE TABLE [Location] (
        [LocationId] int NOT NULL IDENTITY,
        [WarehouseId] int NOT NULL,
        [LocationCode] nvarchar(50) NOT NULL,
        [Zone] nvarchar(100) NULL,
        [Rack] nvarchar(100) NULL,
        [Shelf] nvarchar(100) NULL,
        [Bin] nvarchar(100) NULL,
        [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit),
        CONSTRAINT [PK_Location] PRIMARY KEY ([LocationId]),
        CONSTRAINT [FK_Location_Warehouse_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouse] ([WarehouseId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009084756_CreateBranchAndWarehouseTables'
)
BEGIN
    CREATE INDEX [IX_Location_WarehouseId] ON [Location] ([WarehouseId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009084756_CreateBranchAndWarehouseTables'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261009084756_CreateBranchAndWarehouseTables', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009112916_PriceList'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261009112916_PriceList', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009132132_CheckBranchRelationship'
)
BEGIN
    CREATE TABLE [BundleItems] (
        [BundleItemId] int NOT NULL IDENTITY,
        [BundleId] int NOT NULL,
        [ItemId] int NOT NULL,
        [ItemType] nvarchar(30) NOT NULL,
        [Quantity] int NOT NULL,
        [PricingType] nvarchar(30) NOT NULL,
        [AllocatedPrice] decimal(18,2) NOT NULL,
        [IsOptional] bit NOT NULL,
        CONSTRAINT [PK_BundleItems] PRIMARY KEY ([BundleItemId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009132132_CheckBranchRelationship'
)
BEGIN
    CREATE TABLE [Bundles] (
        [BundleId] int NOT NULL IDENTITY,
        [Code] nvarchar(50) NOT NULL,
        [Name] nvarchar(150) NOT NULL,
        [ParentProductId] int NOT NULL,
        [PackagePrice] decimal(18,2) NOT NULL,
        [TaxMode] nvarchar(30) NOT NULL,
        [EffectiveFrom] datetime2 NOT NULL,
        [EffectiveTo] datetime2 NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Bundles] PRIMARY KEY ([BundleId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009132132_CheckBranchRelationship'
)
BEGIN
    CREATE TABLE [DocumentSeries] (
        [DocumentSeriesId] int NOT NULL IDENTITY,
        [DocumentType] nvarchar(50) NOT NULL,
        [Prefix] nvarchar(20) NOT NULL,
        [NextNumber] int NOT NULL,
        [FinancialYear] nvarchar(20) NOT NULL,
        [BranchId] int NOT NULL,
        CONSTRAINT [PK_DocumentSeries] PRIMARY KEY ([DocumentSeriesId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009132132_CheckBranchRelationship'
)
BEGIN
    CREATE TABLE [PaymentMethods] (
        [PaymentMethodId] int NOT NULL IDENTITY,
        [Code] nvarchar(50) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_PaymentMethods] PRIMARY KEY ([PaymentMethodId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009132132_CheckBranchRelationship'
)
BEGIN
    CREATE TABLE [ProductAccessoryPrices] (
        [Id] int NOT NULL IDENTITY,
        [PriceListId] int NOT NULL,
        [ItemId] int NOT NULL,
        [ItemType] nvarchar(30) NOT NULL,
        [PriceType] nvarchar(30) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [MinQty] int NOT NULL,
        [EffectiveFrom] datetime2 NOT NULL,
        [EffectiveTo] datetime2 NULL,
        CONSTRAINT [PK_ProductAccessoryPrices] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009132132_CheckBranchRelationship'
)
BEGIN
    CREATE TABLE [Reasons] (
        [ReasonId] int NOT NULL IDENTITY,
        [Code] nvarchar(50) NOT NULL,
        [Name] nvarchar(150) NOT NULL,
        [ReasonType] nvarchar(50) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Reasons] PRIMARY KEY ([ReasonId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009132132_CheckBranchRelationship'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261009132132_CheckBranchRelationship', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010115911_AddDocumentSeriesBranchForeignKey'
)
BEGIN
    CREATE INDEX [IX_DocumentSeries_BranchId] ON [DocumentSeries] ([BranchId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010115911_AddDocumentSeriesBranchForeignKey'
)
BEGIN
    ALTER TABLE [DocumentSeries] ADD CONSTRAINT [FK_DocumentSeries_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch] ([BranchId]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010115911_AddDocumentSeriesBranchForeignKey'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261010115911_AddDocumentSeriesBranchForeignKey', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010123007_AddIsDeletedToPriceLists'
)
BEGIN
    ALTER TABLE [PriceLists] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010123007_AddIsDeletedToPriceLists'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261010123007_AddIsDeletedToPriceLists', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010124135_AddIsDeletedToBundles'
)
BEGIN
    ALTER TABLE [Bundles] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010124135_AddIsDeletedToBundles'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261010124135_AddIsDeletedToBundles', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010124916_AddIsDeletedToBundleItems'
)
BEGIN
    ALTER TABLE [BundleItems] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010124916_AddIsDeletedToBundleItems'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261010124916_AddIsDeletedToBundleItems', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010125601_AddIsDeletedToPaymentMethodsReasonsAndDocumentSeries'
)
BEGIN
    ALTER TABLE [Reasons] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010125601_AddIsDeletedToPaymentMethodsReasonsAndDocumentSeries'
)
BEGIN
    ALTER TABLE [PaymentMethods] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010125601_AddIsDeletedToPaymentMethodsReasonsAndDocumentSeries'
)
BEGIN
    ALTER TABLE [DocumentSeries] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261010125601_AddIsDeletedToPaymentMethodsReasonsAndDocumentSeries'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261010125601_AddIsDeletedToPaymentMethodsReasonsAndDocumentSeries', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012054427_AddAccessoriesForeignKeyToBundleItems'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261012054427_AddAccessoriesForeignKeyToBundleItems', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012060545_RemoveItemIdAndItemTypeFromBundleItems'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BundleItems]') AND [c].[name] = N'ItemId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [BundleItems] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [BundleItems] DROP COLUMN [ItemId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012060545_RemoveItemIdAndItemTypeFromBundleItems'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BundleItems]') AND [c].[name] = N'ItemType');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [BundleItems] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [BundleItems] DROP COLUMN [ItemType];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012060545_RemoveItemIdAndItemTypeFromBundleItems'
)
BEGIN
    ALTER TABLE [BundleItems] ADD [AccessoriesId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012060545_RemoveItemIdAndItemTypeFromBundleItems'
)
BEGIN
    CREATE INDEX [IX_BundleItems_AccessoriesId] ON [BundleItems] ([AccessoriesId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012060545_RemoveItemIdAndItemTypeFromBundleItems'
)
BEGIN
    ALTER TABLE [BundleItems] ADD CONSTRAINT [FK_BundleItems_Tbl_Accessories_AccessoriesId] FOREIGN KEY ([AccessoriesId]) REFERENCES [Tbl_Accessories] ([AccessoriesId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012060545_RemoveItemIdAndItemTypeFromBundleItems'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261012060545_RemoveItemIdAndItemTypeFromBundleItems', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012091506_AddCurrencyForeignKeyToPriceList'
)
BEGIN
    EXEC sp_rename N'[PriceLists].[CurrencyId]', N'Id', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012091506_AddCurrencyForeignKeyToPriceList'
)
BEGIN
    CREATE INDEX [IX_PriceLists_Id] ON [PriceLists] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012091506_AddCurrencyForeignKeyToPriceList'
)
BEGIN
    ALTER TABLE [PriceLists] ADD CONSTRAINT [FK_PriceLists_Tbl_Currencies_Id] FOREIGN KEY ([Id]) REFERENCES [Tbl_Currencies] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012091506_AddCurrencyForeignKeyToPriceList'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261012091506_AddCurrencyForeignKeyToPriceList', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tbl_Products]') AND [c].[name] = N'ProductName');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Tbl_Products] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Tbl_Products] ALTER COLUMN [ProductName] nvarchar(150) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tbl_Products]') AND [c].[name] = N'ProductCode');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Tbl_Products] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Tbl_Products] ALTER COLUMN [ProductCode] nvarchar(50) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tbl_Products]') AND [c].[name] = N'HSNCode');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Tbl_Products] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Tbl_Products] ALTER COLUMN [HSNCode] nvarchar(20) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD [BrandId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD [MRP] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD [MinStock] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD [ReorderLevel] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD [TaxId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD [TrackBatch] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD [TrackSerial] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD [TrackStock] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD [UnitId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    CREATE INDEX [IX_Tbl_Products_BrandId] ON [Tbl_Products] ([BrandId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    CREATE INDEX [IX_Tbl_Products_TaxId] ON [Tbl_Products] ([TaxId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    CREATE INDEX [IX_Tbl_Products_UnitId] ON [Tbl_Products] ([UnitId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD CONSTRAINT [FK_Tbl_Products_Brand_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Brand] ([BrandId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD CONSTRAINT [FK_Tbl_Products_Tax_TaxId] FOREIGN KEY ([TaxId]) REFERENCES [Tax] ([TaxId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD CONSTRAINT [FK_Tbl_Products_Unit_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Unit] ([UnitId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261012122632_product'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261012122632_product', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013083135_AddProductImage'
)
BEGIN
    CREATE TABLE [AccessoryPrices] (
        [AccessoryPriceId] int NOT NULL IDENTITY,
        [PriceListId] int NOT NULL,
        [AccessoryId] int NOT NULL,
        [AccessoriesId] int NOT NULL,
        [PriceType] nvarchar(50) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [MinQty] int NOT NULL,
        [EffectiveFrom] datetime2 NOT NULL,
        [EffectiveTo] datetime2 NULL,
        [IsActive] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_AccessoryPrices] PRIMARY KEY ([AccessoryPriceId]),
        CONSTRAINT [FK_AccessoryPrices_PriceLists_PriceListId] FOREIGN KEY ([PriceListId]) REFERENCES [PriceLists] ([PriceListId]) ON DELETE CASCADE,
        CONSTRAINT [FK_AccessoryPrices_Tbl_Accessories_AccessoriesId] FOREIGN KEY ([AccessoriesId]) REFERENCES [Tbl_Accessories] ([AccessoriesId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013083135_AddProductImage'
)
BEGIN
    CREATE TABLE [ProductPrices] (
        [ProductPriceId] int NOT NULL IDENTITY,
        [PriceListId] int NOT NULL,
        [ProductId] int NOT NULL,
        [PriceType] nvarchar(50) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [MinQty] int NOT NULL,
        [EffectiveFrom] datetime2 NOT NULL,
        [EffectiveTo] datetime2 NULL,
        [IsActive] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_ProductPrices] PRIMARY KEY ([ProductPriceId]),
        CONSTRAINT [FK_ProductPrices_PriceLists_PriceListId] FOREIGN KEY ([PriceListId]) REFERENCES [PriceLists] ([PriceListId]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductPrices_Tbl_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Tbl_Products] ([ProductId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013083135_AddProductImage'
)
BEGIN
    CREATE INDEX [IX_AccessoryPrices_AccessoriesId] ON [AccessoryPrices] ([AccessoriesId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013083135_AddProductImage'
)
BEGIN
    CREATE INDEX [IX_AccessoryPrices_PriceListId] ON [AccessoryPrices] ([PriceListId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013083135_AddProductImage'
)
BEGIN
    CREATE INDEX [IX_ProductPrices_PriceListId] ON [ProductPrices] ([PriceListId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013083135_AddProductImage'
)
BEGIN
    CREATE INDEX [IX_ProductPrices_ProductId] ON [ProductPrices] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013083135_AddProductImage'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261013083135_AddProductImage', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261013123528_AddAccessoriesImage'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261013123528_AddAccessoriesImage', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] DROP CONSTRAINT [FK_Tbl_Accessories_Tbl_ProductVariants_VariantId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Products] DROP CONSTRAINT [FK_Tbl_Products_Tbl_ProductVariants_VariantId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_ProductVariants] DROP CONSTRAINT [PK_Tbl_ProductVariants];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Warehouse]') AND [c].[name] = N'Address');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Warehouse] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Warehouse] DROP COLUMN [Address];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Warehouse]') AND [c].[name] = N'City');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Warehouse] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Warehouse] DROP COLUMN [City];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Warehouse]') AND [c].[name] = N'CreatedAt');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Warehouse] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Warehouse] DROP COLUMN [CreatedAt];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Warehouse]') AND [c].[name] = N'Phone');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Warehouse] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Warehouse] DROP COLUMN [Phone];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Warehouse]') AND [c].[name] = N'State');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Warehouse] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Warehouse] DROP COLUMN [State];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[Tbl_ProductVariants]', N'ProductVariant';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Unit] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
                    IF EXISTS (SELECT 1 FROM [tbl_Customer] WHERE LEN([customer_name]) > 150)
                    BEGIN
                        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
                        RAISERROR('Precondition failed: tbl_Customer contains customer_name values exceeding 150 characters. Please review data before migrating.', 16, 127) WITH NOWAIT;
                        SET NOEXEC ON;
                    END
                    IF EXISTS (SELECT 1 FROM [tbl_Customer] WHERE LEN([gst_no]) > 15)
                    BEGIN
                        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
                        RAISERROR('Precondition failed: tbl_Customer contains gst_no values exceeding 15 characters. Please review data before migrating.', 16, 127) WITH NOWAIT;
                        SET NOEXEC ON;
                    END
                    IF EXISTS (SELECT 1 FROM [tbl_Customer] WHERE LEN([cin_no]) > 25)
                    BEGIN
                        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
                        RAISERROR('Precondition failed: tbl_Customer contains cin_no values exceeding 25 characters. Please review data before migrating.', 16, 127) WITH NOWAIT;
                        SET NOEXEC ON;
                    END
                    IF EXISTS (SELECT 1 FROM [tbl_Customer] WHERE LEN([email_address]) > 150)
                    BEGIN
                        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
                        RAISERROR('Precondition failed: tbl_Customer contains email_address values exceeding 150 characters. Please review data before migrating.', 16, 127) WITH NOWAIT;
                        SET NOEXEC ON;
                    END
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[tbl_Customer].[msme_number]', N'MSMENo', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[tbl_Customer].[gst_no]', N'GSTIN', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[tbl_Customer].[doc_attachment]', N'DocsAttachment', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[tbl_Customer].[company_tan]', N'CustomerTAN', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[tbl_Customer].[customer_short_name]', N'CustomerShortName', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[tbl_Customer].[company_pan]', N'CustomerPAN', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[tbl_Customer].[customer_name]', N'CustomerName', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[tbl_Customer].[customer_code]', N'CustomerCode', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[tbl_Customer].[cin_no]', N'CIN', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[tbl_Customer].[email_address]', N'CustomerEmail', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    EXEC sp_rename N'[tbl_Customer].[mobile_no]', N'CustomerMobile', N'COLUMN';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Customer]') AND [c].[name] = N'GSTIN');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Customer] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [tbl_Customer] ALTER COLUMN [GSTIN] nvarchar(15) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Customer]') AND [c].[name] = N'DocsAttachment');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Customer] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [tbl_Customer] ALTER COLUMN [DocsAttachment] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Customer]') AND [c].[name] = N'CustomerName');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Customer] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [tbl_Customer] ALTER COLUMN [CustomerName] nvarchar(150) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Customer]') AND [c].[name] = N'CIN');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Customer] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [tbl_Customer] ALTER COLUMN [CIN] nvarchar(25) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Customer]') AND [c].[name] = N'CustomerEmail');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Customer] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [tbl_Customer] ALTER COLUMN [CustomerEmail] nvarchar(150) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[tbl_Customer]') AND [c].[name] = N'CustomerMobile');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [tbl_Customer] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [tbl_Customer] ALTER COLUMN [CustomerMobile] nvarchar(20) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [tbl_Customer] ADD [BillingAddress] nvarchar(500) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [tbl_Customer] ADD [ShippingAddress] nvarchar(500) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [tbl_Customer] ADD [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tbl_Accessories]') AND [c].[name] = N'AccessoriesName');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Tbl_Accessories] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [Tbl_Accessories] ALTER COLUMN [AccessoriesName] nvarchar(150) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tbl_Accessories]') AND [c].[name] = N'AccessoriesCode');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Tbl_Accessories] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [Tbl_Accessories] ALTER COLUMN [AccessoriesCode] nvarchar(50) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] ADD [Active] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] ADD [BrandId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] ADD [MinStock] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] ADD [TaxId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] ADD [TrackStock] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] ADD [UnitId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tax] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Location] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Brand] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Branch] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductVariant]') AND [c].[name] = N'VariantName');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [ProductVariant] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [ProductVariant] ALTER COLUMN [VariantName] nvarchar(100) NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [ProductVariant] ADD [Active] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [ProductVariant] ADD [Attributes] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [ProductVariant] ADD [ProductId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
                    IF EXISTS (SELECT 1 FROM [ProductVariant] WHERE [ProductId] IS NULL)
                    BEGIN
                        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
                        RAISERROR('Cannot enforce NOT NULL on ProductVariant.ProductId: existing variant rows exist without an unambiguous ProductId mapping. Please backfill ProductVariant.ProductId before applying this migration.', 16, 127) WITH NOWAIT;
                        SET NOEXEC ON;
                    END
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductVariant]') AND [c].[name] = N'ProductId');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [ProductVariant] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [ProductVariant] ALTER COLUMN [ProductId] int NOT NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [ProductVariant] ADD [VariantCode] nvarchar(50) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [ProductVariant] ADD CONSTRAINT [PK_ProductVariant] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE TABLE [ProductAccessoryRule] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [AccessoriesId] int NOT NULL,
        [Quantity] int NOT NULL,
        [MandatoryOptional] nvarchar(20) NOT NULL,
        [PricingType] nvarchar(30) NOT NULL,
        [DefaultPrice] decimal(18,2) NOT NULL,
        [EffectiveFrom] datetime2 NOT NULL,
        [EffectiveTo] datetime2 NULL,
        [Active] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_ProductAccessoryRule] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductAccessoryRule_Tbl_Accessories_AccessoriesId] FOREIGN KEY ([AccessoriesId]) REFERENCES [Tbl_Accessories] ([AccessoriesId]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductAccessoryRule_Tbl_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Tbl_Products] ([ProductId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE TABLE [ProductBarcode] (
        [Id] int NOT NULL IDENTITY,
        [Barcode] nvarchar(100) NOT NULL,
        [BarcodeType] nvarchar(30) NOT NULL,
        [ProductId] int NULL,
        [VariantId] int NULL,
        [AccessoryId] int NULL,
        [IsPrimary] bit NOT NULL,
        [Active] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_ProductBarcode] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductBarcode_ProductVariant_VariantId] FOREIGN KEY ([VariantId]) REFERENCES [ProductVariant] ([Id]),
        CONSTRAINT [FK_ProductBarcode_Tbl_Accessories_AccessoryId] FOREIGN KEY ([AccessoryId]) REFERENCES [Tbl_Accessories] ([AccessoriesId]),
        CONSTRAINT [FK_ProductBarcode_Tbl_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Tbl_Products] ([ProductId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE TABLE [Supplier] (
        [SupplierId] int NOT NULL IDENTITY,
        [BranchId] int NOT NULL,
        [SupplierCode] nvarchar(50) NOT NULL,
        [SupplierName] nvarchar(150) NOT NULL,
        [SupplierGSTIN] nvarchar(15) NULL,
        [SupplierContact] nvarchar(50) NULL,
        [SupplierAddress] nvarchar(300) NULL,
        [PaymentTerms] nvarchar(100) NULL,
        [IsActive] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Supplier] PRIMARY KEY ([SupplierId]),
        CONSTRAINT [FK_Supplier_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch] ([BranchId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE INDEX [IX_Tbl_Accessories_BrandId] ON [Tbl_Accessories] ([BrandId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE INDEX [IX_Tbl_Accessories_TaxId] ON [Tbl_Accessories] ([TaxId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE INDEX [IX_Tbl_Accessories_UnitId] ON [Tbl_Accessories] ([UnitId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE INDEX [IX_ProductVariant_ProductId] ON [ProductVariant] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE INDEX [IX_ProductAccessoryRule_AccessoriesId] ON [ProductAccessoryRule] ([AccessoriesId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE INDEX [IX_ProductAccessoryRule_ProductId] ON [ProductAccessoryRule] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE INDEX [IX_ProductBarcode_AccessoryId] ON [ProductBarcode] ([AccessoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE INDEX [IX_ProductBarcode_ProductId] ON [ProductBarcode] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE INDEX [IX_ProductBarcode_VariantId] ON [ProductBarcode] ([VariantId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    CREATE INDEX [IX_Supplier_BranchId] ON [Supplier] ([BranchId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [ProductVariant] ADD CONSTRAINT [FK_ProductVariant_Tbl_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Tbl_Products] ([ProductId]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] ADD CONSTRAINT [FK_Tbl_Accessories_Brand_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [Brand] ([BrandId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] ADD CONSTRAINT [FK_Tbl_Accessories_ProductVariant_VariantId] FOREIGN KEY ([VariantId]) REFERENCES [ProductVariant] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] ADD CONSTRAINT [FK_Tbl_Accessories_Tax_TaxId] FOREIGN KEY ([TaxId]) REFERENCES [Tax] ([TaxId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Accessories] ADD CONSTRAINT [FK_Tbl_Accessories_Unit_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Unit] ([UnitId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    ALTER TABLE [Tbl_Products] ADD CONSTRAINT [FK_Tbl_Products_ProductVariant_VariantId] FOREIGN KEY ([VariantId]) REFERENCES [ProductVariant] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014064036_BackendCode'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261014064036_BackendCode', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261016095953_AddProductAndVariantForeignKeysToBundleItem'
)
BEGIN
    ALTER TABLE [BundleItems] ADD [ProductId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261016095953_AddProductAndVariantForeignKeysToBundleItem'
)
BEGIN
    ALTER TABLE [BundleItems] ADD [VariantId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261016095953_AddProductAndVariantForeignKeysToBundleItem'
)
BEGIN
    CREATE INDEX [IX_BundleItems_ProductId] ON [BundleItems] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261016095953_AddProductAndVariantForeignKeysToBundleItem'
)
BEGIN
    CREATE INDEX [IX_BundleItems_VariantId] ON [BundleItems] ([VariantId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261016095953_AddProductAndVariantForeignKeysToBundleItem'
)
BEGIN
    ALTER TABLE [BundleItems] ADD CONSTRAINT [FK_BundleItems_ProductVariant_VariantId] FOREIGN KEY ([VariantId]) REFERENCES [ProductVariant] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261016095953_AddProductAndVariantForeignKeysToBundleItem'
)
BEGIN
    ALTER TABLE [BundleItems] ADD CONSTRAINT [FK_BundleItems_Tbl_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Tbl_Products] ([ProductId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261016095953_AddProductAndVariantForeignKeysToBundleItem'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261016095953_AddProductAndVariantForeignKeysToBundleItem', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261016101550_AddVariantIdToBundleItem'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261016101550_AddVariantIdToBundleItem', N'8.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261017053529_AddLegalNameAndCompanyIsActive'
)
BEGIN
    ALTER TABLE [Tbl_Companies] ADD [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261017053529_AddLegalNameAndCompanyIsActive'
)
BEGIN
    ALTER TABLE [Tbl_Companies] ADD [LegalName] nvarchar(200) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261017053529_AddLegalNameAndCompanyIsActive'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261017053529_AddLegalNameAndCompanyIsActive', N'8.0.0');
END;
GO

COMMIT;
GO

