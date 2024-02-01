create procedure arenaCheck @arena_id	int
as
select * from arena where arena_id = @arena_id
go
