use univer
go

-- Исключение except
select p.profession_name, pr.subject, avg(cast(pr.note as float))
from profession p 
inner join groups g on p.profession = g.profession
inner join student s on g.idgroup = s.idgroup
inner join progress pr on s.idstudent = pr.idstudent
where p.faculty = N'ИТ'
group by p.profession_name, pr.subject

except

select p.profession_name, pr.subject, avg(cast(pr.note as float))
from profession p 
inner join groups g on p.profession = g.profession
inner join student s on g.idgroup = s.idgroup
inner join progress pr on s.idstudent = pr.idstudent
where p.faculty = N'ИДиП'
group by p.profession_name, pr.subject;