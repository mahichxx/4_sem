use UNIVER;
go

--1
declare @c char(1) = 'A',
        @vc nvarchar(30) = N'основы t-sql';

--2
declare @dt datetime,
        @tm time,
        @i int,
        @si smallint,
        @ti tinyint,
        @num numeric(12, 5);

--3
set @dt = getdate();

--4
set @tm = '15:20:00';

--5
select @i = 12345,
       @si = 1000;

--6
select @ti = 255,
       @num = 9876.54321;

--7
select 
    @c as N'тип char', 
    @vc as N'тип varchar', 
    @dt as N'тип datetime', 
    @tm as N'тип time';
go

--8
print N'значение int: ' + cast(12345 as varchar);

--9
print N'значение smallint: ' + cast(1000 as varchar);

--10
print N'значение tinyint: ' + cast(255 as varchar);

--11
print N'значение numeric: ' + cast(9876.54321 as varchar);
go