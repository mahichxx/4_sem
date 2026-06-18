use UNIVER;
go

--1
declare @total_capacity int,
        @count_aud int,
        @avg_capacity numeric(10,2),
        @count_less_avg int,
        @percent numeric(10,2);

--2
set @total_capacity = (select sum(AUDITORIUM_CAPACITY) from AUDITORIUM);

--3
if @total_capacity > 200
begin
    --4
    select @count_aud = count(*),
           @avg_capacity = avg(cast(AUDITORIUM_CAPACITY as float))
    from AUDITORIUM;

    --5
    select @count_less_avg = count(*) 
    from AUDITORIUM 
    where AUDITORIUM_CAPACITY < @avg_capacity;

    --6
    set @percent = (cast(@count_less_avg as float) / @count_aud) * 100;

    --7
    select 
        @total_capacity as N'общая вместимость',
        @count_aud as N'количество аудиторий',
        @avg_capacity as N'средняя вместимость',
        @count_less_avg as N'меньше средней',
        @percent as N'процент таких (%)';
end
else
begin
    --8
    print N'общая вместимость аудиторий составляет: ' + cast(@total_capacity as nvarchar(10));
end
go