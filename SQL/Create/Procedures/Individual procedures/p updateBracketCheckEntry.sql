
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

