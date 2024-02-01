--PlayByPlay procedures
--	Checks to see if we have the current game, and the number of actions or rows we have.
--	If we have the game and the number of actions differs from that of the endpoint, we insert every subsequent action
create procedure DupeCheck @game_id int
as 
select game_id, count(actionNumber) actionCount
from PlayByPlay
where game_id = @game_id
group by game_id
go

--Inserts single play from PlayByPlay endpoint
create procedure PlayByPlayInsert
			@game_id				int null, 
			@actionNumber			int null, 
			@clock					varchar(20) null, 
			@timeActual				datetime null, 
			@period					int null, 
			@periodType				varchar(20) null, 
			@team_id				int null, 
			@teamTricode			varchar(3) null, 
			@event_msg_type_id		int null, 
			@actionType				varchar(30) null, 
			@actionSub 				varchar(20) null, 
			@description 			varchar(100) null,
			@descriptor 			varchar(30) null, 
			@qualifier1 			varchar(30) null, 
			@qualifier2 			varchar(30) null, 
			@qualifier3 			varchar(30) null, 
			@player_id 				int null, 
			@x 						float null, 
			@y 						float null, 
			@area 					varchar(50) null, 
			@areaDetail				varchar(50) null, 
			@side 					varchar(30) null, 
			@shotDistance 			float null, 
			@scoreHome 				int null, 
			@scoreAway 				int null, 
			@isFieldGoal 			bit null, 
			@shotResult 			varchar(20) null,  
			@shotActionNumber		int null, 	
			@player_idAST 			int null, 	
			@player_idBLK 			int null, 	
			@player_idSTL 			int null, 	
			@player_idFoulDrawn		int null, 
			@player_idJumpW 		int null, 	
			@player_idJumpL 		int null, 	
			@official_id			int
as
insert into PlayByPlay values(
			@game_id,
			@actionNumber,
			@clock,
			@timeActual,
			@period,
			@periodType,
case when	@team_id = 0 then null else @team_id end,
case when	@teamTricode = '' then null else @teamTricode end,
case when	@isFieldGoal = 1 and @shotResult = 'Made' then 1		--FIELD_GOAL_MADE
	 when	@isFieldGoal = 1 and @shotResult = 'Missed' then 2	--FIELD_GOAL_MISSED
	 when	@actionType = 'freethrow' then 3						--FREE_THROW
	 when	@actionType = 'rebound' then 4						--REBOUND
	 when	@actionType = 'turnover' then 5						--TURNOVER
	 when	@actionType = 'foul' then 6							--FOUL
	 when	@actionType = 'violation' then 7						--VIOLATION
	 when	@actionType = 'substitution' then 8					--SUBSTITUTION
	 when	@actionType = 'timeout' then 9						--TIMEOUT
	 when	@actionType = 'jumpball' then 10						--JUMP_BALL
	 when	@actionType = 'ejection' then 11						--EJECTION
	 when	@actionType = 'period' and @actionSub='start' then 12 --PERIOD_BEGIN
	 when	@actionType = 'period' and @actionSub = 'end' then 13	--PERIOD_END
	 when	@actionType = 'block' then 14				--new
	 when	@actionType = 'steal' then 15				--new
	 else	18 end, --UNKNOWN
case when	@actionType = '2pt' and @shotResult = 'Made' then '2PTM'		--2 Made
	 when	@actionType = '2pt' and @shotResult = 'Missed' then '2PTA'	--2 Attempted
	 when	@actionType = '3pt' and @shotResult = 'Made' then '3PTM'		--3 Made
	 when	@actionType = '3pt' and @shotResult = 'Missed' then '3PTA'	--3 Attempted
	 when	@actionType = 'freethrow' and @shotResult = 'Made' then 'FTM'	--Free Throw Made
	 when	@actionType = 'freethrow' and @shotResult = 'Missed' then 'FTA'--Free Throw Attempted
	 else	null end,
case when	@actionType = '2pt' and @shotResult = 'Made' then 2		--2 Made
	 when	@actionType = '3pt' and @shotResult = 'Made' then 3		--3 Made
	 when	@actionType = 'freethrow' and @shotResult = 'Made' then 1	--Free Throw Made
	 else	0 end,
			@actionType,
			@actionSub,
case when	@description = '' then null else @description end,
case when	@descriptor = '' then null else @descriptor end,
case when	@qualifier1 = '' then null else @qualifier1 end,
case when	@qualifier2 = '' then null else @qualifier2 end,
case when	@qualifier3 = '' then null else @qualifier3 end,
case when	@player_id = 0 then null else @player_id end,
case when	@x = 0 then null else @x end,
case when	@y = 0 then null else @y end,
case when	@area = '' then null else @area end,
case when	@areaDetail = '' then null else @areaDetail end,
case when	@side = '' then null else @side end,
case when	@shotDistance = 0 then null else @shotDistance end,
			@scoreHome,
			@scoreAway,
			@isFieldGoal,
case when	@shotResult = '' then null else @shotResult end,
case when	@shotActionNumber = 0 then null else @shotActionNumber end,
case when	@player_idAST = 0 then null else @player_idAST end,
case when	@player_idBLK = 0 then null else @player_idBLK end,
case when	@player_idSTL = 0 then null else @player_idSTL end,
case when	@player_idFoulDrawn = 0 then null else @player_idFoulDrawn end,
case when	@player_idJumpW = 0 then null else @player_idJumpW end,
case when	@player_idJumpL = 0 then null else @player_idJumpL end,
case when	@official_id = 0 then null else @official_id end)
go


