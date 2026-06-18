use Z_MYBASE_2;
go

if exists (select * from sys.triggers where name = 'TRIG_DB_PROTECT' and parent_class = 0) 
drop trigger TRIG_DB_PROTECT on database;
go

if object_id(N'TR_AUDIT') is not null drop table TR_AUDIT;
go

create table TR_AUDIT
(
    ID int identity,
    STMT varchar(20) check (STMT in ('INS', 'DEL', 'UPD')),
    TRNAME varchar(50),
    CC nvarchar(300)
);
go

if exists (select * from sys.triggers where name = 'TRIG_CLIENT_INS') drop trigger TRIG_CLIENT_INS;
go
create trigger TRIG_CLIENT_INS on КЛИЕНТЫ after insert
as
begin
    declare @info nvarchar(300);
    set @info = (select ФИО from inserted);
    insert into TR_AUDIT (STMT, TRNAME, CC) 
    values ('INS', 'TRIG_CLIENT_INS', N'добавлен клиент: ' + @info);
end;
go

if exists (select * from sys.triggers where name = 'TRIG_CLIENT_DEL') drop trigger TRIG_CLIENT_DEL;
go
create trigger TRIG_CLIENT_DEL on КЛИЕНТЫ after delete
as
begin
    declare @info nvarchar(300);
    set @info = (select ФИО from deleted);
    insert into TR_AUDIT (STMT, TRNAME, CC) 
    values ('DEL', 'TRIG_CLIENT_DEL', N'удален клиент: ' + @info);
end;
go

if exists (select * from sys.triggers where name = 'TRIG_CLIENT_UPD') drop trigger TRIG_CLIENT_UPD;
go
create trigger TRIG_CLIENT_UPD on КЛИЕНТЫ after update
as
begin
    declare @old nvarchar(150), @new nvarchar(150);
    set @old = (select ФИО from deleted);
    set @new = (select ФИО from inserted);
    insert into TR_AUDIT (STMT, TRNAME, CC) 
    values ('UPD', 'TRIG_CLIENT_UPD', N'смена фио с ' + @old + N' на ' + @new);
end;
go

if exists (select * from sys.triggers where name = 'TRIG_LIMIT_CREDIT') drop trigger TRIG_LIMIT_CREDIT;
go
create trigger TRIG_LIMIT_CREDIT on ВЫДАЧА_КРЕДИТОВ after insert, update
as
begin
    declare @total real = (select sum(СУММА) from ВЫДАЧА_КРЕДИТОВ);
    if @total > 10000000
    begin
        raiserror(N'общий лимит банка превышен, отмена!', 16, 1);
        rollback transaction;
    end;
end;
go

if exists (select * from sys.triggers where name = 'TRIG_ORDER_1') drop trigger TRIG_ORDER_1;
go
create trigger TRIG_ORDER_1 on КЛИЕНТЫ after delete as print N'второй по очереди';
go

if exists (select * from sys.triggers where name = 'TRIG_ORDER_2') drop trigger TRIG_ORDER_2;
go
--15
create trigger TRIG_ORDER_2 on КЛИЕНТЫ after delete as print N'самый первый по очереди';
go

--16
exec sp_settriggerorder @triggername = 'TRIG_ORDER_2', @order = 'First', @stmttype = 'DELETE';
go

if exists (select * from sys.triggers where name = 'TRIG_NO_DEL_TYPES') drop trigger TRIG_NO_DEL_TYPES;
go

create trigger TRIG_NO_DEL_TYPES on ВИДЫ_КРЕДИТА instead of delete
as
begin
    raiserror(N'удаление типов кредитов запрещено!', 16, 1);
end;
go

create trigger TRIG_DB_PROTECT on database for drop_table, alter_table
as
begin
    print N'структура базы защищена триггером!';
    rollback;
end;
go