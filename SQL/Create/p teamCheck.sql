create procedure teamCheck @team_id	int
as
select * from team where team_id = @team_id
go
