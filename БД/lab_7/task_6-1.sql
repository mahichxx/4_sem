use z_MyBase_2
go 

-- 4
select 
    статус, 
    код_вида, 
    sum(сумма) as [итоговая_сумма]
-- 1
from выдача_кредитов
-- 2
where сумма > 0
-- 3
group by rollup (статус, код_вида);