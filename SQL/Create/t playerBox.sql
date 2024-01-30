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
