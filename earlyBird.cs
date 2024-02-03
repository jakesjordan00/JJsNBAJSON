using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Microsoft.SqlServer.Server;
using nbaJSON;

namespace earlyBird
{
    public partial class earlyBird
    {
        public static void LoadCheck(Label statusL)
        {
            SqlConnection DupeCheckConnect = new SqlConnection(Bus_Driver.ConnectionString);
            {
                using (DupeCheckConnect)
                {
                    using (SqlCommand DupeSearch = new SqlCommand("loadCheck"))
                    {
                        DupeSearch.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter sDupeSearch = new SqlDataAdapter())
                        {
                            DupeSearch.Connection = DupeCheckConnect;
                            sDupeSearch.SelectCommand = DupeSearch;
                            DupeCheckConnect.Open();
                            SqlDataReader reader = DupeSearch.ExecuteReader();
                            if (!reader.HasRows)
                            {
                                FirstLoad();
                                statusL.ForeColor = System.Drawing.Color.LightSeaGreen;
                                statusL.Text = "Database has been successfully loaded!";
                            }
                            else
                            {
                                statusL.ForeColor = System.Drawing.Color.Red;
                                statusL.Text = "Error 1. Database has already loaded for first time";
                            }
                        }
                    }
                }
            }
        }

        public static void FirstLoad()
        {
            for (int i = 22300001; i <= 22301250; i++)
            {
                string game = "";
                var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
                string jsonLink = "";
                if (i < 22300010)
                {
                    game = i.ToString();
                    jsonLink = "https://cdn.nba.com/static/json/liveData/boxscore/boxscore_00" + game + ".json";
                }
                if (i >= 22300010 && i < 22300100)
                {
                    game = i.ToString();
                    jsonLink = "https://cdn.nba.com/static/json/liveData/boxscore/boxscore_00" + game + ".json";
                }
                if (i >= 22300100 && i < 22301000)
                {
                    game = i.ToString();
                    jsonLink = "https://cdn.nba.com/static/json/liveData/boxscore/boxscore_00" + game + ".json";
                }
                if (i >= 22301000 && i < 22301251)
                {
                    game = i.ToString();
                    jsonLink = "https://cdn.nba.com/static/json/liveData/boxscore/boxscore_00" + game + ".json";
                }
                try
                {
                    WebRequest testRequest = WebRequest.Create(jsonLink);
                    WebResponse testShouldWork = testRequest.GetResponse();
                    string json = client.DownloadString(jsonLink);
                    Root JSON = JsonConvert.DeserializeObject<Root>(json);
                    Official REF = JsonConvert.DeserializeObject<Official>(json);
                    Player PLAYER = JsonConvert.DeserializeObject<Player>(json);

                    int game_id = Int32.Parse(game);
                    GameCheck(JSON, game_id, JSON.game.homeTeam.score, JSON.game.awayTeam.score);
                    int arena_id = JSON.game.arena.arenaId;     //Set arena_id = arenaId from JSON
                    ArenaCheck(JSON, arena_id);                     //Then we send to ArenaCheck. If it's in db, we come back here. If it's not, go to ArenaPost
                    int team_id = JSON.game.homeTeam.teamId;    //Set team_id equal to the teamId of the Home team from JSON
                    TeamCheck(JSON, team_id);                       //Send to TeamCheck, then TeamPost if not duplicate
                    int away_id = JSON.game.awayTeam.teamId;    //Set team_id equal to the teamId of the Away team from JSON                  
                    TeamCheck(JSON, away_id);                       //Send to TeamCheck, then TeamPost if not duplicate
                    //Home Players
                    for (int j = 0; j < JSON.game.homeTeam.players.Count(); j++)
                    {
                        int player_id = JSON.game.homeTeam.players[j].personId;            //Set player_id equal to the player_id of the j home team's player 
                        HomePlayerCheck(JSON, player_id, j);                        //send off to PlayerCheck
                    }
                    //Away Players
                    for (int j = 0; j < JSON.game.awayTeam.players.Count(); j++)
                    {
                        int player_id = JSON.game.awayTeam.players[j].personId;            //Set player_id equal to the player_id of the j away team's player 
                        AwayPlayerCheck(JSON, player_id, j);                        //send off to PlayerCheck again
                    }
                    //Officials
                    for (int j = 0; j < JSON.game.officials.Count(); j++)
                    {
                        int official_id = JSON.game.officials[j].personId;          //Set official_id equal to the official_id of the j official 
                        OfficialCheck(JSON, official_id, j);                  //send off to OfficialCheck
                    }




                    //InsertGames(json, Int32.Parse(game));
                }
                catch (WebException e)
                {

                }
            }
        }

