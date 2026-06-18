use UNIVER;
go

--1
drop view [Дисциплины];
go

--2
create view [Дисциплины] 
--3
as select top 100 percent
    SUBJECT as N'код', 
    SUBJECT_NAME as N'наименование дисциплины', 
    PULPIT as N'код кафедры'
--4
from SUBJECT
--5
order by SUBJECT_NAME; --сортировка по алфавиту
go

--6
select * from [Дисциплины];
go