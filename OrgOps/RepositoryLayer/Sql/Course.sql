/****** Object:  Table [dbo].[Course]    Script Date: 10/28/2020 11:30:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Course](
	[CourseId] [nchar](100) NOT NULL,
	[CourseName] [nvarchar](500) NULL,
	[CourseDesc] [nvarchar](1000) NULL
) ON [PRIMARY]
GO


