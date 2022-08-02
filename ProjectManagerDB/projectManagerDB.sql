USE [master]
GO
/****** Object:  Database [ProjectManager_Test]    Script Date: 8/1/2022 9:19:11 PM ******/
CREATE DATABASE [ProjectManager_Test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjectManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ProjectManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjectManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ProjectManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ProjectManager_Test] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjectManager_Test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjectManager_Test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectManager_Test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjectManager_Test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjectManager_Test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjectManager_Test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET RECOVERY FULL 
GO
ALTER DATABASE [ProjectManager_Test] SET  MULTI_USER 
GO
ALTER DATABASE [ProjectManager_Test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjectManager_Test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjectManager_Test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjectManager_Test] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjectManager_Test] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProjectManager_Test] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProjectManager_Test', N'ON'
GO
ALTER DATABASE [ProjectManager_Test] SET QUERY_STORE = OFF
GO
USE [ProjectManager_Test]
GO
/****** Object:  Table [dbo].[ACTION_STATUSES]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACTION_STATUSES](
	[as_id] [int] IDENTITY(1,1) NOT NULL,
	[as_desc] [varchar](16) NOT NULL,
 CONSTRAINT [PK_ACTION_STATUSES] PRIMARY KEY CLUSTERED 
(
	[as_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ACTIONS]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACTIONS](
	[a_id] [int] IDENTITY(1,1) NOT NULL,
	[a_desc] [varchar](255) NULL,
	[a_priority] [int] NOT NULL,
	[a_projectId] [int] NOT NULL,
	[a_ownerId] [int] NOT NULL,
	[a_statusId] [int] NOT NULL,
	[a_resolution] [varchar](255) NULL,
	[a_insertDate] [datetime] NOT NULL,
	[a_openDate] [datetime] NOT NULL,
	[a_dueDate] [datetime] NULL,
	[a_closedDate] [datetime] NULL,
	[a_lastUpdated] [datetime] NOT NULL,
	[a_isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ACTIONS] PRIMARY KEY CLUSTERED 
(
	[a_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DEPARTMENTS]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DEPARTMENTS](
	[d_id] [int] IDENTITY(1,1) NOT NULL,
	[d_name] [varchar](30) NOT NULL,
	[d_desc] [varchar](50) NULL,
	[d_ownerId] [int] NOT NULL,
	[d_insertDate] [datetime] NOT NULL,
	[d_lastUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_DEPARTMENTS] PRIMARY KEY CLUSTERED 
(
	[d_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OWNER_STATUSES]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OWNER_STATUSES](
	[os_id] [int] IDENTITY(1,1) NOT NULL,
	[os_desc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_OWNER_STATUSES] PRIMARY KEY CLUSTERED 
(
	[os_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OWNERS]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OWNERS](
	[o_id] [int] IDENTITY(1,1) NOT NULL,
	[o_firstName] [varchar](30) NOT NULL,
	[o_lastName] [varchar](30) NOT NULL,
	[o_deptId] [int] NOT NULL,
	[o_statusId] [int] NOT NULL,
	[o_insertDate] [datetime] NOT NULL,
	[o_lastUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_OWNERS] PRIMARY KEY CLUSTERED 
(
	[o_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRIORITY]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRIORITY](
	[pr_id] [int] IDENTITY(1,1) NOT NULL,
	[pr_desc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PRIORITY] PRIMARY KEY CLUSTERED 
(
	[pr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PROJECT_CATEGORIES]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROJECT_CATEGORIES](
	[pc_id] [int] IDENTITY(1,1) NOT NULL,
	[pc_desc] [varchar](255) NOT NULL,
 CONSTRAINT [PK_PROJECT_CATEGORIES] PRIMARY KEY CLUSTERED 
(
	[pc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PROJECT_STATUSES]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROJECT_STATUSES](
	[ps_id] [int] IDENTITY(1,1) NOT NULL,
	[ps_desc] [varchar](30) NOT NULL,
 CONSTRAINT [PK_PROJECT_STATUSES] PRIMARY KEY CLUSTERED 
(
	[ps_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PROJECTS]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROJECTS](
	[p_id] [int] IDENTITY(1,1) NOT NULL,
	[p_name] [varchar](32) NOT NULL,
	[p_desc] [varchar](255) NULL,
	[p_insertDate] [datetime] NOT NULL,
	[p_lastUpdated] [datetime] NOT NULL,
	[p_statusId] [int] NOT NULL,
	[p_ownerId] [int] NOT NULL,
	[p_startDate] [datetime] NULL,
	[p_endDate] [datetime] NULL,
	[p_dueDate] [datetime] NULL,
	[p_categoryId] [int] NOT NULL,
	[p_isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_PROJECTS] PRIMARY KEY CLUSTERED 
(
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ACTIONS]  WITH CHECK ADD  CONSTRAINT [FK_ACTIONS_OWNER] FOREIGN KEY([a_ownerId])
REFERENCES [dbo].[OWNERS] ([o_id])
GO
ALTER TABLE [dbo].[ACTIONS] CHECK CONSTRAINT [FK_ACTIONS_OWNER]
GO
ALTER TABLE [dbo].[ACTIONS]  WITH CHECK ADD  CONSTRAINT [FK_ACTIONS_PRIORITY] FOREIGN KEY([a_priority])
REFERENCES [dbo].[PRIORITY] ([pr_id])
GO
ALTER TABLE [dbo].[ACTIONS] CHECK CONSTRAINT [FK_ACTIONS_PRIORITY]
GO
ALTER TABLE [dbo].[ACTIONS]  WITH CHECK ADD  CONSTRAINT [FK_ACTIONS_PROJECT] FOREIGN KEY([a_projectId])
REFERENCES [dbo].[PROJECTS] ([p_id])
GO
ALTER TABLE [dbo].[ACTIONS] CHECK CONSTRAINT [FK_ACTIONS_PROJECT]
GO
ALTER TABLE [dbo].[ACTIONS]  WITH CHECK ADD  CONSTRAINT [FK_ACTIONS_STATUSES] FOREIGN KEY([a_statusId])
REFERENCES [dbo].[ACTION_STATUSES] ([as_id])
GO
ALTER TABLE [dbo].[ACTIONS] CHECK CONSTRAINT [FK_ACTIONS_STATUSES]
GO
ALTER TABLE [dbo].[PROJECTS]  WITH CHECK ADD  CONSTRAINT [CK_PROJECTS_startdate] CHECK  (([p_startDate] IS NULL OR [p_startDate]<=getdate() OR [p_startDate]<=[p_endDate]))
GO
ALTER TABLE [dbo].[PROJECTS] CHECK CONSTRAINT [CK_PROJECTS_startdate]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteAction]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 9/1/2022
-- Description:	This stored procedure deletes the action
-- with the specified identifier.
-- =============================================
CREATE PROCEDURE [dbo].[usp_DeleteAction]
	@actionId INT
AS
BEGIN
	SET NOCOUNT ON;

	IF(ISNULL(@actionId, 0) = 0)
	BEGIN
		RAISERROR('Invalid parameter: @actionId cannot be null.', 18, 0)
		RETURN
	END
	IF (@actionId < 1)
	BEGIN
		RAISERROR('Invalid parameter: @actionId cannot be less than 1.', 18, 0)
		RETURN
	END

    UPDATE [dbo].[ACTIONS]
	SET a_isDeleted = 1
	WHERE a_id = @actionId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteProject]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 1/9/2022
-- Description:	This stored procedure deletes a project from the dbo.PROJECTS table.
-- =============================================
CREATE PROCEDURE [dbo].[usp_DeleteProject]
	@projectId INT
AS
BEGIN
	SET NOCOUNT ON;

	IF(ISNULL(@projectId, 0) = 0)
	BEGIN
		RAISERROR('Invalid parameter: @@projectId cannot be null.', 18, 0)
		RETURN
	END
	IF (@projectId < 1)
	BEGIN
		RAISERROR('Invalid parameter: @@projectId cannot be less than 1.', 18, 0)
		RETURN
	END

	UPDATE [dbo].[PROJECTS]
	SET p_isDeleted = 1
	WHERE p_id = @projectId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetActionPriorities]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 7/27/2022
-- Description:	This stored procedure queries
-- all action priorities.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetActionPriorities]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		p.pr_id, 
		p.pr_desc 
	FROM dbo.PRIORITY p
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetActions]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 7/29/2022
-- Description:	This stored procedure queries 
-- Actions using the specified search criteria.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetActions]
	@ownerId INT,
	@description VARCHAR(MAX) = NULL
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		a_id AS ActionId,
		a_desc AS ActionDescription,
		a_openDate AS ActionOpenDate,
		a_closedDate AS ActionClosedDate,
		a_dueDate AS ActionDueDate,
		a_ownerId AS ActionOwnerId,
		a_priority AS ActionPriorityId,
		a_projectId AS ActionProjectId,
		a_resolution AS ActionResultion,
		a_statusId AS ActionStatusId,
		o.o_firstName AS ActionOwnerFirstName,
		o.o_lastName AS ActionOwnerLastName
	FROM dbo.ACTIONS a
	JOIN dbo.OWNERS o ON o.o_id = a.a_ownerId
	WHERE
		a_ownerId = @ownerId AND
		a.a_desc LIKE CASE WHEN @description IS NOT NULL THEN '%' + @description + '%' ELSE '%' END AND
		a.a_isDeleted = 0

	-- TODO: dynamic select
	/*
	-- dynamic SQL for conditional WHERE
	DECLARE @sql NVARCHAR(MAX);

	SET @sql = N'SELECT * FROM dbo.ACTIONS a WHERE 1 = 1 '
		+ CASE WHEN @dateOpened IS NOT NULL THEN N'AND a.a_openDate = @dateOpened ' END
		+ CASE WHEN @dateClosed IS NOT NULL THEN N'AND a.a_closedDate = @dateClosed ' END
		+ CASE WHEN @dateDue IS NOT NULL THEN N'AND a.a_dueDate = @dateClosed ' END
		+ CASE WHEN @ownerId IS NOT NULL THEN N'AND a.a_ownerId = @ownerId ' END
		+ CASE WHEN @description IS NOT NULL THEN N'AND a.a_desc = @description ' END
		+ CASE WHEN @resolution IS NOT NULL THEN N'AND a.a_resolution = @resolution ' END
		+ CASE WHEN @priorityId IS NOT NULL THEN N'AND a.a_priorityId = @priorityId ' END
		+ CASE WHEN @statusId IS NOT NULL THEN N'AND a.a_statusId = @statusId ' END
		+ CASE WHEN @projectId IS NOT NULL THEN N'AND a.a_projectId = @projectId ' END

	EXEC sp_executesql @Sql
                    ,N'@dateOpened DATETIME
					,@dateClosed DATETIME
					,@dateDue DATETIME
					,@ownerId INT
					,@description VARCHAR(MAX)
					,@resolution VARCHAR(MAX)
					,@priorityId INT
					,@statusId INT
					,@projectId INT'
                    ,@dateOpened
					,@dateClosed
					,@dateDue
					,@ownerId
					,@description
					,@resolution
					,@priorityId
					,@statusId
					,@projectId
	*/
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetActionStatuses]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 7/27/2022
-- Description:	This stored procedure queries
-- all action statuses.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetActionStatuses]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM dbo.ACTION_STATUSES
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetActiveActionOwners]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 7/27/2022
-- Description:	This stored procedure queries
-- all active project action owners.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetActiveActionOwners]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM dbo.ACTIONS)
	BEGIN
		SELECT DISTINCT 
			o.o_id,
			o.o_firstName,
			o.o_lastName
		FROM dbo.OWNERS o
		WHERE o.o_statusId = 1
	END
	ELSE
	BEGIN
		SELECT DISTINCT
			a.a_ownerId,
			o.o_firstName,
			o.o_lastName
		FROM dbo.ACTIONS a
		JOIN OWNERS o ON o.o_id = a.a_ownerId
		WHERE o.o_statusId = 1
	END
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetActiveProjectOwners]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 1/1/2022
-- Description:	This stored procedure gets all active project owners.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetActiveProjectOwners]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		o_id AS OwnerId,
		o_lastName AS OwnerLastName,
		o_firstName AS OwnerFirstName
	FROM dbo.OWNERS
	WHERE o_statusId = 1 -- active
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllProjectsForUser]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 12/21/2021
-- Description:	This stored procedure queries all 
-- projects tied to the specified user identifier.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetAllProjectsForUser] 
	@userId INT
