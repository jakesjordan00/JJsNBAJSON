create procedure lastGame
as
select top 1 * 
from game 
order by date desc, game_id desc
go
