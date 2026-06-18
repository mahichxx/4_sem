use UNIVER;
go

--16
if object_id('EX') is not null drop table EX;
go

--17
create table EX (
    TKEY int,
    TF nvarchar(500)
);
go

--18
set nocount on;
declare @i int = 0;
while @i < 10000
begin
    insert EX (TKEY, TF)
    values (floor(30000 * rand()), replicate(N'длинная_строка_данных_', 15));
    set @i = @i + 1;
end;
go

--19
create index EX_WHERE on EX(TKEY) 
where (TKEY >= 15000 and TKEY < 20000);
go

--20
select TKEY from EX where TKEY between 15000 and 19999;
go

--21
create index EX_FRAG on EX(TKEY);
go

--22 Создаем "беспорядок" через случайные вставки
declare @j int = 0;
while @j < 2000
begin
    insert EX (TKEY, TF) 
    values (floor(30000 * rand()), N'фрагментация');
    set @j = @j + 1;
end;
go

--23 Оценка фрагментации
select 
    ii.name as N'индекс', 
    ss.avg_fragmentation_in_percent as N'фрагментация (%)'
from sys.dm_db_index_physical_stats(db_id(), object_id('EX'), null, null, 'DETAILED') ss
inner join sys.indexes ii on ss.object_id = ii.object_id and ss.index_id = ii.index_id
where ii.name is not null;
go

--24
drop index EX_FRAG on EX;
go

--25
create index EX_FILL on EX(TKEY) 
with (fillfactor = 65);
go

--26
select 
    ii.name as N'индекс', 
    ss.avg_fragmentation_in_percent as N'фрагментация (%)'
from sys.dm_db_index_physical_stats(db_id(), object_id('EX'), null, null, 'DETAILED') ss
inner join sys.indexes ii on ss.object_id = ii.object_id and ss.index_id = ii.index_id
where ii.name is not null;
go