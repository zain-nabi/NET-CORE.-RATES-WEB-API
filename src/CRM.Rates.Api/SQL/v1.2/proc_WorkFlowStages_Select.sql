USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[proc_WorkFlowStages_Select]    Script Date: 2021/04/23 11:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROC [dbo].[proc_WorkFlowStages_Select]
(
	@DatabaseName		VARCHAR(50),
	@WorkFlowID			INT
)
AS
BEGIN
DECLARE @Sql NVARCHAR(MAX);

 IF(@DatabaseName IS NULL)
 BEGIN
	SET @DatabaseName = 'CRM'
 END

 SET @Sql = N' SELECT 
					WFS.WorkFlowStageID WorkFlowStageID,
					WFS.WorkFlowStage WorkFlowStage,
					WFS.WorkFlowPageName WorkFlowPageName,
					WFS.OrderBy OrderBy,
					WFS.IconName IconName,
					WFS.StageHelp StageHelp,
					WFS.WorkFlowID WorkFlowID
				FROM' + QUOTENAME(@DatabaseName) + N'.dbo.WorkFlowStages WFS WHERE wfs.WorkFlowID =' + CAST(@WorkFlowID AS NVARCHAR(255)) + 
				N'ORDER BY wfs.OrderBy';

 Exec sp_executesql @Sql
END
