create procedure checkGames
as
select * from game 
where date >= GETDATE() - 3
order by game_id desc
go

create procedure checkPicture
as
select top 1 * from PlayoffPicture
go

create procedure deleteFirst @game_id int, @team_id int
as
delete from teamBox where game_id = @game_id and team_id = @team_id
delete from playerBox where game_id = @game_id and team_id = @team_id
go

create procedure DupeCheck @game_id int
as 
select game_id, count(actionNumber) actionCount
from PlayByPlay
where game_id = @game_id
group by game_id
go

create procedure gameCheck @game_id	int
as
select * from game where game_id = @game_id
go

create procedure firstBox
as
select top 1 game_id, team_id from teamBox
go

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

create procedure checkBracket
as
select top 1 * from PlayoffBracket

create procedure checkBox
as
select g.date, t.game_id, t.team_id, t.points, t.atHome
from teamBox t inner join 
		game g on t.game_id = g.game_id
where g.date >= (select max(game.date) from teamBox inner join game on teamBox.game_id = game.game_id)
order by date desc, game_id desc
go

create procedure bracketInsert 
@series_id				int,
@conference				varchar(4),	
@roundNumber			int,
@description			varchar(100),
@status					int,		
@seriesWinner			int,
@highSeedId				int,
@highSeedSeriesWins		int,
@lowSeedId				int,
@lowSeedSeriesWins	 	int,
@nextGame_id			int,		
@nextGameNumber			int,
@nextSeries_id			int
as
insert into PlayoffBracket values(
@series_id			,
@conference			,
@roundNumber		,
@description		,
@status				,
@seriesWinner		,
@highSeedId			,
@highSeedSeriesWins	,
@lowSeedId			,
@lowSeedSeriesWins	,
@nextGame_id		,
@nextGameNumber		,
@nextSeries_id		
)

create procedure blankBracketInsert 
@series_id				int,
@conference				varchar(4),	
@roundNumber			int,
@status					int		
as
insert into PlayoffBracket (series_id, conference, roundNumber, status)
values(
@series_id			,
@conference			,
@roundNumber		,
@status)
go

create procedure arenaInsert 
@arena_id	int,
@team_id	int,
@name		varchar(100),
@city		varchar(2),
@state		varchar(2),
@country	varchar(3)
as
insert into arena values(
@arena_id,
@team_id,
@name,	
@city,	
@state,
@country)
go

create procedure blackBracketCheck @series_id int
as
select * from PlayoffBracket where series_id = @series_id
go

create procedure arenaCheck @arena_id	int
as
select * from arena where arena_id = @arena_id
go

create procedure gameLoadCheck
as
select * from game 
where date >= GETDATE() - 3
order by game_id desc
go


create procedure updatePictureCheck
@conference					varchar(4),	
@matchupType				varchar(30),
@highSeed_id				int,		
@highSeedRank				int,
@highSeedRegSeasonWins		int,
@highSeedRegSeasonLosses	int,
@lowSeed_id					int,		
@lowSeedRank				int,
@lowSeedRegSeasonWins		int,
@lowSeedRegSeasonLosses 	int
as
select * 
from PlayoffPicture
where (conference = @conference and matchupType = @matchupType) and (
highSeed_id				!= @highSeed_id				or
highSeedRank			!= @highSeedRank			or
highSeedRegSeasonWins	!= @highSeedRegSeasonWins	or
highSeedRegSeasonLosses	!= @highSeedRegSeasonLosses or
lowSeed_id				!= @lowSeed_id				or
lowSeedRank				!= @lowSeedRank				or
lowSeedRegSeasonWins	!= @lowSeedRegSeasonWins	or
lowSeedRegSeasonLosses	!= @lowSeedRegSeasonLosses)
go

create procedure teamInsert
@team_id	 int, 
@tricode	 varchar(3),
@city		 varchar(30),
@name		 varchar(30),
@yearFounded int			
as
insert into team values(
@team_id,	
@tricode,	
@city,		
@name,		
@yearFounded
)
go