        public static void ArenaCheck(Root JSON, int arena_id)
        {
            SqlConnection DupeCheckConnect = new SqlConnection(Bus_Driver.ConnectionString);
            {
                using (DupeCheckConnect)
                {
                    using (SqlCommand DupeSearch = new SqlCommand("arenaCheck"))
                    {
                        DupeSearch.CommandType = CommandType.StoredProcedure;
                        DupeSearch.Parameters.AddWithValue("@arena_id", arena_id);
                        using (SqlDataAdapter sDupeSearch = new SqlDataAdapter())
                        {
                            DupeSearch.Connection = DupeCheckConnect;
                            sDupeSearch.SelectCommand = DupeSearch;
                            DupeCheckConnect.Open();
                            SqlDataReader reader = DupeSearch.ExecuteReader();
                            if (!reader.HasRows)
                            {
                                ArenaPost(JSON, arena_id);
                            }
                        }
                    }
                }
            }
        }

        public static void TeamCheck(Root JSON, int team_id)
        {
            SqlConnection DupeCheckConnect = new SqlConnection(Bus_Driver.ConnectionString);
            {
                using (DupeCheckConnect)
                {
                    using (SqlCommand DupeSearch = new SqlCommand("teamCheck"))
                    {
                        DupeSearch.CommandType = CommandType.StoredProcedure;
                        DupeSearch.Parameters.AddWithValue("@team_id", team_id);
                        using (SqlDataAdapter sDupeSearch = new SqlDataAdapter())
                        {
                            DupeSearch.Connection = DupeCheckConnect;
                            sDupeSearch.SelectCommand = DupeSearch;
                            DupeCheckConnect.Open();
                            SqlDataReader reader = DupeSearch.ExecuteReader();
                            if (!reader.HasRows)
                            {
                                TeamPost(JSON, team_id);
                            }
                        }
                    }
                }
            }
        }
        public static void HomePlayerCheck(Root JSON, int player_id, int j)
        {
            SqlConnection DupeCheckConnect = new SqlConnection(Bus_Driver.ConnectionString);
            {
                using (DupeCheckConnect)
                {
                    using (SqlCommand DupeSearch = new SqlCommand("playerCheck"))
                    {
                        DupeSearch.CommandType = CommandType.StoredProcedure;
                        DupeSearch.Parameters.AddWithValue("@player_id", player_id);
                        using (SqlDataAdapter sDupeSearch = new SqlDataAdapter())
                        {
                            DupeSearch.Connection = DupeCheckConnect;
                            sDupeSearch.SelectCommand = DupeSearch;
                            DupeCheckConnect.Open();
                            SqlDataReader reader = DupeSearch.ExecuteReader();
                            reader.Read();
                            if (!reader.HasRows)
                            {
                                HomePlayerPost(JSON, player_id, j);
                            }
                            if(reader.HasRows && JSON.game.homeTeam.players[j].position is not null && reader.GetString(3) != JSON.game.homeTeam.players[j].position)
                            {
                                PlayerUpdate(player_id, JSON.game.homeTeam.players[j].position, reader.GetString(3));
                            }
                        }
                    }
                }
            }
        }

