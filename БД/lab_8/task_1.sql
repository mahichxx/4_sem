use UNIVER;
go

--1
drop view [Преподаватель];
go

--2
create view [Преподаватель] 
--3
as select 
    TEACHER as N'код', 
    TEACHER_NAME as N'имя преподавателя', 
    GENDER as N'пол', 
    PULPIT as N'код кафедры'
--5
from TEACHER;
go

--6
select * from [Преподаватель];
go