create procedure updateBracket
@series_id				int,
@conference				varchar(4),	
@roundNumber			int,
@description			varchar(100),
@status					int,		
@seriesWinner			int,
@highSeed_id			int,
@highSeedSeriesWins		int,
@lowSeed_id				int,
@lowSeedSeriesWins	 	int,
@nextGame_id			int,		
@nextGameNumber			int,
@nextSeries_id			int
as
update PlayoffBracket set
description			= @description			,
status				= @status				,
seriesWinner		= @seriesWinner			,
highSeed_id			= @highSeed_id			,
highSeedSeriesWins	= @highSeedSeriesWins	,
lowSeed_id			= @lowSeed_id			,
lowSeedSeriesWins	= @lowSeedSeriesWins	,
nextGame_id			= @nextGame_id			,
nextGameNumber		= @nextGameNumber		,
nextSeries_id		= @nextSeries_id		
where series_id = @series_id and (
description			!= @description			or
status				!= @status				or
seriesWinner		!= @seriesWinner		or
highSeed_id			!= @highSeed_id			or
highSeedSeriesWins	!= @highSeedSeriesWins	or
lowSeed_id			!= @lowSeed_id			or
lowSeedSeriesWins	!= @lowSeedSeriesWins	or
nextGame_id			!= @nextGame_id			or
nextGameNumber		!= @nextGameNumber		or
nextSeries_id		!= @nextSeries_id		)


create procedure updateBracketCheck
@series_id				int,
@conference				varchar(4),	
@roundNumber			int,
@description			varchar(100),
@status					int,		
@seriesWinner			int,
@highSeed_id			int,
@highSeedSeriesWins		int,
@lowSeed_id				int,
@lowSeedSeriesWins	 	int,
@nextGame_id			int,		
@nextGameNumber			int,
@nextSeries_id			int
as
select * 
from PlayoffBracket
where series_id = @series_id and (
description				!= @description				or
status					!= @status					or
seriesWinner			!= @seriesWinner			or
highSeed_id				!= @highSeed_id				or
highSeedSeriesWins		!= @highSeedSeriesWins		or
lowSeed_id				!= @lowSeed_id				or
lowSeedSeriesWins		!= @lowSeedSeriesWins		or
nextGame_id				!= @nextGame_id				or
nextGameNumber			!= @nextGameNumber			or
nextSeries_id			!= @nextSeries_id)
go



create procedure updateBracketCheckEntry
@series_id				int,
@conference				varchar(4),	
@roundNumber			int,
@description			varchar(100),
@status					int,		
@seriesWinner			int,
@highSeed_id			int,
@highSeedSeriesWins		int,
@lowSeed_id				int,
@lowSeedSeriesWins	 	int,
@nextGame_id			int,		
@nextGameNumber			int,
@nextSeries_id			int
as
select * 
from PlayoffBracket
where 
series_id = @series_id								and 
conference = @conference		and
description				= @description				and
status					= @status					and
seriesWinner			= @seriesWinner				and
highSeed_id				= @highSeed_id				and
highSeedSeriesWins		= @highSeedSeriesWins		and
lowSeed_id				= @lowSeed_id				and
lowSeedSeriesWins		= @lowSeedSeriesWins		and
nextGame_id				= @nextGame_id				and
nextGameNumber			= @nextGameNumber			and
nextSeries_id			= @nextSeries_id
go

