use UNIVER;
go

if object_id('tempdb..#EXPLRE') is not null drop table #EXPLRE;
go

--1
exec sp_helpindex 'TEACHER';
go

--2
create table #EXPLRE (
    TIND int,
    TFIELD nvarchar(100)
);
go

--3
set nocount on;
declare @i int = 0;
while @i < 1100
begin
    insert #EXPLRE (TIND, TFIELD)
    values (floor(30000 * rand()), replicate(N'строка ', 10));
    set @i = @i + 1;
end;
go

--4
checkpoint;
dbcc dropcleanbuffers;
go

--5
select * from #EXPLRE where TIND between 1500 and 2500 order by TIND;
go

--6
create clustered index #EXPLRE_CL on #EXPLRE(TIND asc);
go

--7
select * from #EXPLRE where TIND between 1500 and 2500 order by TIND;
go