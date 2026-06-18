use z_MyBase_2;
go

--1
drop view [Клиенты_Инфо];
go

--2
create view [Клиенты_Инфо]
--3
as select 
    ID_клиента as N'код', 
    ФИО as N'имя заемщика', 
    Паспорт as N'паспортные данные'
--4
from Клиенты;
go

--5
drop view [Итоги_кредитования];
go

--6
create view [Итоги_кредитования]
--7
as select 
    Виды_кредита.Название as N'тип кредита', 
    count(*) as N'количество выдач',
    sum(Сумма) as N'общая сумма'
--8
from Виды_кредита inner join Выдача_кредитов 
--9
on Виды_кредита.Код_вида = Выдача_кредитов.Код_вида
--10
group by Виды_кредита.Название;
go

--11
drop view [Крупные_выдачи];
go

--12
create view [Крупные_выдачи]
--13
as select 
    Номер_договора as N'договор', 
    Сумма as N'сумма кредита',
    Статус as N'статус'
--14
from Выдача_кредитов
--15
where Сумма > 20000
with check option;
go

--16
drop view [Защищенный_вид];
go

--17
create view [Защищенный_вид] with schemabinding
--18
as select 
    dbo.Клиенты.ФИО as N'фио',
    dbo.Клиенты.Дата_регистрации as N'дата регистрации'

from dbo.Клиенты;
go

select * from [Клиенты_Инфо];
go

select * from [Итоги_кредитования];
go

select * from [Крупные_выдачи];
go

select * from [Защищенный_вид];
go