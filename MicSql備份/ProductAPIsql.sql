USE [MangoProductAPI]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2023/9/2 下午 01:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2023/9/2 下午 01:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230522030859_AddProductModelToDb', N'8.0.0-preview.4.23259.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230522090130_SeedProducts', N'8.0.0-preview.4.23259.3')
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [Name], [Price], [Description], [CategoryName], [ImageUrl]) VALUES (1, N'Samosa', 15, N'Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.', N'Appetizer', N'Images/14.jpg')
INSERT [dbo].[Products] ([ProductId], [Name], [Price], [Description], [CategoryName], [ImageUrl]) VALUES (2, N'Paneer Tikka', 13.99, N'Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.', N'Appetizer', N'https://dotnetpenny.blob.core.windows.net/mango/12.jpg')
INSERT [dbo].[Products] ([ProductId], [Name], [Price], [Description], [CategoryName], [ImageUrl]) VALUES (3, N'Sweet Pie', 10.99, N'Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.', N'Dessert', N'C:\Users\peiyun\Downloads\course_19\5 Microservices - Mango\Images\11.jpg')
INSERT [dbo].[Products] ([ProductId], [Name], [Price], [Description], [CategoryName], [ImageUrl]) VALUES (4, N'Pav Bhaji', 15, N'Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.', N'Entree', N'https://dotnetpenny.blob.core.windows.net/mango/13.jpg')
INSERT [dbo].[Products] ([ProductId], [Name], [Price], [Description], [CategoryName], [ImageUrl]) VALUES (6, N'test0523edit2', 523, N'523edit2', N'edit2', N'523')
INSERT [dbo].[Products] ([ProductId], [Name], [Price], [Description], [CategoryName], [ImageUrl]) VALUES (14, N'test0612', 12, N'123', N'0612', N'123')
INSERT [dbo].[Products] ([ProductId], [Name], [Price], [Description], [CategoryName], [ImageUrl]) VALUES (15, N'test0616', 16, N'123', N'123', N'123')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
