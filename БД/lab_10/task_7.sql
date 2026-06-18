use z_MyBase_2;
go

--1
drop index if exists IX_ФИО on КЛИЕНТЫ;
go

--2
create index IX_ФИО on КЛИЕНТЫ(ФИО);
go

--3
drop index if exists IX_ID_DATA on ВЫДАЧА_КРЕДИТОВ;
go

--4
create index IX_ID_DATA on ВЫДАЧА_КРЕДИТОВ(ID_клиента, Дата_выдачи);
go

--5
drop index if exists IX_COVER_STATUS on ВЫДАЧА_КРЕДИТОВ;
go

--6
create index IX_COVER_STATUS on ВЫДАЧА_КРЕДИТОВ(Сумма) include (Статус);
go

--7
drop index if exists IX_BIG_CREDITS on ВЫДАЧА_КРЕДИТОВ;
go

--8
create index IX_BIG_CREDITS on ВЫДАЧА_КРЕДИТОВ(Сумма) 
where (Сумма > 50000);
go

--9
drop index if exists IX_PASSPORT_FILL on КЛИЕНТЫ;
go

--10
create index IX_PASSPORT_FILL on КЛИЕНТЫ(Паспорт) 
with (fillfactor = 70);
go

--11
select * from КЛИЕНТЫ where ФИО like N'Иванов%';
go

--12
select Статус from ВЫДАЧА_КРЕДИТОВ where Сумма = 100000;
go

--13
select Номер_договора from ВЫДАЧА_КРЕДИТОВ where Сумма > 60000;
go