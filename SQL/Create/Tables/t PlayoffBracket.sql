create table PlayoffBracket(
series_id				int,
conference				varchar(4),		--seriesConference
roundNumber				int,
description				varchar(100),	--seriesText
status					int,			--seriesStatus
seriesWinner			int,
highSeed_id				int,
highSeedSeriesWins		int,
lowSeed_id				int,
lowSeedSeriesWins	 	int,
nextGame_id				int,			--nextGameId
nextGameNumber			int,
nextSeries_id			int
)

