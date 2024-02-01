------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
--Game procedures
--	Called by gameRider.cs
--	Checks to see what the most recent game is
create procedure lastGame
as
select top 1 * 
from game 
order by date desc, game_id desc
go

--Checks the games from the last three days to see if their database values need to be updated
create procedure checkGames
as
select * from game 
where date >= GETDATE() - 3
order by game_id desc
go

--Inserts game into game table
create procedure gameInsert 
@game_id	int,
@date		date,
@team_idH	int,
@team_idA	int,
@team_idW	int,
@wScore		int,
@team_idL	int,
@lScore		int,
@arena_id	int,
@sellout	bit
as
insert into game values(
@game_id,
@date,
@team_idH,
@team_idA,
@team_idW,
@wScore,
@team_idL,	
@lScore,	
@arena_id,	
@sellout	
)
go

--If the game already exists and the score has changed, update it
create procedure gameUpdate @game_id int, @team_idW int, @wScore int, @team_idL int, @lScore int
as
update game set team_idW = @team_idW, wScore = @wScore, team_idL = @team_idL, lScore = @lScore
where game_id = @game_id
go
