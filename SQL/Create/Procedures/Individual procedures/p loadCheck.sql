create procedure loadCheck
as
select * from FirstTimeLoad where loadCheck = 1
go