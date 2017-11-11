GO

-- =========================================
-- Create "Player" table
-- =========================================

IF OBJECT_ID('dbo.Player', 'U') IS NOT NULL
  DROP TABLE [dbo].[Player]
GO

CREATE TABLE [dbo].[Player](
	[Id] [uniqueidentifier] NOT NULL,
	[Nickname] [nvarchar](MAX) NOT NULL,
	[WotId] [nvarchar](MAX) NOT NULL,
	[Lang] [nvarchar](MAX) NOT NULL
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

-- =========================================
-- Create "PlayerFrameData" table
-- =========================================

IF OBJECT_ID('dbo.PlayerFrameData', 'U') IS NOT NULL
  DROP TABLE [dbo].[PlayerFrameData]
GO

CREATE TABLE [dbo].[PlayerFrameData](
	[Id] [uniqueidentifier] NOT NULL,
	[PlayerId] [uniqueidentifier] NOT NULL,
	[FrameId] [uniqueidentifier] NOT NULL,
	[Json] [nvarchar](MAX) NOT NULL,
	[DateTime] [datetime2] NOT NULL DEFAULT GETDATE()
 CONSTRAINT [PK_PlayerFrameData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO