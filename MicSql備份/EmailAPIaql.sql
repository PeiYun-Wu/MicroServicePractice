USE [MangoEmailAPI]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2023/9/2 下午 01:03:04 ******/
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
/****** Object:  Table [dbo].[EmailLogs]    Script Date: 2023/9/2 下午 01:03:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Log] [nvarchar](max) NOT NULL,
	[EmailSent] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_EmailLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230614030421_addEmailLog', N'7.0.5')
GO
SET IDENTITY_INSERT [dbo].[EmailLogs] ON 

INSERT [dbo].[EmailLogs] ([Id], [Email], [Log], [EmailSent]) VALUES (1, N'peiyun4699@gmail.com', N'Order - 9 has been created successfully.', CAST(N'2023-06-21T10:34:18.6238973' AS DateTime2))
INSERT [dbo].[EmailLogs] ([Id], [Email], [Log], [EmailSent]) VALUES (2, N'peiyun4699@gmail.com', N'Order - 20 has been created successfully.', CAST(N'2023-06-29T11:30:41.6822921' AS DateTime2))
INSERT [dbo].[EmailLogs] ([Id], [Email], [Log], [EmailSent]) VALUES (3, N'peiyun4699@gmail.com', N'Order - 22 has been created successfully.', CAST(N'2023-06-30T11:33:00.6283186' AS DateTime2))
INSERT [dbo].[EmailLogs] ([Id], [Email], [Log], [EmailSent]) VALUES (4, N'peiyun4699@gmail.com', N'Order - 23 has been created successfully.', CAST(N'2023-06-30T11:37:08.4968439' AS DateTime2))
INSERT [dbo].[EmailLogs] ([Id], [Email], [Log], [EmailSent]) VALUES (5, N'peiyun4699@gmail.com', N'Order - 25 has been created successfully.', CAST(N'2023-06-30T14:33:02.9173255' AS DateTime2))
INSERT [dbo].[EmailLogs] ([Id], [Email], [Log], [EmailSent]) VALUES (6, N'peiyun4699@gmail.com', N'Order - 27 has been created successfully.', CAST(N'2023-06-30T15:11:42.1443467' AS DateTime2))
SET IDENTITY_INSERT [dbo].[EmailLogs] OFF
GO
