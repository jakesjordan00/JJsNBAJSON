--Arena procedures
--  Called in earlyBird.cs. 
--	Only used when clicking the First Load button.
--	Checks to see if the arena from the game being read exists
create procedure arenaCheck @arena_id	int
as
select * from arena where arena_id = @arena_id
go

--If the arena does not exist, this procedure will run and insert into arena table.
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
------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
--Official procedures
--	Same deal as the arenas. No further explanation needed, literally same exact thing but different table.
create procedure officialCheck @official_id	int
as
select * from official where official_id = @official_id
go

create procedure officialInsert 
@official_id int,
@name		 varchar(50),
@number		 int,
@assignment	 varchar(50)
as
insert into official values(
@official_id,
@name,
@number,
@assignment
)
go
------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
--Tean procedures
--	Same deal as the arenas. No further explanation needed, literally same exact thing but different table.
create procedure teamCheck @team_id	int
as
select * from team where team_id = @team_id
go

create procedure teamInsert
@team_id	 int, 
@tricode	 varchar(3),
@city		 varchar(30),
@name		 varchar(30),
@yearFounded int			
as
insert into team values(
@team_id,	
@tricode,	
@city,		
@name,		
@yearFounded
)
go
------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
--Player procedures
--	Same deal as the arenas. Add one procedure to update position
--	Position only appears if they start the game in that poistion.
create procedure playerCheck @player_id	int
as
select * from player where player_id = @player_id
go

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

create procedure playerUpdate @player_id int, @position varchar(30)
as
update player set position = @position where player_id = @player_id
go
