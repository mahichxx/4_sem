--1
use UNIVER;
go
--2
select
    STUDENT.NAME as N'студент',
    PROGRESS.NOTE as N'оценка',
    case
        when PROGRESS.NOTE between 9 and 10 then N'отлично'
        when PROGRESS.NOTE between 7 and 8 then N'хорошо'
        when PROGRESS.NOTE between 5 and 6 then N'удовлетворительно'
        when PROGRESS.NOTE between 1 and 4 then N'неудовлетворительно'
        else N'неявка'
    end as N'статус'
--3
from PROGRESS
inner join STUDENT on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
inner join GROUPS on STUDENT.IDGROUP = GROUPS.IDGROUP
--4
where GROUPS.FACULTY = N'ИТ';
go

--5
use UNIVER;
go
--6
create table #LOCAL_TEMP
(
    ID int,
    STR_DATA nvarchar(50),
    RAND_VAL float
);
go
--7
declare @counter int = 1;
--8
while @counter <= 10
begin
    --9
    insert #LOCAL_TEMP (ID, STR_DATA, RAND_VAL)
    values (@counter, N'запись номер ' + cast(@counter as nvarchar), rand() * 100);
    --10
    set @counter = @counter + 1;
end;
go
--11
select * from #LOCAL_TEMP;
go

--
--12
use UNIVER;
go
--13
declare @test_val int = 10;
--14
print N'проверка оператора return...';
--15
if @test_val > 5
begin
    print N'условие сработало, завершаю выполнение пакета';
    return;
end;
--16
print N'этот текст не будет напечатан из-за return';
go