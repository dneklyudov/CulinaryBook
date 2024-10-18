USE [master]
GO
/****** Object:  Database [CulinaryBook]    Script Date: 19.10.2024 0:40:16 ******/
CREATE DATABASE [CulinaryBook]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CulinaryBook', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CulinaryBook.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CulinaryBook_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CulinaryBook_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CulinaryBook] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CulinaryBook].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CulinaryBook] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CulinaryBook] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CulinaryBook] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CulinaryBook] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CulinaryBook] SET ARITHABORT OFF 
GO
ALTER DATABASE [CulinaryBook] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CulinaryBook] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CulinaryBook] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CulinaryBook] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CulinaryBook] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CulinaryBook] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CulinaryBook] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CulinaryBook] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CulinaryBook] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CulinaryBook] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CulinaryBook] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CulinaryBook] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CulinaryBook] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CulinaryBook] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CulinaryBook] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CulinaryBook] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CulinaryBook] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CulinaryBook] SET RECOVERY FULL 
GO
ALTER DATABASE [CulinaryBook] SET  MULTI_USER 
GO
ALTER DATABASE [CulinaryBook] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CulinaryBook] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CulinaryBook] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CulinaryBook] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CulinaryBook] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CulinaryBook] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CulinaryBook', N'ON'
GO
ALTER DATABASE [CulinaryBook] SET QUERY_STORE = ON
GO
ALTER DATABASE [CulinaryBook] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CulinaryBook]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 19.10.2024 0:40:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[AuthorID] [int] IDENTITY(1,1) NOT NULL,
	[AuthorName] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 19.10.2024 0:40:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CookingSteps]    Script Date: 19.10.2024 0:40:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CookingSteps](
	[StepID] [int] IDENTITY(1,1) NOT NULL,
	[RecipeID] [int] NOT NULL,
	[StepNumber] [int] NOT NULL,
	[StepDescription] [varchar](255) NOT NULL,
 CONSTRAINT [PK_CookingSteps] PRIMARY KEY CLUSTERED 
(
	[StepID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 19.10.2024 0:40:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[IngredientID] [int] IDENTITY(1,1) NOT NULL,
	[IngredientName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED 
(
	[IngredientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeImages]    Script Date: 19.10.2024 0:40:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeImages](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[RecipeID] [int] NOT NULL,
	[ImagePath] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_RecipeImages] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeIngredients]    Script Date: 19.10.2024 0:40:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeIngredients](
	[RecipeIngredientID] [int] IDENTITY(1,1) NOT NULL,
	[RecipeID] [int] NOT NULL,
	[IngredientID] [int] NOT NULL,
	[Quantity] [real] NOT NULL,
 CONSTRAINT [PK_RecipeIngredients] PRIMARY KEY CLUSTERED 
(
	[RecipeIngredientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipes]    Script Date: 19.10.2024 0:40:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipes](
	[RecipeID] [int] IDENTITY(1,1) NOT NULL,
	[RecipeName] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[AuthorID] [int] NOT NULL,
	[CookingTime] [nvarchar](50) NOT NULL,
	[Image] [image] NOT NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[RecipeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeTags]    Script Date: 19.10.2024 0:40:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeTags](
	[RecipeTagID] [int] IDENTITY(1,1) NOT NULL,
	[RecipeID] [int] NOT NULL,
	[TagID] [int] NOT NULL,
 CONSTRAINT [PK_RecipeTags] PRIMARY KEY CLUSTERED 
(
	[RecipeTagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 19.10.2024 0:40:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[ReviewID] [int] IDENTITY(1,1) NOT NULL,
	[RecipeID] [int] NOT NULL,
	[ReviewText] [varchar](255) NULL,
	[Rating] [int] NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 19.10.2024 0:40:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[TagID] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password]) VALUES (1, N'Иван Иванов', N'ivan', N'123')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password]) VALUES (2, N'Петр Петров', N'petr', N'123')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password]) VALUES (3, N'Кирилл Кириллов', N'kirill', N'123')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password]) VALUES (4, N'Алексей Алексеев', N'alex', N'123')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password]) VALUES (5, N'Сергей Сергеев', N'sergey', N'123')
INSERT [dbo].[Authors] ([AuthorID], [AuthorName], [Login], [Password]) VALUES (6, N'Виктор Викторов', N'viktor', N'123')
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (1, N'Категория 1')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (2, N'Категория 2')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (3, N'Категория 3')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
ALTER TABLE [dbo].[CookingSteps]  WITH CHECK ADD  CONSTRAINT [FK_CookingSteps_Recipes] FOREIGN KEY([RecipeID])
REFERENCES [dbo].[Recipes] ([RecipeID])
GO
ALTER TABLE [dbo].[CookingSteps] CHECK CONSTRAINT [FK_CookingSteps_Recipes]
GO
ALTER TABLE [dbo].[RecipeImages]  WITH CHECK ADD  CONSTRAINT [FK_RecipeImages_Recipes] FOREIGN KEY([RecipeID])
REFERENCES [dbo].[Recipes] ([RecipeID])
GO
ALTER TABLE [dbo].[RecipeImages] CHECK CONSTRAINT [FK_RecipeImages_Recipes]
GO
ALTER TABLE [dbo].[RecipeIngredients]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredients_Ingredients] FOREIGN KEY([IngredientID])
REFERENCES [dbo].[Ingredients] ([IngredientID])
GO
ALTER TABLE [dbo].[RecipeIngredients] CHECK CONSTRAINT [FK_RecipeIngredients_Ingredients]
GO
ALTER TABLE [dbo].[RecipeIngredients]  WITH CHECK ADD  CONSTRAINT [FK_RecipeIngredients_Recipes] FOREIGN KEY([RecipeID])
REFERENCES [dbo].[Recipes] ([RecipeID])
GO
ALTER TABLE [dbo].[RecipeIngredients] CHECK CONSTRAINT [FK_RecipeIngredients_Recipes]
GO
ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Authors] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Authors] ([AuthorID])
GO
ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Authors]
GO
ALTER TABLE [dbo].[Recipes]  WITH CHECK ADD  CONSTRAINT [FK_Recipes_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Recipes] CHECK CONSTRAINT [FK_Recipes_Categories]
GO
ALTER TABLE [dbo].[RecipeTags]  WITH CHECK ADD  CONSTRAINT [FK_RecipeTags_Recipes] FOREIGN KEY([RecipeID])
REFERENCES [dbo].[Recipes] ([RecipeID])
GO
ALTER TABLE [dbo].[RecipeTags] CHECK CONSTRAINT [FK_RecipeTags_Recipes]
GO
ALTER TABLE [dbo].[RecipeTags]  WITH CHECK ADD  CONSTRAINT [FK_RecipeTags_Tags] FOREIGN KEY([TagID])
REFERENCES [dbo].[Tags] ([TagID])
GO
ALTER TABLE [dbo].[RecipeTags] CHECK CONSTRAINT [FK_RecipeTags_Tags]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Recipes] FOREIGN KEY([RecipeID])
REFERENCES [dbo].[Recipes] ([RecipeID])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Recipes]
GO
USE [master]
GO
ALTER DATABASE [CulinaryBook] SET  READ_WRITE 
GO
