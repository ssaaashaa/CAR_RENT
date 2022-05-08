USE [CAR_RENT]
GO

/****** Object:  Table [dbo].[CARS]    Script Date: 05.05.2022 16:35:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CARS](
	[ID] int identity primary key,
	[BREND] [nvarchar](50) NULL,
	[MODEL] [nvarchar](100) NULL,
	[CLASS] [nvarchar](100) NULL,
	[REGISTRATION_NUMBER] [nchar](50) NULL,
	[STATUS] [nchar](30) NULL,
	[RENT_ID] int NULL
) ON [PRIMARY]
GO


