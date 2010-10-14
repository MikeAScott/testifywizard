SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ApplicationInfo]') AND type in (N'U'))
DROP TABLE [dbo].[ApplicationInfo]
GO

CREATE TABLE [dbo].[ApplicationInfo](
	[id] [int] NOT NULL DEFAULT(1) CHECK(id = 1),
	[version] [int] NOT NULL,
	[fixedSysTime] [date] NULL,
 CONSTRAINT [PK_ApplicationInfo] PRIMARY KEY CLUSTERED 
	([id] ASC) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO [dbo].[ApplicationInfo](version,fixedSysTime) 
	VALUES(1,NULL);
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SetDbVersion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SetVersion]
GO

-- =============================================
-- Sets db version number
-- =============================================
CREATE PROCEDURE usp_SetDbVersion
	@version int
AS
BEGIN
	UPDATE ApplicationInfo SET version=@version where id=1
END
GO