        public static void AwayPlayerCheck(Root JSON, int player_id, int j)
        {
            SqlConnection DupeCheckConnect = new SqlConnection(Bus_Driver.ConnectionString);
            {
                using (DupeCheckConnect)
                {
                    using (SqlCommand DupeSearch = new SqlCommand("playerCheck"))
                    {
                        DupeSearch.CommandType = CommandType.StoredProcedure;
                        DupeSearch.Parameters.AddWithValue("@player_id", player_id);
                        using (SqlDataAdapter sDupeSearch = new SqlDataAdapter())
                        {
                            DupeSearch.Connection = DupeCheckConnect;
                            sDupeSearch.SelectCommand = DupeSearch;
                            DupeCheckConnect.Open();
                            SqlDataReader reader = DupeSearch.ExecuteReader();
                            reader.Read();
                            if (!reader.HasRows)
                            {
                                AwayPlayerPost(JSON, player_id, j);
                            }
                            if (reader.HasRows && JSON.game.awayTeam.players[j].position is not null && reader.GetString(3) != JSON.game.awayTeam.players[j].position)
                            {
                                PlayerUpdate(player_id, JSON.game.awayTeam.players[j].position, reader.GetString(3));
                            }
                        }
                    }
                }
            }
        }

        public static void OfficialCheck(Root JSON, int official_id, int j)
        {
            SqlConnection DupeCheckConnect = new SqlConnection(Bus_Driver.ConnectionString);
            {
                using (DupeCheckConnect)
                {
                    using (SqlCommand DupeSearch = new SqlCommand("officialCheck"))
                    {
                        DupeSearch.CommandType = CommandType.StoredProcedure;
                        DupeSearch.Parameters.AddWithValue("@official_id", official_id);
                        using (SqlDataAdapter sDupeSearch = new SqlDataAdapter())
                        {
                            DupeSearch.Connection = DupeCheckConnect;
                            sDupeSearch.SelectCommand = DupeSearch;
                            DupeCheckConnect.Open();
                            SqlDataReader reader = DupeSearch.ExecuteReader();
                            if (!reader.HasRows)
                            {
                                OfficialPost(JSON, official_id, j);
                            }
                        }
                    }
                }
            }
        }
        public static void GameCheck(Root JSON, int game_id, int hScore, int aScore)
        {
            SqlConnection DupeCheckConnect = new SqlConnection(Bus_Driver.ConnectionString);
            {
                using (DupeCheckConnect)
                {
                    using (SqlCommand DupeSearch = new SqlCommand("gameCheck"))
                    {
                        DupeSearch.CommandType = CommandType.StoredProcedure;
                        DupeSearch.Parameters.AddWithValue("@game_id", game_id);
                        using (SqlDataAdapter sDupeSearch = new SqlDataAdapter())
                        {
                            DupeSearch.Connection = DupeCheckConnect;
                            sDupeSearch.SelectCommand = DupeSearch;
                            DupeCheckConnect.Open();
                            SqlDataReader reader = DupeSearch.ExecuteReader();
                            reader.Read();
                            if (!reader.HasRows)
                            {
                                GamePost(JSON, game_id, hScore, aScore);
                            }
                            if(reader.HasRows)
                            {
                                if (reader.GetInt32(7) != hScore && reader.GetInt32(7) != aScore)
                                {
                                    GameUpdate(JSON, game_id, hScore, aScore);
                                }
                            }
                        }
                    }
                }
            }
        }


