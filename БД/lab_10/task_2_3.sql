use UNIVER;
go

--8
if object_id('tempdb..#EX') is not null drop table #EX;
go

--9
create table #EX (
    TKEY int,
    CC int identity(1, 1),
    TF nvarchar(100)
);
go

--10
set nocount on;
declare @j int = 0;
while @j < 10000
begin
    insert #EX (TKEY, TF)
    values (floor(30000 * rand()), replicate(N'данные ', 10));
    set @j = @j + 1;
end;
go

--11 Составной индекс
create index #EX_NONCLU on #EX(TKEY, CC);
go

--12
select * from #EX where TKEY between 500 and 1000 and CC > 3; --изменил на диапазон, чтобы были данные
go

--13 ндекс покрытия
if exists (select * from sys.indexes where name = '#EX_TKEY_X')
drop index #EX_TKEY_X on #EX;
go

--14
create index #EX_TKEY_X on #EX(TKEY) include (CC);
go

--15
select CC from #EX where TKEY > 15000;
go