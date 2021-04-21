IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Lojas] (
    [Id] int NOT NULL IDENTITY,
    [Nome] varchar(255) NOT NULL,
    [NomeFantasia] varchar(255) NULL,
    [Cnpj] varchar(14) NOT NULL,
    [Cep] varchar(8) NOT NULL,
    [Logradouro] varchar(255) NOT NULL,
    [Numero] varchar(10) NOT NULL,
    [Complemento] varchar(30) NULL,
    [Bairro] varchar(150) NOT NULL,
    [Cidade] varchar(255) NOT NULL,
    [Uf] varchar(2) NOT NULL,
    CONSTRAINT [PK_Lojas] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Produtos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] varchar(150) NOT NULL,
    [ValorCompra] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Estoques] (
    [Id] int NOT NULL IDENTITY,
    [ProdutoId] int NOT NULL,
    [LojaId] int NOT NULL,
    [Quantidade] int NOT NULL,
    [UltimaAtualizacao] datetime2 NOT NULL,
    CONSTRAINT [PK_Estoques] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Estoques_Lojas_LojaId] FOREIGN KEY ([LojaId]) REFERENCES [Lojas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Estoques_Produtos_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produtos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Estoques_LojaId] ON [Estoques] ([LojaId]);

GO

CREATE INDEX [IX_Estoques_ProdutoId] ON [Estoques] ([ProdutoId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210421212612_InitialMigration', N'3.1.14');

GO

