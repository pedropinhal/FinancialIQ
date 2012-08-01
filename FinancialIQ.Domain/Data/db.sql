/****** Object:  Table [dbo].[Users]    Script Date: 07/24/2012 09:36:21 ******/
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



/****** Object:  Index [IX_Email]    Script Date: 07/24/2012 14:30:14 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Email] ON [dbo].[Users] 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO



/****** Object:  Index [IX_Username]    Script Date: 07/24/2012 14:30:14 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Username] ON [dbo].[Users] 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO




/****** Object:  Table [dbo].[MoneyFlows]    Script Date: 07/24/2012 09:36:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MoneyFlows](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Item] [nvarchar](250) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Debit] [money] NOT NULL,
	[Credit] [money] NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[Subcategory] [nvarchar](50) NULL,
	[User] [int] NULL,
 CONSTRAINT [PK_MoneyFlows] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MoneyFlows]  WITH CHECK ADD  CONSTRAINT [FK_MoneyFlows_Users] FOREIGN KEY([User])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[MoneyFlows] CHECK CONSTRAINT [FK_MoneyFlows_Users]
GO


