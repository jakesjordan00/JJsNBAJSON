create procedure blankBracketCheck @series_id int
as
select * from PlayoffBracket where series_id = @series_id
go