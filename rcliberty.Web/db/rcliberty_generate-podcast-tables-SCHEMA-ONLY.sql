IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Episodes_Series]') AND parent_object_id = OBJECT_ID(N'[dbo].[Episodes]'))
ALTER TABLE [dbo].[Episodes] DROP CONSTRAINT [FK_Episodes_Series]
GO
/****** Object:  Table [dbo].[Series]    Script Date: 12/31/2018 9:52:19 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Series]') AND type in (N'U'))
DROP TABLE [dbo].[Series]
GO
/****** Object:  Table [dbo].[Episodes]    Script Date: 12/31/2018 9:52:19 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Episodes]') AND type in (N'U'))
DROP TABLE [dbo].[Episodes]
GO
/****** Object:  Table [dbo].[Episodes]    Script Date: 12/31/2018 9:52:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Episodes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Episodes](
	[Id] [varchar](max) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Date] [date] NULL,
	[AudioUrl] [varchar](150) NULL,
	[SeriesId] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Series]    Script Date: 12/31/2018 9:52:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Series]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Series](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Image] [nvarchar](100) NULL,
 CONSTRAINT [PK_Series] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Episodes_Series]') AND parent_object_id = OBJECT_ID(N'[dbo].[Episodes]'))
ALTER TABLE [dbo].[Episodes]  WITH CHECK ADD  CONSTRAINT [FK_Episodes_Series] FOREIGN KEY([SeriesId])
REFERENCES [dbo].[Series] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Episodes_Series]') AND parent_object_id = OBJECT_ID(N'[dbo].[Episodes]'))
ALTER TABLE [dbo].[Episodes] CHECK CONSTRAINT [FK_Episodes_Series]
GO
