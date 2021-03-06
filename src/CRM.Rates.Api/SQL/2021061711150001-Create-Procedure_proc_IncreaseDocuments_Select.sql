USE [CRM]
GO

-- =============================================
-- Author:		Sazi Nyathi
-- Create date: 2021-06-14
-- Description:	SELECT IncreaseDocuments
-- =============================================
ALTER PROCEDURE [dbo].[proc_IncreaseDocuments_Select]  
@CustomerAnalysisID INT
AS
BEGIN
     SELECT ID.DocumentID, ID.AnalysisID, ID.ImgName , ID.DateUploaded , DC.[Description] , WF.WorkflowName  
	 FROM CustomerAnalysis CA  WITH(NOLOCK)
     INNER JOIN IncreaseDocuments ID WITH(NOLOCK) ON ID.AnalysisID = CA.CustomerAnalysisID
     INNER JOIN DocumentCategorys  DC WITH(NOLOCK) ON ID.DocumentCategoryID = DC.DocumentCategoryID
	 INNER JOIN WorkFlows WF ON WF.WorkFlowID  =  DC.WorkFlowID
     
     WHERE  CA.CustomerAnalysisID = @CustomerAnalysisID
     AND  DC.WorkFlowID IN (2,6)

END
               
			   

