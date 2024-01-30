create procedure playerInsert 
@player_id	int,
@name		varchar(100),
@number		int,
@position	varchar(20),
@college	varchar(50),
@country	varchar(50),	
@draftYear	int,			
@draftRound	int,			
@draftPick	int				
as
insert into player values(
@player_id,
@name,
@number,
@position,
@college,
@country,
@draftYear,
@draftRound,
@draftPick
)
go
