use univer;
go

--4
select 
    f.faculty_name as [факультет], 
    p.profession_name as [специальность], 
    g.year_first as [год_поступления], 
    round(avg(cast(pr.note as float)), 2) as [средняя_оценка]
--1
from faculty as f
--2
inner join profession as p on f.faculty = p.faculty
inner join groups as g on p.profession = g.profession
inner join student as s on g.idgroup = s.idgroup
inner join progress as pr on s.idstudent = pr.idstudent
--3
group by f.faculty_name, p.profession_name, g.year_first
--5
order by [средняя_оценка] desc;