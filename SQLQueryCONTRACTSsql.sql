USE [CAR_RENT]
GO

/****** Object:  Table [dbo].[CONTRACTS]    Script Date: 05.05.2022 17:46:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CONTRACTS](
	[ID] int identity primary key,
	[CLIENT_ID] int NULL,
	[CAR_ID] int NULL,
	[CONTRACT_START] date NULL,
	[CONTRACT_END] date NULL,
	[COUNT_OF_DAYS] int NULL,
	[PROMO_CODE] [nvarchar](20) NULL,
	[TOTAL_COST] int NULL
) ON [PRIMARY]
GO