        public static void ArenaPost(Root JSON, int arena_id)
        {
            SqlConnection Insert = new SqlConnection(Bus_Driver.ConnectionString);
            using (Insert)
            {
                using (SqlCommand InsertData = new SqlCommand("arenaInsert"))
                {
                    InsertData.Connection = Insert;
                    InsertData.CommandType = CommandType.StoredProcedure;
                    InsertData.Parameters.AddWithValue("@arena_id", arena_id);
                    InsertData.Parameters.AddWithValue("@team_id", JSON.game.homeTeam.teamId);
                    InsertData.Parameters.AddWithValue("@name", JSON.game.arena.arenaName);
                    InsertData.Parameters.AddWithValue("@city", JSON.game.arena.arenaCity);
                    InsertData.Parameters.AddWithValue("@state", JSON.game.arena.arenaState);
                    InsertData.Parameters.AddWithValue("@country", JSON.game.arena.arenaCountry);
                    Insert.Open();
                    InsertData.ExecuteScalar();
                    Insert.Close();
                }
            }
        }


        
        public static void TeamPost(Root JSON, int team_id)
        {
            SqlConnection Insert = new SqlConnection(Bus_Driver.ConnectionString);
            using (Insert)
            {
                using (SqlCommand InsertData = new SqlCommand("teamInsert"))
                {
                    InsertData.Connection = Insert;
                    InsertData.CommandType = CommandType.StoredProcedure;
                    InsertData.Parameters.AddWithValue("@team_id", team_id);
                    if(JSON.game.homeTeam.teamId == team_id)
                    {
                        InsertData.Parameters.AddWithValue("@tricode", JSON.game.homeTeam.teamTricode);
                        InsertData.Parameters.AddWithValue("@city", JSON.game.homeTeam.teamCity);
                        InsertData.Parameters.AddWithValue("@name", JSON.game.homeTeam.teamName);
                    }
                    else
                    {
                        InsertData.Parameters.AddWithValue("@tricode", JSON.game.awayTeam.teamTricode);
                        InsertData.Parameters.AddWithValue("@city", JSON.game.awayTeam.teamCity);
                        InsertData.Parameters.AddWithValue("@name", JSON.game.awayTeam.teamName);
                    }
                    InsertData.Parameters.AddWithValue("@yearFounded", 0);
                    Insert.Open();
                    InsertData.ExecuteScalar();
                    Insert.Close();
                }
            }
        }

        
        public static void HomePlayerPost(Root JSON, int player_id, int j)
        {
            SqlConnection Insert = new SqlConnection(Bus_Driver.ConnectionString);
            using (Insert)
            {
                using (SqlCommand InsertData = new SqlCommand("playerInsert"))
                {
                    InsertData.Connection = Insert;
                    InsertData.CommandType = CommandType.StoredProcedure;
                    InsertData.Parameters.AddWithValue("@player_id", player_id);
                    if (JSON.game.homeTeam.players[j].position is null)
                    {
                        InsertData.Parameters.AddWithValue("@position", "");
                    }
                    else
                    {
                        InsertData.Parameters.AddWithValue("@position", JSON.game.homeTeam.players[j].position);
                    }
                    InsertData.Parameters.AddWithValue("@name", JSON.game.homeTeam.players[j].name);
                    InsertData.Parameters.AddWithValue("@number", JSON.game.homeTeam.players[j].jerseyNum);
                    InsertData.Parameters.AddWithValue("@college", "");
                    InsertData.Parameters.AddWithValue("@country", "");
                    InsertData.Parameters.AddWithValue("@draftYear", "");
                    InsertData.Parameters.AddWithValue("@draftRound", "");
                    InsertData.Parameters.AddWithValue("@draftPick", "");
                    Insert.Open();
                    InsertData.ExecuteScalar();
                    Insert.Close();
                }
            }
        }

        public static void AwayPlayerPost(Root JSON, int player_id, int j)
        {
            SqlConnection Insert = new SqlConnection(Bus_Driver.ConnectionString);
            using (Insert)
            {
                using (SqlCommand InsertData = new SqlCommand("playerInsert"))
                {
                    InsertData.Connection = Insert;
                    InsertData.CommandType = CommandType.StoredProcedure;
                    InsertData.Parameters.AddWithValue("@player_id", player_id);
                    if (JSON.game.awayTeam.players[j].position is null)
                    {
                        InsertData.Parameters.AddWithValue("@position", "");
                    }
                    else
                    {
                        InsertData.Parameters.AddWithValue("@position", JSON.game.awayTeam.players[j].position);
                    }
                    InsertData.Parameters.AddWithValue("@name", JSON.game.awayTeam.players[j].name);
                    InsertData.Parameters.AddWithValue("@number", JSON.game.awayTeam.players[j].jerseyNum);
                    InsertData.Parameters.AddWithValue("@college", "");
                    InsertData.Parameters.AddWithValue("@country", "");
                    InsertData.Parameters.AddWithValue("@draftYear", "");
                    InsertData.Parameters.AddWithValue("@draftRound", "");
                    InsertData.Parameters.AddWithValue("@draftPick", "");
                    Insert.Open();
                    InsertData.ExecuteScalar();
                    Insert.Close();
                }
            }
        }


