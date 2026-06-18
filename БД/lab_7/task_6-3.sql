use z_MyBase_2
go

-- 1
select фио, N'крупный' as [тип_клиента]
from клиенты 
where id_клиента in (select id_клиента from выдача_кредитов where сумма > 1000000)

union

-- 1
select фио, N'мелкий' as [тип_клиента]
from клиенты 
where id_клиента in (select id_клиента from выдача_кредитов where сумма < 200000);