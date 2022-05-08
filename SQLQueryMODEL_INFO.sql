USE [CAR_RENT]
GO

/****** Object:  Table [dbo].[MODEL_INFO]    Script Date: 05.05.2022 17:53:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MODEL_INFO](
	[MODEL] [nvarchar](100) primary key,
	[YEAR_OF_ISSUE] date NULL,
	[BODY_TYPE] [nvarchar](50) NULL,
	[ENGINE_CAPACITY] [nvarchar](50) NULL,
	[ENGINE_TYPE] [nvarchar](50) NULL,
	[TRANSMISSION] [nvarchar](50) NULL,
	[EQUIPMENT] [nvarchar](300) NULL
) ON [PRIMARY]
GO


