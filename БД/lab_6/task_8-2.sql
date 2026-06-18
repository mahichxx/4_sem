use z_mybase_2;
go

-- 3
select 
    vk.название as [неиспользуемый_продукт], 
    vk.ставка as [упущенная_выгода_%],
    N'ни одной выдачи' as [статус_аналитики]
-- 1
from виды_кредита as vk
-- 2
where not exists (
    select * 
    from выдача_кредитов as v 
    where v.код_вида = vk.код_вида
);