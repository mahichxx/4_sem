USE z_MyBase_2;

INSERT INTO Клиенты (Название_фирмы, Телефон) 
VALUES (N'Тест-Фирма-Новая', '555-00-00');
SELECT * FROM Клиенты;
GO

--CHECK
INSERT INTO Выдача_кредитов (ID_Вида, ID_Клиента, Сумма, Дата_возврата) 
VALUES (1, 1, -500, '2025-01-01'); 
GO

--(Задание 7)
SELECT 
    t.name AS [Таблица], 
    ds.name AS [Файловая_группа]
FROM sys.tables AS t
INNER JOIN sys.indexes AS i ON t.object_id = i.object_id
INNER JOIN sys.data_spaces AS ds ON i.data_space_id = ds.data_space_id
WHERE i.index_id < 2; 
GO