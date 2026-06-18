use UNIVER;
go

-- 4
select 
    f.faculty_name as [факультет], 
    p.profession_name as [специальность], 
    g.year_first as [год_поступления], 
    round(avg(cast(pr.note as float)), 2) as [ср_оценка_профильных]
-- 1
from faculty as f
inner join profession as p on f.faculty = p.faculty
inner join groups as g on p.profession = g.profession
inner join student as s on g.idgroup = s.idgroup
inner join progress as pr on s.idstudent = pr.idstudent
-- 2
where pr.subject in (N'БД', N'ОАиП')
-- 3
group by f.faculty_name, p.profession_name, g.year_first
-- 5
order by [ср_оценка_профильных] desc;