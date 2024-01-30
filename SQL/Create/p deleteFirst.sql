create procedure deleteFirst @game_id int, @team_id int
as
delete from teamBox where game_id = @game_id and team_id = @team_id
delete from playerBox where game_id = @game_id and team_id = @team_id
go
