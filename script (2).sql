USE [master]
GO
/****** Object:  Database [BTL]    Script Date: 6/1/2020 3:47:01 PM ******/
CREATE DATABASE [BTL]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BTL', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BTL.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BTL_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BTL_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BTL].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BTL] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BTL] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BTL] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BTL] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BTL] SET ARITHABORT OFF 
GO
ALTER DATABASE [BTL] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BTL] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BTL] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BTL] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BTL] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BTL] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BTL] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BTL] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BTL] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BTL] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BTL] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BTL] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BTL] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BTL] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BTL] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BTL] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BTL] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BTL] SET RECOVERY FULL 
GO
ALTER DATABASE [BTL] SET  MULTI_USER 
GO
ALTER DATABASE [BTL] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BTL] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BTL] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BTL] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BTL] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BTL', N'ON'
GO
ALTER DATABASE [BTL] SET QUERY_STORE = OFF
GO
USE [BTL]
GO
/****** Object:  Table [dbo].[Kho]    Script Date: 6/1/2020 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kho](
	[Makho] [nvarchar](5) NOT NULL,
	[Tenkho] [nvarchar](50) NULL,
 CONSTRAINT [PK_Kho] PRIMARY KEY CLUSTERED 
(
	[Makho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiSach]    Script Date: 6/1/2020 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSach](
	[Maloai] [nvarchar](5) NOT NULL,
	[Tenloai] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoaiSach] PRIMARY KEY CLUSTERED 
(
	[Maloai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Muon]    Script Date: 6/1/2020 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Muon](
	[Masinhvien] [nvarchar](5) NOT NULL,
	[Masach] [nvarchar](5) NOT NULL,
	[Hinhthucmuon] [nvarchar](50) NULL,
	[Ngaymuon] [nvarchar](50) NULL,
	[Ngaytra] [nvarchar](50) NULL,
	[Songaymuon] [int] NULL,
 CONSTRAINT [PK_Muon] PRIMARY KEY CLUSTERED 
(
	[Masinhvien] ASC,
	[Masach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NgonNgu]    Script Date: 6/1/2020 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NgonNgu](
	[Mangonngu] [nvarchar](5) NOT NULL,
	[Tenngonngu] [nvarchar](50) NULL,
 CONSTRAINT [PK_NgonNgu] PRIMARY KEY CLUSTERED 
(
	[Mangonngu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NXB]    Script Date: 6/1/2020 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NXB](
	[Manhaxuatban] [nvarchar](5) NOT NULL,
	[Tennhaxuatban] [nvarchar](50) NULL,
	[Diachi] [nvarchar](50) NULL,
 CONSTRAINT [PK_NXB] PRIMARY KEY CLUSTERED 
(
	[Manhaxuatban] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sach]    Script Date: 6/1/2020 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sach](
	[Masach] [nvarchar](5) NOT NULL,
	[Tensach] [nvarchar](50) NULL,
	[Namxuatban] [nvarchar](50) NULL,
	[Soluong] [int] NULL,
	[Mangonngu] [nvarchar](5) NULL,
	[Manhaxuatban] [nvarchar](5) NULL,
	[Matacgia] [nvarchar](5) NULL,
	[Maloai] [nvarchar](5) NULL,
	[Mavitri] [nvarchar](5) NULL,
	[Makho] [nvarchar](5) NULL,
 CONSTRAINT [PK_Sach] PRIMARY KEY CLUSTERED 
(
	[Masach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien]    Script Date: 6/1/2020 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien](
	[Masinhvien] [nvarchar](5) NOT NULL,
	[Tensinhvien] [nvarchar](50) NULL,
	[Lop] [nvarchar](50) NULL,
 CONSTRAINT [PK_SinhVien] PRIMARY KEY CLUSTERED 
(
	[Masinhvien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TacGia]    Script Date: 6/1/2020 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TacGia](
	[Matacgia] [nvarchar](5) NOT NULL,
	[Tentacgia] [nvarchar](50) NULL,
	[Diachi] [nvarchar](50) NULL,
 CONSTRAINT [PK_TacGia] PRIMARY KEY CLUSTERED 
(
	[Matacgia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/1/2020 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](150) NULL,
	[Password] [nvarchar](250) NULL,
	[ConfirmPassword] [nvarchar](250) NULL,
	[FullName] [nvarchar](100) NULL,
	[Address] [nvarchar](250) NULL,
	[Email] [nvarchar](150) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ViTri]    Script Date: 6/1/2020 3:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ViTri](
	[Mavitri] [nvarchar](5) NOT NULL,
	[Khu] [nvarchar](50) NULL,
	[Ke] [nvarchar](50) NULL,
	[Ngan] [nvarchar](50) NULL,
 CONSTRAINT [PK_ViTri] PRIMARY KEY CLUSTERED 
(
	[Mavitri] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Muon]  WITH CHECK ADD  CONSTRAINT [FK_Muon_Sach] FOREIGN KEY([Masach])
REFERENCES [dbo].[Sach] ([Masach])
GO
ALTER TABLE [dbo].[Muon] CHECK CONSTRAINT [FK_Muon_Sach]
GO
ALTER TABLE [dbo].[Muon]  WITH CHECK ADD  CONSTRAINT [FK_Muon_SinhVien] FOREIGN KEY([Masinhvien])
REFERENCES [dbo].[SinhVien] ([Masinhvien])
GO
ALTER TABLE [dbo].[Muon] CHECK CONSTRAINT [FK_Muon_SinhVien]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_Kho] FOREIGN KEY([Makho])
REFERENCES [dbo].[Kho] ([Makho])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_Kho]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_LoaiSach] FOREIGN KEY([Maloai])
REFERENCES [dbo].[LoaiSach] ([Maloai])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_LoaiSach]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_NgonNgu] FOREIGN KEY([Mangonngu])
REFERENCES [dbo].[NgonNgu] ([Mangonngu])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_NgonNgu]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_NXB] FOREIGN KEY([Manhaxuatban])
REFERENCES [dbo].[NXB] ([Manhaxuatban])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_NXB]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_TacGia] FOREIGN KEY([Matacgia])
REFERENCES [dbo].[TacGia] ([Matacgia])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_TacGia]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_ViTri] FOREIGN KEY([Mavitri])
REFERENCES [dbo].[ViTri] ([Mavitri])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_ViTri]
GO
USE [master]
GO
ALTER DATABASE [BTL] SET  READ_WRITE 
GO
