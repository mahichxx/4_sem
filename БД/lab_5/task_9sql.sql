USE z_MyBase_2;
GO

IF NOT EXISTS (SELECT * FROM Виды_кредита WHERE Название = N'Рефинансирование')
    INSERT INTO Виды_кредита (Код_вида, Название, Ставка) VALUES (4, N'Рефинансирование', 11.5);

IF NOT EXISTS (SELECT * FROM Клиенты WHERE ID_клиента = 104)
    INSERT INTO Клиенты (ID_клиента, ФИО, Паспорт) VALUES (104, N'Кузнецов Игорь', N'МР 9999999');
GO

--IN
SELECT ФИО FROM Клиенты 
WHERE ID_клиента IN (
    SELECT ID_клиента FROM Выдача_кредитов 
    WHERE Код_вида = (SELECT Код_вида FROM Виды_кредита WHERE Название = N'Ипотека')
);
SELECT ФИО FROM Клиенты 
WHERE ID_клиента IN (102, 103
);
GO

GO
--FROM 
SELECT K.ФИО, Sub.Сумма
FROM Клиенты K
INNER JOIN (SELECT ID_клиента, Сумма FROM Выдача_кредитов) AS Sub 
ON K.ID_клиента = Sub.ID_клиента;
GO
-- коррелированный подзапрос 
SELECT Название, 
       (SELECT MAX(Сумма) FROM Выдача_кредитов V WHERE V.Код_вида = VK.Код_вида) AS [Макс. Кредит]
FROM Виды_кредита VK;
GO

--EXISTS
SELECT Название FROM Виды_кредита VK
WHERE NOT EXISTS (
    SELECT * FROM Выдача_кредитов V WHERE V.Код_вида = VK.Код_вида
);
GO

-- SELECT сравнение средних сумм
SELECT 
    (SELECT AVG(Сумма) FROM Выдача_кредитов WHERE Код_вида = 1) AS [Средний Потребительский],
    (SELECT AVG(Сумма) FROM Выдача_кредитов WHERE Код_вида = 2) AS [Средняя Ипотека]
FROM (SELECT TOP 1 * FROM Выдача_кредитов) AS Dummy;
GO

--ALL
SELECT Номер_договора, Сумма FROM Выдача_кредитов
WHERE Сумма > ALL (SELECT Сумма FROM Выдача_кредитов WHERE Код_вида = 1);
GO