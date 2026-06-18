USE z_MyBase_2;
GO

ALTER TABLE Клиенты 
ADD Телефон nvarchar(15);
GO

ALTER TABLE Выдача_кредитов
ADD CONSTRAINT CHK_MaxSum CHECK (Сумма <= 10000000);
GO
 
 ALTER TABLE Клиенты 
DROP COLUMN Телефон;
GO