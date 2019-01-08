USE [UCM]
GO

/****** Object:  StoredProcedure [dbo].[sp_FabricSortOperation]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_FabricSortOperation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_FabricSortOperation]
GO

/****** Object:  StoredProcedure [dbo].[sp_FNCompanyGet]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_FNCompanyGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_FNCompanyGet]
GO

/****** Object:  StoredProcedure [dbo].[sp_FNExpenseMasterOperation]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_FNExpenseMasterOperation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_FNExpenseMasterOperation]
GO

/****** Object:  StoredProcedure [dbo].[sp_FNPettyCashOperation]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_FNPettyCashOperation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_FNPettyCashOperation]
GO

/****** Object:  StoredProcedure [dbo].[sp_FNPettyCashReport]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_FNPettyCashReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_FNPettyCashReport]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetNameValue]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetNameValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetNameValue]
GO

/****** Object:  StoredProcedure [dbo].[sp_UserLogin]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UserLogin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_UserLogin]
GO

/****** Object:  StoredProcedure [dbo].[sp_VendorOperation]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_VendorOperation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_VendorOperation]
GO

/****** Object:  StoredProcedure [dbo].[sp_WHCROperation]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_WHCROperation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_WHCROperation]
GO

/****** Object:  StoredProcedure [dbo].[sp_WHDeliveryPlanOperation]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_WHDeliveryPlanOperation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_WHDeliveryPlanOperation]
GO

/****** Object:  StoredProcedure [dbo].[sp_WHFabricStockInHand]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_WHFabricStockInHand]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_WHFabricStockInHand]
GO

/****** Object:  StoredProcedure [dbo].[sp_WHInwardItemsOperation]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_WHInwardItemsOperation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_WHInwardItemsOperation]
GO

/****** Object:  StoredProcedure [dbo].[sp_WHInwardMasterOperation]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_WHInwardMasterOperation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_WHInwardMasterOperation]
GO

/****** Object:  StoredProcedure [dbo].[sp_WHPackingOperation]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_WHPackingOperation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_WHPackingOperation]
GO

/****** Object:  StoredProcedure [dbo].[sp_WHPieceOperation]    Script Date: 01/08/2019 16:05:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_WHPieceOperation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_WHPieceOperation]
GO

USE [UCM]
GO

/****** Object:  StoredProcedure [dbo].[sp_FabricSortOperation]    Script Date: 01/08/2019 16:05:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_FabricSortOperation]
(	
	@Function varchar(20),
	@ID int,
	@Name varchar(100),
	@WarpCount varchar(50),
	@WeftCount varchar(50),
	@Reed varchar(50),
	@Pic varchar(50),
	@Width varchar(50),
	@GSM varchar(50),
	@retVal varchar(50) OUT
)
AS
BEGIN
	IF @Function = 'GET'
	BEGIN
		Select *
			from FabricSort	
			where ID = @ID 

	END IF @Function = 'GETALL'
	BEGIN
		Select *
			from FabricSort	
			Order by WarpCount, WeftCount, Reed, Pic, Width
	END IF @Function = 'ADD'
	BEGIN
		if Exists (Select Top 1 ID
				from FabricSort 
				Where  WarpCount = @WarpCount AND
					WeftCount = @WeftCount AND
					Reed = @Reed AND
					Pic = @Pic AND
					Width = @Width AND 
					Name = @Name AND
					GSM = @GSM)
		BEGIN
			Set @retVal = 'EXIST'
			return
		END
		
		Insert into FabricSort(Name, WarpCount, WeftCount, Reed, Pic, Width, GSM)
		Values (@Name, @WarpCount, @WeftCount, @Reed, @Pic, @Width, @GSM)
		
	END IF @Function = 'UPDATE'
	BEGIN
		if Exists (Select Top 1 ID
				from FabricSort 
				Where  Name = @Name AND
					WarpCount = @WarpCount AND
					WeftCount = @WeftCount AND
					Reed = @Reed AND
					Pic = @Pic AND
					Width = @Width AND 
					GSM = @GSM AND
					ID <> @ID)
		BEGIN
			Set @retVal = 'EXIST'
			return
		END
		
		Update FabricSort
			Set Name = @Name,
				WarpCount = @WarpCount,
				WeftCount = @WeftCount, 
				Reed = @Reed, 
				Pic = @Pic, 
				Width = @Width, 
				GSM = @GSM
			Where ID = @ID
	END
End
GO

/****** Object:  StoredProcedure [dbo].[sp_FNCompanyGet]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_FNCompanyGet] 
AS
BEGIN
	Select *
		from Company
		Order by Company
END

GO

/****** Object:  StoredProcedure [dbo].[sp_FNExpenseMasterOperation]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_FNExpenseMasterOperation]
(	
	@Function varchar(20),
	@ID int,
	@ExpType varchar(50),
	@ExpName varchar(50),
	@retVal varchar(50) OUT
)
AS 
BEGIN
	IF @Function = 'GETBYTYPE'
	BEGIN
		Select * 
			from FNExpenseMaster
			WHERE ExpType = @ExpType
			Order by ExpName
	END IF @Function = 'ADD'
	BEGIN
		IF EXISTS (Select * from FNExpenseMaster Where ExpType = @ExpType AND ExpName = @ExpName)
		BEGIN
			SET @retVal = 'EXIST'
			return 
		END
		
		INSERT INTO FNExpenseMaster (ExpType, ExpName)
		Values (@ExpType, @ExpName)
	END IF @Function = 'GET'
	BEGIN
		Select * 
			from FNExpenseMaster
			WHERE ID = @ID
	END IF @Function = 'UPDATE'
	BEGIN
		IF EXISTS (Select * from FNExpenseMaster Where ExpType = @ExpType AND ExpName = @ExpName AND ID <> @ID)
		BEGIN
			SET @retVal = 'EXIST'
			return 
		END
		UPDATE FNExpenseMaster	
			SET ExpType = @ExpType, 
				ExpName = @ExpName
			WHERE ID = @ID
		
	END
END
GO

/****** Object:  StoredProcedure [dbo].[sp_FNPettyCashOperation]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[sp_FNPettyCashOperation]
(	
	@Function varchar(20),
	@ID int,
	@TranDate date,
	@TranToDate date,
	@CrDr char(1),
	@CompanyID int,
	@ExpHeadID int,
	@ExpNatureID int,
	@Vendor varchar(50),
	@BillDate date,
	@BillNo varchar(50),
	@Remarks varchar(50),
	@Amount float,
	@Approved int
)
AS
BEGIN
	IF @Function = 'ADD'
	BEGIN
		Insert into FNPettyCash(TranDate, CrDr, CompanyID, ExpHeadID, ExpNatureID, Vendor, BillDate, BillNo,
			Remarks, Amount) 
		Values (@TranDate, @CrDr, @CompanyID, @ExpHeadID, @ExpNatureID, @Vendor, @BillDate, @BillNo,
			@Remarks, @Amount) 
	END IF @Function = 'UPDATE'
	BEGIN
		Update FNPettyCash
			Set CompanyID = @CompanyID,
				ExpHeadID = @ExpHeadID,
				ExpNatureID = @ExpNatureID,
				Vendor = @Vendor,
				BillDate = @BillDate,
				BillNo = @BillNo,
				Remarks = @Remarks,
				Amount = @Amount
			Where ID = @ID
	END IF @Function = 'SEARCH'
	BEGIN
		Select pc.ID, TranDate, CrDr, CompanyID, comp.Company, pc.ExpHeadID, pc.ExpNatureID, exphead.ExpName as 'ExpHeadName', 
			expnature.ExpName as 'ExpNatureName', Vendor, BillDate, BillNo, Remarks, Amount, Approved
			from FNPettyCash pc
			inner join Company comp on comp.ID = pc.CompanyID
			left outer join FNExpenseMaster exphead on exphead.ID = pc.ExpHeadID
			left outer join FNExpenseMaster expnature on expnature.ID = pc.ExpNatureID
			Where ( isnull(@TranDate, '01/01/1900') = '01/01/1900' OR pc.TranDate >= @TranDate)
				AND ( isnull(@TranToDate, '01/01/1900') = '01/01/1900' OR pc.TranDate <= @TranToDate)
				AND ( isnull(@CrDr, '') = '' OR pc.CrDr = @CrDr)
				AND ( isnull(@CompanyID, 0) = 0 OR pc.CompanyID = @CompanyID)
				AND ( isnull(@ExpHeadID, 0) = 0 OR pc.ExpHeadID = @ExpHeadID)
				AND ( isnull(@ExpNatureID, 0) = 0 OR pc.ExpNatureID = @ExpNatureID)
				AND ( isnull(@Vendor, 0) = 0 OR pc.Vendor like '%' + LTRIM(RTRIM(@Remarks)) + '%')
				AND ( isnull(@BillNo, 0) = 0 OR pc.BillNo like '%' + LTRIM(RTRIM(@Remarks)) + '%')
				AND ( isnull(@Remarks, 0) = 0 OR pc.Remarks like '%' + LTRIM(RTRIM(@Remarks)) + '%')
				AND ( isnull(@Amount, 0) = 0 OR pc.Amount = @Amount)
			Order by comp.Company, exphead.ExpName, expnature.ExpName, CrDr
	END IF @Function = 'DELETE'
	BEGIN
		Delete From FNPettyCash
			Where ID = @ID
	END IF @Function = 'APPROVE'
	BEGIN
		Update FNPettyCash
			Set Approved = 1
			Where ID = @ID
	END IF @Function = 'GET'
	BEGIN
		Select *
			from FNPettyCash
			where ID = @ID
	END
END
GO

/****** Object:  StoredProcedure [dbo].[sp_FNPettyCashReport]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_FNPettyCashReport]
(	
	@CompanyID int,
	@FromDate date,
	@ToDate date
)
AS
BEGIN
	Select head.ExpName as 'ExpHeadName', nature.ExpName as 'ExpNatureName'
		,SUM(Amount) as 'Total', COUNT(Amount) as 'Nos'
	from FNPettyCash cas
		inner join FNExpenseMaster head on head.ID = cas.ExpHeadID
		inner join FNExpenseMaster nature on nature.ID = cas.ExpNatureID	
	Where cas.CompanyID = @CompanyID	
			AND ( isnull(@FromDate, '01/01/1900') = '01/01/1900' OR cas.TranDate >= @FromDate)
			AND ( isnull(@ToDate, '01/01/1900') = '01/01/1900' OR cas.TranDate <= @ToDate) 
	group by head.ExpName, nature.ExpName
	
	
End
GO

/****** Object:  StoredProcedure [dbo].[sp_GetNameValue]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetNameValue]
(	
	@Entity varchar(50)
)
AS
BEGIN
	Select ID, EntName, EntValue
		from NameValue 
		Where Entity = @Entity
		Order by DispOrder, EntName, EntValue
End

GO

/****** Object:  StoredProcedure [dbo].[sp_UserLogin]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UserLogin]
(	
	@Function varchar(20),
	@ID int,
	@UserName varchar(50),
	@UserPassword varchar(50),
	@Department varchar(50),
	@Roles varchar(100)
)
AS
BEGIN
	IF @Function = 'AUTH'
	BEGIN
		Select *
			from Users	
			where UserName = @UserName and UserPassword = @UserPassword
	END IF @Function = 'CHANGEPWD'
	BEGIN
		Update Users 
			Set UserPassword = @UserPassword
			Where ID = @ID
	END
End
















GO

/****** Object:  StoredProcedure [dbo].[sp_VendorOperation]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE [dbo].[sp_VendorOperation]
(	
	@Function varchar(20),
	@ID int,
	@VenName varchar(100),
	@VenAddress varchar(100),
	@VenCity varchar(50),
	@VenPhone varchar(50),
	@VenTIN varchar(50),
	@VenGST varchar(50),
	@retVal varchar(50) OUT
)
AS
BEGIN
	IF @Function = 'GET'
	BEGIN
		Select *
			from Vendor
			where ID = @ID 

	END IF @Function = 'GETALL'
	BEGIN
		Select *
			from Vendor
			Order by VenName
	END IF @Function = 'ADD'
	BEGIN
		if Exists (Select Top 1 ID
				from Vendor
				Where  VenName = @VenName)
		BEGIN
			Set @retVal = 'EXIST'
			return
		END
		
		Insert into Vendor(VenName, VenAddress, VenCity, VenPhone, VenTIN, VenGST)
		Values (@VenName, @VenAddress, @VenCity, @VenPhone, @VenTIN, @VenGST)
		
	END IF @Function = 'UPDATE'
	BEGIN
		if Exists (Select Top 1 ID
				from Vendor 
				Where VenName = @VenName AND
					ID <> @ID)
		BEGIN
			Set @retVal = 'EXIST'
			return
		END
		
		Update Vendor
			Set VenName = @VenName,
				VenAddress = @VenAddress, 
				VenCity = @VenCity, 
				VenPhone = @VenPhone, 
				VenTIN = @VenTIN, 
				VenGST = @VenGST
			Where ID = @ID
	END
End
GO

/****** Object:  StoredProcedure [dbo].[sp_WHCROperation]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_WHCROperation]
(	
	@Function varchar(20),
	@ID int,
	@WHInwdItemID int,
	@Meter int,
	@DefectID int
)
AS
BEGIN
	IF @Function = 'GET'
	BEGIN
		Select *
			from WHCheckingReport cr
			inner join NameValue nv on cr.DefectID = nv.ID
			Where WHInwdItemID = @WHInwdItemID
			Order by Meter, nv.EntName
	END IF @Function = 'ADD'
	BEGIN
		IF NOT EXISTS (Select ID 
						from WHCheckingReport 
						Where Meter = @Meter AND DefectID = @DefectID
							AND WHInwdItemID = @WHInwdItemID)
		BEGIN
			Insert into WHCheckingReport(WHInwdItemID, Meter, DefectID)
			Values (@WHInwdItemID, @Meter, @DefectID)
		END
		
	END IF @Function = 'DELETE'
	BEGIN
		Delete from WHCheckingReport
			Where ID = @ID
	END 
End


GO

/****** Object:  StoredProcedure [dbo].[sp_WHDeliveryPlanOperation]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[sp_WHDeliveryPlanOperation]
(	
	@Function varchar(20),
	@ID int,
	@DPNo varchar(50),
	@Remarks varchar(100),
	@Approved int,
	@InvoiceID int,
	@retVal varchar(50) OUT
)
AS
BEGIN
	IF @Function = 'ADD'
	BEGIN
		Insert into WHDeliveryPlan(DPNo, Remarks, Approved)
		Values (@DPNo, @Remarks, 0)
		
		Declare @Ident int
		Select @Ident = SCOPE_IDENTITY() 

		Select @retVal = @Ident
	END IF @Function = 'UPDATE'
	BEGIN
		Update WHDeliveryPlan
			Set DPNo = @DPNo, 
				Remarks = @Remarks, 
				Approved = @Approved
			Where ID = @ID
	END IF @Function = 'SEARCH'
	BEGIN
		Select dp.ID, dp.DPNo, dp.Remarks, inv.InvNo, dp.Approved,  
			COUNT(pack.ID) as 'TotalPackages',
			SUM(pc.ActualLength) as 'TotalMeters'
			from WHDeliveryPlan dp
			left outer join WHPacking pack on pack.DeliveryPlanID = dp.ID 
			left outer join WHPiece pc on pc.PackageID = pack.ID
			left outer join Invoice inv on inv.ID = dp.InvoiceID
			Where ( isnull(@ID, 0) = 0 OR dp.ID = @ID)
				AND ( isnull(@DPNo, '') = '' OR ltrim(rtrim(dp.DPNo)) like '%' + @DPNo + '%' )
				AND ( isnull(@Remarks, '') = '' OR ltrim(rtrim(dp.Remarks)) like '%' + @Remarks + '%' )
				AND ( isnull(@InvoiceID, 0) = 0 OR dp.InvoiceID = @InvoiceID)
			Group By dp.ID, dp.DPNo, dp.Remarks, inv.InvNo, dp.Approved
	END IF @Function = 'GET'
	BEGIN
		Select dp.ID, dp.DPNo, dp.Remarks, inv.InvNo, dp.Approved,  
			COUNT(pack.ID) as 'TotalPackages',
			SUM(pc.ActualLength) as 'TotalMeters'
			from WHDeliveryPlan dp
			inner join WHPacking pack on pack.DeliveryPlanID = dp.ID 
			inner join WHPiece pc on pc.PackageID = pack.ID
			left outer join Invoice inv on inv.ID = dp.InvoiceID
			Where dp.ID = @ID
			Group By dp.ID, dp.DPNo, dp.Remarks, inv.InvNo, dp.Approved
	END IF @Function = 'DELETE'
	BEGIN
		Delete from WHPacking
			Where ID = @ID
	END
END


GO

/****** Object:  StoredProcedure [dbo].[sp_WHFabricStockInHand]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[sp_WHFabricStockInHand] 
AS
BEGIN
Select sort.ID, sort.Name, sum(mas.ApproxMtrs) as 'TotalMtrs'
	from FabricSort sort
	inner join WHInwardMaster mas on mas.SortID = sort.ID
	Where mas.ID not in (Select WHInwdID from WHInwardItems where CheckingDate is not null)
	group by sort.ID, sort.Name
	

Select sort.ID, sort.Name, pc.GradeID, grade.EntName as 'Grade',
	Case 
		When ISNULL(pc.PackageID, 0) = 0 Then 'Checked'
		When ISNULL(pc.PackageID, 0) <> 0 AND ISNULL(pack.DeliveryPlanID, 0) = 0 THEN 'Packed'
		When ISNULL(pc.PackageID, 0) <> 0 AND ISNULL(pack.DeliveryPlanID, 0) <> 0 THEN 'PackedReady'
		else 'None'
	End as 'PackStatus',
	SUM(pc.ActualLength) as 'TotalMtrs',
	COUNT(pc.ID) as 'TotalPieces'
	from WHPiece pc
	inner join NameValue grade on grade.ID = pc.GradeID
	inner join WHInwardItems item on item.ID = pc.WHInwdItemID
	inner join WHInwardMaster mas on mas.ID = item.WHInwdID
	inner join FabricSort sort on sort.ID = mas.SortID
	left outer join WHPacking pack on pack.ID = pc.PackageID
	left outer join WHDeliveryPlan dplan on dplan.ID = pack.DeliveryPlanID
	Group By sort.ID, sort.Name, pc.GradeID, grade.EntName,
	Case 
		When ISNULL(pc.PackageID, 0) = 0 Then 'Checked'
		When ISNULL(pc.PackageID, 0) <> 0 AND ISNULL(pack.DeliveryPlanID, 0) = 0 THEN 'Packed'
		When ISNULL(pc.PackageID, 0) <> 0 AND ISNULL(pack.DeliveryPlanID, 0) <> 0 THEN 'PackedReady'
		else 'None'
	End 
	
END


GO

/****** Object:  StoredProcedure [dbo].[sp_WHInwardItemsOperation]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_WHInwardItemsOperation]
(	
	@Function varchar(20),
	@ID int,
	@WHInwdID int,
	@CheckingDate date,
	@Details varchar(50),
	@Remarks varchar(100),
	@Approved int
)
AS
BEGIN
	IF @Function = 'GET'
	BEGIN
		Select item.*, mas.Approved as 'ItemMasterApproved'
			from WHInwardItems item inner join
				WHInwardMaster mas on item.WHInwdID = mas.ID
			Where item.ID = @ID
	END IF @Function = 'GETBYMASTERID'
	BEGIN
		Select item.ID, item.WHInwdID, item.CheckingDate, item.Details, item.Remarks, item.Approved, mas.Approved as 'ItemMasterApproved',
			COALESCE( (Select COUNT(ID) from WHCheckingReport Where WHInwdItemID = item.ID), 0) as 'Defects',
			COALESCE( (Select SUM(ActualLength) from WHPiece Where WHInwdItemID = item.ID), 0) as 'Meters'
			from WHInwardItems item 
			inner join WHInwardMaster mas on item.WHInwdID = mas.ID
			Where item.WHInwdID = @WHInwdID
		END IF @Function = 'ADD'
	BEGIN
		Insert into WHInwardItems(WHInwdID)
		Values (@WHInwdID)
	END IF @Function = 'DELETE'
	BEGIN
		IF NOT EXISTS (Select ID from WHCheckingReport 
						Where WHInwdItemID = @ID 
						Union 
						Select ID from WHPiece 
						Where WHInwdItemID = @ID)
		BEGIN
			Delete from WHInwardItems
				Where ID = @ID
		END
	END IF @Function = 'UPDATE'
	BEGIN
		Update WHInwardItems
			Set Details = @Details,
				CheckingDate = @CheckingDate,
				Remarks = @Remarks,
				Approved = @Approved
			Where ID = @ID
	END
End


GO

/****** Object:  StoredProcedure [dbo].[sp_WHInwardMasterOperation]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_WHInwardMasterOperation]
(	
	@Function varchar(20),
	@ID int,
	@InwdRef varchar(50),
	@InwdDate date,
	@InwdDateTo date,
	@VendorID int,
	@SortID int,
	@Origin int,
	@ApproxMtrs int,
	@NoOfItems int,
	@Approved int,
	@retVal varchar(50) OUT
)
AS
BEGIN
	IF @Function = 'GET'
	BEGIN
		Select inwd.*,
			ven.VenName, nv.EntName as 'OriginName',
			sort.Name,
			(Select COUNT(ID) from WHInwardItems Where WHInwdID = inwd.ID) as 'NoOfItems'
			
			from WHInwardMaster inwd 
			inner join Vendor ven on inwd.VendorID = ven.ID
			inner join FabricSort sort on inwd.SortID = sort.ID
			inner join NameValue nv on inwd.Origin = nv.ID 
			where inwd.ID = @ID 

	END IF @Function = 'GETALL'
	BEGIN
	
		Select inwd.*,
			ven.VenName, nv.EntName as 'OriginName',
			sort.Name,
			(Select COUNT(ID) from WHInwardItems Where WHInwdID = inwd.ID) as 'NoOfItems'
			
			from WHInwardMaster inwd 
			inner join Vendor ven on inwd.VendorID = ven.ID
			inner join FabricSort sort on inwd.SortID = sort.ID
			inner join NameValue nv on inwd.Origin = nv.ID 
			Order by InwdDate desc
	END IF @Function = 'SEARCH'
	BEGIN
		Select inwd.*,
			ven.VenName, nv.EntName as 'OriginName',
			sort.Name,
			(Select COUNT(ID) from WHInwardItems Where WHInwdID = inwd.ID) as 'NoOfItems'
			
			from WHInwardMaster inwd 
			inner join Vendor ven on inwd.VendorID = ven.ID
			inner join FabricSort sort on inwd.SortID = sort.ID
			inner join NameValue nv on inwd.Origin = nv.ID 
			Where ( isnull(@ID, 0) = 0 OR inwd.ID = @ID)
				AND ( isnull(@InwdDate, '01/01/1900') = '01/01/1900' OR inwd.InwdDate >= @InwdDate)
				AND ( isnull(@InwdDateTo, '01/01/1900') = '01/01/1900' OR inwd.InwdDate <= @InwdDateTo)
				AND ( isnull(@InwdRef, '') = '' OR ltrim(rtrim(inwd.InwdRef)) like '%' + @InwdRef + '%' )
				AND ( isnull(@VendorID, 0) = 0 OR inwd.VendorID = @VendorID)
				AND ( isnull(@SortID, 0) = 0 OR inwd.SortID = @SortID)
				AND ( isnull(@Origin, 0) = 0 OR inwd.Origin = @Origin)
			Order by InwdDate desc
	END IF @Function = 'ADD'
	BEGIN
		Insert into WHInwardMaster(InwdRef, InwdDate, VendorID, SortID, Origin, ApproxMtrs, Approved)
		Values (@InwdRef, @InwdDate, @VendorID, @SortID, @Origin, @ApproxMtrs, 0)
		
		Declare @Ident int
		Select @Ident = SCOPE_IDENTITY() 
		
		While (@NoOfItems < 0)
		Begin
			Insert into WHInwardItems (WHInwdID)
			Values (@Ident)
		End
		
	END IF @Function = 'UPDATE'
	BEGIN
		Update WHInwardMaster
			Set InwdRef = @InwdRef, 
				InwdDate = @InwdDate , 
				VendorID = @VendorID , 
				SortID = @SortID , 
				Origin = @Origin , 
				ApproxMtrs = @ApproxMtrs ,
				Approved = @Approved
			Where ID = @ID
	END
End


GO

/****** Object:  StoredProcedure [dbo].[sp_WHPackingOperation]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[sp_WHPackingOperation]
(	
	@Function varchar(20),
	@ID int,
	@PkDetails varchar(50),
	@PkDate date,
	@PkToDate date,
	@TypeID int,
	@Tare int,
	@Approved int,
	@FabricID int,
	@InvoiceID int,
	@DeliveryPlanID int,
	@retVal varchar(50) OUT
)
AS
BEGIN
	IF @Function = 'ADD'
	BEGIN
		Insert into WHPacking(PkDetails, PkDate, TypeID, Tare, Approved)
		Values (@PkDetails, @PkDate, @TypeID, @Tare, 0)
		
		Declare @Ident int
		Select @Ident = SCOPE_IDENTITY() 

		Select @retVal = @Ident
	END IF @Function = 'UPDATE'
	BEGIN
		Update WHPacking
			Set PkDetails = @PkDetails, 
				PkDate = @PkDate, 
				TypeID = @TypeID, 
				Tare = @Tare,
				Approved = @Approved
			Where ID = @ID
	END IF @Function = 'SEARCH'
	BEGIN
		Select pack.ID, pack.PkDate, pack.PkDetails, pack.Tare, nv.EntName as 'PackType', dp.DPNo, inv.InvNo,
			pack.Approved,  
			COUNT(pc.ID) as 'NoOfPieces', sum(pc.ActualLength) as 'TotalMeters'
			from WHPacking pack
			inner join NameValue nv on nv.ID = pack.TypeID
			left outer join WHDeliveryPlan dp on dp.ID = pack.DeliveryPlanID
			left outer join Invoice inv on inv.ID = dp.InvoiceID
			left outer join WHPiece pc on pc.PackageID = pack.ID
			left outer join WHInwardItems items on items.ID = pc.WHInwdItemID
			left outer join WHInwardMaster mas on mas.ID = items.WHInwdID
			left outer join FabricSort sort on sort.ID = mas.SortID
			Where ( isnull(@ID, 0) = 0 OR pack.ID = @ID)
				AND ( isnull(@PkDate, '01/01/1900') = '01/01/1900' OR pack.PkDate >= @PkDate)
				AND ( isnull(@PkToDate, '01/01/1900') = '01/01/1900' OR pack.PkDate <= @PkToDate)
				AND ( isnull(@PkDetails, '') = '' OR ltrim(rtrim(pack.PkDetails)) like '%' + @PkDetails + '%' )
				AND ( isnull(@TypeID, 0) = 0 OR pack.TypeID = @TypeID)
				AND ( isnull(@FabricID, 0) = 0 OR sort.ID = @FabricID)
				AND ( isnull(@DeliveryPlanID, 0) = 0 OR pack.DeliveryPlanID = @DeliveryPlanID)
				AND ( isnull(@InvoiceID, 0) = 0 OR dp.InvoiceID = @InvoiceID)
			Group By pack.ID, pack.PkDate, pack.PkDetails, pack.Tare, nv.EntName, dp.DPNo, inv.InvNo,
			pack.Approved,  
			sort.WarpCount + ' x ' + sort.WeftCount + ' | ' + sort.Reed + ' x ' + sort.Pic + ' | ' + sort.Width + '"'
	END IF @Function = 'GET'
	BEGIN
		Select pack.ID, pack.PkDate, pack.PkDetails, pack.Tare, pack.TypeID, pack.Approved, nv.EntName as 'PackType', 
			dp.DPNo, inv.InvNo,
			COUNT(pc.ID) as 'NoOfPieces', sum(pc.ActualLength) as 'TotalMeters'
			from WHPacking pack
			inner join NameValue nv on nv.ID = pack.TypeID
			left outer join WHDeliveryPlan dp on dp.ID = pack.DeliveryPlanID
			left outer join Invoice inv on inv.ID = dp.InvoiceID
			left outer join WHPiece pc on pc.PackageID = pack.ID
			where pack.ID = @ID
			Group By pack.ID, pack.PkDate, pack.PkDetails, pack.Tare, pack.TypeID, pack.Approved, nv.EntName, dp.DPNo, inv.InvNo
	END IF @Function = 'GETAPPROVED'
	BEGIN
		Select pack.ID, pack.PkDate, pack.PkDetails, pack.Tare, pack.TypeID, pack.Approved, nv.EntName as 'PackType', 
			COUNT(pc.ID) as 'NoOfPieces', sum(pc.ActualLength) as 'TotalMeters'
			from WHPacking pack
			inner join NameValue nv on nv.ID = pack.TypeID
			inner join WHPiece pc on pc.PackageID = pack.ID
			where pack.Approved = 1
				AND ISNULL(pack.DeliveryPlanID, 0) = 0
			Group By pack.ID, pack.PkDate, pack.PkDetails, pack.Tare, pack.TypeID, pack.Approved, nv.EntName
			Order By nv.EntName, pack.PkDate, pack.PkDetails
	END IF @Function = 'GETBYDELIVERYPLAN'
	BEGIN
		Select pack.ID, pack.PkDate, pack.PkDetails, pack.Tare, pack.TypeID, pack.Approved, nv.EntName as 'PackType', 
			COUNT(pc.ID) as 'NoOfPieces', sum(pc.ActualLength) as 'TotalMeters'
			from WHPacking pack
			inner join NameValue nv on nv.ID = pack.TypeID
			inner join WHPiece pc on pc.PackageID = pack.ID
			where pack.Approved = 1
				AND pack.DeliveryPlanID = @DeliveryPlanID
			Group By pack.ID, pack.PkDate, pack.PkDetails, pack.Tare, pack.TypeID, pack.Approved, nv.EntName
			Order By nv.EntName, pack.PkDate, pack.PkDetails
	END IF @Function = 'CLEARDELIVERYPLAN'
	BEGIN
		Update WHPacking
			Set DeliveryPlanID = 0
			Where DeliveryPlanID = @DeliveryPlanID
	END IF @Function = 'UPDATEDELIVERYPLAN'
	BEGIN
		Update WHPacking
			Set DeliveryPlanID = @DeliveryPlanID
			Where ID = @ID	
	END IF @Function = 'DELETE'
	BEGIN
		Delete from WHPacking
			Where ID = @ID
	END
END
GO

/****** Object:  StoredProcedure [dbo].[sp_WHPieceOperation]    Script Date: 01/08/2019 16:05:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_WHPieceOperation]
(	
	@Function varchar(20),
	@ID int,
	@WHInwdItemID int,
	@PieceMark varchar(50),
	@GradeID int,
	@ActualLength int,
	@AdjustedLength int,
	@PackageID int
)
AS
BEGIN
	IF @Function = 'GET'
	BEGIN
		Select pc.*, nv.EntName
			from WHPiece pc
			inner join NameValue nv on pc.GradeID = nv.ID
			Where WHInwdItemID = @WHInwdItemID
			Order by nv.EntName
	END IF @Function = 'GETAPPROVED'
	BEGIN
		Select pc.*, 
			fab.Name,
			nv.EntName as 'Grade'
			from WHPiece pc
			inner join NameValue nv on pc.GradeID = nv.ID
			inner join WHInwardItems item on pc.WHInwdItemID = item.ID
			inner join WHInwardMaster mas on item.WHInwdID = mas.ID
			inner join FabricSort fab on fab.ID = mas.SortID
			Where item.Approved = 1 
				AND ISNULL(pc.PackageID, 0) = 0
			Order by WarpCount, WeftCount, Reed, Pic, Width, nv.EntName, PieceMark, ActualLength desc
	END IF @Function = 'GETBYPACKAGE'
	BEGIN
		Select pc.*, 
			fab.Name,
			nv.EntName as 'Grade'
			from WHPiece pc
			inner join NameValue nv on pc.GradeID = nv.ID
			inner join WHInwardItems item on pc.WHInwdItemID = item.ID
			inner join WHInwardMaster mas on item.WHInwdID = mas.ID
			inner join FabricSort fab on fab.ID = mas.SortID
			Where pc.PackageID = @PackageID
			Order by WarpCount, WeftCount, Reed, Pic, Width, nv.EntName, PieceMark, ActualLength desc
	END IF @Function = 'ADD'
	BEGIN
		Insert into WHPiece(WHInwdItemID, PieceMark, GradeID, ActualLength)
		Values (@WHInwdItemID, @PieceMark, @GradeID, @ActualLength)
	END IF @Function = 'UPDATELENGTH'
	BEGIN
		Update WHPiece
			Set AdjustedLength = @AdjustedLength
			Where ID = @ID
	END IF @Function = 'CLEARPACKAGE'
	BEGIN
		Update WHPiece
			Set PackageID = 0
			Where PackageID = @PackageID
	END IF @Function = 'UPDATEPACKAGE'
	BEGIN
		Update WHPiece
			Set PackageID = @PackageID
			Where ID = @ID
	END IF @Function = 'DELETE'
	BEGIN
		Delete from WHPiece
			Where ID = @ID
	END 
End


GO


