create table FirstTimeLoad(
loadCheck bit)
go

create procedure loadCheck
as
select * from FirstTimeLoad where loadCheck = 1
go

create procedure loadPost
as 
insert into FirstTimeLoad values(1)
go
