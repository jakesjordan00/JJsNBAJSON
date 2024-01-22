--Arena
create procedure arenaInsert 
@arena_id	int,
@team_id	int,
@name		varchar(100),
@city		varchar(50),
@state		varchar(30),
@country	varchar(20)
as
insert into arena values(
@arena_id,
@team_id,
@name,	
@city,	
@state,
@country)
go

create procedure arenaCheck @arena_id	int
as
select * from arena where arena_id = @arena_id
go

--Team
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

create procedure teamCheck @team_id	int
as
select * from team where team_id = @team_id
go


--Player
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

create procedure playerCheck @player_id	int
as
select * from player where player_id = @player_id
go

create procedure playerUpdate @player_id int, @position varchar(30)
as
update player set position = @position where player_id = @player_id
go

--Official
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

create procedure officialCheck @official_id	int
as
select * from official where official_id = @official_id
go


--Game
create procedure gameInsert 
@game_id	int,
@date		date,
@team_idH	int,
@team_idA	int,
@team_idW	int,
@wScore		int,
@team_idL	int,
@lScore		int,
@arena_id	int,
@sellout	bit
as
insert into game values(
@game_id,
@date,
@team_idH,
@team_idA,
@team_idW,
@wScore,
@team_idL,	
@lScore,	
@arena_id,	
@sellout	
)
go

create procedure gameCheck @game_id	int
as
select * from game where game_id = @game_id
go

create procedure gameUpdate @game_id int, @team_idW int, @wScore int, @team_idL int, @lScore int
as
update game set team_idW = @team_idW, wScore = @wScore, team_idL = @team_idL, lScore = @lScore
where game_id = @game_id
go

create procedure gameLoadCheck @game_id	int
as
select * from game 
where game_id = @game_id 
and date >= GETDATE() - 3
go