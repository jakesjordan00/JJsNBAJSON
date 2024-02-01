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
