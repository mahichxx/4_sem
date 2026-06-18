use UNIVER;
go

select * from AUDITORIUM;
go
print N'число обработанных строк (@@rowcount): ' + cast(@@rowcount as nvarchar);
print N'версия sql server (@@version): ' + @@version;
print N'идентификатор процесса (@@spid): ' + cast(@@spid as nvarchar);
print N'код последней ошибки (@@error): ' + cast(@@error as nvarchar);
print N'имя сервера (@@servername): ' + cast(@@servername as nvarchar);
print N'уровень вложенности транзакций (@@trancount): ' + cast(@@trancount as nvarchar);
print N'результат считывания строк (@@fetch_status): ' + cast(@@fetch_status as nvarchar);
print N'уровень вложенности процедуры (@@nestlevel): ' + cast(@@nestlevel as nvarchar);
go