create procedure checkBox
as
select g.date, t.game_id, t.team_id, t.points, t.atHome
from teamBox t inner join 
		game g on t.game_id = g.game_id
where g.date >= (select max(game.date) from teamBox inner join game on teamBox.game_id = game.game_id)
order by date desc, game_id desc
go


