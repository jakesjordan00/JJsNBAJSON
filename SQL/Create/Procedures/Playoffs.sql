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



create procedure bracketCheck @series_id int, @highSeed_id int, @highSeedSeriesWins	int, @lowSeedId int, @lowSeedSeriesWins	int
as
select * from PlayoffBracket 
where series_id = @series_id and (highSeed_id != @highSeed_id or highSeedSeriesWins != @highSeedSeriesWins or lowSeed_id != @lowSeedId or lowSeedSeriesWins != @lowSeedId)
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
@highSeed_id		,
@highSeedSeriesWins	,
@lowSeedId			,
@lowSeedSeriesWins	,
@nextGame_id		,
@nextGameNumber		,
@nextSeries_id		
)