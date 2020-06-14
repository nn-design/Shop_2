USE [Shop]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2020/6/4 11:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductTitle] [nvarchar](500) NULL,
	[ProductMainImg] [varchar](500) NULL,
	[ProductSlideImgs] [varchar](1000) NULL,
	[ProductDetail] [nvarchar](max) NULL,
	[ProductSku] [varchar](3000) NULL,
	[CreatTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[ProductCategoryID] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[Stock] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttr]    Script Date: 2020/6/4 11:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttr](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductAttrs] [varchar](2000) NULL,
	[ProuductID] [int] NULL,
 CONSTRAINT [PK_ProductAttr] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttrKey]    Script Date: 2020/6/4 11:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttrKey](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AttrName] [nvarchar](50) NULL,
	[ProductCategoryID] [int] NULL,
	[IsSku] [int] NULL,
	[EnterType] [int] NULL,
	[OrderNum] [int] NULL,
 CONSTRAINT [PK_ProductAttrKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttrValue]    Script Date: 2020/6/4 11:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttrValue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AttrValue] [nvarchar](50) NULL,
	[ProductAttrKeyID] [int] NULL,
 CONSTRAINT [PK_ProductAttrValue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 2020/6/4 11:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Img] [nvarchar](200) NULL,
	[PID] [int] NULL,
	[OrderNum] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductSku]    Script Date: 2020/6/4 11:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSku](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductSku] [varchar](1000) NULL,
	[Price] [decimal](18, 2) NULL,
	[Stock] [int] NULL,
	[ProductID] [int] NULL,
	[SkuNum] [varchar](50) NULL,
 CONSTRAINT [PK_ProductSku] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [ProductTitle], [ProductMainImg], [ProductSlideImgs], [ProductDetail], [ProductSku], [CreatTime], [UpdateTime], [ProductCategoryID], [Price], [Stock]) VALUES (1, N'MG小象轻熟短袖白衬衫女夏季新款设计感小众气质复古泡泡袖衬衣潮', NULL, NULL, NULL, N'{"尺码":"S","颜色":"白色","Price":100.00,"Stock":100}{"尺码":"S","颜色":"黑色",Price":100.00,"Stock":120"}', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Product] ([ID], [ProductTitle], [ProductMainImg], [ProductSlideImgs], [ProductDetail], [ProductSku], [CreatTime], [UpdateTime], [ProductCategoryID], [Price], [Stock]) VALUES (2, N'测试1', N'["/Upload/Product/ProductMainImg/202006041057064392583151.png"]', N'["/Upload/Product/ProductSlideImgs/202006041057133337939089.png","/Upload/Product/ProductSlideImgs/202006041057133367839089.png","/Upload/Product/ProductSlideImgs/202006041057134345248807.png"]', N'["/Upload/Product/ProductDetail/202006041057224860225464.png","/Upload/Product/ProductDetail/202006041057224870525464.png"]', NULL, NULL, NULL, 6, CAST(200.00 AS Decimal(18, 2)), 1000)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[ProductAttr] ON 

INSERT [dbo].[ProductAttr] ([ID], [ProductAttrs], [ProuductID]) VALUES (1, N'{"AttrName":" 适用季节","AttrValue":"夏"}', 2)
INSERT [dbo].[ProductAttr] ([ID], [ProductAttrs], [ProuductID]) VALUES (2, N'{"AttrName":" 上市年份","AttrValue":"2020春"}', 2)
SET IDENTITY_INSERT [dbo].[ProductAttr] OFF
SET IDENTITY_INSERT [dbo].[ProductAttrKey] ON 

INSERT [dbo].[ProductAttrKey] ([ID], [AttrName], [ProductCategoryID], [IsSku], [EnterType], [OrderNum]) VALUES (1, N'适用季节', 3, 0, 2, 1)
INSERT [dbo].[ProductAttrKey] ([ID], [AttrName], [ProductCategoryID], [IsSku], [EnterType], [OrderNum]) VALUES (5, N'领型', 3, 0, 1, 2)
INSERT [dbo].[ProductAttrKey] ([ID], [AttrName], [ProductCategoryID], [IsSku], [EnterType], [OrderNum]) VALUES (8, N'颜色', 6, 1, 1, 1)
INSERT [dbo].[ProductAttrKey] ([ID], [AttrName], [ProductCategoryID], [IsSku], [EnterType], [OrderNum]) VALUES (9, N'尺码', 6, 1, 2, 2)
INSERT [dbo].[ProductAttrKey] ([ID], [AttrName], [ProductCategoryID], [IsSku], [EnterType], [OrderNum]) VALUES (11, N'袖长', 6, 1, 2, 4)
INSERT [dbo].[ProductAttrKey] ([ID], [AttrName], [ProductCategoryID], [IsSku], [EnterType], [OrderNum]) VALUES (12, N'适用季节', 6, 0, 2, 1)
INSERT [dbo].[ProductAttrKey] ([ID], [AttrName], [ProductCategoryID], [IsSku], [EnterType], [OrderNum]) VALUES (13, N'上市年份', 6, 0, 1, 2)
INSERT [dbo].[ProductAttrKey] ([ID], [AttrName], [ProductCategoryID], [IsSku], [EnterType], [OrderNum]) VALUES (1012, N'领型', 6, 1, 1, 5)
SET IDENTITY_INSERT [dbo].[ProductAttrKey] OFF
SET IDENTITY_INSERT [dbo].[ProductAttrValue] ON 

INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (7, N'春', 1)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (8, N'夏', 1)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (9, N'秋', 1)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (10, N'东', 1)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (11, N'X', 9)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (12, N'XL', 9)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (13, N'XXL', 9)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (14, N'长袖', 11)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (15, N'短袖', 11)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (16, N'春', 12)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (17, N'夏', 12)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (18, N'秋', 12)
INSERT [dbo].[ProductAttrValue] ([ID], [AttrValue], [ProductAttrKeyID]) VALUES (19, N'东', 12)
SET IDENTITY_INSERT [dbo].[ProductAttrValue] OFF
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

INSERT [dbo].[ProductCategory] ([ID], [Name], [Img], [PID], [OrderNum], [CreateTime], [UpdateTime]) VALUES (1, N'男装', N'202004261611170489013101.png', 0, 1, CAST(N'2020-04-26T16:11:17.053' AS DateTime), NULL)
INSERT [dbo].[ProductCategory] ([ID], [Name], [Img], [PID], [OrderNum], [CreateTime], [UpdateTime]) VALUES (2, N'男士上衣', N'202004261611473235320604.png', 1, 1, CAST(N'2020-04-26T16:11:47.323' AS DateTime), NULL)
INSERT [dbo].[ProductCategory] ([ID], [Name], [Img], [PID], [OrderNum], [CreateTime], [UpdateTime]) VALUES (3, N'T恤', N'202004261612143243264117.png', 2, 1, CAST(N'2020-04-26T16:12:14.327' AS DateTime), NULL)
INSERT [dbo].[ProductCategory] ([ID], [Name], [Img], [PID], [OrderNum], [CreateTime], [UpdateTime]) VALUES (5, N'男士裤子', N'202004281847224031652014.png', 1, 2, CAST(N'2020-04-28T18:47:22.407' AS DateTime), NULL)
INSERT [dbo].[ProductCategory] ([ID], [Name], [Img], [PID], [OrderNum], [CreateTime], [UpdateTime]) VALUES (6, N'牛仔裤', N'202004281847519018111224.png', 5, 1, CAST(N'2020-04-28T18:47:51.903' AS DateTime), NULL)
INSERT [dbo].[ProductCategory] ([ID], [Name], [Img], [PID], [OrderNum], [CreateTime], [UpdateTime]) VALUES (7, N'衬衣', N'202005191045480115024782.png', 2, 3, CAST(N'2020-05-19T10:45:48.013' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
SET IDENTITY_INSERT [dbo].[ProductSku] ON 

INSERT [dbo].[ProductSku] ([ID], [ProductSku], [Price], [Stock], [ProductID], [SkuNum]) VALUES (1, N'{"尺码":"S","颜色":"白色"}', CAST(100.00 AS Decimal(18, 2)), 100, 1, NULL)
INSERT [dbo].[ProductSku] ([ID], [ProductSku], [Price], [Stock], [ProductID], [SkuNum]) VALUES (2, N'{"尺码":"S","颜色":"黑色"}', CAST(100.00 AS Decimal(18, 2)), 120, 1, NULL)
INSERT [dbo].[ProductSku] ([ID], [ProductSku], [Price], [Stock], [ProductID], [SkuNum]) VALUES (3, N'{"尺码":"M","颜色":"白色"}', CAST(100.00 AS Decimal(18, 2)), 200, 1, NULL)
INSERT [dbo].[ProductSku] ([ID], [ProductSku], [Price], [Stock], [ProductID], [SkuNum]) VALUES (4, N'{"尺码":"M","颜色":"黑色"}', CAST(100.00 AS Decimal(18, 2)), 300, 1, NULL)
INSERT [dbo].[ProductSku] ([ID], [ProductSku], [Price], [Stock], [ProductID], [SkuNum]) VALUES (5, N'[{"name":"颜色","value":"白色"},{"name":"尺码","value":"X"},{"name":"袖长","value":"长袖"},{"name":"领型","value":"V领"}]', CAST(100.00 AS Decimal(18, 2)), 200, 2, N'sku001')
INSERT [dbo].[ProductSku] ([ID], [ProductSku], [Price], [Stock], [ProductID], [SkuNum]) VALUES (6, N'[{"name":"颜色","value":"白色"},{"name":"尺码","value":"XL"},{"name":"袖长","value":"长袖"},{"name":"领型","value":"V领"}]', CAST(100.00 AS Decimal(18, 2)), 200, 2, N'sku002')
INSERT [dbo].[ProductSku] ([ID], [ProductSku], [Price], [Stock], [ProductID], [SkuNum]) VALUES (7, N'[{"name":"颜色","value":"黑色"},{"name":"尺码","value":"X"},{"name":"袖长","value":"长袖"},{"name":"领型","value":"V领"}]', CAST(100.00 AS Decimal(18, 2)), 200, 2, N'sku003')
INSERT [dbo].[ProductSku] ([ID], [ProductSku], [Price], [Stock], [ProductID], [SkuNum]) VALUES (8, N'[{"name":"颜色","value":"黑色"},{"name":"尺码","value":"XL"},{"name":"袖长","value":"长袖"},{"name":"领型","value":"V领"}]', CAST(100.00 AS Decimal(18, 2)), 200, 2, N'sku004')
SET IDENTITY_INSERT [dbo].[ProductSku] OFF
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0表示普通属性，1表示规格（sku）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductAttrKey', @level2type=N'COLUMN',@level2name=N'IsSku'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入方式：0表示收的录入，1表示下拉选择' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ProductAttrKey', @level2type=N'COLUMN',@level2name=N'EnterType'
GO