AS
BEGIN
	SET NOCOUNT ON;

	IF (ISNULL(@userId, 0) = 0)
	BEGIN
		RAISERROR('Invalid parameter: @userId cannot be NULL.', 18, 0)
		RETURN
	END
	IF (@userId < 1)
	BEGIN
		RAISERROR('Invalid parameter: @userId cannot be less than 1.', 18, 0)
		RETURN
	END

	SELECT 
		p.p_id AS ProjectId,
		p.p_name AS ProjectName,
		p.p_desc AS ProjectDescription,
		p.p_startDate AS ProjectStartDate,
		p.p_endDate AS ProjectEndDate,
		p.p_dueDate AS ProjectDueDate,
		ps.ps_desc AS ProjectStatus,
		o.o_firstName AS ProjectOwnerFirstName,
		o.o_lastName AS ProjectOwnerLastName
	FROM dbo.PROJECTS p
	INNER JOIN dbo.PROJECT_STATUSES ps ON p.p_statusId = ps.ps_id
	INNER JOIN dbo.OWNERS o ON p.p_ownerId = o.o_id
	WHERE 
		p_ownerId = @userId AND
		p_isDeleted = 0
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetProjectCategories]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 1/1/2022
-- Description:	This stored procedure gets all available project categories.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetProjectCategories]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		pc_id AS CategoryId,
		pc_desc AS CategoryDescription
	FROM PROJECT_CATEGORIES
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetProjectStatuses]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 1/1/2022
-- Description:	This stored procedure queries all available project statuses.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetProjectStatuses]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		ps_id AS ProjectId, 
		ps_desc AS ProjectDescription
	FROM dbo.PROJECT_STATUSES
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetRecentActiveActions]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 7/25/2022
-- Description:	This stored procedure queries the 
-- specified number of active project actions.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetRecentActiveActions]
	@actionCount INT
