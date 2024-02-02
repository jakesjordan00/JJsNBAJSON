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
