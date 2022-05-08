USE [CAR_RENT]
GO

/****** Object:  Table [dbo].[PROMO_CODE]    Script Date: 05.05.2022 17:56:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PROMO_CODE](
	[PROMO_CODE] [nvarchar](20) primary key,
	[DISCOUNT_AMOUNT] [nvarchar](10) NULL
) ON [PRIMARY]
GO


