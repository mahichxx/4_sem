use UNIVER;
go

--1
drop view [Количество кафедр];
go

--2
create view [Количество кафедр]
--3
as select 
    FACULTY.FACULTY_NAME as N'факультет', 
    count(*) as N'количество кафедр'
--4
from FACULTY inner join PULPIT 
--5
on FACULTY.FACULTY = PULPIT.FACULTY
--6
group by FACULTY.FACULTY_NAME;
go

--7
select * from [Количество кафедр];
go