use UNIVER;
go

--1 
declare @t float = 2.0, 
        @x float = 1.0, 
        @z float;

if @t > @x 
    set @z = power(sin(@t), 2);
else if @t < @x 
    set @z = 4 * (@t + @x);
else 
    set @z = 1 - exp(@x - 2);

print N'Результат Z = ' + cast(@z as nvarchar);
go

--2 
declare @fio nvarchar(100) = N'Макейчик Татьяна Леонидовна',
        @res nvarchar(100);

set @res = substring(@fio, 1, charindex(' ', @fio)) + 
           substring(@fio, charindex(' ', @fio) + 1, 1) + N'. ' + 
           substring(@fio, charindex(' ', @fio, charindex(' ', @fio) + 1) + 1, 1) + N'.';

print N'Сокращенное ФИО: ' + @res;
go

--3 
select 
    NAME as N'студент', 
    datediff(year, BDAY, getdate()) as N'возраст'
from STUDENT
where month(BDAY) = month(dateadd(month, 1, getdate()));
go

--4 
select distinct
    datename(weekday, PDATE) as N'день недели экзамена по БД'
from PROGRESS inner join SUBJECT 
on PROGRESS.SUBJECT = SUBJECT.SUBJECT
where SUBJECT.SUBJECT_NAME like N'%Баз%';
go

--5 
declare @avg_note float;
set @avg_note = (select avg(cast(NOTE as float)) from PROGRESS);

if @avg_note > 7
begin
    print N'общий уровень успеваемости высокий';
    print N'средний балл по университету: ' + cast(@avg_note as nvarchar);
end
else
begin
    print N'уровень успеваемости требует внимания';
    print N'средний балл: ' + cast(@avg_note as nvarchar);
end
go