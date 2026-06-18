use UNIVER;
go

--1
begin try
    --2
    declare @x int = 10, @y int = 0;
    print N'попытка выполнить деление на ноль...';
    set @x = @x / @y;
    print N'этот текст не будет напечатан, так как возникнет ошибка';
--4
end try
--5
begin catch
    print N'--- информация об ошибке ---';
    print N'код ошибки: ' + cast(error_number() as nvarchar);
    print N'сообщение: ' + error_message();
    print N'номер строки: ' + cast(error_line() as nvarchar);
    print N'имя процедуры: ' + isnull(error_procedure(), N'выполнялся скрипт');
    print N'уровень серьезности: ' + cast(error_severity() as nvarchar);
    print N'метка состояния: ' + cast(error_state() as nvarchar);
--6
end catch;
go