use UNIVER;
go

--1
drop view [Аудитории];
go

--2
create view [Аудитории] 
--3
as select 
    AUDITORIUM as N'код', 
    AUDITORIUM_NAME as N'наименование аудитории'
--4
from AUDITORIUM
--5
where AUDITORIUM_TYPE like N'ЛК%'; --с букв ntЛК
go

--6
select * from [Аудитории];
go