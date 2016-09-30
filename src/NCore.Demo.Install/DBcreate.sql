USE [master]
GO
/****** Object:  Database [Xena.Members]    Script Date: 07/14/2011 15:44:57 ******/
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'NCore.Demo')
	SET NOEXEC ON
GO
CREATE DATABASE [NCore.Demo] ON  PRIMARY 
( NAME = N'NCore.Demo', FILENAME = N'{0}\NCore.Demo.mdf' , SIZE = 8448KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NCore.DemoLog', FILENAME = N'{0}\NCore.Demo.ldf' , SIZE = 16576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO