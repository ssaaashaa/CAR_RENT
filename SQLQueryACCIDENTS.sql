USE [CAR_RENT]
GO

/****** Object:  Table [dbo].[ACCIDENTS]    Script Date: 05.05.2022 16:28:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ACCIDENTS](
	[ID] int identity primary key,
	[CONTRACT_ID] int NULL,
	[DATE] date NULL,
	[DAMAGE_COST] int NULL,
	[DAMAGE_DESCRIPTION] [nvarchar](300) NULL
) ON [PRIMARY]
GO


