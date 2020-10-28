/****** Object:  Table [dbo].[Student]    Script Date: 10/28/2020 11:31:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Student](
	[StudentId] [nchar](100) NOT NULL,
	[StudentName] [nvarchar](100) NULL,
	[StudentBatch] [nvarchar](50) NULL
) ON [PRIMARY]
GO


