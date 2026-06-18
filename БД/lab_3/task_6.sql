USE master;
GO

CREATE DATABASE z_MyBase_FIN
ON PRIMARY 
( 
    NAME = N'z_MyBase_Data', 
    FILENAME = N'D:\Программирование\4_сем\БД\lab_3\z_MyBase_Data.mdf', 
    SIZE = 10MB, 
    MAXSIZE = 100MB, 
    FILEGROWTH = 5MB 
),
( 
    NAME = N'z_MyBase_Data', 
    FILENAME = N'D:\Программирование\4_сем\БД\lab_3\z_MyBase_Data.mdf', 
    SIZE = 10MB, 
    MAXSIZE = 100MB, 
    FILEGROWTH = 5MB 
)
LOG ON 
( 
    NAME = N'z_MyBase_Log', 
    FILENAME = N'D:\Программирование\4_сем\БД\lab_3\z_MyBase_Log.ldf',
    SIZE = 5MB, 
    MAXSIZE = 50MB, 
    FILEGROWTH = 10% 
);
GO