        public static void OfficialPost(Root JSON, int official_id, int j)
        {
            SqlConnection Insert = new SqlConnection(Bus_Driver.ConnectionString);
            using (Insert)
            {
                using (SqlCommand InsertData = new SqlCommand("officialInsert"))
                {
                    InsertData.Connection = Insert;
                    InsertData.CommandType = CommandType.StoredProcedure;
                    InsertData.Parameters.AddWithValue("@official_id", official_id);
                    InsertData.Parameters.AddWithValue("@name", JSON.game.officials[j].name);
                    InsertData.Parameters.AddWithValue("@number", JSON.game.officials[j].jerseyNum);
                    InsertData.Parameters.AddWithValue("@assignment", JSON.game.officials[j].assignment);
                    Insert.Open();
                    InsertData.ExecuteScalar();
                    Insert.Close();
                }
            }
        }

        public static void GamePost(Root JSON, int game_id, int hScore, int aScore)
        {
            SqlConnection Insert = new SqlConnection(Bus_Driver.ConnectionString);
            using (Insert)
            {
                using (SqlCommand InsertData = new SqlCommand("gameInsert"))
                {
                    InsertData.Connection = Insert;
                    InsertData.CommandType = CommandType.StoredProcedure;
                    InsertData.Parameters.AddWithValue("@game_id", game_id);
                    InsertData.Parameters.AddWithValue("@date", JSON.game.gameTimeLocal.Date);
                    InsertData.Parameters.AddWithValue("@team_idH", JSON.game.homeTeam.teamId);
                    InsertData.Parameters.AddWithValue("@team_idA", JSON.game.awayTeam.teamId);
                    if (JSON.game.homeTeam.score >= JSON.game.awayTeam.score)
                    {
                        InsertData.Parameters.AddWithValue("@team_idW", JSON.game.homeTeam.teamId);
                        InsertData.Parameters.AddWithValue("@wScore", JSON.game.homeTeam.score);
                        InsertData.Parameters.AddWithValue("@team_idL", JSON.game.awayTeam.teamId);
                        InsertData.Parameters.AddWithValue("@lScore", JSON.game.awayTeam.score);
                    }
                    else
                    {
                        InsertData.Parameters.AddWithValue("@team_idW", JSON.game.awayTeam.teamId);
                        InsertData.Parameters.AddWithValue("@wScore", JSON.game.awayTeam.score);
                        InsertData.Parameters.AddWithValue("@team_idL", JSON.game.homeTeam.teamId);
                        InsertData.Parameters.AddWithValue("@lScore", JSON.game.homeTeam.score);
                    }
                    InsertData.Parameters.AddWithValue("@arena_id", JSON.game.arena.arenaId);
                    InsertData.Parameters.AddWithValue("@sellout", JSON.game.sellout);
                    Insert.Open();
                    InsertData.ExecuteScalar();
                    Insert.Close();
                }
            }
        }

