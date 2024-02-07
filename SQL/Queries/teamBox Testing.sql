select date a, * 
from teamBox t inner join 
		game g on t.game_id = g.game_id
order by date desc, t.game_id desc
go
select date a, * 
from playerBox p inner join 
		game g on p.game_id = g.game_id
order by date desc, p.game_id desc
go

update teamBox set points = 100 where game_id in(22300703, 22300702, 22300700)


delete from teamBox where game_id > 22300700
delete from playerBox where game_id > 22300700