AS
BEGIN
	SET NOCOUNT ON;

	IF (ISNULL(@actionCount, 0) = 0)
	BEGIN
		RAISERROR('Invalid parameter: @actionCount cannot be NULL.', 18, 0)
		RETURN
	END
	IF (@actionCount < 1)
	BEGIN
		RAISERROR('Invalid parameter: @actionCount cannot be less than 1.', 18, 0)
		RETURN
	END

	SELECT TOP (@actionCount)
		a.a_id AS ActionId,
		a.a_desc AS ActionDescription,
		a.a_priority AS ActionPriority,
		a.a_projectId AS ActionProjectId,
		a.a_openDate AS ActionOpenDate,
		a.a_closedDate AS ActionCloseDate,
		a.a_dueDate AS ActionDueDate,
		a.a_statusId AS ActionStatusId,
		a.a_resolution AS ActionResolution,
		o.o_firstName AS ActionOwnerFirstName,
		o.o_lastName AS ActionOwnerLastName
	FROM dbo.ACTIONS a
	INNER JOIN dbo.ACTION_STATUSES statuses ON statuses.as_id = a.a_statusId
	INNER JOIN dbo.OWNERS o ON a.a_ownerId = o.o_id
	WHERE 
		a.a_statusId IN (1,2) AND
		a.a_isDeleted = 0
	ORDER BY a.a_openDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetRecentActiveProjects]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		RJ Czajka
