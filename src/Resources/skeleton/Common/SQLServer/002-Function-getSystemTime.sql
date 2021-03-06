USE [NoChangeParking]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getSystemTime]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[getSystemTime]
GO

-- =============================================
-- Description:	Function to return system date with override for testing
-- Use this in preference to getDate()
-- =============================================
CREATE FUNCTION [dbo].[getSystemTime]()
RETURNS dateTime
AS
BEGIN
	-- RETURN getDate() -- Use this for production
	-- Use the following for development and test, remove for production
	DECLARE @Result dateTime
	SELECT  @Result=fixedSysTime from ApplicationInfo
	RETURN coalesce(@Result,getDate())
END
GO
EXEC usp_SetDbVersion 2
GO

