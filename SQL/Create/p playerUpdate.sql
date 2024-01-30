create procedure playerUpdate @player_id int, @position varchar(30)
as
update player set position = @position where player_id = @player_id
go
