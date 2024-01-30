create procedure gameUpdate @game_id int, @team_idW int, @wScore int, @team_idL int, @lScore int
as
update game set team_idW = @team_idW, wScore = @wScore, team_idL = @team_idL, lScore = @lScore
where game_id = @game_id
go