        public static void GameUpdate(Root JSON, int game_id, int hScore, int aScore)
        {
            SqlConnection Insert = new SqlConnection(Bus_Driver.ConnectionString);
            using (Insert)
            {
                using (SqlCommand InsertData = new SqlCommand("gameUpdate"))
                {
                    InsertData.Connection = Insert;
                    InsertData.CommandType = CommandType.StoredProcedure;
                    InsertData.Parameters.AddWithValue("@game_id", game_id);
                    if (JSON.game.homeTeam.score >= JSON.game.awayTeam.score)
                    {
                        InsertData.Parameters.AddWithValue("@team_idW", JSON.game.homeTeam.teamId);
                        InsertData.Parameters.AddWithValue("@wScore", JSON.game.homeTeam.score);
                        InsertData.Parameters.AddWithValue("@team_idL", JSON.game.awayTeam.teamId);
                        InsertData.Parameters.AddWithValue("@lScore", JSON.game.awayTeam.score);
                    }
                    else
                    {
                        InsertData.Parameters.AddWithValue("@team_idW", JSON.game.awayTeam.teamId);
                        InsertData.Parameters.AddWithValue("@wScore", JSON.game.awayTeam.score);
                        InsertData.Parameters.AddWithValue("@team_idL", JSON.game.homeTeam.teamId);
                        InsertData.Parameters.AddWithValue("@lScore", JSON.game.homeTeam.score);
                    }
                    Insert.Open();
                    InsertData.ExecuteScalar();
                    Insert.Close();
                }
            }
        }


        public static void PlayerUpdate(int player_id, string position, string oldPosition)
        {
            SqlConnection Insert = new SqlConnection(Bus_Driver.ConnectionString);
            using (Insert)
            {
                using (SqlCommand InsertData = new SqlCommand("playerUpdate"))
                {
                    InsertData.Connection = Insert;
                    InsertData.CommandType = CommandType.StoredProcedure;
                    InsertData.Parameters.AddWithValue("@player_id", player_id);
                    if(oldPosition != "" && !oldPosition.Contains(position))
                    {
                        InsertData.Parameters.AddWithValue("@position", oldPosition + "/" + position);
                    }
                    else
                    {
                        InsertData.Parameters.AddWithValue("@position", position);
                    }
                    Insert.Open();
                    InsertData.ExecuteScalar();
                    Insert.Close();
                }
            }
        }






        public class Arena
        {
            public int arenaId { get; set; }
            public string arenaName { get; set; }
            public string arenaCity { get; set; }
            public string arenaState { get; set; }
            public string arenaCountry { get; set; }
            public string arenaTimezone { get; set; }
        }

        public class AwayTeam
        {
            public int teamId { get; set; }
            public string teamName { get; set; }
            public string teamCity { get; set; }
            public string teamTricode { get; set; }
            public int score { get; set; }
            public string inBonus { get; set; }
            public int timeoutsRemaining { get; set; }
            public List<Period> periods { get; set; }
            public List<Player> players { get; set; }
            public Statistics statistics { get; set; }
        }

        public class Game
        {
            public string gameId { get; set; }
            public DateTime gameTimeLocal { get; set; }
            public DateTime gameTimeUTC { get; set; }
            public DateTime gameTimeHome { get; set; }
            public DateTime gameTimeAway { get; set; }
            public DateTime gameEt { get; set; }
            public int duration { get; set; }
            public string gameCode { get; set; }
            public string gameStatusText { get; set; }
            public int gameStatus { get; set; }
            public int regulationPeriods { get; set; }
            public int period { get; set; }
            public string gameClock { get; set; }
            public int attendance { get; set; }
            public string sellout { get; set; }
            public Arena arena { get; set; }
            public List<Official> officials { get; set; }
            public HomeTeam homeTeam { get; set; }
            public AwayTeam awayTeam { get; set; }
        }

        public class HomeTeam
        {
            public int teamId { get; set; }
            public string teamName { get; set; }
            public string teamCity { get; set; }
            public string teamTricode { get; set; }
            public int score { get; set; }
            public string inBonus { get; set; }
            public int timeoutsRemaining { get; set; }
            public List<Period> periods { get; set; }
            public List<Player> players { get; set; }
            public Statistics statistics { get; set; }
        }

        public class Meta
        {
            public int version { get; set; }
            public int code { get; set; }
            public string request { get; set; }
            public string time { get; set; }
        }

