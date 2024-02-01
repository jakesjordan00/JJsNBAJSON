--Game
create table game(
game_id		int,
date		date,
team_idH	int,
team_idA	int,
team_idW	int,
wScore		int,
team_idL	int,
lScore		int,
arenta_id	int,
sellout		bit
)

--Official
create table official(
official_id int,
name		varchar(50),
number		int,
assignment	varchar(50)
)

--Arena
create table arena(
arena_id	int,
team_id		int,
name		varchar(100),
city		varchar(2),
state		varchar(2),
country		varchar(3)
)
--------------------------------------------------------------

--Players
create table player(
player_id	int,
name		varchar(100),
number		int,
position	varchar(20),
college		varchar(50),
country		varchar(50),	--Not in API
draftYear	int,			--Not in API
draftRound	int,			--Not in API
draftPick	int				--Not in API
)

create table playerBox(
game_id							int,
team_id							int,
player_id						int,
status							varchar(30),
starter							int,
position						varchar(2),
points							int,
assists							int   ,
blocks							int   ,
blocksReceived					int   ,
fieldGoalsAttempted				int   ,
fieldGoalsMade					int   ,
fieldGoalsPercentage			float,
foulsOffensive					int   ,
foulsDrawn						int   ,
foulsPersonal					int   ,
foulsTechnical					int   ,
freeThrowsAttempted				int   ,
freeThrowsMade					int   ,
freeThrowsPercentage			float,
minus							float,
minutes							varchar(30),
minutesCalculated				varchar(30),
plus							float,
plusMinusPoints					float,
pointsFastBreak					int   ,
pointsInThePaint				int   ,
pointsSecondChance				int   ,
reboundsDefensive				int   ,
reboundsOffensive				int   ,
reboundsTotal					int   ,
steals							int   ,
threePointersAttempted			int   ,
threePointersMade				int   ,
threePointersPercentage			float,
turnovers						int   ,
twoPointersAttempted			int   ,
twoPointersMade					int   ,
twoPointersPercentage			float,
statusReason					varchar(100),
statusDescription				varchar(200)
)
go
--------------------------------------------------------------

--Teams
create table team(
team_id		int, 
tricode		varchar(3),
city		varchar(30),
name		varchar(30),
yearFounded int				--Not in API
)

create table teamBox(
game_id							int,
team_id							int,
atHome							int,
matchup_id						int,
points							int,
pointsAgainst					int   ,
q1Points						int,
q1PointsAgainst					int,
q2Points						int,
q2PointsAgainst					int,
q3Points						int,
q3PointsAgainst					int,
q4Points						int,
q4PointsAgainst					int,
otPoints						int,
otPointsAgainst					int,
assists							int   ,
blocks							int   ,
blocksReceived					int   ,
fieldGoalsAttempted				int   ,
fieldGoalsMade					int   ,
fieldGoalsPercentage			float,
foulsOffensive					int   ,
foulsDrawn						int   ,
foulsPersonal					int   ,
foulsTechnical					int   ,
freeThrowsAttempted				int   ,
freeThrowsMade					int   ,
freeThrowsPercentage			float,
minus							float,
minutes							varchar(30),
minutesCalculated				varchar(30),
plus							float,
plusMinusPoints					float,
pointsFastBreak					int   ,
pointsInThePaint				int   ,
pointsSecondChance				int   ,
reboundsDefensive				int   ,
reboundsOffensive				int   ,
reboundsTotal					int   ,
steals							int   ,
threePointersAttempted			int   ,
threePointersMade				int   ,
threePointersPercentage			float,
turnovers						int   ,
twoPointersAttempted			int   ,
twoPointersMade					int   ,
twoPointersPercentage			float,
assistsTurnoverRatio			float,
benchPoints						int   ,
biggestLead						int   ,
biggestLeadScore				varchar(30),
biggestScoringRun				int   ,
biggestScoringRunScore			varchar(30),
fastBreakPointsAttempted		int   ,
fastBreakPointsMade				int   ,
fastBreakPointsPercentage		float,
fieldGoalsEffectiveAdjusted		float,
foulsTeam						int   ,
foulsTeamTechnical				int   ,
leadChanges						int   ,
pointsFromTurnovers				int   ,
pointsInThePaintAttempted		int   ,
pointsInThePaintMade			int   ,
pointsInThePaintPercentage		float,
reboundsPersonal				int   ,
reboundsTeam					int   ,
reboundsTeamDefensive			int   ,
reboundsTeamOffensive			int   ,
secondChancePointsAttempted		int   ,
secondChancePointsMade			int   ,
secondChancePointsPercentage	float,
timeLeading						varchar(30),
timesTied						int   ,
trueShootingAttempts			float,
trueShootingPercentage			float,
turnoversTeam					int   ,
turnoversTotal					int   )
go

--------------------------------------------------------------
--Playoffs
create table PlayoffPicture(
conference				varchar(4),		--seriesConference
matchupType				varchar(30),
highSeed_id				int,			--highSeedId
highSeedRank			int,
highSeedRegSeasonWins	int,
highSeedRegSeasonLosses int,
lowSeed_id				int,			--lowSeedId
lowSeedRank				int,
lowSeedRegSeasonWins	int,
lowSeedRegSeasonLosses 	int
)

create table PlayoffBracket(
series_id				int,
conference				varchar(4),		--seriesConference
roundNumber				int,
description				varchar(100),	--seriesText
status					int,			--seriesStatus
seriesWinner			int,
highSeedId				int,
highSeedSeriesWins		int,
lowSeedId				int,
lowSeedSeriesWins	 	int,
nextGame_id				int,			--nextGameId
nextGameNumber			int,
nextSeries_id			int
)

--------------------------------------------------------------
--PlayByPlay
create table playByPlay(
			game_id 				int,				--gameId
			actionNumber			int,
			clock 					varchar(20),
			timeActual				datetime,
			period 					int,
			periodType				varchar(20),
			team_id 				int,				--teamId
			teamTricode				varchar(3),
			event_msg_type_id		int,		--Need case statement
			shotType 				varchar(4),
			ptsGenerated			int,
			actionType 				varchar(30),		
			actionSub 				varchar(20),		--subType
			description				varchar(100),   --description
			descriptor 				varchar(30),		--qualifiers[]
			qualifier1 				varchar(30),		--[0]
			qualifier2 				varchar(30),		--[1]
			qualifier3				varchar(30),		--[2]
			player_id				int,				--personId
			x 						float,
			y						float,
			area					varchar(50),
			areaDetail				varchar(50),
			side					varchar(30),
			shotDistance			float,
			scoreHome				int,
			scoreAway				int,
			isFieldGoal				bit,
			shotResult				varchar(20),
			shotActionNumber		int,			--Used for rebounds; What should reb came from
			player_idAST			int,			--assistPersonId
			player_idBLK			int,			--blockPersonId
			player_idSTL			int,			--stealPersonId
			player_idFoulDrawn		int,		--foulDrawnPersonId
			player_idJumpW			int,			--jumpBallWonPersonId
			player_idJumpL			int,			--jumpBallLostPersonId
			official_id				int				--officialId
)