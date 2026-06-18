use UNIVER;
go

select
	case
		when a.AUDITORIUM_CAPACITY < 50 then N'меньше 50'
		when a.AUDITORIUM_CAPACITY between 50 and 100 then '50-100'
		else N'больше 100'
	end as [Диапазон вместимости],

	count(*) as [Количество аудиторий]

from AUDITORIUM a

group by
	case
		when a.AUDITORIUM_CAPACITY < 50 then N'меньше 50'
		when a.AUDITORIUM_CAPACITY between 50 and 100 then '50-100'
		else N'больше 100'
	end
order by [Диапазон вместимости];