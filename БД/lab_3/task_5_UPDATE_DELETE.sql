USE z_MyBase_2;
GO

UPDATE Виды_кредита 
SET Ставка = Ставка + 1 
WHERE Название = N'Ипотека';

SELECT * FROM Виды_кредита;
GO

DELETE FROM Выдача_кредитов 
WHERE Номер_договора = 5003;

SELECT * FROM Выдача_кредитов;
GO