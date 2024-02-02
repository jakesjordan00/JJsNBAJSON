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