-- Create date: 7/23/2022
-- Description:	This stored procedure queries the
-- most recent active projects.
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetRecentActiveProjects] 
	@projectCount INT
AS
BEGIN
	SET NOCOUNT ON;

	IF (ISNULL(@projectCount, 0) = 0)
	BEGIN
		RAISERROR('Invalid parameter: @projectCount cannot be NULL.', 18, 0)
		RETURN
	END
	IF (@projectCount < 1)
	BEGIN
		RAISERROR('Invalid parameter: @projectCount cannot be less than 1.', 18, 0)
		RETURN
	END

	SELECT TOP (@projectCount)
		p.p_id AS ProjectId,
		p.p_name AS ProjectName,
		p.p_desc AS ProjectDescription,
		p.p_startDate AS ProjectStartDate,
		p.p_endDate AS ProjectEndDate,
		p.p_dueDate AS ProjectDueDate,
		ps.ps_desc AS ProjectStatus,
		o.o_firstName AS ProjectOwnerFirstName,
		o.o_lastName AS ProjectOwnerLastName
	FROM dbo.PROJECTS p
	INNER JOIN dbo.PROJECT_STATUSES ps ON p.p_statusId = ps.ps_id
	INNER JOIN dbo.OWNERS o ON p.p_ownerId = o.o_id
	WHERE 
		ps.ps_id = 1 AND
		p.p_isDeleted = 0
	ORDER BY p.p_startDate DESC
