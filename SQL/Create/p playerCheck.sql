create procedure playerCheck @player_id	int
as
select * from player where player_id = @player_id
go

