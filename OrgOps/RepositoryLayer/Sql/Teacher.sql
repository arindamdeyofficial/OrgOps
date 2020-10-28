/****** Object:  Table [dbo].[Teacher]    Script Date: 10/28/2020 11:32:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Teacher](
	[TeacherId] [nchar](100) NULL,
	[TeacherName] [nvarchar](100) NULL,
	[TeacherSkills] [nvarchar](50) NULL,
	[TeacherBatches] [nvarchar](50) NULL
) ON [PRIMARY]
GO


