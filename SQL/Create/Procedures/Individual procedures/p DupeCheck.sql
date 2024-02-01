create procedure DupeCheck @game_id int
as 
select game_id, count(actionNumber) actionCount
from PlayByPlay
where game_id = @game_id
group by game_id
go


