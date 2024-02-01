create procedure jsonGames
as
select distinct 
case when g.game_id is null then p.game_id else g.game_id end game_id
from game g  left join
		PlayByPlay p on g.game_id = p.game_id
where timeActual > getdate() - 3 or timeActual is null
order by game_id asc