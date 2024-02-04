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
using System.Runtime.Remoting.Messaging;
using nbaJSON;

namespace gameRider
{
    public partial class gameRider
    {
        static int limit = 0;
        static int gamesCreatedInt = 0;
        static int gamesUpdatedInt = 0;
        Bus_Driver bus_Driver = new Bus_Driver();
        public static void CheckGames(Label gamesCreatedLbl, Label gamesUpdatedLbl)
        {
            gamesCreatedLbl.Text = "";
            gamesUpdatedLbl.Text = "";
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string jsonLink = "";
            int lastGame_id = 0;
            int game_id = 0;
            SqlConnection sqlConnect = new SqlConnection(Bus_Driver.ConnectionString);
            using (sqlConnect)
            {
                using (SqlCommand lastGame = new SqlCommand("lastGame"))
                {
                    lastGame.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter slastGame = new SqlDataAdapter())
                    {
                        lastGame.Connection = sqlConnect;
                        slastGame.SelectCommand = lastGame;
                        sqlConnect.Open();
                        SqlDataReader reader = lastGame.ExecuteReader();
                        while (reader.Read())
                        {
                            lastGame_id = reader.GetInt32(0) + 1;
                            jsonLink = "https://cdn.nba.com/static/json/liveData/boxscore/boxscore_00" + lastGame_id + ".json";
                            try
                            {
                                WebRequest testRequest = WebRequest.Create(jsonLink);
                                WebResponse testShouldWork = testRequest.GetResponse();
                                string json = client.DownloadString(jsonLink);
                                Root JSON = JsonConvert.DeserializeObject<Root>(json);
                                string lastGameDate = JSON.game.gameTimeLocal.ToShortDateString();
                                CreateGameLoop(lastGame_id, lastGameDate);

                            }
                            catch (WebException e)
                            {

                            }
                        }
                        if (gamesCreatedInt == 0)
                        {
                            gamesCreatedLbl.ForeColor = System.Drawing.Color.DarkGoldenrod;
                            gamesCreatedLbl.Text = "Program run successfully, but no games found to create yet. Please try again once another game has tipped off.";
                        }
                        else
                        {
                            gamesCreatedLbl.ForeColor = System.Drawing.Color.Green;
                            gamesCreatedLbl.Text = "Program run successfully, " + gamesCreatedInt.ToString() + " games created!";
                        }
                        sqlConnect.Close();
                    }
                }
                using (SqlCommand checkGames = new SqlCommand("checkGames"))
                {
                    checkGames.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter scheckGames = new SqlDataAdapter())
                    {
                        checkGames.Connection = sqlConnect;
                        scheckGames.SelectCommand = checkGames;
                        sqlConnect.Open();
                        SqlDataReader reader = checkGames.ExecuteReader();
                        while (reader.Read())
                        {
                            game_id = reader.GetInt32(0);
                            jsonLink = "https://cdn.nba.com/static/json/liveData/boxscore/boxscore_00" + game_id + ".json";
                            try
                            {
                                WebRequest testRequest = WebRequest.Create(jsonLink);
                                WebResponse testShouldWork = testRequest.GetResponse();
                                string json = client.DownloadString(jsonLink);
                                Root JSON = JsonConvert.DeserializeObject<Root>(json);
                                int team_idH = reader.GetInt32(2);
                                int team_idA = reader.GetInt32(3);
                                int wScore = reader.GetInt32(5);
                                int lScore = reader.GetInt32(7);
                                if ((JSON.game.homeTeam.score != wScore || JSON.game.awayTeam.score != lScore) && (JSON.game.homeTeam.score != lScore || JSON.game.awayTeam.score != wScore))
                                {
                                    UpdateGame(JSON);
                                    gamesUpdatedInt++;
                                }
                            }
                            catch (WebException e)
                            {

                            }
                        }
                        if(gamesUpdatedInt == 0)
                        {
                            gamesUpdatedLbl.ForeColor = System.Drawing.Color.DarkGoldenrod;
                            gamesUpdatedLbl.Text = "Program run successfully, but no games found needing updates.";
                        }
                        else
                        {
                            gamesUpdatedLbl.ForeColor = System.Drawing.Color.Green;
                            gamesUpdatedLbl.Text = "Program run successfully, " + gamesUpdatedInt.ToString() + " games updated!";
                        }
                        sqlConnect.Close();
                    }
                }
            }
        }
        public static void CreateGameLoop(int lastGame_id, string lastGameDate)
        {
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 }; 
            limit = 0;

            GetGameRange(limit, lastGame_id, lastGameDate);
            for (int i = lastGame_id; i < limit; i++)    //685
            {
                string jsonLink = "https://cdn.nba.com/static/json/liveData/boxscore/boxscore_00" + i + ".json";
                try
                {
                    WebRequest testRequest = WebRequest.Create(jsonLink);
                    WebResponse testShouldWork = testRequest.GetResponse();
                    string json = client.DownloadString(jsonLink);
                    Root JSON = JsonConvert.DeserializeObject<Root>(json);
                    CreateGame(JSON);
                    gamesCreatedInt++;
                }
                catch (WebException e)
                {

                }
            }

        }

        public static void CreateGame(Root JSON)
        {
            SqlConnection Insert = new SqlConnection(Bus_Driver.ConnectionString);
            using (Insert)
            {
                using (SqlCommand InsertData = new SqlCommand("gameInsert"))
                {
                    InsertData.Connection = Insert;
                    InsertData.CommandType = CommandType.StoredProcedure;
                    InsertData.Parameters.AddWithValue("@game_id", Int32.Parse(JSON.game.gameId));
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


        public static void UpdateGame(Root JSON)
        {
            SqlConnection Insert = new SqlConnection(Bus_Driver.ConnectionString);
            using (Insert)
            {
                using (SqlCommand InsertData = new SqlCommand("gameUpdate"))
                {
                    InsertData.Connection = Insert;
                    InsertData.CommandType = CommandType.StoredProcedure;
                    InsertData.Parameters.AddWithValue("@game_id", Int32.Parse(JSON.game.gameId));
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


        public static void GetGameRange(int limitRange, int smallID, string lastGameDate)
        {
            if (DateTime.Parse(lastGameDate) <= DateTime.Parse("01/31/2024"))
            {
                limitRange = 22300686;
                limit = limitRange;
            }
            else if (DateTime.Parse(lastGameDate) > DateTime.Parse("01/31/2024") && DateTime.Parse(lastGameDate) <= DateTime.Parse("02/15/2024"))
            {
                limitRange = 22300796;
                limit = limitRange;
            }
            else if (DateTime.Parse(lastGameDate) > DateTime.Parse("02/21/2024") && DateTime.Parse(lastGameDate) <= DateTime.Parse("02/29/2024"))
            {
                limitRange = 22300860;
                limit = limitRange;
            }
            else if (DateTime.Parse(lastGameDate) > DateTime.Parse("02/29/2024") && DateTime.Parse(lastGameDate) <= DateTime.Parse("03/15/2024"))
            {
                limitRange = 22300970;
                limit = limitRange;
            }
            else if (DateTime.Parse(lastGameDate) > DateTime.Parse("03/15/2024") && DateTime.Parse(lastGameDate) <= DateTime.Parse("03/31/2024"))
            {
                limitRange = 22301090;
                limit = limitRange;
            }
            else if (DateTime.Parse(lastGameDate) > DateTime.Parse("03/31/2024") && DateTime.Parse(lastGameDate) <= DateTime.Parse("04/14/2024"))
            {
                limitRange = 22301201;
                limit = limitRange;
            }
            else
            {
                limit = limitRange;
            }
            //else if (DateTime.Parse(lastGameDate) == DateTime.Parse("02/16/2024"))
            //{
            //    int first = 32300004;
            //    int last = 32300006 + 1;
            //    GetSpecialGames(first, last);
            //    limitRange = smallID;
            //}
            //else if (DateTime.Parse(lastGameDate) == DateTime.Parse("02/18/2024"))
            //{
            //    int first = 32300001;
            //    int last = 32300001 + 1;
            //    GetSpecialGames(first, last);
            //    limitRange = smallID;
            //}
            //else if (DateTime.Parse(lastGameDate) == DateTime.Parse("04/16/2024"))
            //{
            //    int first = 52300101;
            //    int last = 52300101 + 1;
            //    GetSpecialGames(first, last);
            //    limitRange = smallID;
            //}
            //else if (DateTime.Parse(lastGameDate) == DateTime.Parse("04/17/2024"))
            //{
            //    int first = 52300111;
            //    int last = 52300131 + 1;
            //    GetSpecialGames(first, last);
            //    limitRange = smallID;
            //}
            //else if (DateTime.Parse(lastGameDate) == DateTime.Parse("04/19/2024"))
            //{
            //    int first = 52300201;
            //    int last = 52300211 + 1;
            //    GetSpecialGames(first, last);
            //    limitRange = smallID;
            //}
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
