USE [IdareDB]
GO
/****** Object:  Table [dbo].[DersTablo]    Script Date: 08/12/2022 19:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DersTablo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ad] [nvarchar](50) NOT NULL,
	[Kredisi] [float] NOT NULL,
	[OkulYonetimId] [int] NOT NULL,
 CONSTRAINT [PK_DersTablo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OgrenciDersTablo]    Script Date: 08/12/2022 19:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OgrenciDersTablo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DersId] [int] NOT NULL,
	[OgrenciId] [int] NOT NULL,
 CONSTRAINT [PK_OgrenciDersTablo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OgrenciTablo]    Script Date: 08/12/2022 19:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OgrenciTablo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdSoyad] [nvarchar](50) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[OgrenciNo] [int] NOT NULL,
	[DTarih] [date] NOT NULL,
	[Bolum] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_OgrenciTablo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OkulYonetimTablo]    Script Date: 08/12/2022 19:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OkulYonetimTablo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdSoyad] [nvarchar](50) NOT NULL,
	[Gorevi] [nvarchar](50) NOT NULL,
	[YonetimTip] [int] NOT NULL,
 CONSTRAINT [PK_OkulYonetimTablo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DersTablo]  WITH CHECK ADD  CONSTRAINT [FK_DersTablo_OkulYonetimTablo1] FOREIGN KEY([OkulYonetimId])
REFERENCES [dbo].[OkulYonetimTablo] ([Id])
GO
ALTER TABLE [dbo].[DersTablo] CHECK CONSTRAINT [FK_DersTablo_OkulYonetimTablo1]
GO
ALTER TABLE [dbo].[OgrenciDersTablo]  WITH CHECK ADD  CONSTRAINT [FK_OgrenciDersTablo_DersTablo1] FOREIGN KEY([DersId])
REFERENCES [dbo].[DersTablo] ([Id])
GO
ALTER TABLE [dbo].[OgrenciDersTablo] CHECK CONSTRAINT [FK_OgrenciDersTablo_DersTablo1]
GO
ALTER TABLE [dbo].[OgrenciDersTablo]  WITH CHECK ADD  CONSTRAINT [FK_OgrenciDersTablo_OgrenciTablo1] FOREIGN KEY([OgrenciId])
REFERENCES [dbo].[OgrenciTablo] ([Id])
GO
ALTER TABLE [dbo].[OgrenciDersTablo] CHECK CONSTRAINT [FK_OgrenciDersTablo_OgrenciTablo1]
GO
