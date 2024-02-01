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