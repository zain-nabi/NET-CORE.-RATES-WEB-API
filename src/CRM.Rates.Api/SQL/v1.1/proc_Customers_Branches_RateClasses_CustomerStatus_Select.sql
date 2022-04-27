USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[proc_Customers_Branches_RateClasses_CustomerStatus_Select]    Script Date: 2021/04/23 2:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	ALTER PROC [dbo].[proc_Customers_Branches_RateClasses_CustomerStatus_Select]
	(
		@WorkFlowStageID	INT	
	)
	AS
	BEGIN
	SELECT TOP (10) 
	   VW.[CustomerID] CustomerID
	  ,VW.[AccountCode] AccountCode
	  ,VW.BranchID
	  ,VW.CustomerStatusID CustomerStatusID
      ,VW.[Name] Name
      ,VW.[LinkedAccount]
      ,VW.[LinkedCustomerID]
      ,VW.[FWSystemAccount]
      ,VW.[InternalAccount]
      ,VW.[LastIncreaseDate]
      ,VW.[NextIncreaseDate]
      ,VW.[Contact]
      ,VW.[TelNo]
      ,VW.[FaxNo]
      ,VW.[Email]
      ,VW.[CellNo]
      ,VW.[AddLine1]
      ,VW.[AddLine2]
      ,VW.[AddLine3]
      ,VW.[AddLine4]
      ,VW.[PostalCode]
      ,VW.[MarkforClosure]
      ,VW.[StatusID]
      ,VW.[CustomerAccountTypeID]
      ,VW.[FWRateAccCode]
      ,VW.[FWRepCode]
      ,VW.[HasUniques]
      ,VW.[Insurance]
      ,VW.[VolRatio]
      ,VW.[Swat]
      ,VW.[Avg]
      ,VW.[Compliance]
	  ,VW.RateClassID RateClassID
      ,VW.[AccountCreatedDate]
      ,VW.[SixMonthCompliance]
      ,VW.[InternalDepartmentID]
      ,VW.[RevisedAnvDate]
      ,VW.[Exclusion]
      ,VW.[hasFuelOnSundries]
      ,VW.[PropAddress1]
      ,VW.[PropAddress2]
      ,VW.[PropAddress3]
      ,VW.[PropAddress4]
      ,VW.[PropTel]
      ,VW.[PropFax]
      ,VW.[PropPostalCode]
      ,VW.[CustomerCommisionTypeID]
      ,VW.[hasFixedPerc]
      ,VW.[FixedPerc]
      ,VW.[PaymentProfileID]
      ,VW.[ExcludeTargetCorrection]
      ,VW.[PODVerify]
      ,VW.[VerticalMarketID]
      ,VW.[SubVerticalMarketID]
      ,VW.[CustomerTradeStatusID]
      ,VW.[SaleTypeID]
      ,VW.[ExcludeFromDeliveries]
      ,VW.[FuelSurchargeClassID]
      ,VW.[FWCreditController]
      ,VW.[CreditControllerEmployeeID]
      ,VW.[Country]
      ,VW.[StatementEmailAdd]
      ,VW.[isDelayed]
      ,VW.[FWPriceCheckQuoteNo]
      ,VW.[SitesLastUpdated]
	  ,VW.BranchID BranchID
	  ,VW.BranchFullName
	  ,VW.RateClassID RateClassID
	  ,VW.RateClassesDescription Description
	  ,VW.RateClassesRateCycleID RateCycleID
	  ,VW.CustomerStatusID CustomerStatusID
	  ,VW.CustomerStatusFWCode FWCode
	  ,VW.CustomerStatusShortDescription ShortDescription
	  ,VW.CustomerStatusColour Colour
	FROM vwCustomers VW
	LEFT OUTER JOIN CustomerAnalysis CA on CA.CustomerID = VW.CustomerID
	LEFT OUTER JOIN RateIncreases RI on RI.RateIncreaseID = CA.RateIncreaseID 
	LEFT OUTER JOIN WorkFlowManager WFM on WFM.RecordID = CA.CustomerAnalysisID
	LEFT OUTER JOIN WorkFlowStages WFS on WFS.WorkFlowID = WFM.WorkFlowID AND WFS.WorkFlowStageID = WFM.StageID
	WHERE WFS.WorkFlowStageID = @WorkFlowStageID AND RI.RateIncreaseID = (SELECT MAX(RateIncreaseID) FROM RateIncreases)
	END


