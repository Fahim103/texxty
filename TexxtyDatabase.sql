USE [master]
GO
/****** Object:  Database [Texxty]    Script Date: 3/13/2020 9:31:30 PM ******/
CREATE DATABASE [Texxty]
 CONTAINMENT = NONE
ALTER DATABASE [Texxty] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Texxty].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Texxty] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Texxty] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Texxty] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Texxty] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Texxty] SET ARITHABORT OFF 
GO
ALTER DATABASE [Texxty] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Texxty] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Texxty] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Texxty] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Texxty] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Texxty] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Texxty] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Texxty] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Texxty] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Texxty] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Texxty] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Texxty] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Texxty] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Texxty] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Texxty] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Texxty] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Texxty] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Texxty] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Texxty] SET  MULTI_USER 
GO
ALTER DATABASE [Texxty] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Texxty] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Texxty] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Texxty] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Texxty] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Texxty] SET QUERY_STORE = OFF
GO
USE [Texxty]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 3/13/2020 9:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[BlogID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](200) NOT NULL,
	[Description] [text] NULL,
	[UrlField] [varchar](max) NOT NULL,
	[Private] [bit] NOT NULL,
	[UserID] [int] NOT NULL,
	[TopicID] [int] NOT NULL,
	[ViewCount] [int] NOT NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[BlogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogFollow]    Script Date: 3/13/2020 9:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogFollow](
	[BlogFollowID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_BlogFollow] PRIMARY KEY CLUSTERED 
(
	[BlogFollowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogTopic]    Script Date: 3/13/2020 9:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogTopic](
	[BlogTopicID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
 CONSTRAINT [PK_BlogTopic] PRIMARY KEY CLUSTERED 
(
	[BlogTopicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 3/13/2020 9:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[PublishedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[UrlField] [varchar](max) NOT NULL,
	[Draft] [bit] NOT NULL,
	[PostContent] [text] NOT NULL,
	[BlogID] [int] NOT NULL,
	[ViewCount] [int] NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopicFollow]    Script Date: 3/13/2020 9:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopicFollow](
	[TopicFollowID] [int] IDENTITY(1,1) NOT NULL,
	[TopicID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_TopicFollow] PRIMARY KEY CLUSTERED 
(
	[TopicFollowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/13/2020 9:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[FullName] [varchar](150) NOT NULL,
	[ActiveStatus] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Blog] ON 

INSERT [dbo].[Blog] ([BlogID], [Title], [Description], [UrlField], [Private], [UserID], [TopicID], [ViewCount]) VALUES (1, N'Test blog Fahim', N'This is a test blog', N'test-blog-fahim', 1, 7, 1, 0)
INSERT [dbo].[Blog] ([BlogID], [Title], [Description], [UrlField], [Private], [UserID], [TopicID], [ViewCount]) VALUES (2, N'Blog 2', N'taiosa', N'blog-2', 0, 7, 2, 0)
INSERT [dbo].[Blog] ([BlogID], [Title], [Description], [UrlField], [Private], [UserID], [TopicID], [ViewCount]) VALUES (4, N'Fahim2 Blog', N'Fahim2 Blog', N'fahim2-blog', 0, 9, 12, 0)
INSERT [dbo].[Blog] ([BlogID], [Title], [Description], [UrlField], [Private], [UserID], [TopicID], [ViewCount]) VALUES (5, N'Hello Blog', N'Hello Blog', N'hello-blog', 0, 12, 2, 0)
INSERT [dbo].[Blog] ([BlogID], [Title], [Description], [UrlField], [Private], [UserID], [TopicID], [ViewCount]) VALUES (7, N'Test blog 454', N'description', N'test-blog-454', 0, 7, 5, 0)
INSERT [dbo].[Blog] ([BlogID], [Title], [Description], [UrlField], [Private], [UserID], [TopicID], [ViewCount]) VALUES (8, N'Blog Title 101', NULL, N'blog-title-101', 0, 7, 1, 0)
INSERT [dbo].[Blog] ([BlogID], [Title], [Description], [UrlField], [Private], [UserID], [TopicID], [ViewCount]) VALUES (12, N'Test travel', N'test travel', N'test-travel', 0, 9, 10, 0)
SET IDENTITY_INSERT [dbo].[Blog] OFF
SET IDENTITY_INSERT [dbo].[BlogTopic] ON 

INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (1, N'General')
INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (2, N'Physics')
INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (3, N'Technology')
INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (4, N'Chemistry')
INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (5, N'Book')
INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (6, N'Marketing')
INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (7, N'Personal')
INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (8, N'Bilogy')
INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (9, N'Health and Fitness')
INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (10, N'Travel')
INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (11, N'Animals')
INSERT [dbo].[BlogTopic] ([BlogTopicID], [Name]) VALUES (12, N'Photography')
SET IDENTITY_INSERT [dbo].[BlogTopic] OFF
SET IDENTITY_INSERT [dbo].[Post] ON 

INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (3, N'  Tony Title ', CAST(N'2020-03-11T08:23:58.000' AS DateTime), CAST(N'2020-03-11T08:26:16.037' AS DateTime), N'tony-title', 1, N' Tony content  ', 5, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (4, N' Hello title', CAST(N'2020-03-11T08:29:00.000' AS DateTime), CAST(N'2020-03-11T11:42:37.257' AS DateTime), N'hello-title', 0, N'    test  contentssssss  101 

<br>
<strong>Test</strong> 

<h3>Header test</h3>', 1, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (5, N' Blog 2 post 1', CAST(N'2020-03-11T08:39:42.077' AS DateTime), CAST(N'2020-03-11T08:39:42.077' AS DateTime), N'blog-2-post-1', 0, N' Blog 2 post 1 ', 2, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (6, N'  Blog 2 post 2', CAST(N'2020-03-11T08:39:50.223' AS DateTime), CAST(N'2020-03-11T08:39:50.223' AS DateTime), N'blog-2-post-2', 1, N' Blog 2 post 2 ', 2, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (7, N'Test title', CAST(N'2020-03-11T08:50:26.037' AS DateTime), CAST(N'2020-03-11T08:50:26.037' AS DateTime), N'test-title', 0, N'TSarar ', 5, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (8, N'Hello Title 2 ', CAST(N'2020-03-11T08:56:34.993' AS DateTime), CAST(N'2020-03-11T08:56:34.993' AS DateTime), N'hello-title-2', 0, N'asdasdja ', 1, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (11, N'Post 454', CAST(N'2020-03-11T10:52:10.857' AS DateTime), CAST(N'2020-03-11T10:52:10.857' AS DateTime), N'post-454', 0, N'Post 454', 7, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (12, N'Post title 101', CAST(N'2020-03-11T10:58:46.737' AS DateTime), CAST(N'2020-03-11T10:58:46.737' AS DateTime), N'post-title-101', 0, N'Post title 101', 8, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (17, N'Blog   2 post 3', CAST(N'2020-03-11T12:02:54.567' AS DateTime), CAST(N'2020-03-11T12:02:54.567' AS DateTime), N'blog-2-post-3', 0, N'asndkasdja;', 2, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (18, N'Photography post', CAST(N'2020-03-11T20:44:32.840' AS DateTime), CAST(N'2020-03-11T20:44:32.840' AS DateTime), N'photography-post', 0, N'Photography content', 4, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (19, N'Travel Post', CAST(N'2020-03-11T20:44:52.250' AS DateTime), CAST(N'2020-03-11T20:44:52.250' AS DateTime), N'travel-post', 0, N'Travel Content', 12, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (20, N'Travel Post 2', CAST(N'2020-03-11T20:52:26.090' AS DateTime), CAST(N'2020-03-11T20:52:26.090' AS DateTime), N'travel-post-2', 0, N'Travel post 2', 12, 0)
INSERT [dbo].[Post] ([PostID], [Title], [PublishedDate], [ModifiedDate], [UrlField], [Draft], [PostContent], [BlogID], [ViewCount]) VALUES (21, N'Photography post 2', CAST(N'2020-03-11T20:52:43.130' AS DateTime), CAST(N'2020-03-11T20:52:43.130' AS DateTime), N'photography-post-2', 0, N'Photography post 2', 4, 0)
SET IDENTITY_INSERT [dbo].[Post] OFF
SET IDENTITY_INSERT [dbo].[TopicFollow] ON 

INSERT [dbo].[TopicFollow] ([TopicFollowID], [TopicID], [UserID]) VALUES (14, 1, 7)
INSERT [dbo].[TopicFollow] ([TopicFollowID], [TopicID], [UserID]) VALUES (15, 4, 7)
INSERT [dbo].[TopicFollow] ([TopicFollowID], [TopicID], [UserID]) VALUES (16, 6, 7)
INSERT [dbo].[TopicFollow] ([TopicFollowID], [TopicID], [UserID]) VALUES (17, 7, 7)
INSERT [dbo].[TopicFollow] ([TopicFollowID], [TopicID], [UserID]) VALUES (18, 9, 7)
INSERT [dbo].[TopicFollow] ([TopicFollowID], [TopicID], [UserID]) VALUES (19, 10, 7)
INSERT [dbo].[TopicFollow] ([TopicFollowID], [TopicID], [UserID]) VALUES (20, 12, 7)
SET IDENTITY_INSERT [dbo].[TopicFollow] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [Username], [Email], [Password], [FullName], [ActiveStatus]) VALUES (7, N'fahim', N'fahimali103@gmail.com', N'2345', N'Fahim Ali', 1)
INSERT [dbo].[User] ([UserID], [Username], [Email], [Password], [FullName], [ActiveStatus]) VALUES (9, N'fahim2', N'fahim@gmail.com', N'1234', N'Fahim Ali', 1)
INSERT [dbo].[User] ([UserID], [Username], [Email], [Password], [FullName], [ActiveStatus]) VALUES (10, N'1234', N'Fahim1@gmail.com', N'1234', N'Ahmed Ali Fahim', 1)
INSERT [dbo].[User] ([UserID], [Username], [Email], [Password], [FullName], [ActiveStatus]) VALUES (11, N'123', N'test@gmail.com', N'1234', N'test', 1)
INSERT [dbo].[User] ([UserID], [Username], [Email], [Password], [FullName], [ActiveStatus]) VALUES (12, N'tony', N'tony.stark@gmail.com', N'12345678', N'Tony Stark', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [User_Email]    Script Date: 3/13/2020 9:31:31 PM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [User_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [User_Username]    Script Date: 3/13/2020 9:31:31 PM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [User_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Blog] ADD  CONSTRAINT [DF_Blog_ViewCount]  DEFAULT ((0)) FOR [ViewCount]
GO
ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_ViewCount]  DEFAULT ((0)) FOR [ViewCount]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Topic] FOREIGN KEY([TopicID])
REFERENCES [dbo].[BlogTopic] ([BlogTopicID])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_Topic]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_User]
GO
ALTER TABLE [dbo].[BlogFollow]  WITH CHECK ADD  CONSTRAINT [FK_BlogFollow_Blog] FOREIGN KEY([BlogID])
REFERENCES [dbo].[Blog] ([BlogID])
GO
ALTER TABLE [dbo].[BlogFollow] CHECK CONSTRAINT [FK_BlogFollow_Blog]
GO
ALTER TABLE [dbo].[BlogFollow]  WITH CHECK ADD  CONSTRAINT [FK_BlogFollow_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[BlogFollow] CHECK CONSTRAINT [FK_BlogFollow_User]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_Blog] FOREIGN KEY([BlogID])
REFERENCES [dbo].[Blog] ([BlogID])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_Blog]
GO
ALTER TABLE [dbo].[TopicFollow]  WITH CHECK ADD  CONSTRAINT [FK_TopicFollow_BlogTopic] FOREIGN KEY([TopicID])
REFERENCES [dbo].[BlogTopic] ([BlogTopicID])
GO
ALTER TABLE [dbo].[TopicFollow] CHECK CONSTRAINT [FK_TopicFollow_BlogTopic]
GO
ALTER TABLE [dbo].[TopicFollow]  WITH CHECK ADD  CONSTRAINT [FK_TopicFollow_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[TopicFollow] CHECK CONSTRAINT [FK_TopicFollow_User]
GO
USE [master]
GO
ALTER DATABASE [Texxty] SET  READ_WRITE 
GO
