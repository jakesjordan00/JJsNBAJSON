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

