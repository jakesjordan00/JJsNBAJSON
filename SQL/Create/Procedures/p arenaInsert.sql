create procedure arenaInsert 
@arena_id	int,
@team_id	int,
@name		varchar(100),
@city		varchar(2),
@state		varchar(2),
@country	varchar(3)
as
insert into arena values(
@arena_id,
@team_id,
@name,	
@city,	
@state,
@country)
go


