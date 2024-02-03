create procedure pictureCheck @highSeed int, @highSeedRank int, @lowSeed int, @lowSeedRank int
as
select * from PlayoffPicture 
where highSeed_id = @highSeed and highSeedRank = @highSeedRank and lowSeed_id = @lowSeed and lowSeedRank = @lowSeedRank
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



create procedure bracketCheck @series_id int, @highSeed_id int, @highSeedSeriesWins	int, @lowSeed_id int, @lowSeedSeriesWins	int
as
select * from PlayoffBracket 
where series_id = @series_id and (highSeed_id != @highSeed_id or highSeedSeriesWins != @highSeedSeriesWins or lowSeed_id != @lowSeed_id or lowSeedSeriesWins != @lowSeed_id)
go

create procedure bracketInsert 
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
insert into PlayoffBracket values(
@series_id			,
@conference			,
@roundNumber		,
@description		,
@status				,
@seriesWinner		,
@highSeed_id		,
@highSeedSeriesWins	,
@lowSeed_id			,
@lowSeedSeriesWins	,
@nextGame_id		,
@nextGameNumber		,
@nextSeries_id		
)
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
where (conference = @conference and matchupType = @matchupType and highSeedRank = @highSeedRank) and (
highSeed_id				!= @highSeed_id				or
highSeedRank			!= @highSeedRank			or
highSeedRegSeasonWins	!= @highSeedRegSeasonWins	or
highSeedRegSeasonLosses	!= @highSeedRegSeasonLosses or
lowSeed_id				!= @lowSeed_id				or
lowSeedRank				!= @lowSeedRank				or
lowSeedRegSeasonWins	!= @lowSeedRegSeasonWins	or
lowSeedRegSeasonLosses	!= @lowSeedRegSeasonLosses)
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
where (conference = @conference and matchupType = @matchupType and highSeedRank = @highSeedRank) and (
highSeed_id				!= @highSeed_id				or
highSeedRank			!= @highSeedRank			or
highSeedRegSeasonWins	!= @highSeedRegSeasonWins	or
highSeedRegSeasonLosses	!= @highSeedRegSeasonLosses or
lowSeed_id				!= @lowSeed_id				or
lowSeedRank				!= @lowSeedRank				or
lowSeedRegSeasonWins	!= @lowSeedRegSeasonWins	or
lowSeedRegSeasonLosses	!= @lowSeedRegSeasonLosses)
go

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
nextSeries_id			!= @nextSeries_id) and description is not null
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

create procedure blackBracketCheck @series_id int
as
select * from PlayoffBracket where series_id = @series_id
go

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