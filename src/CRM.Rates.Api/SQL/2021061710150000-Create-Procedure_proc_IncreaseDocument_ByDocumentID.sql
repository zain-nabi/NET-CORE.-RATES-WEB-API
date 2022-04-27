USE [CRM]
GO

-- =============================================
-- Author:		Sazi Nyathi
-- Create date: 2021-06-14
-- Description:	SELECT IncreaseDocuments
-- =============================================
 ALTER PROCEDURE [dbo].[proc_IncreaseDocument_ByDocumentID]  
 @DocumentID INT
 AS
 BEGIN
    
	 SELECT  ID.DocumentID, ID.AnalysisID, ID.ImgName , ID.DateUploaded , ID.ImgData FROM IncreaseDocuments ID WITH(NOLOCK)
     WHERE ID.DocumentID = LTRIM(RTRIM(@DocumentID))
	 
    
 END