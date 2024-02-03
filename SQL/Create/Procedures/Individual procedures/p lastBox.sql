create procedure lastBox
as 
select top 1 * 
from teamBox t inner join 
		game g on t.game_id = g.game_id
order by date desc, t.game_id desc
go
