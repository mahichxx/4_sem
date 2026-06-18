use Z_MYBASE_2;
go
set implicit_transactions on;

if object_id(N'ПРОТОКОЛ') is not null drop table ПРОТОКОЛ;

create table ПРОТОКОЛ (ID int, ИНФО nvarchar(50));
insert into ПРОТОКОЛ values (10, N'запись 1'), (20, N'запись 2');
select count(*) as N'Строк в неявной транзакции' from ПРОТОКОЛ;
commit;

set implicit_transactions off;
go


begin try
    begin transaction;
    
    insert into КЛИЕНТЫ (ID_КЛИЕНТА, ФИО, ПАСПОРТ) 
    values (777, N'Иванов Иван', N'МС 111222');
    
    update ВЫДАЧА_КРЕДИТОВ set СТАТУС = N'Закрыт' 
    where НОМЕР_ДОГОВОРА = 1;
    
    commit transaction;
    print N'Свойство атомарности: оба действия выполнены успешно.';
end try
begin catch

    if @@trancount > 0 rollback transaction;
    print N'Свойство атомарности: произошла ошибка, всё отменено. Причина: ' + error_message();
end catch
go

declare @point nvarchar(32);
begin try
    begin transaction;
    
    insert into ПРОТОКОЛ values (100, N'начало');
    set @point = N'p1';
    save transaction @point; 
    
    insert into ПРОТОКОЛ values (200, N'середина');
    set @point = N'p2';
    save transaction @point; 
    
    insert into ПРОТОКОЛ values (300, N'ошибка');
    
    set @point = N'p2';
    rollback transaction @point;
    
    commit transaction;
    print N'Оператор SAVE TRAN: выполнен частичный откат к точке p2.';
end try
begin catch
    if @@trancount > 0 rollback transaction;
end catch
go


set transaction isolation level read uncommitted;
begin transaction;
select * from КЛИЕНТЫ;
commit;
go


set transaction isolation level read committed;
begin transaction;
select count(*) from ВЫДАЧА_КРЕДИТОВ where СТАТУС = N'Активен';
commit;
go


set transaction isolation level repeatable read;
begin transaction;
select * from ВИДЫ_КРЕДИТА where НАЗВАНИЕ = N'Потребительский';
commit;
go


set transaction isolation level serializable;
begin transaction;
select * from ВЫДАЧА_КРЕДИТОВ where НОМЕР_ДОГОВОРА > 0;
commit;
go


begin transaction;
insert into ПРОТОКОЛ values (1, N'внешняя');

    begin transaction;
    update ПРОТОКОЛ set ИНФО = N'вложенная' where ID = 1;
    commit;

select * from ПРОТОКОЛ;
go