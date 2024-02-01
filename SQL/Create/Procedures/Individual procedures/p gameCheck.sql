create procedure gameCheck @game_id	int
as
select * from game where game_id = @game_id
go

