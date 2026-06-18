use z_mybase_2;
go

-- 4
select 
    к.фио, 
    в.сумма as [сумма_кредита], 
    (select round(avg(сумма), 2) from выдача_кредитов) as [среднее_по_банку]
-- 1
from клиенты as к
-- 2
inner join выдача_кредитов as в on к.id_клиента = в.id_клиента
-- 3
where в.сумма > (select avg(сумма) from выдача_кредитов)
-- 5
order by в.сумма desc;