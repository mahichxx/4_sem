USE z_MyBase_2;
GO

SELECT * FROM Клиенты;

SELECT ФИО, Паспорт FROM Клиенты;

SELECT COUNT(*) AS [Общее кол-во договоров] FROM Выдача_кредитов;

SELECT * FROM Выдача_кредитов 
WHERE Сумма > 1000000;
GO