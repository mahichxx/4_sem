use univer;
go

-- 4
select 
    p.profession_name as [специальность], 
    pr.subject as [дисциплина], 
    round(avg(cast(pr.note as float)), 2) as [средняя_оценка]
-- 1
from faculty as f
inner join profession as p on f.faculty = p.faculty
inner join groups as g on p.profession = g.profession
inner join student as s on g.idgroup = s.idgroup
inner join progress as pr on s.idstudent = pr.idstudent
-- 2
where f.faculty = N'ИТ'
-- 3
group by p.profession_name, pr.subject;