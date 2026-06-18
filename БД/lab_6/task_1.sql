use UNIVER;
go

--5
select
--4
	t.AUDITORIUM_TYPENAME as [Тип аудитории],
	MAX(a.AUDITORIUM_CAPACITY) as [Макс вместимость],
	MIN(a.AUDITORIUM_CAPACITY) AS [Мин вместимость],
    AVG(a.AUDITORIUM_CAPACITY) AS [Средняя вместимость],
    SUM(a.AUDITORIUM_CAPACITY) AS [Суммарная вместимость],
	COUNT(*) as [Количество аудиторий]
--1
from AUDITORIUM a
--2
inner join AUDITORIUM_TYPE t
	on a.AUDITORIUM_TYPE = t.AUDITORIUM_TYPE
--3
group by t.AUDITORIUM_TYPENAME;