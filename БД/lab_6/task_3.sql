use univer;
go

--5
select *
from (
--3
    select 
        case 
            when note between 1 and 3 then  N'неудовлетворительно'
            when note between 4 and 6 then N'удовлетворительно'
            when note between 7 and 8 then N'хорошо'
            else N'отлично'
        end as [категория_оценки],
        --4
        count(*) as [количество_студентов]
    --1
    from progress
    --2
    group by 
        case 
            when note between 1 and 3 then N'неудовлетворительно'
            when note between 4 and 6 then N'удовлетворительно'
            when note between 7 and 8 then N'хорошо'
            else N'отлично'
        end
) as t
--6
order by [категория_оценки] desc;