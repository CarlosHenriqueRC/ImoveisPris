CREATE DATABASE imovelDB;
go 

USE [ImovelDB]
GO

/****** Object: Table [dbo].[TipoDeImovel] Script Date: 25/05/2021 02:25:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TipoDeImovel] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Descricao] VARCHAR (50) NOT NULL
);
Go 

USE [ImovelDB]
GO

/****** Object: Table [dbo].[TipoDeDisponibilidade] Script Date: 25/05/2021 02:25:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TipoDeDisponibilidade] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Descricao] VARCHAR (50) NOT NULL
);
Go 

USE [ImovelDB]
GO

/****** Object: Table [dbo].[Imovel] Script Date: 25/05/2021 02:25:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Imovel] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [Logradouro]              VARCHAR (100)   NOT NULL,
    [Numero]                  INT             NOT NULL,
    [Bairro]                  VARCHAR (50)    NOT NULL,
    [CEP]                     INT             NOT NULL,
    [Quartos]                 INT             NOT NULL,
    [Banheiros]               INT             NOT NULL,
    [Vagas]                   INT             NOT NULL,
    [Suites]                  INT             NOT NULL,
    [Area]                    DECIMAL (18, 2) NOT NULL,
    [Valor]                   DECIMAL (18, 2) NOT NULL,
    [Complemento]             VARCHAR (200)   NULL,
    [Observacoes]             VARCHAR (1000)  NULL,
    [TipoDeImovelId]          INT             NOT NULL,
    [TipoDeDisponibilidadeId] INT             NOT NULL
);
go 

Alter Table [dbo].[imovel]
	Add constraint [PK_Imovel] Primary Key (Id);
go
	
Alter Table [dbo].[TipoDeDisponibilidade]
	Add constraint [PK_TipoDeDisponibilidade] Primary Key (Id);
go

Alter Table [dbo].[TipoDeImovel]
	Add constraint [PK_TipoDeImovel] Primary Key (Id);
go

ALTER TABLE [dbo].[Imovel]
    ADD CONSTRAINT [FK_Imovel_TipoDeDisponibilidade] FOREIGN KEY ([TipoDeDisponibilidadeId]) REFERENCES [dbo].[TipoDeDisponibilidade] ([Id]);
go

ALTER TABLE [dbo].[Imovel]
    ADD CONSTRAINT [FK_Imovel_TipoDeImovel] FOREIGN KEY ([TipoDeImovelId]) REFERENCES [dbo].[TipoDeImovel] ([Id]);
go
