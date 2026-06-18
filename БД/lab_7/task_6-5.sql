use z_MyBase_2
go

select id_клиента from выдача_кредитов
except

select id_клиента from выдача_кредитов where статус = N'Закрыт';