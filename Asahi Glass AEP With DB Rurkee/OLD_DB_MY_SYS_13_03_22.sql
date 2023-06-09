USE [SATO_AIS_PRINTING]
GO
/****** Object:  UserDefinedTableType [dbo].[UserDefineTBL_JobDetail]    Script Date: 13-03-2022 13:43:37 ******/
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
/****** Object:  UserDefinedFunction [dbo].[GetShiftTime]    Script Date: 13-03-2022 13:43:37 ******/
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
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 13-03-2022 13:43:37 ******/
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
/****** Object:  UserDefinedFunction [dbo].[Split_Columns]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create FUNCTION [dbo].[Split_Columns] (
    @string NVARCHAR(MAX),
    @delimiter CHAR(1)
    )
RETURNS @out_put TABLE (
    [column_id] INT IDENTITY(1, 1) NOT NULL,
    [value] NVARCHAR(MAX)
    )
AS
BEGIN
    DECLARE @value NVARCHAR(MAX),
        @pos INT = 0,
        @len INT = 0

    SET @string = CASE 
            WHEN RIGHT(@string, 1) != @delimiter
                THEN @string + @delimiter
            ELSE @string
            END

    WHILE CHARINDEX(@delimiter, @string, @pos + 1) > 0
    BEGIN
        SET @len = CHARINDEX(@delimiter, @string, @pos + 1) - @pos
        SET @value = SUBSTRING(@string, @pos, @len)

        INSERT INTO @out_put ([value])
        SELECT LTRIM(RTRIM(@value)) AS [column]

        SET @pos = CHARINDEX(@delimiter, @string, @pos + @len) + 1
    END

    RETURN
END


GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 13-03-2022 13:43:37 ******/
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
/****** Object:  Table [dbo].[TBL_BarcodeIndex]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_BarcodePrinting]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Customer]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerCode] [varchar](50) NOT NULL,
	[CustomerName] [varchar](100) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_TBL_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Fields]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_GroupMaster]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_GroupRight]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_GroupRight](
	[GroupName] [varchar](20) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[View] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [varchar](20) NULL,
	[Add] [bit] NULL CONSTRAINT [DF_TBL_GroupRight_Add]  DEFAULT ((0)),
	[Update] [bit] NULL CONSTRAINT [DF_TBL_GroupRight_Update]  DEFAULT ((0)),
	[Delete] [bit] NULL CONSTRAINT [DF_TBL_GroupRight_Delete]  DEFAULT ((0)),
 CONSTRAINT [PK_UserTypeRight] PRIMARY KEY CLUSTERED 
(
	[GroupName] ASC,
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_LINE_PART_MAPPING]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_LINE_PART_MAPPING](
	[InternalPartNo] [varchar](50) NOT NULL,
	[Line] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TBL_LINE_PART_MAPPING] PRIMARY KEY CLUSTERED 
(
	[InternalPartNo] ASC,
	[Line] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_ModuleMaster]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_ModuleMaster](
	[ModuleId] [int] NOT NULL,
	[ModuleName] [varchar](50) NULL,
	[Active] [bit] NULL CONSTRAINT [DF_ModuleMaster_Active]  DEFAULT ((1)),
 CONSTRAINT [PK_ScreenMaster] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Pallet_Mapping]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_Pallet_Mapping](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PartNo] [varchar](50) NULL,
	[WorkOrderNo] [varchar](50) NULL,
	[PalletNo] [varchar](50) NOT NULL,
	[Barcode] [varchar](100) NOT NULL,
	[PalletQty] [int] NULL,
	[Line_No] [varchar](50) NULL,
	[Shift] [varchar](1) NULL,
	[MappedBy] [varchar](50) NULL,
	[MappedOn] [datetime] NULL,
	[IsComplete] [bit] NULL,
	[CompletedID] [varchar](50) NULL,
	[CompletedOn] [datetime] NULL,
	[CompletedBy] [varchar](50) NULL,
 CONSTRAINT [PK_TBL_Pallet_Mapping] PRIMARY KEY CLUSTERED 
(
	[PalletNo] ASC,
	[Barcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_PartMaster]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_PartMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InternalPartNo] [varchar](50) NOT NULL,
	[InternalPartName] [varchar](100) NULL,
	[CustomerPartNo] [varchar](50) NOT NULL,
	[CustomerPartName] [varchar](100) NULL,
	[VendorCode] [varchar](50) NOT NULL,
	[VendorName] [varchar](50) NULL,
	[PackSize] [int] NOT NULL,
	[CustomerCode] [varchar](50) NULL,
	[IsQAEnable] [varchar](1) NULL,
	[Separator] [varchar](1) NULL,
	[PrintQty] [int] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_TBL_PartMaster] PRIMARY KEY CLUSTERED 
(
	[InternalPartNo] ASC,
	[CustomerPartNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_PartMaster_History]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
	[CustomerCode] [varchar](50) NULL,
	[IsQAEnable] [varchar](1) NULL,
	[Separator] [varchar](1) NULL,
	[PrintQty] [int] NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedOn] [datetime] NULL,
	[Status] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_PrinterLine_Mapping]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_RunningSerial]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_StatusMaster]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TBL_StatusMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](100) NULL,
	[ProcessName] [varchar](50) NULL,
	[Status] [int] NULL,
	[Description] [varchar](250) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_UserMaster]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TBL_Version]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Version](
	[AppName] [nvarchar](50) NULL,
	[App_Version] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbShiftMaster]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbShiftMaster](
	[ShiftCode] [varchar](5) NOT NULL,
	[ShiftName] [varchar](50) NULL,
	[ShiftFrom] [varchar](50) NULL,
	[ShiftTo] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[TBL_BarcodeIndex] ON 

INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1031, N'FG.ACF.LFH.GCG2120000', N'00552367100101', N'CustomerPart', 14, 0, N'admin', CAST(N'2021-09-13 15:08:23.793' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1032, N'FG.ACF.LFH.GCG2120000', N'A65688', N'VendorCode', 6, 3, N'admin', CAST(N'2021-09-13 15:08:23.890' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1035, N'FG.ACF.LFH.GCG2120000', N'KO', N'RevisionNo', 2, 2, N'admin', CAST(N'2021-10-07 10:52:35.930' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1036, N'FG.ACF.LFH.GCG2120000', N'MMyy', N'MfgFormat', 4, 1, N'admin', CAST(N'2021-09-13 15:08:23.840' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1038, N'FG.ACF.LFH.GCG2120000', N'SerialNo', N'SerialNo', 5, 4, N'admin', CAST(N'2021-10-06 16:10:23.277' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1, N'FG.TCB.LFH.GCG2120000', N'00282967100101', N'CustomerPart', 4, 0, N'admin', CAST(N'2021-09-09 16:23:28.320' AS DateTime), N'admin', CAST(N'2021-09-13 14:48:28.080' AS DateTime))
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1006, N'FG.TCB.LFH.GCG2120000', N'A65688', N'VendorCode', 6, 4, N'admin', CAST(N'2021-09-16 12:41:28.953' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1018, N'FG.TCB.LFH.GCG2120000', N'KO', N'RevisionNo', 2, 2, N'admin', CAST(N'2021-10-07 10:51:58.957' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1015, N'FG.TCB.LFH.GCG2120000', N'MMyy', N'MfgFormat', 4, 1, N'admin', CAST(N'2021-10-06 16:02:59.880' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1019, N'FG.TCB.LFH.GCG2120000', N'SerialNo', N'SerialNo', 5, 3, N'admin', CAST(N'2021-10-07 10:51:58.957' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (9, N'FG.TCF.LFH.GCG2120000', N'00552367100101', N'CustomerPart', 14, 0, N'admin', CAST(N'2021-09-13 15:08:23.793' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (13, N'FG.TCF.LFH.GCG2120000', N'A65688', N'VendorCode', 6, 3, N'admin', CAST(N'2021-09-13 15:08:23.890' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1020, N'FG.TCF.LFH.GCG2120000', N'KO', N'RevisionNo', 2, 2, N'admin', CAST(N'2021-10-07 10:52:35.930' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (10, N'FG.TCF.LFH.GCG2120000', N'MMyy', N'MfgFormat', 4, 1, N'admin', CAST(N'2021-09-13 15:08:23.840' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1017, N'FG.TCF.LFH.GCG2120000', N'SerialNo', N'SerialNo', 5, 4, N'admin', CAST(N'2021-10-06 16:10:23.277' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1007, N'Interna001', N'customer001', N'CustomerPart', 11, 1, N'12', CAST(N'2021-09-23 13:19:20.910' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1011, N'Interna001', N'KO', N'RevisionNo', 2, 3, N'admin', CAST(N'2021-09-24 11:55:01.230' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1010, N'Interna001', N'MMyy', N'MfgFormat', 4, 2, N'admin', CAST(N'2021-09-24 11:55:01.170' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1021, N'Interna001', N'SerialNo', N'SerialNo', 8, 0, N'Admin', CAST(N'2021-10-07 12:41:42.430' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_BarcodeIndex] ([ID], [AISPartNo], [FieldValue], [FieldName], [ValueLength], [ValueIndex], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1022, N'Interna001', N'v001', N'VendorCode', 4, 4, N'Admin', CAST(N'2021-10-07 12:41:42.433' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[TBL_BarcodeIndex] OFF
SET IDENTITY_INSERT [dbo].[TBL_BarcodePrinting] ON 

INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (52, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA65688000001', 1, N'1', N'A', CAST(N'2021-09-19 11:13:51.823' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (53, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA65688000002', 1, N'1', N'A', CAST(N'2021-09-19 11:13:56.693' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (54, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA65688000003', 1, N'1', N'A', CAST(N'2021-09-19 11:14:02.180' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (55, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA65688000004', 1, N'1', N'A', CAST(N'2021-09-19 11:14:05.320' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (56, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA65688000005', 1, N'1', N'A', CAST(N'2021-09-19 11:14:07.677' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (57, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA65688000006', 1, N'1', N'A', CAST(N'2021-09-20 09:09:04.180' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (58, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA65688000007', 1, N'1', N'A', CAST(N'2021-09-20 09:09:21.577' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (23, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA6568800001', 1, N'2', N'A', CAST(N'2021-09-17 10:25:11.260' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (45, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA656880000119092021', 1, N'1', N'A', CAST(N'2021-09-19 10:23:56.567' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (24, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA6568800002', 1, N'2', N'A', CAST(N'2021-09-17 10:25:16.807' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (46, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA656880000219092021', 1, N'1', N'A', CAST(N'2021-09-19 10:24:00.633' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (25, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA6568800003', 1, N'2', N'A', CAST(N'2021-09-17 10:25:20.117' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (47, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA656880000319092021', 1, N'1', N'A', CAST(N'2021-09-19 10:24:03.270' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (48, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA656880000419092021', 1, N'1', N'A', CAST(N'2021-09-19 10:24:05.550' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (49, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA656880000519092021', 1, N'1', N'A', CAST(N'2021-09-19 10:24:07.983' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (50, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA656880000619092021', 1, N'1', N'A', CAST(N'2021-09-19 10:24:10.143' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (51, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA656880000719092021', 1, N'1', N'A', CAST(N'2021-09-19 10:24:12.467' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (14, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA6568800009', 1, N'2', N'A', CAST(N'2021-09-16 10:39:36.243' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (16, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA6568800010', 1, N'2', N'A', CAST(N'2021-09-16 10:40:14.123' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (17, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA6568800011', 1, N'2', N'A', CAST(N'2021-09-16 10:46:19.947' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (18, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA6568800012', 1, N'2', N'A', CAST(N'2021-09-16 10:46:23.563' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (19, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA6568800013', 1, N'2', N'A', CAST(N'2021-09-16 11:09:03.317' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (20, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA6568800014', 1, N'2', N'A', CAST(N'2021-09-16 11:16:34.403' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (21, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA6568800015', 1, N'2', N'A', CAST(N'2021-09-16 11:26:23.040' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (22, N'FG.TCB.LFH.GCG2120000', N'002829671001010921KOA6568800016', 1, N'2', N'A', CAST(N'2021-09-16 11:26:25.263' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (26, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA6568800001', 1, N'2', N'A', CAST(N'2021-09-17 11:25:15.770' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (27, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA6568800002', 1, N'1', N'A', CAST(N'2021-09-17 11:25:20.717' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (28, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA6568800003', 1, N'2', N'A', CAST(N'2021-09-17 11:47:44.430' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (29, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA6568800004', 1, N'2', N'A', CAST(N'2021-09-17 12:00:00.627' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (34, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA656880000419092021', 1, N'1', N'A', CAST(N'2021-09-19 10:22:05.300' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (30, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA6568800005', 1, N'2', N'A', CAST(N'2021-09-17 12:00:46.750' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (35, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA656880000519092021', 1, N'1', N'A', CAST(N'2021-09-19 10:22:25.050' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (36, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA656880000619092021', 1, N'1', N'A', CAST(N'2021-09-19 10:22:31.087' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (37, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA656880000719092021', 1, N'1', N'A', CAST(N'2021-09-19 10:22:36.700' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (38, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA656880000819092021', 1, N'1', N'A', CAST(N'2021-09-19 10:22:38.847' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (39, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA656880000919092021', 1, N'1', N'A', CAST(N'2021-09-19 10:22:40.030' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (40, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA656880001019092021', 1, N'1', N'A', CAST(N'2021-09-19 10:22:41.270' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (41, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA656880001119092021', 1, N'1', N'A', CAST(N'2021-09-19 10:22:43.680' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (42, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA656880001219092021', 1, N'1', N'A', CAST(N'2021-09-19 10:22:46.750' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (43, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA656880001319092021', 1, N'2', N'A', CAST(N'2021-09-19 10:22:48.073' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (44, N'FG.TCF.LFH.GCG2120000', N'005523671001010921KOA656880001419092021', 1, N'2', N'A', CAST(N'2021-09-19 10:22:49.173' AS DateTime), N'AEP')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (59, N'FG.TCF.LFH.GCG2120000', N'005523671001011021KOA6568800001', 1, N'1', N'A', CAST(N'2021-10-25 12:40:52.483' AS DateTime), N'ADMIN')
INSERT [dbo].[TBL_BarcodePrinting] ([ID], [PartNo], [Barcode], [Qty], [Status], [Shift], [PrintedOn], [PrintedBy]) VALUES (60, N'FG.ACF.LFH.GCG2120000', N'005523671001011021KOA6568800002', 1, N'1', N'A', CAST(N'2021-10-25 12:41:05.750' AS DateTime), N'ADMIN')
SET IDENTITY_INSERT [dbo].[TBL_BarcodePrinting] OFF
SET IDENTITY_INSERT [dbo].[TBL_Customer] ON 

INSERT [dbo].[TBL_Customer] ([ID], [CustomerCode], [CustomerName], [CreatedOn], [CreatedBy]) VALUES (1, N'A', N'A', NULL, NULL)
INSERT [dbo].[TBL_Customer] ([ID], [CustomerCode], [CustomerName], [CreatedOn], [CreatedBy]) VALUES (2, N'B', N'B', NULL, NULL)
SET IDENTITY_INSERT [dbo].[TBL_Customer] OFF
SET IDENTITY_INSERT [dbo].[TBL_Fields] ON 

INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (1, N'CustomerPart', N'FG.TCB.LFH.GCG2120000')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (6, N'CustomerPart', N'FG.TCF.LFH.GCG2120000')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (11, N'CustomerPart', N'Interna001')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (4, N'MfgFormat', N'FG.TCB.LFH.GCG2120000')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (7, N'MfgFormat', N'FG.TCF.LFH.GCG2120000')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (13, N'MfgFormat', N'Interna001')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (3, N'RevisionNo', N'FG.TCB.LFH.GCG2120000')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (8, N'RevisionNo', N'FG.TCF.LFH.GCG2120000')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (14, N'RevisionNo', N'Interna001')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (19, N'SerialNo', N'FG.TCF.LFH.GCG2120000')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (2, N'VendorCode', N'FG.TCB.LFH.GCG2120000')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (9, N'VendorCode', N'FG.TCF.LFH.GCG2120000')
INSERT [dbo].[TBL_Fields] ([ID], [FieldName], [InternalPartNo]) VALUES (15, N'VendorCode', N'Interna001')
SET IDENTITY_INSERT [dbo].[TBL_Fields] OFF
SET IDENTITY_INSERT [dbo].[TBL_GroupMaster] ON 

INSERT [dbo].[TBL_GroupMaster] ([ID], [GroupName], [Description], [IsActive], [Sitecode], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, N'aaa', NULL, 0, NULL, CAST(N'2021-09-23 13:00:01.167' AS DateTime), N'12', NULL, NULL)
INSERT [dbo].[TBL_GroupMaster] ([ID], [GroupName], [Description], [IsActive], [Sitecode], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Admin', NULL, 1, NULL, CAST(N'2020-03-02 09:51:44.613' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[TBL_GroupMaster] ([ID], [GroupName], [Description], [IsActive], [Sitecode], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'User', NULL, 1, NULL, CAST(N'2020-03-02 09:52:33.250' AS DateTime), N'admin', NULL, NULL)
SET IDENTITY_INSERT [dbo].[TBL_GroupMaster] OFF
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 101, 1, CAST(N'2020-10-21 15:38:24.397' AS DateTime), N'1', 1, 1, 1)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 102, 1, CAST(N'2020-10-21 15:38:24.407' AS DateTime), N'1', 1, 1, 1)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 103, 1, CAST(N'2020-10-21 15:38:24.400' AS DateTime), N'1', 1, NULL, NULL)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 104, 1, CAST(N'2020-10-21 15:38:24.400' AS DateTime), N'1', NULL, NULL, NULL)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 105, 1, CAST(N'2020-10-21 15:38:24.400' AS DateTime), N'1', NULL, NULL, NULL)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 106, 1, CAST(N'2021-06-23 12:32:51.023' AS DateTime), N'1', 0, 0, 0)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 201, 1, CAST(N'2021-06-23 12:32:51.023' AS DateTime), N'1', NULL, NULL, NULL)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'Admin', 202, 1, CAST(N'2021-06-23 12:32:51.023' AS DateTime), N'1', NULL, NULL, NULL)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'User', 101, 1, CAST(N'2021-09-23 12:58:36.780' AS DateTime), N'admin', 1, 1, 1)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'User', 102, 1, CAST(N'2021-09-23 12:58:36.780' AS DateTime), N'admin', 1, 1, 1)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'User', 103, 1, CAST(N'2021-09-23 12:58:36.783' AS DateTime), N'admin', 1, 1, 1)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'User', 104, 1, CAST(N'2021-09-23 12:58:36.783' AS DateTime), N'admin', 1, 1, 1)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'User', 105, 1, CAST(N'2021-09-23 12:58:36.787' AS DateTime), N'admin', 1, 1, 1)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'User', 106, 1, CAST(N'2021-09-23 12:58:36.790' AS DateTime), N'admin', 1, 1, 1)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'User', 201, 1, CAST(N'2021-09-23 12:58:36.820' AS DateTime), N'admin', 1, 1, 1)
INSERT [dbo].[TBL_GroupRight] ([GroupName], [ModuleId], [View], [CreatedOn], [CreatedBy], [Add], [Update], [Delete]) VALUES (N'User', 202, 1, CAST(N'2021-09-23 12:58:36.833' AS DateTime), N'admin', 1, 1, 1)
INSERT [dbo].[TBL_LINE_PART_MAPPING] ([InternalPartNo], [Line]) VALUES (N'FG.TCB.LFH.GCG2120000', N'1')
INSERT [dbo].[TBL_LINE_PART_MAPPING] ([InternalPartNo], [Line]) VALUES (N'FG.TCF.LFH.GCG2120000', N'2')
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (101, N'Group Master', 1)
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (102, N'User Master', 1)
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (103, N'Part Master', 1)
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (104, N'Report Label Printing', 1)
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (105, N'Report Pallet Mapping', 1)
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (106, N'Barcode Generation', 1)
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (201, N'Pallet Mapping', 1)
INSERT [dbo].[TBL_ModuleMaster] ([ModuleId], [ModuleName], [Active]) VALUES (202, N'Status', 1)
SET IDENTITY_INSERT [dbo].[TBL_Pallet_Mapping] ON 

INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (56, N'FG.TCF.LFH.GCG2120000', N'w', N'p001', N'005523671001010921KOA656880001319092021', 4, NULL, N'A', N'Admin', CAST(N'2021-09-19 10:47:57.587' AS DateTime), 1, N'190921110122', CAST(N'2021-09-19 11:01:22.360' AS DateTime), N'Admin')
INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (55, N'FG.TCF.LFH.GCG2120000', N'w', N'p001', N'005523671001010921KOA656880001419092021', 4, NULL, N'A', N'Admin', CAST(N'2021-09-19 10:47:12.093' AS DateTime), 1, N'190921110122', CAST(N'2021-09-19 11:01:22.360' AS DateTime), N'Admin')
INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (38, N'FG.TCB.LFH.GCG2120000', N'w', N'p1', N'002829671001010921KOA6568800001', 6, NULL, N'A', N'Admin', CAST(N'2021-09-17 10:34:36.093' AS DateTime), 1, N'1709202100003', CAST(N'2021-09-17 10:58:48.880' AS DateTime), N'Admin')
INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (39, N'FG.TCB.LFH.GCG2120000', N'w', N'p1', N'002829671001010921KOA6568800002', 6, NULL, N'A', N'Admin', CAST(N'2021-09-17 10:36:34.413' AS DateTime), 1, N'1709202100003', CAST(N'2021-09-17 10:58:48.880' AS DateTime), N'Admin')
INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (40, N'FG.TCB.LFH.GCG2120000', N'ww', N'p2', N'002829671001010921KOA6568800003', 6, NULL, N'A', N'Admin', CAST(N'2021-09-17 11:17:23.023' AS DateTime), 1, N'1709202100004', CAST(N'2021-09-17 11:23:46.290' AS DateTime), N'Admin')
INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (41, N'FG.TCB.LFH.GCG2120000', N'ww', N'p2', N'002829671001010921KOA6568800009', 6, NULL, N'A', N'Admin', CAST(N'2021-09-17 11:18:54.650' AS DateTime), 1, N'1709202100004', CAST(N'2021-09-17 11:23:46.290' AS DateTime), N'Admin')
INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (42, N'FG.TCB.LFH.GCG2120000', N'ww', N'p2', N'002829671001010921KOA6568800011', 6, NULL, N'A', N'Admin', CAST(N'2021-09-17 11:23:05.510' AS DateTime), 1, N'1709202100004', CAST(N'2021-09-17 11:23:46.290' AS DateTime), N'Admin')
INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (43, N'FG.TCF.LFH.GCG2120000', N'w', N'p3', N'005523671001010921KOA6568800001', 4, NULL, N'A', N'Admin', CAST(N'2021-09-17 11:26:27.103' AS DateTime), 1, N'1709202100013', CAST(N'2021-09-17 13:17:00.390' AS DateTime), N'Admin')
INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (45, N'FG.TCF.LFH.GCG2120000', N'w', N'p3', N'005523671001010921KOA6568800003', 4, NULL, N'A', N'Admin', CAST(N'2021-09-17 11:53:57.160' AS DateTime), 1, N'1709202100013', CAST(N'2021-09-17 13:17:00.390' AS DateTime), N'Admin')
INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (46, N'FG.TCF.LFH.GCG2120000', N'w', N'p3', N'005523671001010921KOA6568800004', 4, NULL, N'A', N'Admin', CAST(N'2021-09-17 12:00:24.260' AS DateTime), 1, N'1709202100013', CAST(N'2021-09-17 13:17:00.390' AS DateTime), N'Admin')
INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (54, N'FG.TCF.LFH.GCG2120000', N'w', N'p3', N'005523671001010921KOA6568800005', 4, NULL, N'A', N'Admin', CAST(N'2021-09-17 13:17:00.320' AS DateTime), 1, N'1709202100013', CAST(N'2021-09-17 13:17:00.390' AS DateTime), N'Admin')
INSERT [dbo].[TBL_Pallet_Mapping] ([ID], [PartNo], [WorkOrderNo], [PalletNo], [Barcode], [PalletQty], [Line_No], [Shift], [MappedBy], [MappedOn], [IsComplete], [CompletedID], [CompletedOn], [CompletedBy]) VALUES (44, N'FG.TCB.LFH.GCG2120000', N'w', N'p5', N'002829671001010921KOA6568800015', 6, NULL, N'A', N'Admin', CAST(N'2021-09-17 11:44:40.847' AS DateTime), 1, N'1709202100005', CAST(N'2021-09-17 11:45:01.703' AS DateTime), N'Admin')
SET IDENTITY_INSERT [dbo].[TBL_Pallet_Mapping] OFF
SET IDENTITY_INSERT [dbo].[TBL_PartMaster] ON 

INSERT [dbo].[TBL_PartMaster] ([ID], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CustomerCode], [IsQAEnable], [Separator], [PrintQty], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1006, N'FG.ACF.LFH.GCG2120000', N'Windshield', N'00552367100101', NULL, N'A65688', NULL, 10, N'AL', NULL, NULL, 2, N'12', CAST(N'2021-09-23 13:11:54.217' AS DateTime), NULL, NULL)
INSERT [dbo].[TBL_PartMaster] ([ID], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CustomerCode], [IsQAEnable], [Separator], [PrintQty], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (1, N'FG.TCB.LFH.GCG2120000', N'Windshield', N'00282967100101', NULL, N'A65688', NULL, 4, N'TATA', N'Y', N'~', 2, N'admin', CAST(N'2021-09-08 09:22:47.450' AS DateTime), N'admin', CAST(N'2021-10-06 15:45:00.423' AS DateTime))
INSERT [dbo].[TBL_PartMaster] ([ID], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CustomerCode], [IsQAEnable], [Separator], [PrintQty], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn]) VALUES (5, N'FG.TCF.LFH.GCG2120000', N'Windshield', N'00552367100101', NULL, N'A65688', NULL, 4, N'TATA', NULL, N'~', 1, N'admin', CAST(N'2021-09-08 12:31:34.230' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[TBL_PartMaster] OFF
SET IDENTITY_INSERT [dbo].[TBL_PartMaster_History] ON 

INSERT [dbo].[TBL_PartMaster_History] ([ID], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CustomerCode], [IsQAEnable], [Separator], [PrintQty], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [Status]) VALUES (1, N'FG.TCB.LFH.GCG2120000', N'Windshield', N'00282967100101', NULL, N'A65688', NULL, 10, NULL, NULL, NULL, NULL, N'admin', CAST(N'2021-09-08 09:59:40.003' AS DateTime), NULL, NULL, N'Update')
INSERT [dbo].[TBL_PartMaster_History] ([ID], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CustomerCode], [IsQAEnable], [Separator], [PrintQty], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [Status]) VALUES (2, N'FG.TCB.LFH.GCG2120000', N'Windshield', N'00282967100101', NULL, N'A65688', NULL, 12, NULL, NULL, NULL, NULL, N'admin', CAST(N'2021-09-08 09:59:49.173' AS DateTime), NULL, NULL, N'Update')
INSERT [dbo].[TBL_PartMaster_History] ([ID], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CustomerCode], [IsQAEnable], [Separator], [PrintQty], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [Status]) VALUES (3, N'FG.TCB.LFH.GCG2120000', N'Windshield', N'00282967100101', NULL, N'A65688', NULL, 12, NULL, NULL, NULL, NULL, N'admin', CAST(N'2021-09-08 12:19:51.437' AS DateTime), NULL, NULL, N'Update')
INSERT [dbo].[TBL_PartMaster_History] ([ID], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CustomerCode], [IsQAEnable], [Separator], [PrintQty], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [Status]) VALUES (4, N'FG.TCB.LFH.GCG2120000', N'Windshield', N'00282967100101', NULL, N'A65688', NULL, 4, NULL, NULL, N'~', NULL, N'admin', CAST(N'2021-10-06 15:45:00.370' AS DateTime), NULL, NULL, N'Update')
INSERT [dbo].[TBL_PartMaster_History] ([ID], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CustomerCode], [IsQAEnable], [Separator], [PrintQty], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [Status]) VALUES (5, N'sss', N'sss', N'ssssd', NULL, N'dd', NULL, 10, N'A', N'Y', N'@', NULL, NULL, CAST(N'2021-10-06 15:47:07.120' AS DateTime), NULL, NULL, N'Delete')
INSERT [dbo].[TBL_PartMaster_History] ([ID], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CustomerCode], [IsQAEnable], [Separator], [PrintQty], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [Status]) VALUES (6, N'aaa', N'ss', N'ss', NULL, N'sss', NULL, 2, N'A', N'Y', N'!', 1, N'admin', CAST(N'2021-10-22 14:44:50.337' AS DateTime), NULL, NULL, N'Update')
INSERT [dbo].[TBL_PartMaster_History] ([ID], [InternalPartNo], [InternalPartName], [CustomerPartNo], [CustomerPartName], [VendorCode], [VendorName], [PackSize], [CustomerCode], [IsQAEnable], [Separator], [PrintQty], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [Status]) VALUES (7, N'aaa', N'ss', N'ss', NULL, N'sss', NULL, 2, N'A', N'Y', N'!', 44, NULL, CAST(N'2021-10-22 14:45:07.417' AS DateTime), NULL, NULL, N'Delete')
SET IDENTITY_INSERT [dbo].[TBL_PartMaster_History] OFF
INSERT [dbo].[TBL_PrinterLine_Mapping] ([PrinterIp], [Line]) VALUES (N'123', N'1')
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'RANDOMNO', 2021, 9, 17, 13, NULL, NULL, NULL)
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCB.LFH.GCG2120000', 2021, 9, 17, 3, NULL, NULL, NULL)
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCF.LFH.GCG2120000', 2021, 9, 17, 5, NULL, NULL, NULL)
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCF.LFH.GCG2120000', 2021, 9, 19, 14, NULL, NULL, NULL)
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCB.LFH.GCG2120000', 2021, 9, 19, 7, NULL, NULL, NULL)
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCB.LFH.GCG2120000', 2021, 9, 19, 41, 12, CAST(N'2021-10-06 12:21:57.090' AS DateTime), N'LotNo')
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCF.LFH.GCG2120000', 2021, 10, 6, 4, 16, CAST(N'2021-10-06 16:13:28.783' AS DateTime), N'LotNo')
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'Interna001', 2021, 10, 7, 12, 12, CAST(N'2021-10-07 12:48:57.670' AS DateTime), N'1021')
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'Interna001', 2021, 11, 7, 2, 12, CAST(N'2021-11-07 12:48:39.370' AS DateTime), N'1121')
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'Interna001', 2021, 9, 24, 12, 12, CAST(N'2021-10-07 12:44:42.420' AS DateTime), N'LotNo')
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.ACF.LFH.GCG2120000', 2021, 10, 25, 2, 12, CAST(N'2021-10-25 12:41:05.787' AS DateTime), N'1021')
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCF.LFH.GCG2120000', 2021, 10, 25, 1, 12, CAST(N'2021-10-25 12:40:52.490' AS DateTime), N'1021')
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.ASK.LFH.GCG2120000', 2021, 10, 25, 2, 14, CAST(N'2021-10-25 14:52:43.000' AS DateTime), N'1021')
INSERT [dbo].[TBL_RunningSerial] ([TranType], [Year], [Month], [Day], [SerialNo], [Hours], [UpdateDate], [LotNo]) VALUES (N'FG.TCB.LFH.GCG2120000', 2021, 10, 25, 1, 15, CAST(N'2021-10-25 15:20:00.837' AS DateTime), N'1021')
SET IDENTITY_INSERT [dbo].[TBL_StatusMaster] ON 

INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (1, N'TBL_BarcodePrinting', N'RM Receiving', 1, N'RM Receiving and Label Printing')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (2, N'TBL_BarcodePrinting', N'RM Put way', 2, N'RM Roll Put way')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (3, N'TBL_BarcodePrinting', N'RM Picking', 3, N'RM Picking for SF Printing')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (4, N'TBL_BarcodePrinting', N'SF Roll Printing', 1, N'SF Roll Printing')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (5, N'TBL_BarcodePrinting', N'SF Roll Put way', 2, N'SF Roll Put way')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (6, N'TBL_BarcodePrinting', N'SF Picking', 3, N'SF Picking')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (7, N'TBL_BarcodePrinting', N'SF Roll Issuance on Filling Machine', 4, N'SF Roll Issuance on Filling Machine')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (8, N'TBL_BarcodePrinting', N'FG Printing', 1, N'FG Printing')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (9, N'TBL_BarcodePrinting', N'FG Put way', 2, N'FG Put way')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (10, N'TBL_BarcodePrinting', N'FG Picking for Dispatch', 3, N'FG Picking for Dispatch')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (11, N'TBL_Inventory_Details', N'RM Put way', 1, N'RM Put way')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (12, N'TBL_Inventory_Details', N'RM Picking', 2, N'RM Picking')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (13, N'TBL_Inventory_Details', N'SF Roll Put way', 1, N'SF Roll Put way')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (14, N'TBL_Inventory_Details', N'SF Picking', 2, N'SF Picking')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (15, N'TBL_Inventory_Details', N'SF Roll Issuance on Filling Machine', 3, N'SF Roll Issuance on Filling Machine')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (16, N'TBL_Inventory_Details', N'FG Put way', 1, N'FG Put way')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (17, N'TBL_Inventory_Details', N'FG Picking for Dispatch', 2, N'FG Picking for Dispatch')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (18, N'TBL_PickList_Detail', N'RM Picking', 1, N'RM Picking')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (19, N'TBL_PickList_Detail', N'SF Roll Printing', 2, N'SF Roll Printing')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (20, N'TBL_BarcodePrinting', N'SF Roll Printing', 4, N'SF Roll Printing')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (21, N'TBL_PickList_Header', N'Picklist generate', 1, N'Picklist generate')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (22, N'TBL_PickList_Header', N'Picking', 2, N'Picking')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (23, N'TBL_PickList_Header', N'Picking completed', 3, N'Picking completed')
INSERT [dbo].[TBL_StatusMaster] ([ID], [TableName], [ProcessName], [Status], [Description]) VALUES (24, N'TBL_PickList_Header', N'FG Picked Picklist Approved', 4, N'FG Picked Picklist Approved')
SET IDENTITY_INSERT [dbo].[TBL_StatusMaster] OFF
SET IDENTITY_INSERT [dbo].[TBL_UserMaster] ON 

INSERT [dbo].[TBL_UserMaster] ([ID], [UserID], [UserName], [Password], [GroupName], [Sitecode], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (5, N'12', N'aa', N'2', N'User', NULL, 1, CAST(N'2021-09-23 12:57:16.473' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[TBL_UserMaster] ([ID], [UserID], [UserName], [Password], [GroupName], [Sitecode], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'2', N'23', N'2', N'User', NULL, 0, CAST(N'2021-03-02 16:36:46.630' AS DateTime), N'1', CAST(N'2021-10-05 14:21:28.027' AS DateTime), NULL)
INSERT [dbo].[TBL_UserMaster] ([ID], [UserID], [UserName], [Password], [GroupName], [Sitecode], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (3, N'aa', N'aa', N'a', N'User', NULL, 0, CAST(N'2021-06-23 12:34:24.010' AS DateTime), N'1', CAST(N'2021-06-23 12:36:02.630' AS DateTime), NULL)
INSERT [dbo].[TBL_UserMaster] ([ID], [UserID], [UserName], [Password], [GroupName], [Sitecode], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (4, N'aads', N'aa', N'12', N'Admin', NULL, 0, CAST(N'2021-07-07 10:29:50.140' AS DateTime), N'1', CAST(N'2021-07-07 10:29:56.323' AS DateTime), NULL)
INSERT [dbo].[TBL_UserMaster] ([ID], [UserID], [UserName], [Password], [GroupName], [Sitecode], [IsActive], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Admin', N'1', N'1', N'Admin', NULL, 1, CAST(N'2020-03-02 09:53:02.023' AS DateTime), N'admin', NULL, NULL)
SET IDENTITY_INSERT [dbo].[TBL_UserMaster] OFF
INSERT [dbo].[TBL_Version] ([AppName], [App_Version]) VALUES (N'PC', N'1.0.0.0')
INSERT [dbo].[TBL_Version] ([AppName], [App_Version]) VALUES (N'Device', N'1.0.0.0')
INSERT [dbo].[tbShiftMaster] ([ShiftCode], [ShiftName], [ShiftFrom], [ShiftTo]) VALUES (N'1', N'A', N'08:00:00', N'16:30:59')
INSERT [dbo].[tbShiftMaster] ([ShiftCode], [ShiftName], [ShiftFrom], [ShiftTo]) VALUES (N'2', N'B', N'16:31:00', N'00:00:59')
INSERT [dbo].[tbShiftMaster] ([ShiftCode], [ShiftName], [ShiftFrom], [ShiftTo]) VALUES (N'3', N'C', N'00:01:00', N'07:59:59')
/****** Object:  StoredProcedure [dbo].[PRC_AEP_PRINTNG]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PRC_AEP_PRINTNG]
@TYPE VARCHAR(100)=NULL,
@PART_NO VARCHAR(100)=NULL,
@QTY NUMERIC(18,0)=NULL,
@CREATED_BY VARCHAR(100)=NULL,
@PRINTER_IP VARCHAR(100)=NULL,
@CUSTOMER VARCHAR(100)=NULL
AS
BEGIN
    DECLARE @BARCODE VARCHAR(100)=NULL,
	        @SERIAL_NO VARCHAR(100)=NULL,
		    @SHIFT VARCHAR(10)=NULL,
			@LINE VARCHAR(100)=NULL,
			@GET_CUSTOMER VARCHAR(100)=NULL,
			@CREATED_ON DATETIME=NULL,
			@DATE NVARCHAR(10)=NULL,
			@PART_NAME VARCHAR(100)=NULL
    IF @TYPE='BIND_MODEL'
	  BEGIN
	    SELECT @LINE=Line FROM TBL_PrinterLine_Mapping  WHERE PrinterIp=@PRINTER_IP

		  SELECT distinct MST.InternalPartNo FROM TBL_PartMaster MST JOIN TBL_LINE_PART_MAPPING MP on MST.InternalPartNo=MP.InternalPartNo
		  WHERE MP.Line=@LINE AND MST.CustomerCode=@CUSTOMER
	  END
	 IF @TYPE='GET_QTY'
	  BEGIN
	    SELECT @LINE=Line FROM TBL_PrinterLine_Mapping  WHERE PrinterIp=@PRINTER_IP

		  SELECT distinct MST.PrintQty FROM TBL_PartMaster MST JOIN TBL_LINE_PART_MAPPING MP on MST.InternalPartNo=MP.InternalPartNo
		  WHERE MP.Line=@LINE AND MST.CustomerCode=@CUSTOMER AND MST.InternalPartNo=@PART_NO
	  END
	IF @TYPE='SAVE'
	  BEGIN
	       SET @GET_CUSTOMER=''
	       SET @SERIAL_NO=''
		   SET @BARCODE=''
		   SELECT @CREATED_ON=GETDATE() 
		   SELECT @DATE=REPLACE(CONVERT(varchar,@CREATED_ON,103),'/','.')
		   SELECT @GET_CUSTOMER=CustomerCode FROM TBL_PartMaster WHERE InternalPartNo=@PART_NO
		   SELECT @PART_NAME=InternalPartName FROM TBL_PartMaster WHERE InternalPartNo=@PART_NO
	       EXECUTE PRC_GetBarcode @PART_NO,@BARCODE output;
		    SET @SHIFT=(SELECT dbo.GetShiftTime(convert(TIME(7),getdate())))
		SELECT @SERIAL_NO=LEFT(@BARCODE,4)
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
				,@CREATED_ON)
			IF @GET_CUSTOMER='AL'
			 BEGIN
			   SELECT 'Y' As RESULT,@BARCODE As BARCODE,@PART_NAME AS PART_NAME,@SERIAL_NO AS SERIAL_NO,@DATE AS MFG_DATE
			 END
			ELSE
			 BEGIN
			   SELECT 'Y' As RESULT,@BARCODE As BARCODE
			 END
	  END
END

GO
/****** Object:  StoredProcedure [dbo].[PRC_BarcodeGenerationIndex]    Script Date: 13-03-2022 13:43:37 ******/
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
		FROM TBL_Fields --where InternalPartNo=@AISPartNo
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
/****** Object:  StoredProcedure [dbo].[PRC_BIND_COMBO]    Script Date: 13-03-2022 13:43:37 ******/
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
/****** Object:  StoredProcedure [dbo].[PRC_GET_VERSION]    Script Date: 13-03-2022 13:43:37 ******/
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
/****** Object:  StoredProcedure [dbo].[PRC_GetBarcode]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PRC_GetBarcode]
    @AISPartNo varchar(50) = NULL,
	@RESULT VARCHAR(MAX) OUTPUT,
	@SERIAL VARCHAR(100) OUTPUT
AS
DECLARE @MaxCount int
DECLARE @Count int
DECLARE @Barcode VARCHAR(100),@RunningSerialNo varchar(20)='',@MFD varchar(20)='',@MFDValue varchar(20)='',@SerialLen int=0,@RunningNo varchar(20)='0',@separator varchar(1)=''
BEGIN
		        SET @Count = 0
				SET @Barcode = ''
				SET @MaxCount = (SELECT max(ValueIndex) from TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo)
				SET @separator = (SELECT top 1 isnull(Separator,'') from TBL_PartMaster WHERE InternalPartNo=@AISPartNo)
				WHILE @Count<=@MaxCount
				 BEGIN
					IF @Barcode!=''
						SET @Barcode=@Barcode + (SELECT @separator+FieldValue FROM TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo and ValueIndex=@Count)
					ELSE
						SET @Barcode=(SELECT FieldValue FROM TBL_BarcodeIndex WHERE AISPartNo=@AISPartNo and ValueIndex=@Count)
					SET @Count=@Count+1
				END

				 SET @MFD=(select FieldValue from TBL_BarcodeIndex where AISPartNo=@AISPartNo and FieldName='MfgFormat')
				 SET @SerialLen=(select ValueLength from TBL_BarcodeIndex where AISPartNo=@AISPartNo and FieldName='SerialNo')

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

				 EXECUTE PRC_GetRunningSerialLotWise @AISPartNo,@MFDValue,@RunningSerialNo output

				 SET @RunningNo=(select RIGHT(REPLACE(STR(@RunningSerialNo),' ','0'),@SerialLen))

				 SET @Barcode=REPLACE(@Barcode,'SerialNo',@RunningNo)--+@RunningSerialNo
				 
				 SET @Barcode=(SELECT REPLACE(@Barcode ,@MFD,@MFDValue))

				 SET @RESULT=Upper((@Barcode))
				 SET @SERIAL=@RunningNo
END

GO
/****** Object:  StoredProcedure [dbo].[PRC_GetRunningSerial]    Script Date: 13-03-2022 13:43:37 ******/
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
/****** Object:  StoredProcedure [dbo].[PRC_GetRunningSerialLotWise]    Script Date: 13-03-2022 13:43:37 ******/
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
                 
           SET @RESPONSE= CAST(@SERIAL AS VARCHAR(20))--RIGHT('000000'+CAST(@SERIAL AS VARCHAR(20)),6)
         
   
                   
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
/****** Object:  StoredProcedure [dbo].[PRC_GroupMaster]    Script Date: 13-03-2022 13:43:37 ******/
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
/****** Object:  StoredProcedure [dbo].[PRC_Pallet_Mapping]    Script Date: 13-03-2022 13:43:37 ******/
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
 -- SELECT DISTINCT AISPartNo FROM TBL_BarcodeIndex ORDER BY AISPartNo
   SELECT DISTINCT InternalPartNo AS AISPartNo FROM TBL_PartMaster WHERE ISNULL(IsQAEnable,'Y')='Y'
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
/****** Object:  StoredProcedure [dbo].[PRC_Pallet_Mapping_19092021]    Script Date: 13-03-2022 13:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROC [dbo].[PRC_Pallet_Mapping_19092021]
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
				    SET @PalletQty=(SELECT top 1 Packsize FROM  TBL_PartMaster WHERE InternalPartNo=@PartNo)
					SET @GET_TOTAL_QTY=(SELECT top 1 PalletQty FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False') 
					
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
						   ,(case when @GET_TOTAL_QTY=0 then @PalletQty else @GET_TOTAL_QTY end)
						   ,GETDATE()
						   ,@CREATED_BY)
			END

			 UPDATE TBL_BarcodePrinting SET [Status]='2' WHERE Barcode=@Barcode

			 SET @GET_SCAN_QTY=(SELECT Count(*) from TBL_Pallet_Mapping where PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False')
			 SET @GET_TOTAL_QTY=(SELECT top 1 PalletQty FROM  TBL_Pallet_Mapping WHERE PartNo=@PartNo AND PalletNo=@PalletNo and isnull(IsComplete,'False')='False') 

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
/****** Object:  StoredProcedure [dbo].[PRC_PartMaster]    Script Date: 13-03-2022 13:43:37 ******/
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
@CustomerCode varchar(50)	=NULL,
@IsQAEnable varchar(1)='Y',
@Separator varchar(1)='',
@CreatedBy	varchar(20)	=NULL,
@PrintQty int =0

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
			isnull(CustomerCode,'') CustomerCode,
			isnull(IsQAEnable,'Y') IsQAEnable,
			isnull(Separator,'') Separator,
			isnull(PrintQty,1) PrintQty,
			CreatedBy,
			CreatedOn
			from TBL_PartMaster
	  END

	 IF @TYPE='GETCUSTOMERCODE'
	  BEGIN
	     SELECT CustomerCode
			from TBL_Customer
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
			CustomerCode,
			IsQAEnable,
			Separator,
			PrintQty,
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
			@CustomerCode,
			@IsQAEnable,
			@Separator,
			@PrintQty,
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
			CustomerCode,
			IsQAEnable,
			Separator,
			PrintQty,
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
			CustomerCode,
			IsQAEnable,
			Separator,
			PrintQty,
			@CreatedBy,
			getdate(),
		    'Update' from TBL_PartMaster where InternalPartNo=@InternalPartNo and CustomerPartNo=@CustomerPartNo)
	      
			 UPDATE TBL_PartMaster
		     SET InternalPartName=@InternalPartName
			,CustomerPartName=@CustomerPartName
			,VendorCode=@VendorCode
			,VendorName=@VendorName
			,PackSize=@PackSize
			,CustomerCode=@CustomerCode
			,IsQAEnable=@IsQAEnable
			,Separator=@Separator
			,PrintQty=@PrintQty
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
			CustomerCode,
			IsQAEnable,
			Separator,
			PrintQty,
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
			CustomerCode,
			IsQAEnable,
			Separator,
			PrintQty,
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
/****** Object:  StoredProcedure [dbo].[PRC_PrintBarcode]    Script Date: 13-03-2022 13:43:37 ******/
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
/****** Object:  StoredProcedure [dbo].[PRC_REPORT]    Script Date: 13-03-2022 13:43:37 ******/
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
	       SELECT ROW_NUMBER() OVER(ORDER BY PrintedOn asc) AS ''SN'',PartNo ''Part No'',Barcode ''Barcode'',(case when [Status]=1 then ''Not Mapped'' when [Status]=2 then ''Mapped'' end )'' Status'',PrintedOn ''Printed On'',PrintedBy ''Printed By''  from TBL_BarcodePrinting  WHERE CONVERT(VARCHAR(10),PrintedOn,121) BETWEEN '''+@FROM_DATE+''' AND '''+@TO_DATE+''''
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
/****** Object:  StoredProcedure [dbo].[PRC_SET_CHILDSEC_RIGHTS]    Script Date: 13-03-2022 13:43:37 ******/
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
/****** Object:  StoredProcedure [dbo].[PRC_UserMaster]    Script Date: 13-03-2022 13:43:37 ******/
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
/****** Object:  StoredProcedure [dbo].[PRS_BarcodeStatus]    Script Date: 13-03-2022 13:43:37 ******/
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
