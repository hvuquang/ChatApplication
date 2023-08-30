USE [chatDB]
GO
/****** Object:  Table [dbo].[Background]    Script Date: 6/29/2023 11:38:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Background](
	[id_bg] [int] IDENTITY(1,1) NOT NULL,
	[id_nguoi1] [int] NULL,
	[id_nguoi2] [int] NULL,
	[id_group] [int] NULL,
	[hinh_anh] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_bg] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 6/29/2023 11:38:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[id_nhom] [int] IDENTITY(1,1) NOT NULL,
	[id_nguoi_tao] [int] NULL,
	[id_thanh_vien] [nvarchar](max) NULL,
	[hinh_anh] [image] NULL,
	[ten_nhom] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_nhom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 6/29/2023 11:38:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SenderUsername] [int] NULL,
	[ReceiverUsername] [int] NULL,
	[MessageText] [nvarchar](max) NULL,
	[SentTime] [datetime] NULL,
	[img] [varbinary](max) NULL,
	[AudioLink] [varchar](max) NULL,
	[FileLink] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageGroup]    Script Date: 6/29/2023 11:38:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageGroup](
	[message_id] [int] IDENTITY(1,1) NOT NULL,
	[content] [nvarchar](255) NULL,
	[sender_id] [int] NULL,
	[group_id] [int] NULL,
	[send_time] [datetime] NULL,
	[AudioLink] [varchar](max) NULL,
	[FileLink] [varchar](max) NULL,
	[img] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[message_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReactionMessage]    Script Date: 6/29/2023 11:38:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReactionMessage](
	[reactID] [int] IDENTITY(1,1) NOT NULL,
	[hearts] [int] NULL,
	[likes] [int] NULL,
	[laughs] [int] NULL,
	[messageID] [int] NULL,
	[mode] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[reactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReactionMessage1]    Script Date: 6/29/2023 11:38:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReactionMessage1](
	[reactID] [int] IDENTITY(1,1) NOT NULL,
	[hearts] [int] NULL,
	[likes] [int] NULL,
	[laughs] [int] NULL,
	[messageID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[reactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userTab]    Script Date: 6/29/2023 11:38:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userTab](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[image] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ReactionMessage] ADD  DEFAULT ((0)) FOR [hearts]
GO
ALTER TABLE [dbo].[ReactionMessage] ADD  DEFAULT ((0)) FOR [likes]
GO
ALTER TABLE [dbo].[ReactionMessage] ADD  DEFAULT ((0)) FOR [laughs]
GO
ALTER TABLE [dbo].[ReactionMessage1] ADD  DEFAULT ((0)) FOR [hearts]
GO
ALTER TABLE [dbo].[ReactionMessage1] ADD  DEFAULT ((0)) FOR [likes]
GO
ALTER TABLE [dbo].[ReactionMessage1] ADD  DEFAULT ((0)) FOR [laughs]
GO
ALTER TABLE [dbo].[ReactionMessage]  WITH CHECK ADD FOREIGN KEY([messageID])
REFERENCES [dbo].[Message] ([ID])
GO
ALTER TABLE [dbo].[ReactionMessage1]  WITH CHECK ADD FOREIGN KEY([messageID])
REFERENCES [dbo].[MessageGroup] ([message_id])
GO