create procedure updatePicture
@conference					varchar(4),	
@matchupType				varchar(30),
@highSeed_id				int,		
@highSeedRank				int,
@highSeedRegSeasonWins		int,
@highSeedRegSeasonLosses	int,
@lowSeed_id					int,		
@lowSeedRank				int,
@lowSeedRegSeasonWins		int,
@lowSeedRegSeasonLosses 	int
as
update PlayoffPicture set
highSeed_id				= @highSeed_id				,
highSeedRank			= @highSeedRank				,
highSeedRegSeasonWins	= @highSeedRegSeasonWins	,
highSeedRegSeasonLosses	= @highSeedRegSeasonLosses	,
lowSeed_id				= @lowSeed_id				,
lowSeedRank				= @lowSeedRank				,
lowSeedRegSeasonWins	= @lowSeedRegSeasonWins		,
lowSeedRegSeasonLosses	= @lowSeedRegSeasonLosses
where (conference = @conference and matchupType = @matchupType) and (
highSeed_id				!= @highSeed_id				or
highSeedRank			!= @highSeedRank			or
highSeedRegSeasonWins	!= @highSeedRegSeasonWins	or
highSeedRegSeasonLosses	!= @highSeedRegSeasonLosses or
lowSeed_id				!= @lowSeed_id				or
lowSeedRank				!= @lowSeedRank				or
lowSeedRegSeasonWins	!= @lowSeedRegSeasonWins	or
lowSeedRegSeasonLosses	!= @lowSeedRegSeasonLosses)
go

create procedure updatePictureCheckEntry
@conference					varchar(4),	
@matchupType				varchar(30),
@highSeed_id				int,		
@highSeedRank				int,
@highSeedRegSeasonWins		int,
@highSeedRegSeasonLosses	int,
@lowSeed_id					int,		
@lowSeedRank				int,
@lowSeedRegSeasonWins		int,
@lowSeedRegSeasonLosses 	int
as
select * 
from PlayoffPicture
where 
conference				= @conference				and 
matchupType				= @matchupType				and 
highSeedRank			= @highSeedRank				and 
highSeed_id				= @highSeed_id				and
highSeedRank			= @highSeedRank				and
highSeedRegSeasonWins	= @highSeedRegSeasonWins	and
highSeedRegSeasonLosses	= @highSeedRegSeasonLosses	and
lowSeed_id				= @lowSeed_id				and
lowSeedRank				= @lowSeedRank				and
lowSeedRegSeasonWins	= @lowSeedRegSeasonWins		and
lowSeedRegSeasonLosses	= @lowSeedRegSeasonLosses
go

create procedure lastGame
as
select top 1 * 
from game 
order by date desc, game_id desc
go

create procedure loadCheck
as
select * from FirstTimeLoad where loadCheck = 1
go

create procedure officialCheck @official_id	int
as
select * from official where official_id = @official_id
go

create procedure officialInsert 
@official_id int,
@name		 varchar(50),
@number		 int,
@assignment	 varchar(50)
as
insert into official values(
@official_id,
@name,
@number,
@assignment
)
go

create procedure pictureInsert
@conference					varchar(4),	
@matchupType				varchar(30),
@highSeed_id				int,		
@highSeedRank				int,
@highSeedRegSeasonWins		int,
@highSeedRegSeasonLosses	int,
@lowSeed_id					int,		
@lowSeedRank				int,
@lowSeedRegSeasonWins		int,
@lowSeedRegSeasonLosses 	int
as
insert into PlayoffPicture values(
@conference				,
@matchupType			,
@highSeed_id			,
@highSeedRank			,
@highSeedRegSeasonWins	,
@highSeedRegSeasonLosses,
@lowSeed_id				,
@lowSeedRank			,
@lowSeedRegSeasonWins	,
@lowSeedRegSeasonLosses)
go

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

create procedure playerCheck @player_id	int
as
select * from player where player_id = @player_id
go


