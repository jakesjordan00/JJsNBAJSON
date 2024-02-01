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