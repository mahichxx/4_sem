use UNIVER;
go

--1
drop view [Лекционные_аудитории];
go

--2
create view [Лекционные_аудитории] 
--3
as select 
    AUDITORIUM as N'код', 
    AUDITORIUM_NAME as N'наименование аудитории'
--4
from AUDITORIUM
--5
where AUDITORIUM_TYPE like N'ЛК%'
with check option; --запрет вставки не лекционных
go

--6
select * from [Лекционные_аудитории];
go