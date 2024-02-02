
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
where (conference = @conference and matchupType = @matchupType) and (
highSeed_id				!= @highSeed_id				or
highSeedRank			!= @highSeedRank			or
highSeedRegSeasonWins	!= @highSeedRegSeasonWins	or
highSeedRegSeasonLosses	!= @highSeedRegSeasonLosses or
lowSeed_id				!= @lowSeed_id				or
lowSeedRank				!= @lowSeedRank				or
lowSeedRegSeasonWins	!= @lowSeedRegSeasonWins	or
lowSeedRegSeasonLosses	!= @lowSeedRegSeasonLosses)
go


