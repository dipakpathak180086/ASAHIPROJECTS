USE [SATO_AIS_PRINTING]
GO
/****** Object:  UserDefinedTableType [dbo].[UserDefineTBL_JobDetail]    Script Date: 10/8/2021 17:56:20 ******/
CREATE TYPE [dbo].[UserDefineTBL_JobDetail] AS TABLE(
	[ProductCode] [nvarchar](50) NULL,
	[LotNo] [nvarchar](50) NULL,
	[ManufacturedDate] [nvarchar](50) NULL,
	[SerialNo] [nvarchar](50) NULL,
	[PassStatus] [nvarchar](50) NULL,
	[Line] [nvarchar](20) NULL,
	[FilePath] [nvarchar](500) NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[GetShiftTime]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetShiftTime]
(
	@TimeToCheck TIME(7)
)
RETURNS 
VARCHAR AS 
	BEGIN      
		DECLARE @RetCode varchar(50)
		
		select @RetCode=[ShiftName] from tbShiftMaster where 
		([ShiftFrom]<[ShiftTo] AND @TimeToCheck>=[ShiftFrom] AND @TimeToCheck<=[ShiftTo]) OR 
		([ShiftFrom]>[ShiftTo] AND (@TimeToCheck>=[ShiftFrom] OR @TimeToCheck<=[ShiftTo]))

		if (@RetCode is null)
			set @RetCode ='Z'

			RETURN @RetCode
	END


GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[Split](@String varchar(MAX), @Delimiter char(1))       
returns @temptable TABLE (items varchar(MAX))       
as       
begin      
    declare @idx int       
    declare @slice varchar(8000)       

    select @idx = 1       
        if len(@String)<1 or @String is null  return       

    while @idx!= 0       
    begin       
        set @idx = charindex(@Delimiter,@String)       
        if @idx!=0       
            set @slice = left(@String,@idx - 1)       
        else       
            set @slice = @String       

        if(len(@slice)>0)  
            insert into @temptable(Items) values(@slice)       

        set @String = right(@String,len(@String) - @idx)       
        if len(@String) = 0 break       
    end   
return 
end;

GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitString]  
(  
   @Input NVARCHAR(MAX),  
   @Character CHAR(1)  
)  
RETURNS @Output TABLE (  
   Item NVARCHAR(1000)  
)  
AS  
BEGIN  
DECLARE @StartIndex INT, @EndIndex INT  
SET @StartIndex = 1  
IF SUBSTRING(@Input, LEN(@Input) - 1, LEN(@Input)) <> @Character  
BEGIN  
SET @Input = @Input + @Character  
END  
WHILE CHARINDEX(@Character, @Input) > 0  
BEGIN  
SET @EndIndex = CHARINDEX(@Character, @Input)  
INSERT INTO @Output(Item)  
SELECT SUBSTRING(@Input, @StartIndex, @EndIndex - 1)  
SET @Input = SUBSTRING(@Input, @EndIndex + 1, LEN(@Input))  
END  
RETURN 
END  

GO
/****** Object:  Table [dbo].[TBL_BarcodeIndex]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_BarcodeIndex](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AISPartNo] [varchar](50) NOT NULL,
	[FieldValue] [varchar](50) NOT NULL,
	[FieldName] [varchar](50) NULL,
	[ValueLength] [int] NULL,
	[ValueIndex] [int] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_TBL_BarcodeIndex] PRIMARY KEY CLUSTERED 
(
	[AISPartNo] ASC,
	[FieldValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_BarcodePrinting]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_BarcodePrinting](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PartNo] [varchar](50) NULL,
	[Barcode] [varchar](100) NOT NULL,
	[Qty] [int] NULL,
	[Status] [varchar](10) NULL,
	[Shift] [varchar](1) NULL,
	[PrintedOn] [datetime] NULL,
	[PrintedBy] [varchar](50) NULL,
 CONSTRAINT [PK_TBL_BarcodePrinting] PRIMARY KEY CLUSTERED 
(
	[Barcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_Fields]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Fields](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FieldName] [varchar](50) NOT NULL,
	[InternalPartNo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TBL_Fields_1] PRIMARY KEY CLUSTERED 
(
	[FieldName] ASC,
	[InternalPartNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_GroupMaster]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_GroupMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](20) NOT NULL,
	[Description] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[Sitecode] [varchar](10) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](20) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](20) NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[GroupName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_GroupRight]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_GroupRight](
	[GroupName] [varchar](20) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[View] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](20) NULL,
	[Add] [bit] NULL,
	[Update] [bit] NULL,
	[Delete] [bit] NULL,
 CONSTRAINT [PK_UserTypeRight] PRIMARY KEY CLUSTERED 
(
	[GroupName] ASC,
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_ModuleMaster]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_ModuleMaster](
	[ModuleId] [int] NOT NULL,
	[ModuleName] [varchar](50) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_ScreenMaster] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_Pallet_Mapping]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Pallet_Mapping](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PartNo] [varchar](50) NULL,
	[WorkOrderNo] [varchar](50) NULL,
	[PalletNo] [varchar](50) NOT NULL,
	[Barcode] [varchar](100) NOT NULL,
	[Line_No] [varchar](50) NULL,
	[Shift] [varchar](1) NULL,
	[MappedBy] [varchar](50) NULL,
	[IsComplete] [bit] NULL,
	[MappedOn] [datetime] NULL,
	[CompletedOn] [datetime] NULL,
	[CompletedBy] [varchar](50) NULL,
	[CompletedID] [varchar](20) NULL,
	[PalletQty] [int] NULL,
 CONSTRAINT [PK_TBL_Pallet_Mapping] PRIMARY KEY CLUSTERED 
(
	[PalletNo] ASC,
	[Barcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_PartMaster]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_PartMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Line] [varchar](10) NOT NULL,
	[InternalPartNo] [varchar](50) NOT NULL,
	[InternalPartName] [varchar](100) NULL,
	[CustomerPartNo] [varchar](50) NOT NULL,
	[CustomerPartName] [varchar](100) NULL,
	[VendorCode] [varchar](50) NOT NULL,
	[VendorName] [varchar](50) NULL,
	[PackSize] [int] NOT NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_TBL_PartMaster] PRIMARY KEY CLUSTERED 
(
	[Line] ASC,
	[InternalPartNo] ASC,
	[CustomerPartNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_PartMaster_History]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_PartMaster_History](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InternalPartNo] [varchar](50) NULL,
	[InternalPartName] [varchar](100) NULL,
	[CustomerPartNo] [varchar](50) NULL,
	[CustomerPartName] [varchar](100) NULL,
	[VendorCode] [varchar](50) NULL,
	[VendorName] [varchar](50) NULL,
	[PackSize] [int] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedOn] [datetime] NULL,
	[Status] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_PrinterLine_Mapping]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_PrinterLine_Mapping](
	[PrinterIp] [varchar](50) NOT NULL,
	[Line] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TBL_PrinterLine_Mapping_1] PRIMARY KEY CLUSTERED 
(
	[PrinterIp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_RunningSerial]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_RunningSerial](
	[TranType] [varchar](50) NULL,
	[Year] [int] NULL,
	[Month] [int] NULL,
	[Day] [int] NULL,
	[SerialNo] [int] NULL,
	[Hours] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[LotNo] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_UserMaster]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_UserMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](20) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](10) NOT NULL,
	[GroupName] [varchar](20) NOT NULL,
	[Sitecode] [varchar](20) NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](20) NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [varchar](20) NULL,
 CONSTRAINT [PK_TBL_UserMaster] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_Version]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Version](
	[AppName] [nvarchar](50) NULL,
	[App_Version] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbShiftMaster]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbShiftMaster](
	[ShiftCode] [varchar](5) NOT NULL,
	[ShiftName] [varchar](50) NULL,
	[ShiftFrom] [varchar](50) NULL,
	[ShiftTo] [varchar](50) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TBL_BarcodeIndex] ON 
GO
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (15, N'FG.TCB.LFH.GCG2120000', N'00282967100101', N'CustomerPart', 14, 0, N'admin', CAST(N'2021-09-16T12:31:26.600' AS DateTime), N'admin', CAST(N'2021-09-16T14:52:20.617' AS DateTime))
GO
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (18, N'FG.TCB.LFH.GCG2120000', N'A65688', N'VendorCode', 6, 3, N'admin', CAST(N'2021-09-21T19:03:42.710' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (14, N'FG.TCB.LFH.GCG2120000', N'K0', N'RevisionNo', 2, 2, N'admin', CAST(N'2021-09-16T12:29:35.687' AS DateTime), N'admin', CAST(N'2021-09-16T14:52:20.660' AS DateTime))
GO
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (16, N'FG.TCB.LFH.GCG2120000', N'MMyy', N'MfgFormat', 4, 1, N'admin', CAST(N'2021-09-16T12:32:13.523' AS DateTime), N'admin', CAST(N'2021-09-16T14:52:20.660' AS DateTime))
GO
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (9, N'FG.TCF.LFH.GCG2120000', N'00552367100101', N'CustomerPart', 14, 0, N'admin', CAST(N'2021-09-13T15:08:23.793' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (13, N'FG.TCF.LFH.GCG2120000', N'A65688', N'VendorCode', 6, 3, N'admin', CAST(N'2021-09-13T15:08:23.890' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (19, N'FG.TCF.LFH.GCG2120000', N'K0', N'RevisionNo', 2, 2, N'admin', CAST(N'2021-09-21T19:05:19.467' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (10, N'FG.TCF.LFH.GCG2120000', N'MMyy', N'MfgFormat', 4, 1, N'admin', CAST(N'2021-09-13T15:08:23.840' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[TBL_BarcodeIndex] OFF
GO
SET IDENTITY_INSERT [dbo].[TBL_BarcodePrinting] ON 
GO
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (216, N'FG.TCB.LFH.GCG2120000', N'002829671001011021K0A65688000025', 1, N'1', N'B', CAST(N'2021-10-08T17:46:08.247' AS DateTime), N'AEP')
GO
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (217, N'FG.TCF.LFH.GCG2120000', N'005523671001011021K0A65688000008', 2, N'1', N'B', CAST(N'2021-10-08T17:46:27.167' AS DateTime), N'AEP')
GO
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (218, N'FG.TCF.LFH.GCG2120000', N'005523671001011021K0A65688000009', 1, N'1', N'B', CAST(N'2021-10-08T17:46:51.710' AS DateTime), N'AEP')
GO
SET IDENTITY_INSERT [dbo].[TBL_BarcodePrinting] OFF
GO
SET IDENTITY_INSERT [dbo].[TBL_Fields] ON 
GO
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (1, N'CustomerPart', N'FG.TCB.LFH.GCG2120000')
GO
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (6, N'CustomerPart', N'FG.TCF.LFH.GCG2120000')
GO
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (4, N'MfgFormat', N'FG.TCB.LFH.GCG2120000')
GO
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (7, N'MfgFormat', N'FG.TCF.LFH.GCG2120000')
GO
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (3, N'RevisionNo', N'FG.TCB.LFH.GCG2120000')
GO
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (8, N'RevisionNo', N'FG.TCF.LFH.GCG2120000')
GO
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (2, N'VendorCode', N'FG.TCB.LFH.GCG2120000')
GO
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (9, N'VendorCode', N'FG.TCF.LFH.GCG2120000')
GO
SET IDENTITY_INSERT [dbo].[TBL_Fields] OFF
GO
SET IDENTITY_INSERT [dbo].[TBL_GroupMaster] ON 
GO
INSERT [dbo].[TBL_GroupMaster] ([ID], [GroupName], [Description], [IsActive], [Sitecode], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Admin', NULL, 1, NULL, CAST(N'2020-03-02T09:51:44.613' AS DateTime), N'admin', NULL, NULL)
GO
INSERT [dbo].[TBL_GroupMaster] ([ID], [GroupName], [Description], [IsActive], [Sitecode], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'User', NULL, 1, NULL, CAST(N'2020-03-02T09:52:33.250' AS DateTime), N'admin', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[TBL_GroupMaster] OFF
GO
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 101, 1, CAST(N'2020-10-21T15:38:24.397' AS DateTime), N'1', 1, 1, 1)
GO
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 102, 1, CAST(N'2020-10-21T15:38:24.407' AS DateTime), N'1', 1, 1, 1)
GO
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 103, 1, CAST(N'2020-10-21T15:38:24.400' AS DateTime), N'1', 1, NULL, NULL)
GO
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 104, 1, CAST(N'2020-10-21T15:38:24.400' AS DateTime), N'1', NULL, NULL, NULL)
GO
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 105, 1, CAST(N'2020-10-21T15:38:24.400' AS DateTime), N'1', NULL, NULL, NULL)
GO
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 106, 1, CAST(N'2021-06-23T12:32:51.023' AS DateTime), N'1', 0, 0, 0)
GO
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 201, 1, CAST(N'2021-06-23T12:32:51.023' AS DateTime), N'1', NULL, NULL, NULL)
GO
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 202, 1, CAST(N'2021-06-23T12:32:51.023' AS DateTime), N'1', NULL, NULL, NULL)
GO
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (101, N'Group Master', 1)
GO
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (102, N'User Master', 1)
GO
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (103, N'Part Master', 1)
GO
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (104, N'Report Label Printing', 1)
GO
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (105, N'Report Pallet Mapping', 1)
GO
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (106, N'Barcode Generation', 1)
GO
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (201, N'Pallet Mapping', 1)
GO
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (202, N'Status', 1)
GO
SET IDENTITY_INSERT [dbo].[TBL_PartMaster] ON 
GO
INSERT [dbo].[TBL_PartMaster] ([ID], [Line], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1, N'2', N'FG.TCB.LFH.GCG2120000', N'Windshield', N'00282967100101', NULL, N'A65688', N'TATA', 10, N'admin', CAST(N'2021-09-08T09:22:47.450' AS DateTime), N'admin', CAST(N'2021-09-08T12:19:51.510' AS DateTime))
GO
INSERT [dbo].[TBL_PartMaster] ([ID], [Line], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (5, N'2', N'FG.TCF.LFH.GCG2120000', N'Windshield', N'00552367100101', NULL, N'A65688', N'TATA', 10, N'admin', CAST(N'2021-09-08T12:31:34.230' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[TBL_PartMaster] ([ID], [Line], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (11, N'3', N'FG.ASK.LFH.GCG2120000', N'Windshield', N'00552367100101', NULL, N'A65688', N'ASK', 10, N'admin', CAST(N'2021-09-08T12:31:34.230' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[TBL_PartMaster] OFF
GO
INSERT [dbo].[TBL_PrinterLine_Mapping] ([PrinterIp], [Line]) VALUES (N'10.91.2.8', N'2')
GO
INSERT [dbo].[TBL_PrinterLine_Mapping] ([PrinterIp], [Line]) VALUES (N'10.91.2.84', N'1')
GO
INSERT [dbo].[TBL_PrinterLine_Mapping] ([PrinterIp], [Line]) VALUES (N'192.168.1.103', N'3')
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCB.LFH.GCG2120000', 2021, 9, 14, 7, NULL, NULL, NULL)
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCB.LFH.GCG2120000', 2021, 9, 15, 10, NULL, NULL, NULL)
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCB.LFH.GCG2120000', 2021, 9, 16, 64, NULL, NULL, NULL)
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCF.LFH.GCG2120000', 2021, 9, 16, 69, NULL, NULL, NULL)
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'1', 2021, 9, 16, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'nil', 2021, 9, 16, 7, NULL, NULL, NULL)
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'RANDOMNO', 2021, 9, 17, 3, NULL, NULL, NULL)
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'Interna001', 2021, 9, 24, 1, 18, CAST(N'2021-09-24T18:22:46.623' AS DateTime), N'LotNo')
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCB.LFH.GCG2120000', 2021, 9, 24, 25, 17, CAST(N'2021-10-08T17:46:08.243' AS DateTime), N'LotNo')
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'ABCDFEFG', 2021, 10, 1, 17, 15, CAST(N'2021-10-01T15:48:40.820' AS DateTime), N'LotNo')
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'1', 2021, 10, 1, 1, 16, CAST(N'2021-10-01T16:23:45.630' AS DateTime), N'LotNo')
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'nil', 2021, 10, 8, 23, 17, CAST(N'2021-10-08T17:21:46.163' AS DateTime), N'LotNo')
GO
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCF.LFH.GCG2120000', 2021, 10, 8, 9, 17, CAST(N'2021-10-08T17:46:51.707' AS DateTime), N'LotNo')
GO
SET IDENTITY_INSERT [dbo].[TBL_UserMaster] ON 
GO
INSERT [dbo].[TBL_UserMaster] ([ID], [UserID], [UserName], [Password], [GroupName], [Sitecode], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'2', N'2', N'2', N'User', NULL, 1, CAST(N'2021-03-02T16:36:46.630' AS DateTime), N'1', NULL, NULL)
GO
INSERT [dbo].[TBL_UserMaster] ([ID], [UserID], [UserName], [Password], [GroupName], [Sitecode], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, N'aa', N'aa', N'a', N'User', NULL, 0, CAST(N'2021-06-23T12:34:24.010' AS DateTime), N'1', CAST(N'2021-06-23T12:36:02.630' AS DateTime), NULL)
GO
INSERT [dbo].[TBL_UserMaster] ([ID], [UserID], [UserName], [Password], [GroupName], [Sitecode], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (4, N'aads', N'aa', N'12', N'Admin', NULL, 0, CAST(N'2021-07-07T10:29:50.140' AS DateTime), N'1', CAST(N'2021-07-07T10:29:56.323' AS DateTime), NULL)
GO
INSERT [dbo].[TBL_UserMaster] ([ID], [UserID], [UserName], [Password], [GroupName], [Sitecode], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Admin', N'1', N'1', N'Admin', NULL, 1, CAST(N'2020-03-02T09:53:02.023' AS DateTime), N'admin', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[TBL_UserMaster] OFF
GO
INSERT [dbo].[TBL_Version] ([AppName], [App_Version]) VALUES (N'PC', N'1.0.0.0')
GO
INSERT [dbo].[TBL_Version] ([AppName], [App_Version]) VALUES (N'Device', N'1.0.0.0')
GO
INSERT [dbo].[tbShiftMaster] ([ShiftCode], [ShiftName], [ShiftFrom], [ShiftTo]) VALUES (N'1', N'A', N'08:00:00', N'16:30:59')
GO
INSERT [dbo].[tbShiftMaster] ([ShiftCode], [ShiftName], [ShiftFrom], [ShiftTo]) VALUES (N'2', N'B', N'16:31:00', N'00:00:59')
GO
INSERT [dbo].[tbShiftMaster] ([ShiftCode], [ShiftName], [ShiftFrom], [ShiftTo]) VALUES (N'3', N'C', N'00:01:00', N'07:59:59')
GO
ALTER TABLE [dbo].[TBL_GroupRight] ADD  CONSTRAINT [DF_TBL_GroupRight_Add]  DEFAULT ((0)) FOR [Add]
GO
ALTER TABLE [dbo].[TBL_GroupRight] ADD  CONSTRAINT [DF_TBL_GroupRight_Update]  DEFAULT ((0)) FOR [Update]
GO
ALTER TABLE [dbo].[TBL_GroupRight] ADD  CONSTRAINT [DF_TBL_GroupRight_Delete]  DEFAULT ((0)) FOR [Delete]
GO
ALTER TABLE [dbo].[TBL_ModuleMaster] ADD  CONSTRAINT [DF_ModuleMaster_Active]  DEFAULT ((1)) FOR [Active]
GO
/****** Object:  StoredProcedure [dbo].[PRC_AEP_PRINTNG]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PRC_AEP_PRINTNG]
@TYPE VARCHAR(100)=NULL,
@PART_NO VARCHAR(100)=NULL,
@QTY NUMERIC(18,0)=NULL,
@CREATED_BY VARCHAR(100)=NULL,
@PRINTER_IP VARCHAR(100)=NULL
AS
BEGIN
    DECLARE @BARCODE VARCHAR(100)=NULL,
	        @SERIAL_NO VARCHAR(100)=NULL,
		    @SHIFT VARCHAR(10)=NULL,
			@LINE VARCHAR(100)=NULL
    IF @TYPE='BIND_MODEL'
	  BEGIN
	     --Commented by dipak 08_10_21 for line adding req
	     --SELECT distinct  AISPartNo as InternalPartNo FROM TBL_BarcodeIndex
		   SELECT @LINE=Line FROM TBL_PrinterLine_Mapping WHERE PrinterIp=@PRINTER_IP

		  SELECT distinct InternalPartNo FROM TBL_PartMaster MST JOIN TBL_BarcodeIndex DTL ON MST.InternalPartNo =DTL.AISPartNo
		  WHERE Line=@LINE
	  END
	IF @TYPE='SAVE'
	  BEGIN
	       SET @SERIAL_NO=''
		   SET @BARCODE=''
	       EXECUTE PRC_GetBarcode @PART_NO,@BARCODE output;
		    SET @SHIFT=(SELECT dbo.GetShiftTime(convert(TIME(7),getdate())))
		INSERT INTO [dbo].[TBL_BarcodePrinting]
				([PartNo]
				,[Barcode]
				,[Qty]
				,[Status]
				,[Shift]
				,[PrintedBy]
				,[PrintedOn])
			VALUES
				(@PART_NO
				,@BARCODE
				,@QTY
				,1
				,@SHIFT
				,@CREATED_BY
				,GETDATE())
			SELECT 'Y' As RESULT,@BARCODE As BARCODE
	  END
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_AEP_PRINTNG_OLD_08_10_21_ADD_LINE]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[PRC_AEP_PRINTNG_OLD_08_10_21_ADD_LINE]
@TYPE VARCHAR(100)=NULL,
@PART_NO VARCHAR(100)=NULL,
@QTY NUMERIC(18,0)=NULL,
@CREATED_BY VARCHAR(100)=NULL
AS
BEGIN
    DECLARE @BARCODE VARCHAR(100)=NULL,
	        @SERIAL_NO VARCHAR(100)=NULL,
		    @SHIFT VARCHAR(10)=NULL
    IF @TYPE='BIND_MODEL'
	  BEGIN
	     SELECT distinct  AISPartNo as InternalPartNo FROM TBL_BarcodeIndex
	  END
	IF @TYPE='SAVE'
	  BEGIN
	       SET @SERIAL_NO=''
		   SET @BARCODE=''
	       EXECUTE PRC_GetBarcode @PART_NO,@BARCODE output;
		    SET @SHIFT=(SELECT dbo.GetShiftTime(convert(TIME(7),getdate())))
		INSERT INTO [dbo].[TBL_BarcodePrinting]
				([PartNo]
				,[Barcode]
				,[Qty]
				,[Status]
				,[Shift]
				,[PrintedBy]
				,[PrintedOn])
			VALUES
				(@PART_NO
				,@BARCODE
				,@QTY
				,1
				,@SHIFT
				,@CREATED_BY
				,GETDATE())
			SELECT 'Y' As RESULT,@BARCODE As BARCODE
	  END
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_BarcodeGenerationIndex]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_BarcodeGenerationIndex]
	@Type varchar(100),
	@AISPartNo varchar(50) = NULL,
	@FieldValue varchar(50) = NULL,
	@FieldName varchar(50) = NULL,
	@ValueLength int = NULL,
	@ValueIndex int=null,
	@CreatedBy varchar(20) = NULL

AS
DECLARE @PickHID int=0
BEGIN
	if(@Type='SELECT')
	  begin
		SELECT DISTINCT CAST('FALSE' AS BIT) AS IsValid,
			AISPartNo
		FROM TBL_BarcodeIndex
		ORDER BY AISPartNo
	  end
   
   if(@Type='GETINDEXDETAILS')
	  begin
		SELECT 
		    AISPartNo as AIS_PartNo,
			FieldValue ,FieldName ,ValueLength,ValueIndex
		FROM TBL_BarcodeIndex
		where AISPartNo=@AISPartNo order by ValueIndex 
	  end

 if(@Type='GETPARTNO')
	  begin
		SELECT 
		    distinct InternalPartNo
		FROM TBL_PartMaster
  end
  if(@Type='GETFIELDS')
	  begin
		SELECT 
		    distinct FieldName
		FROM TBL_Fields where InternalPartNo=@AISPartNo
  end
   if(@Type='CustomerPart')
	  begin
		SELECT 
		    CustomerPartNo
		FROM TBL_PartMaster where InternalPartNo=@AISPartNo
   end
   if(@Type='VendorCode')
	  begin
		SELECT 
		    VendorCode
		FROM TBL_PartMaster where InternalPartNo=@AISPartNo
   end

	if(@Type='INSERT')
	    BEGIN  
		  INSERT INTO TBL_BarcodeIndex (AISPartNo, FieldValue,FieldName,ValueLength,ValueIndex,CreatedBy,CreatedOn)
		  VALUES
			   (@AISPartNo,@FieldValue,@FieldName,@ValueLength,@ValueIndex,@CreatedBy,getdate())
			    Select 'Y' AS RESULT
		END
	  
 if(@Type='UPDATE')
	  begin
    IF(Exists(SELECT 1 FROM  TBL_BarcodeIndex  WHERE  AISPartNo = @AISPartNo and FieldName=@FieldName))
      BEGIN
	     UPDATE TBL_BarcodeIndex SET
		 FieldValue = @FieldValue,
		 ValueLength=@ValueLength,
		 ValueIndex=@ValueIndex,
		 ModifiedBy = @CreatedBy,
		 ModifiedOn = getdate()
		 where AISPartNo = @AISPartNo and FieldName=@FieldName
		 Select 'Y' AS RESULT
		 END
		else
		  begin
			INSERT INTO TBL_BarcodeIndex (AISPartNo, FieldValue,FieldName,ValueLength,ValueIndex,CreatedBy,CreatedOn)
		  VALUES
			   (@AISPartNo,@FieldValue,@FieldName,@ValueLength,@ValueIndex,@CreatedBy,getdate())
			    Select 'Y' AS RESULT
		 end
    END

  if(@Type='CHECKDUPLICATEPART')
	  begin
		SELECT 
		   top 1 AISPartNo
		FROM TBL_BarcodeIndex where AISPartNo = @AISPartNo and FieldName=@FieldName
   end

    if(@Type='CHECKDUPFIELD')
	  begin
	    IF(Exists(SELECT 1 FROM  TBL_BarcodeIndex  WHERE  AISPartNo = @AISPartNo and ValueIndex=@ValueIndex))
		 begin
		  Select 'N' As Result,'Index No ('+cast(@ValueIndex as varchar(10))+') already exist against AIS Part No ('+@AISPartNo+')' as Msg
		 end
		 else if (Exists(SELECT 1 FROM  TBL_BarcodeIndex  WHERE  AISPartNo = @AISPartNo and FieldName=@FieldName))
		 begin
		  Select 'N' As Result,'FieldName ('+@FieldName+') already exist against AIS Part No ('+@AISPartNo+')' as Msg
		 end
		 else
		 begin
		  Select 'Y' AS RESULT
		 end
   end

   if(@Type='CHECKDUPINDEX')
	  begin
	    IF(Exists(SELECT 1 FROM  TBL_BarcodeIndex  WHERE  AISPartNo = @AISPartNo and ValueIndex=@ValueIndex))
		 begin
		  Select 'N' As Result,'Index No ('+cast(@ValueIndex as varchar(10))+') already exist against AIS Part No ('+@AISPartNo+')' as Msg
		 end
		 else
		 begin
		  Select 'Y' AS RESULT
		 end
   end

 if(@Type='DELETE')
	  begin
	    IF(Exists(SELECT 1 FROM  TBL_BarcodeIndex  WHERE  AISPartNo = @AISPartNo and FieldValue=@FieldValue and FieldName=@FieldName))
	    begin
			DELETE TBL_BarcodeIndex
			WHERE  AISPartNo = @AISPartNo and FieldName=@FieldName and FieldValue=@FieldValue
			Select 'Y' AS RESULT
		end
		else
		begin
		  Select 'N' As Result,'AISPart No ('+@AISPartNo+') dose not exist' as Msg
		end
END
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_BIND_COMBO]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PRC_BIND_COMBO]
@TYPE NVARCHAR(100)
AS
BEGIN
 DECLARE @QUERY NVARCHAR(MAX)=NULL;
   IF @TYPE='SELECT_GROUP'
     BEGIN
        SET @QUERY='SELECT GroupName FROM TBl_GroupMaster Order By GroupName'
		 EXEC(@QUERY)
	 END
   IF @TYPE='BIND_PART'
     BEGIN
	    SET @QUERY='SELECT PART_NO,PART_NO FROM TBL_PART_MASTER'
		EXEC(@QUERY)
	 END
   IF @TYPE='BIND_OPERATOR'
     BEGIN
	    SET @QUERY='SELECT UserName,UserName FROM TBL_UserMaster'
		EXEC(@QUERY)
	 END
   IF @TYPE='BIND_CUSTOMER'
     BEGIN
	    SET @QUERY='SELECT CUST_CODE,CUST_NAME FROM TBL_CUSTOMER_MASTER'
		EXEC(@QUERY)
	 END
	
END




GO
/****** Object:  StoredProcedure [dbo].[PRC_GET_VERSION]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[PRC_GET_VERSION]
@APP_NAME NVARCHAR(10),
@APP_VERSION NVARCHAR(100)
AS
BEGIN
   DECLARE @DB_APP_VERSION NVARCHAR(100)=NULL
   IF NOT EXISTS(SELECT App_Version FROM TBL_Version WHERE  AppName=@APP_NAME AND App_Version=@APP_VERSION )
     BEGIN
	    SELECT @DB_APP_VERSION =MAX(App_Version) FROM TBL_Version WHERE  AppName=@APP_NAME 
		
	    SELECT 'New application version('+cast( @DB_APP_VERSION as varchar)+') and your application version is ('+cast(@APP_VERSION as varchar)+'). Kindly update with new version!!!!!!'  ;
	 END
   else
     SELECT 'OK'
END





GO
/****** Object:  StoredProcedure [dbo].[PRC_GetBarcode]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetBarcode]
    @AISPartNo varchar(50) = NULL,
	@RESULT VARCHAR(MAX) OUTPUT
AS
DECLARE @MaxCount int
DECLARE @Count int
DECLARE @Barcode VARCHAR(100),@RunningSerialNo varchar(20)='',@MFD varchar(20)='',@MFDValue varchar(20)=''
--update TBL_BarcodeIndex set FieldValue='MMyy' where FieldName='MfgFormat'
BEGIN
		        SET @Count = 0
				SET @Barcode = ''
				SET @MaxCount = (SELECT max(ValueIndex) from TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo)
				WHILE @Count<=@MaxCount
				 BEGIN
					IF @Barcode!=''
						SET @Barcode=@Barcode + (SELECT FieldValue FROM TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo and ValueIndex=@Count)
					ELSE
						SET @Barcode=(SELECT FieldValue FROM TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo and ValueIndex=@Count)
					SET @Count=@Count+1
				END
				-- EXECUTE PRC_GetRunningSerial @AISPartNo,@RunningSerialNo output;

				SET @MFD=(select FieldValue from TBL_BarcodeIndex where AISPartNo=@AISPartNo and FieldName='MfgFormat')

				 EXECUTE PRC_GetRunningSerialLotWise @AISPartNo,'LotNo',@RunningSerialNo output
				 SET @Barcode=@Barcode+@RunningSerialNo
				 

				 IF (@MFD='ddMMyy')
				 BEGIN
				   SET @MFDValue=(select FORMAT(GETDATE(),'ddMMyy'))
				 END
				 ELSE IF (@MFD='yy')
				 BEGIN
				   SET @MFDValue=(select FORMAT(GETDATE(),'yy'))
				 END
				 ELSE
				 BEGIN
				  SET @MFDValue=(select FORMAT(GETDATE(),'MMyy'))
				 END

				 SET @Barcode=(SELECT REPLACE(@Barcode ,@MFD,@MFDValue))

				 SET @RESULT=Upper((@Barcode))
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetBarcode_old]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetBarcode_old]
    @AISPartNo varchar(50) = NULL,
	@RESULT VARCHAR(MAX) OUTPUT
AS
DECLARE @MaxCount int
DECLARE @Count int
DECLARE @Barcode VARCHAR(100),@RunningSerialNo varchar(20)=''
BEGIN

		        SET @Count = 0
				SET @Barcode = ''
				SET @MaxCount = (SELECT max(ValueIndex) from TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo)
				WHILE @Count<=@MaxCount
				 BEGIN
					IF @Barcode!=''
						SET @Barcode=@Barcode + (SELECT FieldValue FROM TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo and ValueIndex=@Count)
					ELSE
						SET @Barcode=(SELECT FieldValue FROM TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo and ValueIndex=@Count)
					SET @Count=@Count+1
				END
				 EXECUTE PRC_GetRunningSerial @AISPartNo,@RunningSerialNo output;
				 SET @Barcode=@Barcode+@RunningSerialNo
				 set @RESULT=Upper((@Barcode))
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetRunningSerial]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetRunningSerial]
                @TrnType VARCHAR(50),
				@RESULT VARCHAR(MAX) OUTPUT
                
AS
BEGIN
                DECLARE @SERIAL AS BIGINT
                DECLARE @RESPONSE AS VARCHAR(max)
                DECLARE @YEAR AS INT
                DECLARE @MONTH AS INT
                DECLARE @DAY AS INT
               
BEGIN TRAN
 
                SELECT @YEAR=ISNULL([YEAR],0),@MONTH=ISNULL([MONTH],0),@DAY=ISNULL([DAY],0) FROM TBL_RunningSerial
                WHERE [YEAR]=RIGHT(YEAR(GETDATE()),4) AND [MONTH]=MONTH(GETDATE()) AND [DAY]=RIGHT('00'+CAST(DAY(GETDATE()) AS VARCHAR(2)),2)
                AND TranType=@TrnType
 
                IF (@@ERROR <> 0) GOTO PROBLEM     
                IF @YEAR>0
                                BEGIN
                                IF @MONTH>0
                                                BEGIN  
                                                                UPDATE TBL_RunningSerial
                                                                SET @SERIAL = SERIALNo + 1,
                                                                SERIALNo = SERIALNo + 1
                                                                WHERE TranType=@TrnType
                                                                AND [YEAR]=@YEAR
                                                                AND [MONTH]=@MONTH          
                                                                AND [DAY]=@DAY
                                                                IF (@@ERROR <> 0) GOTO PROBLEM     
                                                END      
                                ELSE
                                                BEGIN
                                                                INSERT INTO TBL_RunningSerial
                                                                ([YEAR],[MONTH],[DAY],TRANTYPE,SERIALNo)
                                                                VALUES
                                                                (RIGHT(YEAR(GETDATE()),4),MONTH(GETDATE()),DAY(GETDATE()),@TrnType,1)
                                                                SET @SERIAL = 1
                                                                SET @YEAR=RIGHT(YEAR(GETDATE()),2)
                                                                SET @MONTH=MONTH(GETDATE())      
                                                                SET @DAY=RIGHT('00'+CAST(DAY(GETDATE()) AS VARCHAR(2)),2)
                                                                IF (@@ERROR <> 0) GOTO PROBLEM     
                                                END
                                END
                ELSE
                                BEGIN
                                                INSERT INTO TBL_RunningSerial
                                                ([YEAR],[MONTH],[DAY],TRANTYPE,SERIALNo)
                                                VALUES
                                                (RIGHT(YEAR(GETDATE()),4),MONTH(GETDATE()),DAY(GETDATE()),@TrnType,1)
                                                SET @SERIAL = 1
                                                SET @YEAR=RIGHT(YEAR(GETDATE()),2)
                                                SET @MONTH=MONTH(GETDATE())      
                                                SET @DAY=RIGHT('00'+CAST(DAY(GETDATE()) AS VARCHAR(2)),2)
                                                IF (@@ERROR <> 0) GOTO PROBLEM
                                END
                                               
   IF @TrnType=@TrnType
         BEGIN
           --SET @RESPONSE=  RIGHT('00'+CAST(@DAY AS VARCHAR(2)),2)+RIGHT('00'+CAST(@MONTH AS VARCHAR(2)),2)+RIGHT('00'+CAST(@YEAR AS VARCHAR(4)),4) +'~'+ RIGHT('0000'+CAST(@SERIAL AS VARCHAR(4)),4)
		   SET @RESPONSE=  RIGHT('00000'+CAST(@SERIAL AS VARCHAR(5)),5)
         END
   
                   
COMMIT TRAN
set @RESULT=Upper((@RESPONSE))
PROBLEM:
                IF (@@ERROR <> 0)
                BEGIN
                                ROLLBACK TRAN
                END
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_GetRunningSerialLotWise]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetRunningSerialLotWise] --'FG_PRINT'
                @TrnType VARCHAR(50),
				@LotNo varchar (50),
				@SERIAL_NO NVARCHAR(100) OUTPUT
                
AS
BEGIN
                DECLARE @SERIAL AS BIGINT
                DECLARE @RESPONSE AS VARCHAR(max)
                DECLARE @YEAR AS INT
                DECLARE @MONTH AS INT
                DECLARE @DAY AS INT
				DECLARE @HOURS AS INT
				DECLARE @Count AS INT
               
BEGIN TRAN
   
                IF EXISTS(SELECT 1 FROM TBL_RunningSerial WHERE TranType=@TrnType and LotNo=@LotNo )
                  BEGIN
					  begin

					   UPDATE TBL_RunningSerial
                        SET @SERIAL = SERIALNo + 1,
                        SERIALNo = SERIALNo + 1,
						[Hours]=DATEPART(hh,GETDATE()),
						UpdateDate=getdate()
                        WHERE TranType=@TrnType
						and LotNo=@LotNo
					  end
                        IF (@@ERROR <> 0) GOTO PROBLEM     
                    END
				ELSE
				  BEGIN
				      INSERT INTO TBL_RunningSerial
						([YEAR],[MONTH],[DAY],TRANTYPE,SERIALNo,[Hours],LotNo,UpdateDate)
						VALUES
						(RIGHT(YEAR(GETDATE()),4),MONTH(GETDATE()),DAY(GETDATE()),@TrnType,1,DATEPART(hh,GETDATE()),@LotNo,getdate())
						SET @SERIAL = 1
						SET @YEAR=RIGHT(YEAR(GETDATE()),2)
						SET @MONTH=MONTH(GETDATE())      
						SET @DAY=RIGHT('000'+CAST(DAY(GETDATE()) AS VARCHAR(3)),3)
						IF (@@ERROR <> 0) GOTO PROBLEM     
				  END      
                 
           SET @RESPONSE= RIGHT('000000'+CAST(@SERIAL AS VARCHAR(6)),6)
         
   
                   
COMMIT TRAN
SET @SERIAL_NO=@RESPONSE
--SELECT @SERIAL_NO
PROBLEM:
                IF (@@ERROR <> 0)
                BEGIN
                                ROLLBACK TRAN
                END
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_GroupMaster]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[PRC_GroupMaster] 
@Type varchar(100),
@GroupName varchar(20)=null,
@ModuleId int=null,
@CreatedBy varchar(20)=null,
@View bit=0,
@Add bit=0,
@Update bit=0,
@Delete bit=0
	AS
	Begin
	 DECLARE @Query nvarchar(max)=null;
	if(@Type='SELECT')
	  begin
		Select GroupName,CreatedBy,CONVERT(varchar(10),CreatedOn,103) CreatedOn
			  from TBL_GroupMaster where IsActive='True' Order By GroupName
		Select ModuleId,ModuleName From TBL_ModuleMaster Where Active = 'True' Order By ModuleId
	  end
	else if(@Type='SELECTBYID')
	  begin
			Select ModuleId,[View],[Add],[Update],[Delete] From TBL_GroupRight Where GroupName = @GroupName
	  end	  
	else if(@Type='INSERT')
	  begin
	 
	    Insert Into TBL_GroupMaster (GroupName,IsActive,CreatedBy,CreatedOn)
	    values(@GroupName,'True',@CreatedBy,GETDATE())
	    
		Select 'SUCCESS'
		end
	  else if(@Type='INSERT_GROUP_RIGHT')
	  begin
	    INSERT INTO [dbo].[TBL_GroupRight]
           ([GroupName]
           ,[ModuleId]
		   ,[View]
		   ,[Add]
		   ,[Update]
		   ,[Delete]
           ,[CreatedOn]
           ,[CreatedBy]) VALUES(@GroupName,@ModuleId,@View,@Add,@Update,@Delete,GETDATE(),@CreatedBy)
		 Select 'SUCCESS'
	  end
	else if(@Type='DELETE_GROUP_RIGHT')
	  begin
	    Delete From TBL_GroupRight Where GroupName = @GroupName
		 Select 'SUCCESS'
	  end

	   else if(@Type='GET_USER_RIGHTS')
		  begin
		        set @Query='
				Select ModuleId From TBL_GroupRight'
				if @GroupName !='ADMIN'
				  begin
				    set @Query=@Query+' Where GroupName = '''+@GroupName+''''
				  end
				exec(@Query)
		  end		

	else if(@Type='DELETE')
	  begin
		  Begin Try
			Begin Tran
				 update TBL_GroupMaster set IsActive='false'
				 WHERE GroupName=@GroupName

				 delete from TBL_GroupRight  Where GroupName = @GroupName

				 Select 'SUCCESS'
			Commit Tran
		End Try
		Begin Catch
			Rollback Tran
			Select 'DB Error-'+  ERROR_MESSAGE()
		End Catch
	  end
	
  End













GO
/****** Object:  StoredProcedure [dbo].[PRC_Pallet_Mapping]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PRC_Pallet_Mapping]
@TYPE VARCHAR(100)=NULL,
@PartNo VARCHAR(100)=NULL,
@PalletNo VARCHAR(100)=NULL,
@Barcode VARCHAR(100)=NULL,
@Line_No VARCHAR(100)=NULL,
@WorkOrderNo VARCHAR(100)=NULL,
@CREATED_BY VARCHAR(100)=NULL
AS
BEGIN
DECLARE @COUNT_PUT INT=NULL,@GET_TOTAL_QTY INT=0,@GET_SCAN_QTY INT=0,@PartName varchar (100),@Shift varchar(50)=''
DECLARE @SACNCOUNT varchar(10)=NULL, @NEXDATE DATE,@PalletQty INT=0,@CurrentDate VARCHAR (20),@RANDOMNO VARCHAR (20),
@RunningSerialNo VARCHAR (50),@PALLETCOUNT VARCHAR(10),@MappedOn varchar (50)='',@MappedPallet varchar (50)='',@MappedPart varchar (50)=''
    IF @TYPE='BIND_PARTNO'
BEGIN
  SELECT DISTINCT AISPartNo FROM TBL_BarcodeIndex ORDER BY AISPartNo
END

IF @TYPE='BIND_VIEW'
BEGIN
  IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE PartNo=@PartNo and isnull(IsComplete,'False')='False')
  BEGIN
	 SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
	 SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
	 select PalletNo,CASE WHEN @PALLETCOUNT=0 THEN @SACNCOUNT ELSE @PALLETCOUNT END PackSize,count(Barcode) TotalScanned
	 from TBL_Pallet_Mapping where PartNo=@PartNo and isnull(IsComplete,'False')='False' group by PartNo,PalletNo
 END
 ELSE
 BEGIN
    SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
    SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
	select '' PalletNo,@SACNCOUNT PackSize,0 TotalScanned
    from TBL_PartMaster where InternalPartNo=@PartNo
 END
END

IF @TYPE='BIND_TOTAL_AND_SCAN_QTY'
BEGIN
  IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE PartNo=@PartNo and isnull(IsComplete,'False')='False')
  BEGIN
    SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
    SET @PartName=(SELECT top 1 InternalPartName FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
    SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
    select TOP 1 CASE WHEN @PALLETCOUNT=0 THEN @SACNCOUNT ELSE @PALLETCOUNT END PackSize,count(Barcode) TotalScanned,@PartName from TBL_Pallet_Mapping where PartNo=@PartNo AND isnull(IsComplete,'False')='False' group by PartNo,PalletNo
  END
  ELSE
  BEGIN
    SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
    SET @PartName=(SELECT top 1 InternalPartName FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
    SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
    select @SACNCOUNT PackSize,0 TotalScanned,@PartName from TBL_PartMaster where InternalPartNo=@PartNo
  END
END

IF @TYPE='VALIDATEPALLET'
BEGIN
  IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo and isnull(IsComplete,'False')='False')
  BEGIN
   SELECT TOP  1 @NEXDATE=DATEADD(day,+1,CONVERT(DATE, CompletedOn)) FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo AND isnull(IsComplete,'False')='TRUE' ORDER BY CompletedOn DESC
	IF (@NEXDATE>(SELECT CONVERT(DATE, GETDATE())))
	BEGIN
	  SELECT 'PALLET SHOULD NOT BE USED TILL DATE ('+ CAST(@NEXDATE AS VARCHAR(20))+')' AS RESULT,'' MSG
	  RETURN;
	END
	ELSE
	BEGIN
	  SET @MappedPart=(SELECT TOP 1 PartNo FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo AND isnull(IsComplete,'False')='False')
	  IF (@PartNo=@MappedPart)
	   BEGIN
	    SELECT 'EXISTS' AS RESULT,'' MSG
	    RETURN;
	   END
	  ELSE
	   BEGIN
	    SELECT 'PALLET ('+@PalletNo+') IN A TRACTION WITH AIS PART ('+@MappedPart+') ' AS RESULT,'' MSG
	    RETURN;
	  END
	END
 END
 ELSE
  BEGIN
    SELECT TOP  1 @NEXDATE=DATEADD(day,+1,CONVERT(DATE, CompletedOn)) FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo AND isnull(IsComplete,'False')='TRUE' ORDER BY CompletedOn DESC
	IF (@NEXDATE>(SELECT CONVERT(DATE, GETDATE())))
	BEGIN
	  SELECT 'PALLET SHOULD NOT BE USED TILL DATE ('+ CAST(@NEXDATE AS VARCHAR(20))+')' AS RESULT,'' MSG
	  RETURN;
	END
 ELSE
  BEGIN
  SELECT 'NOTEXISTS' AS RESULT,'' MSG
  RETURN;
 END
END
END

IF @TYPE='SAVE'
 BEGIN
  IF NOT EXISTS(SELECT 1 FROM TBL_BarcodePrinting WHERE Barcode=@BARCODE and PartNo =@PartNo)
   BEGIN
   SELECT 'INVALID BARCODE SCANNED ('+@BARCODE+') AGAINST PART NO ('+@PartNo+')' AS RESULT,'' MSG
  RETURN;
END
IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE Barcode=@BARCODE)
   BEGIN
   SET @MappedOn=(SELECT MappedOn FROM TBL_Pallet_Mapping WHERE Barcode=@BARCODE)
   SET @MappedPallet=(SELECT PalletNo FROM TBL_Pallet_Mapping WHERE Barcode=@BARCODE)
   SELECT 'BARCODE ('+@BARCODE+') ALREADY MAPPED WITH PALLET ('+@MappedPallet+') ON ('+@MappedOn+')' AS RESULT,'' MSG
  RETURN;
END
  SELECT TOP  1 @NEXDATE=DATEADD(day,+1,CONVERT(DATE, CompletedOn)) FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo AND isnull(IsComplete,'False')='TRUE' ORDER BY CompletedOn DESC
	IF (@NEXDATE>(SELECT CONVERT(DATE, GETDATE())))
	BEGIN
	SELECT 'PALLET SHOULD NOT BE USED TILL DATE ('+ CAST(@NEXDATE AS VARCHAR(20))+')' AS RESULT,'' MSG
	RETURN;
END
ELSE
BEGIN
   SET @Shift=(SELECT dbo.GetShiftTime(convert(TIME(7),getdate())))
   SET @PalletQty=(SELECT top 1 isnull(Packsize,0) FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
   SET @GET_TOTAL_QTY=(SELECT top 1 isnull(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False')
	INSERT INTO [dbo].[TBL_Pallet_Mapping]
	  (PartNo
	  ,WorkOrderNo
	  ,PalletNo
	  ,Barcode
	  ,Line_No
	  ,[Shift]
	  ,PalletQty
	  ,MappedOn
	  ,MappedBy
	  )
	VALUES
	  (@PartNo
	  ,@WorkOrderNo
	  ,@PalletNo
	  ,@Barcode
	  ,@Line_No
	  ,@Shift
	  ,(case when ISNULL(@GET_TOTAL_QTY,0)=0 then @PalletQty else @GET_TOTAL_QTY end)
	  ,GETDATE()
	  ,@CREATED_BY)
	END

	UPDATE TBL_BarcodePrinting SET [Status]='2' WHERE Barcode=@Barcode

	SET @GET_SCAN_QTY=(SELECT Count(*) from TBL_Pallet_Mapping where PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False')
	SET @GET_TOTAL_QTY=(SELECT top 1 isnull(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False')

  IF @GET_TOTAL_QTY=@GET_SCAN_QTY
   BEGIN
       --SET @CurrentDate=(SELECT REPLACE(CONVERT(CHAR(10), GETDATE(), 103), '/', ''));
      -- EXECUTE PRC_GetRunningSerial 'RANDOMNO',@RunningSerialNo output;
       SET @RANDOMNO=(SELECT Format(GETDATE(),'ddMMyyHHmmss')) --@CurrentDate+@RunningSerialNo;
       UPDATE TBL_Pallet_Mapping SET IsComplete='true',CompletedID=@RANDOMNO,CompletedOn=getdate(),CompletedBy=@CREATED_BY WHERE PartNo=@PartNo  AND PalletNo=@PalletNo
       SELECT 'PALLETCOMPLETED' AS RESULT,'Pallet Completed' MSG
       RETURN;
  END
    SELECT @COUNT_PUT= Count(*) from TBL_Pallet_Mapping where CONVERT(VARCHAR,MappedOn,103) =CONVERT(VARCHAR,getdate(),103)and PartNo=@PartNo AND PalletNo=@PalletNo
    SELECT 'Y' AS RESULT,'MAPPED SUCESSFULLY!! - COUNT : '+CAST(@COUNT_PUT AS VARCHAR(20)) AS MSG
END

 IF @TYPE='PALLET_COMP'
    BEGIN
	 IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE PartNo=@PartNo  AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False')
     BEGIN
     SET @RANDOMNO=(SELECT Format(GETDATE(),'ddMMyyHHmmss'))
     UPDATE TBL_Pallet_Mapping SET IsComplete='true',CompletedID=@RANDOMNO,CompletedOn=getdate(),CompletedBy=@CREATED_BY WHERE PartNo=@PartNo  AND PalletNo=@PalletNo
    SELECT 'PALLETCOMPLETED' AS RESULT,'Pallet Completed' MSG
	END
	ELSE
	BEGIN
	 SELECT 'INVALID PALLET SCANNED' AS RESULT,'INVALID PALLET SCANNED' MSG
	END
  END
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_Pallet_Mapping_200921]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PRC_Pallet_Mapping_200921]
@TYPE VARCHAR(100)=NULL,
@PartNo VARCHAR(100)=NULL,
@PalletNo VARCHAR(100)=NULL,
@Barcode VARCHAR(100)=NULL,
@Line_No VARCHAR(100)=NULL,
@WorkOrderNo VARCHAR(100)=NULL,
@CREATED_BY VARCHAR(100)=NULL
AS
BEGIN
DECLARE @COUNT_PUT INT=NULL,@GET_TOTAL_QTY INT=0,@GET_SCAN_QTY INT=0,@PartName varchar (100),@Shift varchar(50)=''
DECLARE @SACNCOUNT varchar(10)=NULL, @NEXDATE DATE,@PalletQty INT=0,@CurrentDate VARCHAR (20),@RANDOMNO VARCHAR (20),
@RunningSerialNo VARCHAR (50),@PALLETCOUNT VARCHAR(10)
    IF @TYPE='BIND_PARTNO'
	 BEGIN
	   SELECT DISTINCT AISPartNo FROM TBL_BarcodeIndex ORDER BY AISPartNo
	 END

	IF @TYPE='BIND_VIEW'
	 BEGIN
	   IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE PartNo=@PartNo and isnull(IsComplete,'False')='False')
	   BEGIN
		  SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo) 
		  SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
		  select PalletNo,CASE WHEN @PALLETCOUNT=0 THEN @SACNCOUNT ELSE @PALLETCOUNT END PackSize,count(Barcode) TotalScanned 
		  from TBL_Pallet_Mapping where PartNo=@PartNo and isnull(IsComplete,'False')='False' group by PartNo,PalletNo
	  END
	  ELSE
	  BEGIN
	      SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo) 
		  SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
		  select '' PalletNo,@SACNCOUNT PackSize,0 TotalScanned 
		  from TBL_PartMaster where InternalPartNo=@PartNo 
	  END
	END

	IF @TYPE='BIND_TOTAL_AND_SCAN_QTY'
	 BEGIN
	   IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE PartNo=@PartNo and isnull(IsComplete,'False')='False')
	   BEGIN
	     SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo) 
	     SET @PartName=(SELECT top 1 InternalPartName FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
		 SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
	     select TOP 1 CASE WHEN @PALLETCOUNT=0 THEN @SACNCOUNT ELSE @PALLETCOUNT END PackSize,count(Barcode) TotalScanned,@PartName from TBL_Pallet_Mapping where PartNo=@PartNo AND isnull(IsComplete,'False')='False' group by PartNo,PalletNo
	   END
	   ELSE
	   BEGIN
	     SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo) 
	     SET @PartName=(SELECT top 1 InternalPartName FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
		 SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
	     select @SACNCOUNT PackSize,0 TotalScanned,@PartName from TBL_PartMaster where InternalPartNo=@PartNo
	   END
	END

	IF @TYPE='VALIDATEPALLET'
	 BEGIN
	   IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo and isnull(IsComplete,'False')='False')
		   BEGIN
		    SELECT TOP  1 @NEXDATE=DATEADD(day,+1,CONVERT(DATE, CompletedOn)) FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo AND isnull(IsComplete,'False')='TRUE' ORDER BY CompletedOn DESC
			IF (@NEXDATE>(SELECT CONVERT(DATE, GETDATE())))
			BEGIN
			   SELECT 'PALLET SHOULD NOT BE USED TILL DATE ('+ CAST(@NEXDATE AS VARCHAR(20))+')' AS RESULT,'' MSG
			   RETURN;
			END
			ELSE
			BEGIN
			   SELECT 'EXISTS' AS RESULT,'' MSG
			   RETURN;
			END
		END
		ELSE
		BEGIN
		   SELECT TOP  1 @NEXDATE=DATEADD(day,+1,CONVERT(DATE, CompletedOn)) FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo AND isnull(IsComplete,'False')='TRUE' ORDER BY CompletedOn DESC
			IF (@NEXDATE>(SELECT CONVERT(DATE, GETDATE())))
			BEGIN
			   SELECT 'PALLET SHOULD NOT BE USED TILL DATE ('+ CAST(@NEXDATE AS VARCHAR(20))+')' AS RESULT,'' MSG
			   RETURN;
			END
			ELSE
			BEGIN
			   SELECT 'NOTEXISTS' AS RESULT,'' MSG
			   RETURN;
			END
		  
		 RETURN;
		END
	 END

    IF @TYPE='SAVE'
	  BEGIN
		   IF NOT EXISTS(SELECT 1 FROM TBL_BarcodePrinting WHERE Barcode=@BARCODE and PartNo =@PartNo)
		    BEGIN
			   SELECT 'INVALID BARCODE SCANNED ('+@BARCODE+') AGAINST PART NO ('+@PartNo+')' AS RESULT,'' MSG
			   RETURN;
			END
			IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE Barcode=@BARCODE)
		    BEGIN
			   SELECT 'BARCODE ('+@BARCODE+') ALREADY MAPPED !!!!' AS RESULT,'' MSG
			   RETURN;
			END
			SELECT TOP  1 @NEXDATE=DATEADD(day,+1,CONVERT(DATE, CompletedOn)) FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo AND isnull(IsComplete,'False')='TRUE' ORDER BY CompletedOn DESC
			IF (@NEXDATE>(SELECT CONVERT(DATE, GETDATE())))
			BEGIN
			 SELECT 'PALLET SHOULD NOT BE USED TILL DATE ('+ CAST(@NEXDATE AS VARCHAR(20))+')' AS RESULT,'' MSG
			 RETURN;
			END
			ELSE
			BEGIN
			        SET @Shift=(SELECT dbo.GetShiftTime(convert(TIME(7),getdate())))
				    SET @PalletQty=(SELECT top 1 isnull(Packsize,0) FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
					SET @GET_TOTAL_QTY=(SELECT top 1 isnull(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False') 
					
					INSERT INTO [dbo].[TBL_Pallet_Mapping]
						   (PartNo
						   ,WorkOrderNo
						   ,PalletNo
						   ,Barcode
						   ,Line_No
						   ,[Shift]
						   ,PalletQty
						   ,MappedOn
						   ,MappedBy
						   )
					 VALUES
						   (@PartNo
						   ,@WorkOrderNo
						   ,@PalletNo
						   ,@Barcode
						   ,@Line_No
						   ,@Shift
						   ,(case when ISNULL(@GET_TOTAL_QTY,0)=0 then @PalletQty else @GET_TOTAL_QTY end)
						   ,GETDATE()
						   ,@CREATED_BY)
			END

			 UPDATE TBL_BarcodePrinting SET [Status]='2' WHERE Barcode=@Barcode

			 SET @GET_SCAN_QTY=(SELECT Count(*) from TBL_Pallet_Mapping where PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False')
			 SET @GET_TOTAL_QTY=(SELECT top 1 isnull(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False') 

			 IF @GET_TOTAL_QTY=@GET_SCAN_QTY
			  BEGIN
			     SET @CurrentDate=(SELECT REPLACE(CONVERT(CHAR(10), GETDATE(), 103), '/', ''));
	             EXECUTE PRC_GetRunningSerial 'RANDOMNO',@RunningSerialNo output;
		         SET @RANDOMNO= @CurrentDate+@RunningSerialNo; 
				 UPDATE TBL_Pallet_Mapping SET IsComplete='true',CompletedID=@RANDOMNO,CompletedOn=getdate(),CompletedBy=@CREATED_BY WHERE PartNo=@PartNo  AND PalletNo=@PalletNo
			     SELECT 'PALLETCOMPLETED' AS RESULT,'Pallet Completed' MSG
				 RETURN;
			  END

			SELECT @COUNT_PUT= Count(*) from TBL_Pallet_Mapping where CONVERT(VARCHAR,MappedOn,103) =CONVERT(VARCHAR,getdate(),103)and PartNo=@PartNo AND PalletNo=@PalletNo

			SELECT 'Y' AS RESULT,'MAPPED SUCESSFULLY!! - COUNT : '+CAST(@COUNT_PUT AS VARCHAR(20)) AS MSG
	  END

 IF @TYPE='PALLET_COMP'
     BEGIN
	     SET @CurrentDate=(SELECT REPLACE(CONVERT(CHAR(10), GETDATE(), 103), '/', ''));
	     EXECUTE PRC_GetRunningSerial 'RANDOMNO',@RunningSerialNo output;
		 SET @RANDOMNO= @CurrentDate+@RunningSerialNo; 
	      UPDATE TBL_Pallet_Mapping SET IsComplete='true',CompletedID=@RANDOMNO,CompletedOn=getdate(),CompletedBy=@CREATED_BY WHERE PartNo=@PartNo  AND PalletNo=@PalletNo
		  SELECT 'PALLETCOMPLETED' AS RESULT,'Pallet Completed' MSG
	 END
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_Pallet_Mapping_old]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[PRC_Pallet_Mapping_old]
@TYPE VARCHAR(100)=NULL,
@PartNo VARCHAR(100)=NULL,
@PalletNo VARCHAR(100)=NULL,
@Barcode VARCHAR(100)=NULL,
@Line_No VARCHAR(100)=NULL,
@WorkOrderNo VARCHAR(100)=NULL,
@CREATED_BY VARCHAR(100)=NULL
AS
BEGIN
DECLARE @COUNT_PUT INT=NULL,@GET_TOTAL_QTY INT=0,@GET_SCAN_QTY INT=0,@PartName varchar (100),@Shift varchar(50)=''
DECLARE @SACNCOUNT varchar(10)=NULL, @NEXDATE DATE,@PalletQty INT=0,@CurrentDate VARCHAR (20),@RANDOMNO VARCHAR (20),
@RunningSerialNo VARCHAR (50),@PALLETCOUNT VARCHAR(10)
    IF @TYPE='BIND_PARTNO'
	 BEGIN
	   SELECT DISTINCT AISPartNo FROM TBL_BarcodeIndex ORDER BY AISPartNo
	 END

	IF @TYPE='BIND_VIEW'
	 BEGIN
	   IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE PartNo=@PartNo and isnull(IsComplete,'False')='False')
	   BEGIN
		  SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo) 
		  SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
		  select PalletNo,CASE WHEN @PALLETCOUNT=0 THEN @SACNCOUNT ELSE @PALLETCOUNT END PackSize,count(Barcode) TotalScanned 
		  from TBL_Pallet_Mapping where PartNo=@PartNo and isnull(IsComplete,'False')='False' group by PartNo,PalletNo
	  END
	  ELSE
	  BEGIN
	      SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo) 
		  SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
		  select '' PalletNo,@SACNCOUNT PackSize,0 TotalScanned 
		  from TBL_PartMaster where InternalPartNo=@PartNo 
	  END
	END

	IF @TYPE='BIND_TOTAL_AND_SCAN_QTY'
	 BEGIN
	   IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE PartNo=@PartNo and isnull(IsComplete,'False')='False')
	   BEGIN
	     SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo) 
	     SET @PartName=(SELECT top 1 InternalPartName FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
		 SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
	     select TOP 1 CASE WHEN @PALLETCOUNT=0 THEN @SACNCOUNT ELSE @PALLETCOUNT END PackSize,count(Barcode) TotalScanned,@PartName from TBL_Pallet_Mapping where PartNo=@PartNo AND isnull(IsComplete,'False')='False' group by PartNo,PalletNo
	   END
	   ELSE
	   BEGIN
	     SET @SACNCOUNT=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo) 
	     SET @PartName=(SELECT top 1 InternalPartName FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
		 SET @PALLETCOUNT=(SELECT top 1 ISNULL(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo)
	     select @SACNCOUNT PackSize,0 TotalScanned,@PartName from TBL_PartMaster where InternalPartNo=@PartNo
	   END
	END

	IF @TYPE='VALIDATEPALLET'
	 BEGIN
	   IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo and isnull(IsComplete,'False')='False')
		   BEGIN
		    SELECT TOP  1 @NEXDATE=DATEADD(day,+1,CONVERT(DATE, CompletedOn)) FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo AND isnull(IsComplete,'False')='TRUE' ORDER BY CompletedOn DESC
			IF (@NEXDATE>(SELECT CONVERT(DATE, GETDATE())))
			BEGIN
			   SELECT 'PALLET SHOULD NOT BE USED TILL DATE ('+ CAST(@NEXDATE AS VARCHAR(20))+')' AS RESULT,'' MSG
			   RETURN;
			END
			ELSE
			BEGIN
			   SELECT 'EXISTS' AS RESULT,'' MSG
			   RETURN;
			END
		END
		ELSE
		BEGIN
		   SELECT TOP  1 @NEXDATE=DATEADD(day,+1,CONVERT(DATE, CompletedOn)) FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo AND isnull(IsComplete,'False')='TRUE' ORDER BY CompletedOn DESC
			IF (@NEXDATE>(SELECT CONVERT(DATE, GETDATE())))
			BEGIN
			   SELECT 'PALLET SHOULD NOT BE USED TILL DATE ('+ CAST(@NEXDATE AS VARCHAR(20))+')' AS RESULT,'' MSG
			   RETURN;
			END
			ELSE
			BEGIN
			   SELECT 'NOTEXISTS' AS RESULT,'' MSG
			   RETURN;
			END
		  
		 RETURN;
		END
	 END

    IF @TYPE='SAVE'
	  BEGIN
		   IF NOT EXISTS(SELECT 1 FROM TBL_BarcodePrinting WHERE Barcode=@BARCODE and PartNo =@PartNo)
		    BEGIN
			   SELECT 'INVALID BARCODE SCANNED ('+@BARCODE+') AGAINST PART NO ('+@PartNo+')' AS RESULT,'' MSG
			   RETURN;
			END
			IF EXISTS(SELECT 1 FROM TBL_Pallet_Mapping WHERE Barcode=@BARCODE)
		    BEGIN
			   SELECT 'BARCODE ('+@BARCODE+') ALREADY MAPPED !!!!' AS RESULT,'' MSG
			   RETURN;
			END
			SELECT TOP  1 @NEXDATE=DATEADD(day,+1,CONVERT(DATE, CompletedOn)) FROM TBL_Pallet_Mapping WHERE PalletNo=@PalletNo AND isnull(IsComplete,'False')='TRUE' ORDER BY CompletedOn DESC
			IF (@NEXDATE>(SELECT CONVERT(DATE, GETDATE())))
			BEGIN
			 SELECT 'PALLET SHOULD NOT BE USED TILL DATE ('+ CAST(@NEXDATE AS VARCHAR(20))+')' AS RESULT,'' MSG
			 RETURN;
			END
			ELSE
			BEGIN
			        SET @Shift=(SELECT dbo.GetShiftTime(convert(TIME(7),getdate())))
				    SET @PalletQty=(SELECT top 1 isnull(Packsize,0) FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
					SET @GET_TOTAL_QTY=(SELECT top 1 isnull(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False') 
					
					INSERT INTO [dbo].[TBL_Pallet_Mapping]
						   (PartNo
						   ,WorkOrderNo
						   ,PalletNo
						   ,Barcode
						   ,Line_No
						   ,[Shift]
						   ,PalletQty
						   ,MappedOn
						   ,MappedBy
						   )
					 VALUES
						   (@PartNo
						   ,@WorkOrderNo
						   ,@PalletNo
						   ,@Barcode
						   ,@Line_No
						   ,@Shift
						   ,(case when ISNULL(@GET_TOTAL_QTY,0)=0 then @PalletQty else @GET_TOTAL_QTY end)
						   ,GETDATE()
						   ,@CREATED_BY)
			END

			 UPDATE TBL_BarcodePrinting SET [Status]='2' WHERE Barcode=@Barcode

			 SET @GET_SCAN_QTY=(SELECT Count(*) from TBL_Pallet_Mapping where PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False')
			 SET @GET_TOTAL_QTY=(SELECT top 1 isnull(PalletQty,0) FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False') 

			 IF @GET_TOTAL_QTY=@GET_SCAN_QTY
			  BEGIN
			     SET @CurrentDate=(SELECT REPLACE(CONVERT(CHAR(10), GETDATE(), 103), '/', ''));
	             EXECUTE PRC_GetRunningSerial 'RANDOMNO',@RunningSerialNo output;
		         SET @RANDOMNO= @CurrentDate+@RunningSerialNo; 
				 UPDATE TBL_Pallet_Mapping SET IsComplete='true',CompletedID=@RANDOMNO,CompletedOn=getdate(),CompletedBy=@CREATED_BY WHERE PartNo=@PartNo  AND PalletNo=@PalletNo
			     SELECT 'PALLETCOMPLETED' AS RESULT,'Pallet Completed' MSG
				 RETURN;
			  END

			SELECT @COUNT_PUT= Count(*) from TBL_Pallet_Mapping where CONVERT(VARCHAR,MappedOn,103) =CONVERT(VARCHAR,getdate(),103)and PartNo=@PartNo AND PalletNo=@PalletNo

			SELECT 'Y' AS RESULT,'MAPPED SUCESSFULLY!! - COUNT : '+CAST(@COUNT_PUT AS VARCHAR(20)) AS MSG
	  END

 IF @TYPE='PALLET_COMP'
     BEGIN
	     SET @CurrentDate=(SELECT REPLACE(CONVERT(CHAR(10), GETDATE(), 103), '/', ''));
	     EXECUTE PRC_GetRunningSerial 'RANDOMNO',@RunningSerialNo output;
		 SET @RANDOMNO= @CurrentDate+@RunningSerialNo; 
	      UPDATE TBL_Pallet_Mapping SET IsComplete='true',CompletedID=@RANDOMNO,CompletedOn=getdate(),CompletedBy=@CREATED_BY WHERE PartNo=@PartNo  AND PalletNo=@PalletNo
		  SELECT 'PALLETCOMPLETED' AS RESULT,'Pallet Completed' MSG
	 END
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_PartMaster]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[PRC_PartMaster]
@TYPE VARCHAR(100),
@InternalPartNo	varchar(50),
@InternalPartName	varchar(100)=NULL,
@CustomerPartNo	varchar(50)	=NULL,
@CustomerPartName	varchar(100)=null,
@VendorCode	varchar(50)	=NULL,
@VendorName	varchar(50)	=NULL,
@PackSize	int	=NULL,
@CreatedBy	varchar(20)	=NULL

AS 
BEGIN
  BEGIN TRY
    IF @TYPE='SELECT'
	  BEGIN
	     SELECT InternalPartNo,
			InternalPartName,
			CustomerPartNo,
			VendorCode,
			PackSize,
			CreatedBy,
			CreatedOn
			from TBL_PartMaster
	  END
  
    IF @TYPE='INSERT'
	  BEGIN
	      IF EXISTS(SELECT 1 FROM TBL_PartMaster WHERE InternalPartNo=@InternalPartNo and CustomerPartNo=@CustomerPartName)
		   BEGIN
		      SELECT 'THIS PART NO ALREADY EXISTS !!!' AS RESULT
			  RETURN;
		   END
	      INSERT INTO TBL_PartMaster
           (InternalPartNo,
			InternalPartName,
			CustomerPartNo,
			CustomerPartName,
			VendorCode,
			VendorName,
			PackSize,
			CreatedBy,
			CreatedOn)
         VALUES
           (@InternalPartNo,
			@InternalPartName,
			@CustomerPartNo,
			@CustomerPartName,
			@VendorCode,
			@VendorName,
			@PackSize,
			@CreatedBy,
			Getdate())
		   Select 'Y' AS RESULT
	  END
	IF @TYPE='UPDATE'
	  BEGIN
	  	    INSERT INTO TBL_PartMaster_History
           (InternalPartNo,
			InternalPartName,
			CustomerPartNo,
			CustomerPartName,
			VendorCode,
			VendorName,
			PackSize,
			CreatedBy,
			CreatedOn
			,Status)
           (select InternalPartNo,
			InternalPartName,
			CustomerPartNo,
			CustomerPartName,
			VendorCode,
			VendorName,
			PackSize,
			@CreatedBy,
			getdate(),
		    'Update' from TBL_PartMaster where InternalPartNo=@InternalPartNo and CustomerPartNo=@CustomerPartNo)
	      
			 UPDATE TBL_PartMaster
		     SET InternalPartName=@InternalPartName
			,CustomerPartName=@CustomerPartName
			,VendorCode=@VendorCode
			,VendorName=@VendorName
			,PackSize=@PackSize
			,ModifiedBy=@CreatedBy
			,ModifiedOn=getdate()
		  WHERE InternalPartNo=@InternalPartNo and CustomerPartNo=@CustomerPartNo
		 Select 'Y' AS RESULT
	  END
	IF @TYPE='DELETE'
	  BEGIN
	    	  	    INSERT INTO TBL_PartMaster_History
           (InternalPartNo,
			InternalPartName,
			CustomerPartNo,
			CustomerPartName,
			VendorCode,
			VendorName,
			PackSize,
			CreatedBy,
			CreatedOn
			,Status)
           (select InternalPartNo,
			InternalPartName,
			CustomerPartNo,
			CustomerPartName,
			VendorCode,
			VendorName,
			PackSize,
			@CreatedBy,
			getdate(),
		   'Delete' from TBL_PartMaster where InternalPartNo=@InternalPartNo and CustomerPartNo=@CustomerPartNo)
	     Delete from TBL_PartMaster WHERE InternalPartNo=@InternalPartNo and CustomerPartNo=@CustomerPartNo
		 Select 'Y' AS RESULT
	  END
   	END TRY
		 BEGIN CATCH
				SELECT 
				ERROR_MESSAGE() AS ErrorMessage;
		 END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[PRC_PrintBarcode]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_PrintBarcode]
	@Type varchar(100),
	@AISPartNo varchar(50) = NULL
AS
DECLARE @MaxCount int
DECLARE @Count int
DECLARE @Barcode VARCHAR(100),@RunningSerialNo varchar(20)='',@Shift varchar(50)='',@AISPartName varchar (100)
BEGIN

 IF(@Type='GETPARTNO')
   BEGIN
	SELECT DISTINCT AISPartNo FROM TBL_BarcodeIndex ORDER BY AISPartNo
  END

 IF(@Type='PrintBarcode')
  BEGIN
	 IF(Exists(SELECT 1 FROM  TBL_BarcodeIndex  WHERE  AISPartNo = @AISPartNo))
	 BEGIN
	    IF(Exists(SELECT 1 FROM  TBL_BarcodeIndex  WHERE  AISPartNo = @AISPartNo))
		  BEGIN
		        SET @Count = 0
				SET @Barcode = ''
				SET @MaxCount = (SELECT max(ValueIndex) from TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo)
				WHILE @Count<=@MaxCount
				 BEGIN
					IF @Barcode!=''
						SET @Barcode=@Barcode + (SELECT FieldValue FROM TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo and ValueIndex=@Count)
					ELSE
						SET @Barcode=(SELECT FieldValue FROM TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo and ValueIndex=@Count)
					SET @Count=@Count+1
				END
				 EXECUTE PRC_GetRunningSerial @AISPartNo,@RunningSerialNo output;
				 SET @Barcode=@Barcode+@RunningSerialNo
				 SET @Shift=(SELECT dbo.GetShiftTime(convert(TIME(7),getdate())))
				 INSERT INTO TBL_BarcodePrinting (PartNo,Barcode,[Status],PrintedOn,PrintedBy,[Shift])
				 VALUES (@AISPartNo,@Barcode,'PRINT',GETDATE(),'AEP',@Shift)
				 SELECT TOP 1 @AISPartName=InternalPartName FROM TBL_PartMaster WHERE InternalPartNo=@AISPartNo
				 SELECT @Barcode AS BARCODE,@AISPartNo AS AISPARTNO,@AISPartName AISPARTNAME
		   END
		 ELSE
		  BEGIN
		  SELECT 'N' As Result,'AISPartNo ('+@AISPartNo+') does not exist' as Msg
		 END
	  END
	 ELSE
	  BEGIN
	  SELECT 'N' As Result,'AISPartNo ('+@AISPartNo+') does not exist' as Msg
      END
  END
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_REPORT]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PRC_REPORT]
@TYPE VARCHAR(100)=NULL,
@FROM_DATE VARCHAR(50)=NULL,
@TO_DATE VARCHAR(50)=NULL,
@PartNo VARCHAR(50)=NULL,
@PalletNo VARCHAR(50)=NULL,
@RPT_Type VARCHAR(50)=NULL
AS
BEGIN

 DECLARE @QUERY NVARCHAR(MAX)=NULL
  if(@Type='GETPARTNO')
	begin
	    SELECT DISTINCT InternalPartNo as AISPartNo FROM TBL_PartMaster
   end

   if(@Type='GETPALLETNO')
	begin
	  SELECT DISTINCT PalletNo FROM TBL_Pallet_Mapping
   end

  IF @TYPE='GETREPORTLABELPRINTING'
    BEGIN
	    SET @QUERY='
	       SELECT ROW_NUMBER() OVER(ORDER BY PrintedOn asc) AS ''SN'',PartNo ''Part No'',Barcode ''Barcode'',PrintedOn ''Printed On'',PrintedBy ''Printed By''  from TBL_BarcodePrinting  WHERE CONVERT(VARCHAR(10),PrintedOn,121) BETWEEN '''+@FROM_DATE+''' AND '''+@TO_DATE+''''
		 IF @PartNo<>'' AND @PartNo<>'--Select--'
			 BEGIN
			    SET @QUERY= @QUERY+' AND PartNo='''+@PartNo+''''
			 END
	  EXEC(@QUERY)
	END

 IF @TYPE='GETREPORTPALLETMAPPING'
    BEGIN
	   SET @QUERY='
	       SELECT ROW_NUMBER() OVER(ORDER BY MappedOn asc) AS ''SN'',PartNo ''Part No'',PalletNo ''Pallet'',Barcode ''Barcode'',MappedOn ''Mapped On'',MappedBy ''Mapped By''  from TBL_Pallet_Mapping  WHERE CONVERT(VARCHAR(10),MappedOn,121)  BETWEEN '''+@FROM_DATE+''' AND '''+@TO_DATE+''''
	    IF @PartNo<>'' AND @PartNo<>'--Select--'
			 BEGIN
			    SET @QUERY= @QUERY+' AND PartNo='''+@PartNo+''''
			 END
			  IF @PalletNo<>'' AND @PalletNo<>'--Select--'
			 BEGIN
			    SET @QUERY= @QUERY+' AND PalletNo='''+@PalletNo+''''
			 END
	  EXEC(@QUERY)
	  END
    
END


GO
/****** Object:  StoredProcedure [dbo].[PRC_SET_CHILDSEC_RIGHTS]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[PRC_SET_CHILDSEC_RIGHTS] 
@GROUP_NAME NVARCHAR(50)=NULL,
@MODULE_NAME NVARCHAR(100)=NULL
AS
BEGIN
  
SELECT distinct [Add] as [Save],[Update],[Delete]  FROM TBL_GROUPRIGHT GR JOIN TBL_MODULEMASTER MD ON GR.MODULEID=MD.MODULEID
WHERE MODULENAME='Part Master' AND GroupName='User'
	
END
GO
/****** Object:  StoredProcedure [dbo].[PRC_UserMaster]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[PRC_UserMaster] 
@Type varchar(max),
@UserID nvarchar(50)=null,
@UserName nvarchar(150)=null,
@Password nvarchar(50)=null,
@Group nvarchar(50)=null,
@CreatedBy nvarchar(50)=null,
@NewPassword nvarchar(50)=null,
@EmailId nvarchar(100)=null,
@EmpCode nvarchar(100)=null,
@EmpDesignation nvarchar(100)=null

AS
BEGIN
	Declare @Count int;
	 DECLARE @QUERY NVARCHAR(MAX)=NULL;
	if (@Type = 'SELECT')
		begin
			 SELECT USERID,USERNAME,PASSWORD,GROUPNAME,CreatedBy,CONVERT(varchar(10),CreatedOn,103) CreatedOn
			 FROM TBL_UserMaster us where IsActive='True'
			 ORDER BY USERNAME

		end
	
	else if (@Type = 'SEARCH')
		begin
			SELECT USERID,USERNAME,PASSWORD,GROUPNAME,CreatedBy,CONVERT(varchar(10),CreatedOn,103) CreatedOn 
			FROM TBL_UserMaster us
			WHERE IsActive='True' and USERID like '%'+CAST(@UserID AS nvarchar)+'%' or  UserName like '%'+CAST(@UserID AS nvarchar)+'%'
		
		end
	else if(@Type='SELECTBYID')
	  begin
		 SELECT USERID,USERNAME,PASSWORD,GROUPNAME,CreatedBy,CONVERT(varchar(10),CreatedOn,103) CreatedOn
		 FROM TBL_UserMaster us 
		 WHERE IsActive='True'  and USERID = ''+CAST(@UserID AS nvarchar)+''
	  end
	else if(@Type='INSERT')
	  begin
	    IF EXISTS(SELECT 1 FROM TBL_UserMaster WHERE UserId=@UserID)
		 BEGIN
		     Select 'This user id is already exists!!!!' AS RESULT
			 RETURN
		 END
	    INSERT INTO [dbo].[TBL_UserMaster]
           ([UserId]
           ,[UserName]
           ,[Password]
           ,[GroupName]
		   ,[IsActive]
           ,[CreatedOn]
           ,[CreatedBy])
		 VALUES
           (@UserID
           ,@UserName
           ,@Password
           ,@Group
		   ,'True'
           ,GETDATE()
           ,@CreatedBy)

			Select 'Y' AS RESULT
	  end
	 else if(@Type='UPDATE')
	  begin
	     UPDATE [dbo].[TBL_UserMaster]
		   SET [UserName] = @UserName
			  ,[Password] = @Password
			  ,[GroupName] = @Group
			  ,ModifiedOn=getdate()
			  ,ModifiedBy=@CreatedBy
		 WHERE UserId = @UserId 
		 Select 'Y' AS RESULT
	  end
	else if(@Type='DELETE')
	  begin
	     Update [TBL_UserMaster] set IsActive='false' ,ModifiedOn=getdate() ,ModifiedBy=@CreatedBy WHERE UserId=@UserID 
		 Select 'Y' AS RESULT
	  end
	
	else if (@Type='VALIDATEUSER') 
		    begin
			      Select distinct UserName,GroupName,USERID,Sitecode From TBL_UserMaster
				  Where UserId = @UserID And Password = @Password 
		    end
    else if (@Type='ACCESSUSER') 
		    begin
			   if exists(select UserID from TBL_UserMaster where UserId = @UserID And Password = @Password AND GroupName in ('ADMIN','SUPERVISOR'))
			     begin
				      DECLARE @MSG_RS nvarchar(50)=null
					  Select distinct @MSG_RS=UserName From TBL_UserMaster
					   Where UserId = @UserID And Password = @Password 
					  Select 'Y' AS RESULT,@MSG_RS AS MSG
				 end
			   else
			      Select 'N' AS RESULT,'Only Admin/Supervisor can give the access!!' AS MSG
		    end
	else if (@Type='UPDATEPASSWORD')
		begin
			Declare @UserOldPassword varchar(50)
			Select @UserOldPassword = Password From TBL_UserMaster Where UserId = @UserID
		
			If(@Password = @UserOldPassword)
			Begin
				UPDATE TBL_UserMaster set PASSWORD = @NewPassword, ModifiedOn=getdate() ,ModifiedBy=@CreatedBy where  UserId=@UserID
				Select 'Y' AS RESULT
			End
			Else
			Begin
				Select 'Wrong Old Password' AS RESULT
			End
		end

END


GO
/****** Object:  StoredProcedure [dbo].[PRS_BarcodeStatus]    Script Date: 10/8/2021 17:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure   [dbo].[PRS_BarcodeStatus]

@Barcode varchar(100)=null,
@Type varchar (100)
AS
Declare @status varchar(50),
@ParentBarcode varchar(50);
BEGIN

if (@Type='VALIDATEBARCODE')
begin
  IF(Exists(select 1 From TBL_BarcodePrinting WHERE Barcode =@Barcode and status='1' ))
    BEGIN
	  SELECT PartNo ,Barcode, 'PRINT' [Status] FROM TBL_BarcodePrinting WHERE Barcode =@Barcode AND status='1';
	  END
	  ELSE IF(Exists(select 1 From TBL_BarcodePrinting WHERE Barcode =@Barcode and status='2' ))
	   BEGIN
	     SELECT PartNo ,Barcode, 'MAPPED'[Status] FROM TBL_BarcodePrinting WHERE Barcode =@Barcode AND status='2';
	   END
	  ELSE IF(Exists(select 1 From TBL_Pallet_Mapping WHERE PalletNo =@Barcode ))
	   BEGIN
	   SELECT PartNo ,Barcode, WorkOrderNo FROM TBL_Pallet_Mapping WHERE PalletNo =@Barcode;
	  END

	 END
  ELSE 
	BEGIN
		SELECT 'Barcode not exists in database'
 END
end

GO
