create table arena(
arena_id	int,
team_id		int,
name		varchar(100),
city		varchar(50),
state		varchar(30),
country		varchar(20)
)



create table official(
official_id int,
name		varchar(50),
number		int,
assignment	varchar(50)
)


create table team(
team_id		int, 
tricode		varchar(3),
city		varchar(30),
name		varchar(30),
yearFounded int				--Not in API
)


create table player(
player_id	int,
name		varchar(100),
number		int,
position	varchar(20),
college		varchar(50),
country		varchar(50),	--Not in API
draftYear	int,			--Not in API
draftRound	int,			--Not in API
draftPick	int				--Not in API
)


create table game(
game_id		int,
date		date,
team_idH	int,
team_idA	int,
team_idW	int,
wScore		int,
team_idL	int,
lScore		int,
arena_id	int,
sellout		bit
)

drop table game