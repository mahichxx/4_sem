use univer;
go

select p.profession_name, pr.subject, avg(cast(pr.note as float)) as [ср_балл]
from profession p 
inner join groups g on p.profession = g.profession
inner join student s on g.idgroup = s.idgroup
inner join progress pr on s.idstudent = pr.idstudent
where p.faculty = N'ИТ'
group by p.profession_name, pr.subject

union -- Объединение 

select p.profession_name, pr.subject, avg(cast(pr.note as float)) as [ср_балл]
from profession p 
inner join groups g on p.profession = g.profession
inner join student s on g.idgroup = s.idgroup
inner join progress pr on s.idstudent = pr.idstudent
where p.faculty = N'хтит'
group by p.profession_name, pr.subject;