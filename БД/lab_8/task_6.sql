use UNIVER;
go

--1
alter view [Количество кафедр] with schemabinding
--2
as select 
    FACULTY_NAME as N'факультет', 
    count_big(*) as N'количество кафедр'
--3
from dbo.FACULTY inner join dbo.PULPIT 
--4
on dbo.FACULTY.FACULTY = dbo.PULPIT.FACULTY
--5
group by FACULTY_NAME;
go

--6
select * from [Количество кафедр];
go