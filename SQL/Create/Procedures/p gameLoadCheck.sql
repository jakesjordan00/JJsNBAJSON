create procedure gameLoadCheck
as
select * from game 
where date >= GETDATE() - 3
order by game_id desc
go
