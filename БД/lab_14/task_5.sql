use Z_MYBASE_2;
go

if object_id(N'dbo.COUNT_CLIENTS') is not null drop function dbo.COUNT_CLIENTS;
go
--
create function dbo.COUNT_CLIENTS(@type_name nvarchar(50)) returns int
as
begin
    declare @res int = 0;
    select @res = count(*) 
    from ВЫДАЧА_КРЕДИТОВ inner join ВИДЫ_КРЕДИТА 
    on ВЫДАЧА_КРЕДИТОВ.КОД_ВИДА = ВИДЫ_КРЕДИТА.КОД_ВИДА
    where ВИДЫ_КРЕДИТА.НАЗВАНИЕ = @type_name;
    return @res;
end;
go

select НАЗВАНИЕ, dbo.COUNT_CLIENTS(НАЗВАНИЕ) as N'Кол-во выдач' 
from ВИДЫ_КРЕДИТА;
go

alter function dbo.COUNT_CLIENTS(@type_name nvarchar(50), @status nvarchar(20) = null) returns int
as
begin
    declare @res int = 0;
    select @res = count(*) 
    from ВЫДАЧА_КРЕДИТОВ inner join ВИДЫ_КРЕДИТА 
    on ВЫДАЧА_КРЕДИТОВ.КОД_ВИДА = ВИДЫ_КРЕДИТА.КОД_ВИДА
    where ВИДЫ_КРЕДИТА.НАЗВАНИЕ = @type_name 
      and (СТАТУС = @status or @status is null);
    return @res;
end;
go

if object_id(N'dbo.FCLIENT_LOANS') is not null drop function dbo.FCLIENT_LOANS;
go

create function dbo.FCLIENT_LOANS(@cid int) returns nvarchar(500)
as
begin
    declare @all_sums nvarchar(500) = N'', @cur_sum real;
    declare curs cursor local static for 
        select СУММА from ВЫДАЧА_КРЕДИТОВ where ID_КЛИЕНТА = @cid;
    
    open curs;
    fetch curs into @cur_sum;
    while @@fetch_status = 0
    begin
        set @all_sums = cast(@cur_sum as nvarchar) + N'; ' + @all_sums;
        fetch curs into @cur_sum;
    end;
    close curs; deallocate curs;
    
    return isnull(@all_sums, N'нет кредитов');
end;
go

select ФИО, dbo.FCLIENT_LOANS(ID_КЛИЕНТА) as N'История сумм' from КЛИЕНТЫ;
go

if object_id(N'dbo.FGET_LOANS_TAB') is not null drop function dbo.FGET_LOANS_TAB;
go

create function dbo.FGET_LOANS_TAB(@type_id int, @sum_min real) returns table
as
return 
(
    select НОМЕР_ДОГОВОРА, СУММА, СТАТУС 
    from ВЫДАЧА_КРЕДИТОВ 
    where (КОД_ВИДА = @type_id or @type_id is null) 
      and (СУММА >= @sum_min or @sum_min is null)
);
go

select * from dbo.FGET_LOANS_TAB(null, 50000);
go

if object_id(N'dbo.FSTAT_LOANS') is not null drop function dbo.FSTAT_LOANS;
go

create function dbo.FSTAT_LOANS(@cid int) returns int
as
begin
    declare @c int;
    set @c = (select count(*) from ВЫДАЧА_КРЕДИТОВ where ID_КЛИЕНТА = isnull(@cid, ID_КЛИЕНТА));
    return @c;
end;
go

select dbo.FSTAT_LOANS(null) as N'Всего выдач в банке';
go

if object_id(N'dbo.FCREDIT_REPORT') is not null drop function dbo.FCREDIT_REPORT;
go

create function dbo.FCREDIT_REPORT() 
returns @res table (Название nvarchar(50), Кол_во int, Общая_сумма real)
as
begin
    insert into @res
    select НАЗВАНИЕ, count(НОМЕР_ДОГОВОРА), sum(СУММА)
    from ВИДЫ_КРЕДИТА left join ВЫДАЧА_КРЕДИТОВ on ВИДЫ_КРЕДИТА.КОД_ВИДА = ВЫДАЧА_КРЕДИТОВ.КОД_ВИДА
    group by НАЗВАНИЕ;
    return;
end;
go

select * from dbo.FCREDIT_REPORT();
go