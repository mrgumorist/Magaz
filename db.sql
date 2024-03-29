USE [master]
GO
/****** Object:  Database [Lol]    Script Date: 22.02.2020 14:22:27 ******/
CREATE DATABASE [Lol]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Lol', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Lol.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Lol_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Lol_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Lol] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Lol].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Lol] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Lol] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Lol] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Lol] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Lol] SET ARITHABORT OFF 
GO
ALTER DATABASE [Lol] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Lol] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Lol] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Lol] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Lol] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Lol] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Lol] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Lol] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Lol] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Lol] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Lol] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Lol] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Lol] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Lol] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Lol] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Lol] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Lol] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Lol] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Lol] SET  MULTI_USER 
GO
ALTER DATABASE [Lol] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Lol] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Lol] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Lol] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Lol] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Lol] SET QUERY_STORE = OFF
GO
USE [Lol]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 22.02.2020 14:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Checks]    Script Date: 22.02.2020 14:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Checks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SumPrice] [float] NOT NULL,
	[DateCreatingOfCheck] [datetime] NOT NULL,
	[DateCloseOfCheck] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Checks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 22.02.2020 14:22:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Logs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductReports]    Script Date: 22.02.2020 14:22:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductReports](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[SpecialCode] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Count] [int] NULL,
	[Massa] [float] NULL,
	[IsNumurable] [bit] NULL,
	[DateOfIt] [datetime] NULL,
	[transaction_ID] [int] NULL,
	[IDOfProduct] [int] NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_dbo.ProductReports] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 22.02.2020 14:22:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[SpecialCode] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Count] [int] NULL,
	[Massa] [float] NULL,
	[IsNumurable] [bit] NULL,
	[CameToTheStorage] [datetime] NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductsInChecks]    Script Date: 22.02.2020 14:22:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsInChecks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDOfProduct] [int] NULL,
	[Name] [nvarchar](max) NULL,
	[SpecialCode] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Count] [int] NULL,
	[Massa] [float] NULL,
	[IsNumurable] [bit] NULL,
	[Price] [float] NOT NULL,
	[Check_ID] [int] NULL,
 CONSTRAINT [PK_dbo.ProductsInChecks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 22.02.2020 14:22:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NOT NULL,
	[transactionType_ID] [int] NULL,
	[Sum] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Transactions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionTypes]    Script Date: 22.02.2020 14:22:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.TransactionTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 22.02.2020 14:22:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Login] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[LastLogin] [datetime] NOT NULL,
	[UsersType_Id] [int] NULL,
	[Surname] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersTypes]    Script Date: 22.02.2020 14:22:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UsersTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_transaction_ID]    Script Date: 22.02.2020 14:22:29 ******/
CREATE NONCLUSTERED INDEX [IX_transaction_ID] ON [dbo].[ProductReports]
(
	[transaction_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Check_ID]    Script Date: 22.02.2020 14:22:29 ******/
CREATE NONCLUSTERED INDEX [IX_Check_ID] ON [dbo].[ProductsInChecks]
(
	[Check_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_transactionType_ID]    Script Date: 22.02.2020 14:22:29 ******/
CREATE NONCLUSTERED INDEX [IX_transactionType_ID] ON [dbo].[Transactions]
(
	[transactionType_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UsersType_Id]    Script Date: 22.02.2020 14:22:29 ******/
CREATE NONCLUSTERED INDEX [IX_UsersType_Id] ON [dbo].[Users]
(
	[UsersType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Checks] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DateCreatingOfCheck]
GO
ALTER TABLE [dbo].[Checks] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DateCloseOfCheck]
GO
ALTER TABLE [dbo].[ProductReports] ADD  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[Transactions] ADD  DEFAULT ((0)) FOR [Sum]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('') FOR [Surname]
GO
ALTER TABLE [dbo].[ProductReports]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductReports_dbo.Transactions_transaction_ID] FOREIGN KEY([transaction_ID])
REFERENCES [dbo].[Transactions] ([ID])
GO
ALTER TABLE [dbo].[ProductReports] CHECK CONSTRAINT [FK_dbo.ProductReports_dbo.Transactions_transaction_ID]
GO
ALTER TABLE [dbo].[ProductsInChecks]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductsInChecks_dbo.Checks_Check_ID] FOREIGN KEY([Check_ID])
REFERENCES [dbo].[Checks] ([ID])
GO
ALTER TABLE [dbo].[ProductsInChecks] CHECK CONSTRAINT [FK_dbo.ProductsInChecks_dbo.Checks_Check_ID]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Transactions_dbo.TransactionTypes_transactionType_ID] FOREIGN KEY([transactionType_ID])
REFERENCES [dbo].[TransactionTypes] ([ID])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_dbo.Transactions_dbo.TransactionTypes_transactionType_ID]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.UsersTypes_UsersType_Id] FOREIGN KEY([UsersType_Id])
REFERENCES [dbo].[UsersTypes] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.UsersTypes_UsersType_Id]
GO
USE [master]
GO
ALTER DATABASE [Lol] SET  READ_WRITE 
GO