        public class Official
        {
            public int personId { get; set; }
            public string name { get; set; }
            public string nameI { get; set; }
            public string firstName { get; set; }
            public string familyName { get; set; }
            public string jerseyNum { get; set; }
            public string assignment { get; set; }
        }

        public class Period
        {
            public int period { get; set; }
            public string periodType { get; set; }
            public int score { get; set; }
        }

        public class Player
        {
            public string status { get; set; }
            public int order { get; set; }
            public int personId { get; set; }
            public string jerseyNum { get; set; }
            public string position { get; set; }
            public string starter { get; set; }
            public string oncourt { get; set; }
            public string played { get; set; }
            public Statistics statistics { get; set; }
            public string name { get; set; }
            public string nameI { get; set; }
            public string firstName { get; set; }
            public string familyName { get; set; }
            public string notPlayingReason { get; set; }
            public string notPlayingDescription { get; set; }
        }

        public class Root
        {
            public Meta meta { get; set; }
            public Game game { get; set; }
        }

        public class Statistics
        {
            public int assists { get; set; }
            public int blocks { get; set; }
            public int blocksReceived { get; set; }
            public int fieldGoalsAttempted { get; set; }
            public int fieldGoalsMade { get; set; }
            public double fieldGoalsPercentage { get; set; }
            public int foulsOffensive { get; set; }
            public int foulsDrawn { get; set; }
            public int foulsPersonal { get; set; }
            public int foulsTechnical { get; set; }
            public int freeThrowsAttempted { get; set; }
            public int freeThrowsMade { get; set; }
            public double freeThrowsPercentage { get; set; }
            public double minus { get; set; }
            public string minutes { get; set; }
            public string minutesCalculated { get; set; }
            public double plus { get; set; }
            public double plusMinusPoints { get; set; }
            public int points { get; set; }
            public int pointsFastBreak { get; set; }
            public int pointsInThePaint { get; set; }
            public int pointsSecondChance { get; set; }
            public int reboundsDefensive { get; set; }
            public int reboundsOffensive { get; set; }
            public int reboundsTotal { get; set; }
            public int steals { get; set; }
            public int threePointersAttempted { get; set; }
            public int threePointersMade { get; set; }
            public double threePointersPercentage { get; set; }
            public int turnovers { get; set; }
            public int twoPointersAttempted { get; set; }
            public int twoPointersMade { get; set; }
            public double twoPointersPercentage { get; set; }
            public double assistsTurnoverRatio { get; set; }
            public int benchPoints { get; set; }
            public int biggestLead { get; set; }
            public string biggestLeadScore { get; set; }
            public int biggestScoringRun { get; set; }
            public string biggestScoringRunScore { get; set; }
            public int fastBreakPointsAttempted { get; set; }
            public int fastBreakPointsMade { get; set; }
            public double fastBreakPointsPercentage { get; set; }
            public double fieldGoalsEffectiveAdjusted { get; set; }
            public int foulsTeam { get; set; }
            public int foulsTeamTechnical { get; set; }
            public int leadChanges { get; set; }
            public int pointsAgainst { get; set; }
            public int pointsFromTurnovers { get; set; }
            public int pointsInThePaintAttempted { get; set; }
            public int pointsInThePaintMade { get; set; }
            public double pointsInThePaintPercentage { get; set; }
            public int reboundsPersonal { get; set; }
            public int reboundsTeam { get; set; }
            public int reboundsTeamDefensive { get; set; }
            public int reboundsTeamOffensive { get; set; }
            public int secondChancePointsAttempted { get; set; }
            public int secondChancePointsMade { get; set; }
            public double secondChancePointsPercentage { get; set; }
            public string timeLeading { get; set; }
            public int timesTied { get; set; }
            public double trueShootingAttempts { get; set; }
            public double trueShootingPercentage { get; set; }
            public int turnoversTeam { get; set; }
            public int turnoversTotal { get; set; }
        }
    }

}