create procedure playerBoxInsert
@game_id						int,
@team_id						int,
@player_id						int,
@status							varchar(30),
@starter						int,
@position						varchar(2),
@points							int,
@assists						int   ,
@blocks							int   ,
@blocksReceived					int   ,
@fieldGoalsAttempted			int   ,
@fieldGoalsMade					int   ,
@fieldGoalsPercentage			float,
@foulsOffensive					int   ,
@foulsDrawn						int   ,
@foulsPersonal					int   ,
@foulsTechnical					int   ,
@freeThrowsAttempted			int   ,
@freeThrowsMade					int   ,
@freeThrowsPercentage			float,
@minus							float,
@minutes						varchar(30),
@minutesCalculated				varchar(30),
@plus							float,
@plusMinusPoints				float,
@pointsFastBreak				int   ,
@pointsInThePaint				int   ,
@pointsSecondChance				int   ,
@reboundsDefensive				int   ,
@reboundsOffensive				int   ,
@reboundsTotal					int   ,
@steals							int   ,
@threePointersAttempted			int   ,
@threePointersMade				int   ,
@threePointersPercentage		float,
@turnovers						int   ,
@twoPointersAttempted			int   ,
@twoPointersMade				int   ,
@twoPointersPercentage			float,
@statusReason					varchar(100),
@statusDescription				varchar(200)
as
insert into playerBox values(
@game_id						,
@team_id						,
@player_id						,
@status							,
@starter						,
@position						,
@points							,
@assists						,
@blocks							,
@blocksReceived					,
@fieldGoalsAttempted			,
@fieldGoalsMade					,
@fieldGoalsPercentage			,
@foulsOffensive					,
@foulsDrawn						,
@foulsPersonal					,
@foulsTechnical					,
@freeThrowsAttempted			,
@freeThrowsMade					,
@freeThrowsPercentage			,
@minus							,
@minutes						,
@minutesCalculated				,
@plus							,
@plusMinusPoints				,
@pointsFastBreak				,
@pointsInThePaint				,
@pointsSecondChance				,
@reboundsDefensive				,
@reboundsOffensive				,
@reboundsTotal					,
@steals							,
@threePointersAttempted			,
@threePointersMade				,
@threePointersPercentage		,
@turnovers						,
@twoPointersAttempted			,
@twoPointersMade				,
@twoPointersPercentage			,
@statusReason					,
@statusDescription				)
go

create procedure playerInsert 
@player_id	int,
@name		varchar(100),
@number		int,
@position	varchar(20),
@college	varchar(50),
@country	varchar(50),	
@draftYear	int,			
@draftRound	int,			
@draftPick	int				
as
insert into player values(
@player_id,
@name,
@number,
@position,
@college,
@country,
@draftYear,
@draftRound,
@draftPick
)
go

create procedure playerUpdate @player_id int, @position varchar(30)
as
update player set position = @position where player_id = @player_id
go

