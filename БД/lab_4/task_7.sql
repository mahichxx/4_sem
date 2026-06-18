USE z_MyBase_2;
GO

SELECT К.ФИО, В.Сумма, Т.Название
FROM Выдача_кредитов AS В
INNER JOIN Клиенты AS К ON В.ID_клиента = К.ID_клиента
INNER JOIN Виды_кредита AS Т ON В.Код_вида = Т.Код_вида;

SELECT К.ФИО, В.Номер_договора
FROM Клиенты AS К
LEFT JOIN Выдача_кредитов AS В ON К.ID_клиента = В.ID_клиента
WHERE В.Номер_договора IS NULL;