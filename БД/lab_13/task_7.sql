use Z_MYBASE_2;
go

delete from ВЫДАЧА_КРЕДИТОВ where Код_вида in (5, 10) or Номер_договора = 999;
delete from ВИДЫ_КРЕДИТА where Код_вида in (5, 10) or Название = N'Автокредит';
go

if object_id(N'P_CLIENTS_ALL') is not null drop procedure P_CLIENTS_ALL;
go


create procedure P_CLIENTS_ALL
as
begin
    declare @k int = (select count(*) from КЛИЕНТЫ);
    select ID_клиента as N'код', ФИО as N'клиент', Паспорт as N'паспорт' from КЛИЕНТЫ;
    return @k;
end;
go

declare @res int;
exec @res = P_CLIENTS_ALL;
print N'всего клиентов: ' + cast(@res as nvarchar);
go


alter procedure P_CLIENTS_ALL 
    @name nvarchar(100) = null, 
    @c int output
as
begin
    declare @total int = (select count(*) from КЛИЕНТЫ);
    set @c = (select count(*) from КЛИЕНТЫ where ФИО like @name + N'%');
    select ID_клиента, ФИО, Паспорт, Дата_регистрации from КЛИЕНТЫ where ФИО like @name + N'%';
    return @total;
end;
go

declare @all int, @found int;
exec @all = P_CLIENTS_ALL @name = N'И', @c = @found output;
print N'всего в базе: ' + cast(@all as nvarchar);
print N'найдено по фильтру: ' + cast(@found as nvarchar);
go


if object_id(N'tempdb..#CLIENT_TEMP') is not null drop table #CLIENT_TEMP;
create table #CLIENT_TEMP (ID int, NAME nvarchar(100), DOC nvarchar(20), REG_DATE date);

declare @temp_c int;
insert #CLIENT_TEMP exec P_CLIENTS_ALL @name = N'И', @c = @temp_c output;
select * from #CLIENT_TEMP;
go

if object_id(N'P_INSERT_CREDIT_TYPE') is not null drop procedure P_INSERT_CREDIT_TYPE;
go

create procedure P_INSERT_CREDIT_TYPE
    @id int, @name nvarchar(50), @rate real = 10.5
as
begin try
    insert into ВИДЫ_КРЕДИТА (Код_вида, Название, Ставка) values (@id, @name, @rate);
    return 1;
end try
begin catch
    print N'номер ошибки: ' + cast(error_number() as nvarchar);
    print N'сообщение: ' + error_message();
    return -1;
end catch;
go

declare @st int;
exec @st = P_INSERT_CREDIT_TYPE @id = 5, @name = N'Ипотека', @rate = 12.0;
go

if object_id(N'P_REPORT_BY_CLIENT') is not null drop procedure P_REPORT_BY_CLIENT;
go

create procedure P_REPORT_BY_CLIENT @id int
as
begin try
    declare @rc int = 0, @sum real, @report nvarchar(max) = N'';
    if not exists (select * from КЛИЕНТЫ where ID_клиента = @id)
        raiserror(N'ошибка: клиент не найден', 11, 1);
    else
    begin
        declare curs_rep cursor for select Сумма from ВЫДАЧА_КРЕДИТОВ where ID_клиента = @id;
        open curs_rep;
        fetch curs_rep into @sum;
        while @@fetch_status = 0
        begin
            set @report = cast(@sum as nvarchar) + N', ' + @report;
            set @rc = @rc + 1;
            fetch curs_rep into @sum;
        end;
        print N'суммы кредитов клиента: ' + @report;
        close curs_rep; deallocate curs_rep;
        return @rc;
    end
end try
begin catch
    print N'ошибка в параметрах: ' + error_message();
    return -1;
end catch;
go

exec P_REPORT_BY_CLIENT @id = 101;
go

if object_id(N'P_COMPLEX_REGISTRATION') is not null drop procedure P_COMPLEX_REGISTRATION;
go

create procedure P_COMPLEX_REGISTRATION
    @type_id int, @type_name nvarchar(50), @rate real,
    @contract_id int, @client_id int, @amount real
as
begin try
    set transaction isolation level serializable;
    begin tran;
    exec P_INSERT_CREDIT_TYPE @id = @type_id, @name = @type_name, @rate = @rate;
    insert into ВЫДАЧА_КРЕДИТОВ (Номер_договора, ID_клиента, Код_вида, Сумма, Дата_выдачи, Статус)
    values (@contract_id, @client_id, @type_id, @amount, getdate(), N'Активен');
    commit tran;
    return 1;
end try
begin catch
    print N'ошибка транзакции: ' + error_message();
    if @@trancount > 0 rollback tran;
    return -1;
end catch;
go

exec P_COMPLEX_REGISTRATION 
    @type_id = 10, @type_name = N'Автокредит', @rate = 15.5,
    @contract_id = 999, @client_id = 101, @amount = 350000;
go