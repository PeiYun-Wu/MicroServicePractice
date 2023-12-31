USE [MangoShoppingCartAPI]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2023/9/2 下午 01:07:35 ******/
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
/****** Object:  Table [dbo].[CartDetails]    Script Date: 2023/9/2 下午 01:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetails](
	[CartDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[CartHeaderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK_CartDetails] PRIMARY KEY CLUSTERED 
(
	[CartDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartHeaders]    Script Date: 2023/9/2 下午 01:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartHeaders](
	[CartHeaderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[CouponCode] [nvarchar](max) NULL,
 CONSTRAINT [PK_CartHeaders] PRIMARY KEY CLUSTERED 
(
	[CartHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2023/9/2 下午 01:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CategoryName] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230530034034_AddProductAndCartModels', N'7.0.0-rc.2.22472.11')
GO
SET IDENTITY_INSERT [dbo].[CartDetails] ON 

INSERT [dbo].[CartDetails] ([CartDetailsId], [CartHeaderId], [ProductId], [Count]) VALUES (6, 2, 3, 4)
SET IDENTITY_INSERT [dbo].[CartDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[CartHeaders] ON 

INSERT [dbo].[CartHeaders] ([CartHeaderId], [UserId], [CouponCode]) VALUES (2, N'eefcf28c-408e-4324-8d34-2becf806a5fc', N'10OFF')
SET IDENTITY_INSERT [dbo].[CartHeaders] OFF
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Price], [Description], [CategoryName], [ImageUrl]) VALUES (1, N'Samosa', 15, N'Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.', N'Appetizer', N'https://dotnetpenny.blob.core.windows.net/mango/14.jpg')
INSERT [dbo].[Products] ([ProductId], [Name], [Price], [Description], [CategoryName], [ImageUrl]) VALUES (2, N'Paneer Tikka', 13.99, N'Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.', N'Appetizer', N'https://dotnetpenny.blob.core.windows.net/mango/12.jpg')
INSERT [dbo].[Products] ([ProductId], [Name], [Price], [Description], [CategoryName], [ImageUrl]) VALUES (3, N'Sweet Pie', 10.99, N'Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.', N'Dessert', N'https://dotnetpenny.blob.core.windows.net/mango/11.jpg')
INSERT [dbo].[Products] ([ProductId], [Name], [Price], [Description], [CategoryName], [ImageUrl]) VALUES (4, N'Pav Bhaji', 15, N'Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.', N'Entree', N'https://dotnetpenny.blob.core.windows.net/mango/13.jpg')
GO
ALTER TABLE [dbo].[CartDetails]  WITH CHECK ADD  CONSTRAINT [FK_CartDetails_CartHeaders_CartHeaderId] FOREIGN KEY([CartHeaderId])
REFERENCES [dbo].[CartHeaders] ([CartHeaderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartDetails] CHECK CONSTRAINT [FK_CartDetails_CartHeaders_CartHeaderId]
GO
ALTER TABLE [dbo].[CartDetails]  WITH CHECK ADD  CONSTRAINT [FK_CartDetails_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartDetails] CHECK CONSTRAINT [FK_CartDetails_Products_ProductId]
GO
