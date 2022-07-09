IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Clients] (
    [Id] int NOT NULL IDENTITY,
    [Name] VARCHAR(80) NOT NULL,
    [PhoneNumber] CHAR(11) NOT NULL,
    [Cep] VARCHAR(8) NOT NULL,
    [State] CHAR(2) NOT NULL,
    [City] nvarchar(60) NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [BarCode] VARCHAR(14) NOT NULL,
    [Description] VARCHAR(60) NOT NULL,
    [Value] DECIMAL(10,2) NOT NULL,
    [ProductType] nvarchar(max) NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [ClientId] int NOT NULL,
    [StartedIn] datetime2 NOT NULL DEFAULT (GETDATE()),
    [EndIn] datetime2 NOT NULL,
    [TypeFreigth] int NOT NULL,
    [OrderStatus] nvarchar(max) NOT NULL,
    [observation] VARCHAR(512) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderItems] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Amount] int NOT NULL DEFAULT 0,
    [Value] DECIMAL(10,2) NOT NULL DEFAULT 0.0,
    [Discount] DECIMAL(10,2) NOT NULL DEFAULT 0.0,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [idx_number_client] ON [Clients] ([PhoneNumber]);
GO

CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
GO

CREATE INDEX [IX_OrderItems_ProductId] ON [OrderItems] ([ProductId]);
GO

CREATE INDEX [IX_Orders_ClientId] ON [Orders] ([ClientId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220708142340_FirstMigration', N'6.0.6');
GO

COMMIT;
GO

