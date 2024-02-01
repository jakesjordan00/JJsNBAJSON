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