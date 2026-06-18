use z_MyBase_2
go

select id_клиента from выдача_кредитов where код_вида = 1
intersect

select id_клиента from выдача_кредитов where код_вида <> 1;