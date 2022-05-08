USE [CAR_RENT]
GO

/****** Object:  Table [dbo].[RENT]    Script Date: 05.05.2022 18:00:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RENT](
	[ID] int identity primary key,
	[COUNT_OF_DAYS] int NULL,
	[BYN_PER_DAY] [nvarchar](40) NULL
) ON [PRIMARY]
GO


