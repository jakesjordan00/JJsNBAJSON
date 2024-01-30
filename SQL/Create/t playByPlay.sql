create table playByPlay(
			game_id 				int,				--gameId
			actionNumber			int,
			clock 					varchar(20),
			timeActual				datetime,
			period 					int,
			periodType				varchar(20),
			team_id 				int,				--teamId
			teamTricode				varchar(3),
			event_msg_type_id		int,		--Need case statement
			shotType 				varchar(4),
			ptsGenerated			int,
			actionType 				varchar(30),		
			actionSub 				varchar(20),		--subType
			description				varchar(100),   --description
			descriptor 				varchar(30),		--qualifiers[]
			qualifier1 				varchar(30),		--[0]
			qualifier2 				varchar(30),		--[1]
			qualifier3				varchar(30),		--[2]
			player_id				int,				--personId
			x 						float,
			y						float,
			area					varchar(50),
			areaDetail				varchar(50),
			side					varchar(30),
			shotDistance			float,
			scoreHome				int,
			scoreAway				int,
			isFieldGoal				bit,
			shotResult				varchar(20),
			shotActionNumber		int,			--Used for rebounds; What should reb came from
			player_idAST			int,			--assistPersonId
			player_idBLK			int,			--blockPersonId
			player_idSTL			int,			--stealPersonId
			player_idFoulDrawn		int,		--foulDrawnPersonId
			player_idJumpW			int,			--jumpBallWonPersonId
			player_idJumpL			int,			--jumpBallLostPersonId
			official_id				int				--officialId
)