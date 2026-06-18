use z_MyBase_2
go

-- 4
select 
    статус, 
    код_вида, 
    sum(сумма) as [сумма_аналитика]
-- 1
from выдача_кредитов
-- 2
where сумма > 0
-- 3
group by cube (статус, код_вида);