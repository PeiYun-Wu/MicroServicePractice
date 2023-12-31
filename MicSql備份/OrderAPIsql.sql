USE [MangoOrderAPI]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2023/9/2 下午 01:05:19 ******/
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
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 2023/9/2 下午 01:05:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[OrderHeaderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderHeaders]    Script Date: 2023/9/2 下午 01:05:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderHeaders](
	[OrderHeaderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NOT NULL,
	[CouponCode] [nvarchar](max) NOT NULL,
	[OrderTotal] [float] NOT NULL,
	[DiscountTotal] [float] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[PickupDateTime] [datetime2](7) NULL,
	[OrderTime] [datetime2](7) NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[CardNumber] [nvarchar](max) NOT NULL,
	[CVV] [nvarchar](max) NOT NULL,
	[ExpiryMonthYear] [nvarchar](max) NOT NULL,
	[CartTotalItems] [int] NOT NULL,
	[PaymentStatus] [bit] NOT NULL,
 CONSTRAINT [PK_OrderHeaders] PRIMARY KEY CLUSTERED 
(
	[OrderHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230607091140_pushOrderTablesToDb', N'7.0.5')
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (6, 2, 2, 7, N'Paneer Tikka', 13.99)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (7, 2, 1, 4, N'Samosa', 15)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (8, 2, 3, 6, N'Sweet Pie', 10.99)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (9, 3, 1, 1, N'Samosa', 15)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (10, 3, 2, 5, N'Paneer Tikka', 13.99)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (12, 7, 3, 1, N'Sweet Pie', 10.99)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (13, 7, 2, 2, N'Paneer Tikka', 13.99)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (14, 9, 1, 2, N'Samosa', 15)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (15, 9, 3, 2, N'Sweet Pie', 10.99)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (16, 20, 2, 1, N'Paneer Tikka', 13.99)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (17, 20, 3, 1, N'Sweet Pie', 10.99)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (18, 22, 1, 2, N'Samosa', 15)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (19, 22, 4, 3, N'Pav Bhaji', 15)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (20, 23, 1, 2, N'Samosa', 15)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (21, 23, 3, 1, N'Sweet Pie', 10.99)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (22, 25, 1, 1, N'Samosa', 15)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (23, 25, 4, 1, N'Pav Bhaji', 15)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (24, 27, 1, 2, N'Samosa', 15)
INSERT [dbo].[OrderDetails] ([OrderDetailsId], [OrderHeaderId], [ProductId], [Count], [ProductName], [Price]) VALUES (25, 27, 3, 1, N'Sweet Pie', 10.99)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderHeaders] ON 

INSERT [dbo].[OrderHeaders] ([OrderHeaderId], [UserId], [CouponCode], [OrderTotal], [DiscountTotal], [FirstName], [LastName], [PickupDateTime], [OrderTime], [Phone], [Email], [CardNumber], [CVV], [ExpiryMonthYear], [CartTotalItems], [PaymentStatus]) VALUES (2, N'4d52ae56-8f3f-49b0-a5ff-9e5306d43bf5', N'10OFF', 223.87, 10, N'PEI-YUN WU', N'0619', CAST(N'2023-06-20T17:25:00.0000000' AS DateTime2), CAST(N'2023-06-19T18:37:20.8412551' AS DateTime2), N'0911554699', N'123', N'0619', N'0619', N'0619', 17, 0)
INSERT [dbo].[OrderHeaders] ([OrderHeaderId], [UserId], [CouponCode], [OrderTotal], [DiscountTotal], [FirstName], [LastName], [PickupDateTime], [OrderTime], [Phone], [Email], [CardNumber], [CVV], [ExpiryMonthYear], [CartTotalItems], [PaymentStatus]) VALUES (3, N'4d52ae56-8f3f-49b0-a5ff-9e5306d43bf5', N'10OFF', 84.95, 10, N'PEI-YUN WU', N'orderTest619', CAST(N'2023-06-20T13:45:00.0000000' AS DateTime2), CAST(N'2023-06-19T19:18:50.4653936' AS DateTime2), N'0911554699', N'123', N'123', N'123', N'0619', 6, 0)
INSERT [dbo].[OrderHeaders] ([OrderHeaderId], [UserId], [CouponCode], [OrderTotal], [DiscountTotal], [FirstName], [LastName], [PickupDateTime], [OrderTime], [Phone], [Email], [CardNumber], [CVV], [ExpiryMonthYear], [CartTotalItems], [PaymentStatus]) VALUES (7, N'4d52ae56-8f3f-49b0-a5ff-9e5306d43bf5', N'10OFF', 38.97, 10, N'test620_1', N'peiyun', CAST(N'2023-06-20T17:25:00.0000000' AS DateTime2), CAST(N'2023-06-20T16:40:48.7976887' AS DateTime2), N'0911554699', N'peiyun4699@gmail.com', N'123', N'620', N'0620', 3, 0)
INSERT [dbo].[OrderHeaders] ([OrderHeaderId], [UserId], [CouponCode], [OrderTotal], [DiscountTotal], [FirstName], [LastName], [PickupDateTime], [OrderTime], [Phone], [Email], [CardNumber], [CVV], [ExpiryMonthYear], [CartTotalItems], [PaymentStatus]) VALUES (9, N'4d52ae56-8f3f-49b0-a5ff-9e5306d43bf5', N'10OFF', 41.980000000000004, 10, N'testCoupon', N'0621', CAST(N'2023-11-29T10:25:00.0000000' AS DateTime2), CAST(N'2023-06-21T10:34:00.5534565' AS DateTime2), N'0911554699', N'peiyun4699@gmail.com', N'1234', N'1234', N'0621', 4, 1)
INSERT [dbo].[OrderHeaders] ([OrderHeaderId], [UserId], [CouponCode], [OrderTotal], [DiscountTotal], [FirstName], [LastName], [PickupDateTime], [OrderTime], [Phone], [Email], [CardNumber], [CVV], [ExpiryMonthYear], [CartTotalItems], [PaymentStatus]) VALUES (20, N'4d52ae56-8f3f-49b0-a5ff-9e5306d43bf5', N'10OFF', 14.98, 10, N'test0629', N'0629', CAST(N'2023-06-30T14:50:00.0000000' AS DateTime2), CAST(N'2023-06-29T11:30:34.7172942' AS DateTime2), N'0911554699', N'peiyun4699@gmail.com', N'123', N'123', N'0629', 2, 1)
INSERT [dbo].[OrderHeaders] ([OrderHeaderId], [UserId], [CouponCode], [OrderTotal], [DiscountTotal], [FirstName], [LastName], [PickupDateTime], [OrderTime], [Phone], [Email], [CardNumber], [CVV], [ExpiryMonthYear], [CartTotalItems], [PaymentStatus]) VALUES (22, N'4d52ae56-8f3f-49b0-a5ff-9e5306d43bf5', N'10OFF', 65, 10, N'PEI-YUN WU', N'0630test', CAST(N'2023-07-04T09:45:00.0000000' AS DateTime2), CAST(N'2023-06-30T11:32:25.3147596' AS DateTime2), N'0911554699', N'peiyun4699@gmail.com', N'0630', N'0630', N'0630', 5, 1)
INSERT [dbo].[OrderHeaders] ([OrderHeaderId], [UserId], [CouponCode], [OrderTotal], [DiscountTotal], [FirstName], [LastName], [PickupDateTime], [OrderTime], [Phone], [Email], [CardNumber], [CVV], [ExpiryMonthYear], [CartTotalItems], [PaymentStatus]) VALUES (23, N'4d52ae56-8f3f-49b0-a5ff-9e5306d43bf5', N'10OFF', 30.990000000000002, 10, N'test0630test2', N'2', CAST(N'2023-07-11T09:45:00.0000000' AS DateTime2), CAST(N'2023-06-30T11:37:08.4791203' AS DateTime2), N'0911554699', N'peiyun4699@gmail.com', N'123', N'123', N'2222', 3, 1)
INSERT [dbo].[OrderHeaders] ([OrderHeaderId], [UserId], [CouponCode], [OrderTotal], [DiscountTotal], [FirstName], [LastName], [PickupDateTime], [OrderTime], [Phone], [Email], [CardNumber], [CVV], [ExpiryMonthYear], [CartTotalItems], [PaymentStatus]) VALUES (25, N'4d52ae56-8f3f-49b0-a5ff-9e5306d43bf5', N'10OFF', 20, 10, N'0630test3', N'0630test3', CAST(N'2023-06-30T18:50:00.0000000' AS DateTime2), CAST(N'2023-06-30T14:32:17.4479937' AS DateTime2), N'0911554699', N'peiyun4699@gmail.com', N'123', N'123', N'123', 2, 1)
INSERT [dbo].[OrderHeaders] ([OrderHeaderId], [UserId], [CouponCode], [OrderTotal], [DiscountTotal], [FirstName], [LastName], [PickupDateTime], [OrderTime], [Phone], [Email], [CardNumber], [CVV], [ExpiryMonthYear], [CartTotalItems], [PaymentStatus]) VALUES (27, N'4d52ae56-8f3f-49b0-a5ff-9e5306d43bf5', N'10OFF', 30.990000000000002, 10, N'PEI-YUN WU', N'test0630demo', CAST(N'2023-07-12T10:50:00.0000000' AS DateTime2), CAST(N'2023-06-30T15:11:38.3218930' AS DateTime2), N'0911554699', N'peiyun4699@gmail.com', N'123', N'123', N'0630', 3, 1)
SET IDENTITY_INSERT [dbo].[OrderHeaders] OFF
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_OrderHeaders_OrderHeaderId] FOREIGN KEY([OrderHeaderId])
REFERENCES [dbo].[OrderHeaders] ([OrderHeaderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_OrderHeaders_OrderHeaderId]
GO
