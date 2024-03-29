/****** Script for SelectTopNRows command from SSMS  ******/
--SELECT [game_id]
--      ,[actionNumber]
--      ,[clock]
--      ,[timeActual]
--      ,[period]
--      ,[periodType]
--      ,[team_id]
--      ,[teamTricode]
--      ,[event_msg_type_id]
--      ,[shotType]
--      ,[actionType]
--      ,[actionSub]
--      ,[description]
--      ,[descriptor]
--      ,[qualifier1]
--      ,[qualifier2]
--      ,[qualifier3]
--      ,[player_id]
--      ,[x]
--      ,[y]
--      ,[area]
--      ,[areaDetail]
--      ,[side]
--      ,[shotDistance]
--      ,[scoreHome]
--      ,[scoreAway]
--      ,[isFieldGoal]
--      ,[shotResult]
--      ,[shotActionNumber]
--      ,[player_idAST]
--      ,[player_idBLK]
--      ,[player_idSTL]
--      ,[player_idFoulDrawn]
--      ,[player_idJumpW]
--      ,[player_idJumpL]
--      ,[official_id]
--  FROM [nba24].[dbo].[PlayByPlay]

select   g.date, p.game_id, 
p.player_id, pl.player_name,
p.team_id, t.nickname, 
(select distinct team_id from PlayByPlay where game_id = p.game_id and team_id != p.team_id) matchup_id,
(select distinct nickname from team where team_id = (select distinct team_id from PlayByPlay where game_id = p.game_id and team_id != p.team_id)) Matchup,
sum(case when p.shotType in('2PTM', '3PTM', 'FTM') then p.ptsGenerated else 0 end) Points,
sum(case when p.shotType in('2PTM', '3PTM') then 1 else 0 end) FGM,
sum(case when p.shotType in('2PTM', '2PTA', '3PTM', '3PTA') then 1 else 0 end) FGA,
sum(case when p.shotType in('3PTM') then 1 else 0 end) [3PM],
sum(case when p.shotType in('3PTM', '3PTA') then 1 else 0 end) [3PA],
sum(case when p.shotType in('FTM') then 1 else 0 end) FTM,
sum(case when p.shotType in('FTM', 'FTA') then 1 else 0 end) FTA

from PlayByPlay p inner join
		game g on p.game_id = g.game_id inner join
		team t on p.team_id = t.team_id inner join
		player pl on p.player_id = pl.player_id left join
		player pla on p.player_idAST = pla.player_id 
group by p.player_id, p.game_id, g.date, p.team_id, pl.player_name, t.nickname
order by g.date desc, p.game_id desc, Points desc