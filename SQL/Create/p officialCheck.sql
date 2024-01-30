create procedure officialCheck @official_id	int
as
select * from official where official_id = @official_id
go