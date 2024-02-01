create procedure lastBox
as 
select top 1 * 
from teamBox 
order by date desc, game_id desc
go
