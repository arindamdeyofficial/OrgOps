/****** Object:  Table [dbo].[Batch]    Script Date: 10/28/2020 11:28:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Batch](
	[BatchId] [nchar](100) NULL,
	[BatchStudentNumber] [smallint] NULL,
	[BatchTeacherId] [nchar](100) NULL
) ON [PRIMARY]
GO