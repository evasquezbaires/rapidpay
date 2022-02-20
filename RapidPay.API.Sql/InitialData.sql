USE [RapidPayDB]
GO
SET IDENTITY_INSERT [dbo].[CreditCard] ON 
GO
INSERT [dbo].[CreditCard] ([Id], [CardNumber], [CardHolder], [ExpirationMonth], [ExpirationYear], [CVV], [TotalAmount], [BalanceAmount], [CreatedDate], [CreatedBy]) VALUES (1, N'464658585960018', N'EVASQUEZ', 12, 25, N'484', 100.0000, 80.0875, CAST(N'2022-02-19' AS Date), N'edwin')
GO
INSERT [dbo].[CreditCard] ([Id], [CardNumber], [CardHolder], [ExpirationMonth], [ExpirationYear], [CVV], [TotalAmount], [BalanceAmount], [CreatedDate], [CreatedBy]) VALUES (2, N'253058584918091', N'LUNAY', 12, 25, N'008', 1000.0000, 989.3750, CAST(N'2022-02-19' AS Date), N'lula')
GO
SET IDENTITY_INSERT [dbo].[CreditCard] OFF
GO
SET IDENTITY_INSERT [dbo].[PaymentCard] ON 
GO
INSERT [dbo].[PaymentCard] ([Id], [CreditCardId], [Description], [Amount], [Fee], [CreatedDate], [CreatedBy]) VALUES (1, 1, N'Water', 2.0000, 1.5625, CAST(N'2022-02-19' AS Date), N'edwin')
GO
INSERT [dbo].[PaymentCard] ([Id], [CreditCardId], [Description], [Amount], [Fee], [CreatedDate], [CreatedBy]) VALUES (2, 1, N'Coke', 5.0000, 1.5625, CAST(N'2022-02-19' AS Date), N'edwin')
GO
INSERT [dbo].[PaymentCard] ([Id], [CreditCardId], [Description], [Amount], [Fee], [CreatedDate], [CreatedBy]) VALUES (3, 2, N'Rice', 2.5000, 1.5625, CAST(N'2022-02-19' AS Date), N'lula')
GO
INSERT [dbo].[PaymentCard] ([Id], [CreditCardId], [Description], [Amount], [Fee], [CreatedDate], [CreatedBy]) VALUES (4, 2, N'Butter', 5.0000, 1.5625, CAST(N'2022-02-19' AS Date), N'lula')
GO
INSERT [dbo].[PaymentCard] ([Id], [CreditCardId], [Description], [Amount], [Fee], [CreatedDate], [CreatedBy]) VALUES (5, 1, N'Sugar', 8.0000, 1.7875, CAST(N'2022-02-19' AS Date), N'lula')
GO
SET IDENTITY_INSERT [dbo].[PaymentCard] OFF
GO
SET IDENTITY_INSERT [dbo].[FeeHistory] ON 
GO
INSERT [dbo].[FeeHistory] ([Id], [FeeExchange], [FeeRate], [CreatedDate], [CreatedBy]) VALUES (1, CAST(1.25 AS Decimal(18, 2)), CAST(1.25 AS Decimal(18, 2)), CAST(N'2022-02-19T22:28:44.583' AS DateTime), N'edwin')
GO
INSERT [dbo].[FeeHistory] ([Id], [FeeExchange], [FeeRate], [CreatedDate], [CreatedBy]) VALUES (2, CAST(1.43 AS Decimal(18, 2)), CAST(1.79 AS Decimal(18, 2)), CAST(N'2022-02-19T22:36:25.700' AS DateTime), N'lula')
GO
SET IDENTITY_INSERT [dbo].[FeeHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[UserIdentity] ON 
GO
INSERT [dbo].[UserIdentity] ([Id], [Name], [Password], [CreatedDate]) VALUES (1, N'edwin', N'ZXZhc3F1ZXo=', CAST(N'2022-02-19' AS Date))
GO
INSERT [dbo].[UserIdentity] ([Id], [Name], [Password], [CreatedDate]) VALUES (2, N'lula', N'am9qb2pv', CAST(N'2022-02-19' AS Date))
GO
SET IDENTITY_INSERT [dbo].[UserIdentity] OFF
GO