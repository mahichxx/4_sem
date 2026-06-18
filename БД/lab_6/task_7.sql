use univer;
go

-- 4
select 
    s.subject_name as [дисциплина], 
    count(*) as [количество_студентов]
-- 1
from progress as p
inner join subject as s on p.subject = s.subject
-- 2
where p.note in (8, 9)
-- 3
group by s.subject_name
-- 5
having count(*) >= 1
-- 6
order by [количество_студентов] desc;