create procedure teamBoxInsert
@game_id							int,
@team_id							int,
@atHome							int,
@matchup_id						int,
@points							int,
@pointsAgainst					int   ,
@q1Points						int,
@q1PointsAgainst					int,
@q2Points						int,
@q2PointsAgainst					int,
@q3Points						int,
@q3PointsAgainst					int,
@q4Points						int,
@q4PointsAgainst					int,
@otPoints						int,
@otPointsAgainst					int,
@assists							int   ,
@blocks							int   ,
@blocksReceived					int   ,
@fieldGoalsAttempted				int   ,
@fieldGoalsMade					int   ,
@fieldGoalsPercentage			float,
@foulsOffensive					int   ,
@foulsDrawn						int   ,
@foulsPersonal					int   ,
@foulsTechnical					int   ,
@freeThrowsAttempted				int   ,
@freeThrowsMade					int   ,
@freeThrowsPercentage			float,
@minus							float,
@minutes							varchar(30),
@minutesCalculated				varchar(30),
@plus							float,
@plusMinusPoints					float,
@pointsFastBreak					int   ,
@pointsInThePaint				int   ,
@pointsSecondChance				int   ,
@reboundsDefensive				int   ,
@reboundsOffensive				int   ,
@reboundsTotal					int   ,
@steals							int   ,
@threePointersAttempted			int   ,
@threePointersMade				int   ,
@threePointersPercentage			float,
@turnovers						int   ,
@twoPointersAttempted			int   ,
@twoPointersMade					int   ,
@twoPointersPercentage			float,
@assistsTurnoverRatio			float,
@benchPoints						int   ,
@biggestLead						int   ,
@biggestLeadScore				varchar(30),
@biggestScoringRun				int   ,
@biggestScoringRunScore			varchar(30),
@fastBreakPointsAttempted		int   ,
@fastBreakPointsMade				int   ,
@fastBreakPointsPercentage		float,
@fieldGoalsEffectiveAdjusted		float,
@foulsTeam						int   ,
@foulsTeamTechnical				int   ,
@leadChanges						int   ,
@pointsFromTurnovers				int   ,
@pointsInThePaintAttempted		int   ,
@pointsInThePaintMade			int   ,
@pointsInThePaintPercentage		float,
@reboundsPersonal				int   ,
@reboundsTeam					int   ,
@reboundsTeamDefensive			int   ,
@reboundsTeamOffensive			int   ,
@secondChancePointsAttempted		int   ,
@secondChancePointsMade			int   ,
@secondChancePointsPercentage	float,
@timeLeading						varchar(30),
@timesTied						int   ,
@trueShootingAttempts			float,
@trueShootingPercentage			float,
@turnoversTeam					int   ,
@turnoversTotal					int   
as
insert into teamBox values(
@game_id						,
@team_id						,
@atHome							,
@matchup_id						,
@points							,
@pointsAgainst					,
@q1Points						,
@q1PointsAgainst				,
@q2Points						,
@q2PointsAgainst				,
@q3Points						,
@q3PointsAgainst				,
@q4Points						,
@q4PointsAgainst				,
@otPoints						,
@otPointsAgainst				,
@assists						,
@blocks							,
@blocksReceived					,
@fieldGoalsAttempted			,
@fieldGoalsMade					,
@fieldGoalsPercentage			,
@foulsOffensive					,
@foulsDrawn						,
@foulsPersonal					,
@foulsTechnical					,
@freeThrowsAttempted			,
@freeThrowsMade					,
@freeThrowsPercentage			,
@minus							,
@minutes						,
@minutesCalculated				,
@plus							,
@plusMinusPoints				,
@pointsFastBreak				,
@pointsInThePaint				,
@pointsSecondChance				,
@reboundsDefensive				,
@reboundsOffensive				,
@reboundsTotal					,
@steals							,
@threePointersAttempted			,
@threePointersMade				,
@threePointersPercentage		,
@turnovers						,
@twoPointersAttempted			,
@twoPointersMade				,
@twoPointersPercentage			,
@assistsTurnoverRatio			,
@benchPoints					,
@biggestLead					,
@biggestLeadScore				,
@biggestScoringRun				,
@biggestScoringRunScore			,
@fastBreakPointsAttempted		,
@fastBreakPointsMade			,
@fastBreakPointsPercentage		,
@fieldGoalsEffectiveAdjusted	,
@foulsTeam						,
@foulsTeamTechnical				,
@leadChanges					,
@pointsFromTurnovers			,
@pointsInThePaintAttempted		,
@pointsInThePaintMade			,
@pointsInThePaintPercentage		,
@reboundsPersonal				,
@reboundsTeam					,
@reboundsTeamDefensive			,
@reboundsTeamOffensive			,
@secondChancePointsAttempted	,
@secondChancePointsMade			,
@secondChancePointsPercentage	,
@timeLeading					,
@timesTied						,
@trueShootingAttempts			,
@trueShootingPercentage			,
@turnoversTeam					,
@turnoversTotal					)
go

create procedure lastBox
as 
select top 1 * 
from teamBox t inner join 
		game g on t.game_id = g.game_id
order by date desc, t.game_id desc
go

create procedure gameUpdate @game_id int, @team_idW int, @wScore int, @team_idL int, @lScore int
as
update game set team_idW = @team_idW, wScore = @wScore, team_idL = @team_idL, lScore = @lScore
where game_id = @game_id
go

create procedure jsonGames
as
select distinct 
case when g.game_id is null then p.game_id else g.game_id end game_id
from game g  left join
		PlayByPlay p on g.game_id = p.game_id
where timeActual > getdate() - 3 or timeActual is null
order by game_id asc
go

create procedure teamCheck @team_id	int
as
select * from team where team_id = @team_id
go












