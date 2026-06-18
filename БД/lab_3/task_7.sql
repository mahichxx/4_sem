USE  master;
GO

ALTER DATABASE z_MyBase_FIN ADD FILEGROUP FG1;
GO

ALTER DATABASE z_MyBase_FIN 
ADD FILE 
( 
    NAME = N'z_MyBase_Extra', 
    FILENAME = N'D:\Программирование\4_сем\БД\lab_3\z_MyBase_Extra.ndf',
    SIZE = 5MB, MAXSIZE = 50MB, FILEGROWTH = 2MB 
)
TO FILEGROUP FG1;
GO

USE z_MyBase_FIN;
GO

CREATE TABLE Архив_кредитов (
    ID_записи int PRIMARY KEY,
    Дата_архивации date DEFAULT GETDATE(),
    Сумма real
) ON FG1; 
GO