END
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertAction]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 7/28/2022
-- Description:	This stored procedure inserts
-- a new Action.
-- =============================================
CREATE PROCEDURE [dbo].[usp_InsertAction]
	@dateOpened DATETIME,
	@dateClosed DATETIME = NULL,
	@dateDue DATETIME = NULL,
	@ownerId INT,
	@description VARCHAR(MAX) = NULL,
	@resolution VARCHAR(MAX) = NULL,
	@priorityId INT,
	@statusId INT,
	@projectId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @dateOpened IS NULL
	BEGIN
		SET @dateOpened = GETDATE()
	END

    INSERT INTO dbo.ACTIONS
	VALUES
	(
		@description,
		@priorityId,
		@projectId,
		@ownerId,
		@statusId,
		@resolution,
		GETDATE(),
		@dateOpened,
		@dateDue,
		@dateClosed,
		GETDATE(),
		0
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertProject]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 1/1/2022
-- Description:	This stored procedure inserts a new project to the dbo.PROJECTS table.
-- =============================================
CREATE PROCEDURE [dbo].[usp_InsertProject]	
	@name VARCHAR(32),
	@description VARCHAR(255) = NULL,
	@statusId INT,
	@ownerId INT,
	@startDate DATETIME = NULL,
	@endDate DATETIME = NULL,
	@dueDate DATETIME = NULL,
	@categoryId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO dbo.PROJECTS
	VALUES (
		@name,
		@description,
		GETDATE(),
		GETDATE(),
		@statusId,
		@ownerId,
		@startDate,
		@endDate,
		@dueDate,
		@categoryId,
		0
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SearchProjectsForOwner]    Script Date: 8/1/2022 9:19:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RJ Czajka
-- Create date: 1/11/2022
-- Description:	This stored procedure gets all projects based on the specified criteria.
-- =============================================
CREATE PROCEDURE [dbo].[usp_SearchProjectsForOwner]
	@name VARCHAR(32) = NULL,
	@description VARCHAR(255) = NULL,
	@statusId INT = NULL,
	@ownerId INT,
	@startDate DATETIME = NULL,
	@endDate DATETIME = NULL,
	@dueDate DATETIME = NULL,
	@categoryId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF (@ownerId < 1)
	BEGIN
		RAISERROR('Invalid parameter: @ownerId cannot be less than 1.', 18, 0)
		RETURN
	END
	
    SELECT
		p.p_id AS ProjectId,
		p.p_name AS ProjectName,
		p.p_desc AS ProjectDesc,
		p.p_startDate AS ProjectStartDate,
		p.p_endDate AS ProjectEndDate,
		p.p_dueDate AS ProjectDueDate,
		ps.ps_desc AS ProjectStatus,
		o.o_firstName AS ProjectOwnerFirstName,
		o.o_lastName AS ProjectOwnerLastName
	FROM dbo.PROJECTS p
	INNER JOIN dbo.PROJECT_STATUSES ps ON p.p_statusId = ps.ps_id
	INNER JOIN dbo.OWNERS o ON p.p_ownerId = o.o_id
	WHERE
		p.p_ownerId = @ownerId AND
		p.p_name LIKE CASE WHEN @name IS NOT NULL THEN '%' + @name + '%' ELSE '%' END AND
		p.p_isDeleted = 0
END
GO
USE [master]
GO
ALTER DATABASE [ProjectManager_Test] SET  READ_WRITE 
GO
