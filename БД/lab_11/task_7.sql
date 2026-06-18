use Z_MYBASE_2;
go

declare @client_name nvarchar(100), 
        @all_clients nvarchar(max) = N'';

declare curs_list cursor local
    for select ФИО from КЛИЕНТЫ;

open curs_list;

fetch curs_list into @client_name;

while @@fetch_status = 0
begin
    set @all_clients = @all_clients + rtrim(@client_name) + N', ';
    fetch curs_list into @client_name;
end;

print N'Все клиенты банка: ' + @all_clients;

close curs_list;
deallocate curs_list;
go

use Z_MYBASE_2;
go

declare @rn int, @id int, @fio nvarchar(100);

declare curs_scroll cursor local dynamic scroll
    for select row_number() over (order by ФИО), ID_КЛИЕНТА, ФИО from КЛИЕНТЫ;

open curs_scroll;

fetch last from curs_scroll into @rn, @id, @fio;
print N'Последний в списке (№' + cast(@rn as nvarchar) + N'): ' + cast(@id as nvarchar) + N' - ' + @fio;

fetch first from curs_scroll into @rn, @id, @fio;
print N'Первый в списке (№' + cast(@rn as nvarchar) + N'): ' + cast(@id as nvarchar) + N' - ' + @fio;

fetch absolute 2 from curs_scroll into @rn, @id, @fio;
print N'Второй по счету (№' + cast(@rn as nvarchar) + N'): ' + cast(@id as nvarchar) + N' - ' + @fio;

close curs_scroll;
deallocate curs_scroll;
go

use Z_MYBASE_2;
go

declare @contract_num int, @status nvarchar(20);

declare curs_update cursor local dynamic
    for select НОМЕР_ДОГОВОРА, СТАТУС from ВЫДАЧА_КРЕДИТОВ
    for update; 

open curs_update;

fetch curs_update into @contract_num, @status;

while @@fetch_status = 0
begin
    if @status = N'Активен'
    begin
        update ВЫДАЧА_КРЕДИТОВ 
        set СТАТУС = N'Проверка'
        where current of curs_update; 
    end

    fetch curs_update into @contract_num, @status;
end;

close curs_update;
deallocate curs_update;
go