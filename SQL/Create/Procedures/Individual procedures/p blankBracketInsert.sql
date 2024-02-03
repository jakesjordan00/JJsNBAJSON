create procedure blankBracketInsert 
@series_id				int,
@conference				varchar(4),	
@roundNumber			int,
@status					int		
as
insert into PlayoffBracket (series_id, conference, roundNumber, status)
values(
@series_id			,
@conference			,
@roundNumber		,
@status)
go