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

namespace playoffRider
{
    public partial class playoffRider
    {
        Bus_Driver bus_Driver = new Bus_Driver();
        static WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
        static SqlConnection SQL = new SqlConnection(Bus_Driver.ConnectionString);
        public static void init()
        {
            GetPicture();
            GetBracket();
        }
        public static void GetPicture()
        {
            string jsonLink = "https://stats.nba.com/stats/playoffbracket?LeagueID=00&SeasonYear=2023&State=0";
            try
            {
                string json = client.DownloadString(jsonLink);
                Root JSON = JsonConvert.DeserializeObject<Root>(json);
                CheckPicture(JSON);
            }
            catch (WebException e)
            {

            }
        }
        public static void GetBracket()
        {
            string jsonLink = "https://stats.nba.com/stats/playoffbracket?LeagueID=00&SeasonYear=2023&State=2";
            try
            {
                string json = client.DownloadString(jsonLink);
                Root JSON = JsonConvert.DeserializeObject<Root>(json);
                if (JSON.bracket.playoffBracketSeries[0].highSeedId == 0)
                {
                    PostBlankBracket(JSON);
                }
                else
                {
                    CheckBracket(JSON);
                }                
            }
            catch (WebException e)
            {

            }
        }
        public static void CheckPicture(Root JSON)
        {
            SqlConnection SQL = new SqlConnection(Bus_Driver.ConnectionString);
            using (SQL)
            {
                using (SqlCommand CheckPicture = new SqlCommand("checkPicture"))
                {
                    CheckPicture.Connection = SQL;
                    CheckPicture.CommandType = CommandType.StoredProcedure;
                    SQL.Open();
                    SqlDataReader reader = CheckPicture.ExecuteReader();
                    if(reader.Read())
                    {
                        SQL.Close();
                        UpdatePicture(JSON);
                    }
                    else
                    {
                        SQL.Close();
                        PostPicture(JSON);
                    }
                }
            }
        }
        public static void CheckBracket(Root JSON)
        {
            //If there arent any results
            SqlConnection SQL = new SqlConnection(Bus_Driver.ConnectionString);
            using (SQL)
            {
                using (SqlCommand CheckBracket = new SqlCommand("checkBracket"))
                {
                    CheckBracket.Connection = SQL;
                    CheckBracket.CommandType = CommandType.StoredProcedure;
                    SQL.Open();
                    SqlDataReader reader = CheckBracket.ExecuteReader();
                    if (reader.Read())
                    {
                        SQL.Close();
                        UpdateBracket(JSON);
                    }
                    else
                    {
                        SQL.Close();
                        PostBracket(JSON);
                    }
                }
            }
        }

        public static void UpdatePicture(Root JSON)
        {
            int count = JSON.bracket.playoffPictureSeries.Count();
            SqlConnection SQL = new SqlConnection(Bus_Driver.ConnectionString);
            using (SQL)
            {
                for (int i = 0; i < count; i++)
                {
                    using (SqlCommand UpdatePictureCheck = new SqlCommand("updatePictureCheck"))
                    {
                        UpdatePictureCheck.Connection = SQL;
                        UpdatePictureCheck.CommandType = CommandType.StoredProcedure;
                        UpdatePictureCheck.Parameters.AddWithValue("@conference", JSON.bracket.playoffPictureSeries[i].conference);
                        UpdatePictureCheck.Parameters.AddWithValue("@matchupType", JSON.bracket.playoffPictureSeries[i].matchupType);
                        UpdatePictureCheck.Parameters.AddWithValue("@highSeed_id", JSON.bracket.playoffPictureSeries[i].highSeedId);
                        UpdatePictureCheck.Parameters.AddWithValue("@highSeedRank", JSON.bracket.playoffPictureSeries[i].highSeedRank);
                        UpdatePictureCheck.Parameters.AddWithValue("@highSeedRegSeasonWins", JSON.bracket.playoffPictureSeries[i].highSeedRegSeasonWins);
                        UpdatePictureCheck.Parameters.AddWithValue("@highSeedRegSeasonLosses", JSON.bracket.playoffPictureSeries[i].highSeedRegSeasonLosses);
                        UpdatePictureCheck.Parameters.AddWithValue("@lowSeed_id", JSON.bracket.playoffPictureSeries[i].lowSeedId);
                        UpdatePictureCheck.Parameters.AddWithValue("@lowSeedRank", JSON.bracket.playoffPictureSeries[i].lowSeedRank);
                        UpdatePictureCheck.Parameters.AddWithValue("@lowSeedRegSeasonWins", JSON.bracket.playoffPictureSeries[i].lowSeedRegSeasonWins);
                        UpdatePictureCheck.Parameters.AddWithValue("@lowSeedRegSeasonLosses", JSON.bracket.playoffPictureSeries[i].lowSeedRegSeasonLosses);
                        SQL.Open();
                        SqlDataReader reader = UpdatePictureCheck.ExecuteReader();
                        if (reader.Read())
                        {
                            SQL.Close();
                            using (SqlCommand UpdatePicture = new SqlCommand("updatePicture"))
                            {
                                UpdatePicture.Connection = SQL;
                                UpdatePicture.CommandType = CommandType.StoredProcedure;
                                UpdatePicture.Parameters.AddWithValue("@conference", JSON.bracket.playoffPictureSeries[i].conference);
                                UpdatePicture.Parameters.AddWithValue("@matchupType", JSON.bracket.playoffPictureSeries[i].matchupType);
                                UpdatePicture.Parameters.AddWithValue("@highSeed_id", JSON.bracket.playoffPictureSeries[i].highSeedId);
                                UpdatePicture.Parameters.AddWithValue("@highSeedRank", JSON.bracket.playoffPictureSeries[i].highSeedRank);
                                UpdatePicture.Parameters.AddWithValue("@highSeedRegSeasonWins", JSON.bracket.playoffPictureSeries[i].highSeedRegSeasonWins);
                                UpdatePicture.Parameters.AddWithValue("@highSeedRegSeasonLosses", JSON.bracket.playoffPictureSeries[i].highSeedRegSeasonLosses);
                                UpdatePicture.Parameters.AddWithValue("@lowSeed_id", JSON.bracket.playoffPictureSeries[i].lowSeedId);
                                UpdatePicture.Parameters.AddWithValue("@lowSeedRank", JSON.bracket.playoffPictureSeries[i].lowSeedRank);
                                UpdatePicture.Parameters.AddWithValue("@lowSeedRegSeasonWins", JSON.bracket.playoffPictureSeries[i].lowSeedRegSeasonWins);
                                UpdatePicture.Parameters.AddWithValue("@lowSeedRegSeasonLosses", JSON.bracket.playoffPictureSeries[i].lowSeedRegSeasonLosses);
                                SQL.Open();
                                UpdatePicture.ExecuteScalar();
                                SQL.Close();
                            }
                        }
                        else
                        {
                            SQL.Close();
                            using (SqlCommand UpdatePictureCheckEntry = new SqlCommand("updatePictureCheckEntry"))
                            {
                                UpdatePictureCheckEntry.Connection = SQL;
                                UpdatePictureCheckEntry.CommandType = CommandType.StoredProcedure;
                                UpdatePictureCheckEntry.Parameters.AddWithValue("@conference", JSON.bracket.playoffPictureSeries[i].conference);
                                UpdatePictureCheckEntry.Parameters.AddWithValue("@matchupType", JSON.bracket.playoffPictureSeries[i].matchupType);
                                UpdatePictureCheckEntry.Parameters.AddWithValue("@highSeed_id", JSON.bracket.playoffPictureSeries[i].highSeedId);
                                UpdatePictureCheckEntry.Parameters.AddWithValue("@highSeedRank", JSON.bracket.playoffPictureSeries[i].highSeedRank);
                                UpdatePictureCheckEntry.Parameters.AddWithValue("@highSeedRegSeasonWins", JSON.bracket.playoffPictureSeries[i].highSeedRegSeasonWins);
                                UpdatePictureCheckEntry.Parameters.AddWithValue("@highSeedRegSeasonLosses", JSON.bracket.playoffPictureSeries[i].highSeedRegSeasonLosses);
                                UpdatePictureCheckEntry.Parameters.AddWithValue("@lowSeed_id", JSON.bracket.playoffPictureSeries[i].lowSeedId);
                                UpdatePictureCheckEntry.Parameters.AddWithValue("@lowSeedRank", JSON.bracket.playoffPictureSeries[i].lowSeedRank);
                                UpdatePictureCheckEntry.Parameters.AddWithValue("@lowSeedRegSeasonWins", JSON.bracket.playoffPictureSeries[i].lowSeedRegSeasonWins);
                                UpdatePictureCheckEntry.Parameters.AddWithValue("@lowSeedRegSeasonLosses", JSON.bracket.playoffPictureSeries[i].lowSeedRegSeasonLosses);
                                SQL.Open();
                                SqlDataReader reader1 = UpdatePictureCheckEntry.ExecuteReader();
                                if (reader1.Read())
                                {
                                    SQL.Close();
                                }
                                else
                                {
                                    SQL.Close();
                                    PostPicture(JSON);
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void PostPicture(Root JSON)
        {
            int count = JSON.bracket.playoffPictureSeries.Count();
            using (SQL)
            {
                for (int i = 0; i < count; i++)
                {
                    using (SqlCommand InsertData = new SqlCommand("pictureInsert"))
                    {
                        InsertData.Connection = SQL;
                        InsertData.CommandType = CommandType.StoredProcedure;
                        InsertData.Parameters.AddWithValue("@conference",               JSON.bracket.playoffPictureSeries[i].conference);
                        InsertData.Parameters.AddWithValue("@matchupType",              JSON.bracket.playoffPictureSeries[i].matchupType);
                        InsertData.Parameters.AddWithValue("@highSeed_id",              JSON.bracket.playoffPictureSeries[i].highSeedId);
                        InsertData.Parameters.AddWithValue("@highSeedRank",             JSON.bracket.playoffPictureSeries[i].highSeedRank);
                        InsertData.Parameters.AddWithValue("@highSeedRegSeasonWins",    JSON.bracket.playoffPictureSeries[i].highSeedRegSeasonWins);
                        InsertData.Parameters.AddWithValue("@highSeedRegSeasonLosses",  JSON.bracket.playoffPictureSeries[i].highSeedRegSeasonLosses);
                        InsertData.Parameters.AddWithValue("@lowSeed_id",               JSON.bracket.playoffPictureSeries[i].lowSeedId);
                        InsertData.Parameters.AddWithValue("@lowSeedRank",              JSON.bracket.playoffPictureSeries[i].lowSeedRank);
                        InsertData.Parameters.AddWithValue("@lowSeedRegSeasonWins",     JSON.bracket.playoffPictureSeries[i].lowSeedRegSeasonWins);
                        InsertData.Parameters.AddWithValue("@lowSeedRegSeasonLosses",   JSON.bracket.playoffPictureSeries[i].lowSeedRegSeasonLosses);
                        SQL.Open();
                        InsertData.ExecuteScalar();
                        SQL.Close();
                    }
                }
            }
        }
        public static void PostBlankBracket(Root JSON)
        {
            SqlConnection SQL = new SqlConnection(Bus_Driver.ConnectionString);
            int count = JSON.bracket.playoffBracketSeries.Count();
            using (SQL)
            {
                for (int i = 0; i < count; i++)
                {
                    string series_id = JSON.bracket.playoffBracketSeries[i].seriesId.Remove(0, 3).Remove(1, 2).Replace("0000000000", "").Insert(3, "00").Replace("_", "");
                    using (SqlCommand BlankCheck = new SqlCommand("blackBracketCheck"))
                    {
                        BlankCheck.Connection = SQL;
                        BlankCheck.CommandType = CommandType.StoredProcedure;
                        BlankCheck.Parameters.AddWithValue("@series_id", series_id);
                        SQL.Open();
                        SqlDataReader reader1 = BlankCheck.ExecuteReader();
                        if (reader1.Read())
                        {
                            SQL.Close();
                        }
                        else
                        {
                            using (SqlCommand InsertData = new SqlCommand("blankBracketInsert"))
                            {
                                InsertData.Connection = SQL;
                                InsertData.CommandType = CommandType.StoredProcedure;
                                InsertData.Parameters.AddWithValue("@series_id", series_id);
                                InsertData.Parameters.AddWithValue("@conference", JSON.bracket.playoffBracketSeries[i].seriesConference);
                                InsertData.Parameters.AddWithValue("@roundNumber", JSON.bracket.playoffBracketSeries[i].roundNumber);
                                InsertData.Parameters.AddWithValue("@status", JSON.bracket.playoffBracketSeries[i].seriesStatus);
                                SQL.Open();
                                InsertData.ExecuteScalar();
                                SQL.Close();
                            }
                        }
                    }
                }
            } 
        }
        public static void PostBracket(Root JSON)
        {
            SqlConnection SQL = new SqlConnection(Bus_Driver.ConnectionString);
            int count = JSON.bracket.playoffBracketSeries.Count();
            using (SQL)
            {
                for (int i = 0; i < count; i++)
                {
                    int highSeed_id = JSON.bracket.playoffBracketSeries[i].highSeedId;
                    int lowSeed_id = JSON.bracket.playoffBracketSeries[i].lowSeedId;
                    string series_id = JSON.bracket.playoffBracketSeries[i].seriesId.Remove(0, 3).Remove(1, 2).Replace(lowSeed_id.ToString(), "").Replace(highSeed_id.ToString(), "").Insert(3, "00").Replace("_", "");
                    using (SqlCommand BlankCheck = new SqlCommand("blackBracketCheck"))
                    {
                        BlankCheck.Connection = SQL;
                        BlankCheck.CommandType = CommandType.StoredProcedure;
                        BlankCheck.Parameters.AddWithValue("@series_id", series_id);
                        SQL.Open();
                        SqlDataReader reader1 = BlankCheck.ExecuteReader();
                        if (reader1.Read())
                        {
                            SQL.Close();
                        }
                        else
                        {
                            SQL.Close();
                            using (SqlCommand InsertData = new SqlCommand("bracketInsert"))
                            {
                                InsertData.Connection = SQL;
                                InsertData.CommandType = CommandType.StoredProcedure;
                                InsertData.Parameters.AddWithValue("@series_id", series_id);
                                InsertData.Parameters.AddWithValue("@conference", JSON.bracket.playoffBracketSeries[i].seriesConference);
                                InsertData.Parameters.AddWithValue("@roundNumber", JSON.bracket.playoffBracketSeries[i].roundNumber);
                                InsertData.Parameters.AddWithValue("@description", JSON.bracket.playoffBracketSeries[i].seriesText);
                                InsertData.Parameters.AddWithValue("@status", JSON.bracket.playoffBracketSeries[i].seriesStatus);
                                InsertData.Parameters.AddWithValue("@seriesWinner", JSON.bracket.playoffBracketSeries[i].seriesWinner);
                                InsertData.Parameters.AddWithValue("@highSeed_id", JSON.bracket.playoffBracketSeries[i].highSeedId);
                                InsertData.Parameters.AddWithValue("@highSeedSeriesWins", JSON.bracket.playoffBracketSeries[i].highSeedSeriesWins);
                                InsertData.Parameters.AddWithValue("@lowSeed_id", JSON.bracket.playoffBracketSeries[i].lowSeedId);
                                InsertData.Parameters.AddWithValue("@lowSeedSeriesWins", JSON.bracket.playoffBracketSeries[i].lowSeedSeriesWins);
                                InsertData.Parameters.AddWithValue("@nextGame_id", JSON.bracket.playoffBracketSeries[i].nextGameId);
                                InsertData.Parameters.AddWithValue("@nextGameNumber", JSON.bracket.playoffBracketSeries[i].nextGameNumber);
                                InsertData.Parameters.AddWithValue("@nextSeries_id", 0); //Need to fill placeholder

                                SQL.Open();
                                InsertData.ExecuteScalar();
                                SQL.Close();
                            }
                        }
                    }
                    
                }
            }
        }






        public static void UpdateBracket(Root JSON)
        {
            int count = JSON.bracket.playoffBracketSeries.Count();
            SqlConnection SQL = new SqlConnection(Bus_Driver.ConnectionString);
            using (SQL)
            {
                for (int i = 0; i < count; i++)
                {
                    int highSeed_id = JSON.bracket.playoffBracketSeries[i].highSeedId;
                    int lowSeed_id = JSON.bracket.playoffBracketSeries[i].lowSeedId;
                    string series_id = JSON.bracket.playoffBracketSeries[i].seriesId.Remove(0, 3).Remove(1, 2).Replace(lowSeed_id.ToString(), "").Replace(highSeed_id.ToString(), "").Insert(3, "00").Replace("_", "");
                    using (SqlCommand UpdateBracketCheck = new SqlCommand("updateBracketCheck"))
                    {
                        UpdateBracketCheck.Connection = SQL;
                        UpdateBracketCheck.CommandType = CommandType.StoredProcedure;
                        UpdateBracketCheck.Parameters.AddWithValue("@series_id", series_id);
                        UpdateBracketCheck.Parameters.AddWithValue("@conference", JSON.bracket.playoffBracketSeries[i].seriesConference);
                        UpdateBracketCheck.Parameters.AddWithValue("@roundNumber", JSON.bracket.playoffBracketSeries[i].roundNumber);
                        UpdateBracketCheck.Parameters.AddWithValue("@description", JSON.bracket.playoffBracketSeries[i].seriesText);
                        UpdateBracketCheck.Parameters.AddWithValue("@status", JSON.bracket.playoffBracketSeries[i].seriesStatus);
                        UpdateBracketCheck.Parameters.AddWithValue("@seriesWinner", JSON.bracket.playoffBracketSeries[i].seriesWinner);
                        UpdateBracketCheck.Parameters.AddWithValue("@highSeed_id", JSON.bracket.playoffBracketSeries[i].highSeedId);
                        UpdateBracketCheck.Parameters.AddWithValue("@highSeedSeriesWins", JSON.bracket.playoffBracketSeries[i].highSeedSeriesWins);
                        UpdateBracketCheck.Parameters.AddWithValue("@lowSeed_id", JSON.bracket.playoffBracketSeries[i].lowSeedId);
                        UpdateBracketCheck.Parameters.AddWithValue("@lowSeedSeriesWins", JSON.bracket.playoffBracketSeries[i].lowSeedSeriesWins);
                        if (JSON.bracket.playoffBracketSeries[i].nextGameId.ToString() == "")
                        {
                            UpdateBracketCheck.Parameters.AddWithValue("@nextGame_id", 0);
                            UpdateBracketCheck.Parameters.AddWithValue("@nextGameNumber", 0);
                        }
                        else
                        {
                            UpdateBracketCheck.Parameters.AddWithValue("@nextGame_id", JSON.bracket.playoffBracketSeries[i].nextGameId);
                            UpdateBracketCheck.Parameters.AddWithValue("@nextGameNumber", JSON.bracket.playoffBracketSeries[i].nextGameNumber);
                        }
                        UpdateBracketCheck.Parameters.AddWithValue("@nextSeries_id", 0); //Need to fill placeholder
                        SQL.Open();
                        SqlDataReader reader = UpdateBracketCheck.ExecuteReader();
                        if (reader.Read())
                        {
                            SQL.Close();
                            using (SqlCommand UpdateBracket = new SqlCommand("updateBracket"))
                            {
                                UpdateBracket.Connection = SQL;
                                UpdateBracket.CommandType = CommandType.StoredProcedure;
                                UpdateBracket.Parameters.AddWithValue("@series_id", series_id);
                                UpdateBracket.Parameters.AddWithValue("@conference", JSON.bracket.playoffBracketSeries[i].seriesConference);
                                UpdateBracket.Parameters.AddWithValue("@roundNumber", JSON.bracket.playoffBracketSeries[i].roundNumber);
                                UpdateBracket.Parameters.AddWithValue("@description", JSON.bracket.playoffBracketSeries[i].seriesText);
                                UpdateBracket.Parameters.AddWithValue("@status", JSON.bracket.playoffBracketSeries[i].seriesStatus);
                                UpdateBracket.Parameters.AddWithValue("@seriesWinner", JSON.bracket.playoffBracketSeries[i].seriesWinner);
                                UpdateBracket.Parameters.AddWithValue("@highSeed_id", JSON.bracket.playoffBracketSeries[i].highSeedId);
                                UpdateBracket.Parameters.AddWithValue("@highSeedSeriesWins", JSON.bracket.playoffBracketSeries[i].highSeedSeriesWins);
                                UpdateBracket.Parameters.AddWithValue("@lowSeed_id", JSON.bracket.playoffBracketSeries[i].lowSeedId);
                                UpdateBracket.Parameters.AddWithValue("@lowSeedSeriesWins", JSON.bracket.playoffBracketSeries[i].lowSeedSeriesWins);
                                if (JSON.bracket.playoffBracketSeries[i].nextGameId.ToString() == "")
                                {
                                    UpdateBracket.Parameters.AddWithValue("@nextGame_id", 0);
                                    UpdateBracket.Parameters.AddWithValue("@nextGameNumber", 0);
                                }
                                else
                                {
                                    UpdateBracket.Parameters.AddWithValue("@nextGame_id", JSON.bracket.playoffBracketSeries[i].nextGameId);
                                    UpdateBracket.Parameters.AddWithValue("@nextGameNumber", JSON.bracket.playoffBracketSeries[i].nextGameNumber);
                                }
                                UpdateBracket.Parameters.AddWithValue("@nextSeries_id", 0); //Need to fill placeholder
                                SQL.Open();
                                UpdateBracket.ExecuteScalar();
                                SQL.Close();
                            }
                        }
                        else
                        {
                            SQL.Close();
                            using (SqlCommand UpdateBracketCheckEntry = new SqlCommand("updateBracketCheckEntry"))
                            {
                                UpdateBracketCheckEntry.Connection = SQL;
                                UpdateBracketCheckEntry.CommandType = CommandType.StoredProcedure;
                                UpdateBracketCheckEntry.Parameters.AddWithValue("@series_id", series_id);
                                UpdateBracketCheckEntry.Parameters.AddWithValue("@conference", JSON.bracket.playoffBracketSeries[i].seriesConference);
                                UpdateBracketCheckEntry.Parameters.AddWithValue("@roundNumber", JSON.bracket.playoffBracketSeries[i].roundNumber);
                                UpdateBracketCheckEntry.Parameters.AddWithValue("@description", JSON.bracket.playoffBracketSeries[i].seriesText);
                                UpdateBracketCheckEntry.Parameters.AddWithValue("@status", JSON.bracket.playoffBracketSeries[i].seriesStatus);
                                UpdateBracketCheckEntry.Parameters.AddWithValue("@seriesWinner", JSON.bracket.playoffBracketSeries[i].seriesWinner);
                                UpdateBracketCheckEntry.Parameters.AddWithValue("@highSeed_id", JSON.bracket.playoffBracketSeries[i].highSeedId);
                                UpdateBracketCheckEntry.Parameters.AddWithValue("@highSeedSeriesWins", JSON.bracket.playoffBracketSeries[i].highSeedSeriesWins);
                                UpdateBracketCheckEntry.Parameters.AddWithValue("@lowSeed_id", JSON.bracket.playoffBracketSeries[i].lowSeedId);
                                UpdateBracketCheckEntry.Parameters.AddWithValue("@lowSeedSeriesWins", JSON.bracket.playoffBracketSeries[i].lowSeedSeriesWins);
                                if (JSON.bracket.playoffBracketSeries[i].nextGameId.ToString() == "")
                                {
                                    UpdateBracketCheckEntry.Parameters.AddWithValue("@nextGame_id", 0);
                                    UpdateBracketCheckEntry.Parameters.AddWithValue("@nextGameNumber", 0);
                                }
                                else
                                {
                                    UpdateBracketCheckEntry.Parameters.AddWithValue("@nextGame_id", JSON.bracket.playoffBracketSeries[i].nextGameId);
                                    UpdateBracketCheckEntry.Parameters.AddWithValue("@nextGameNumber", JSON.bracket.playoffBracketSeries[i].nextGameNumber);
                                }
                                UpdateBracketCheckEntry.Parameters.AddWithValue("@nextSeries_id", 0); //Need to fill placeholder
                                SQL.Open();
                                SqlDataReader reader1 = UpdateBracketCheckEntry.ExecuteReader();
                                if (reader1.Read())
                                {
                                    SQL.Close();
                                }
                                else
                                {
                                    SQL.Close();
                                    PostBracket(JSON);
                                }
                            }
                        }
                    }
                }
            }
        }
















    }






    public class Bracket
    {
        public string leagueId { get; set; }
        public string seasonYear { get; set; }
        public string bracketType { get; set; }
        public List<PlayoffPictureSeries> playoffPictureSeries { get; set; }
        public List<PlayInBracketSeries> playInBracketSeries { get; set; }
        public List<PlayoffBracketSeries> playoffBracketSeries { get; set; }
        public List<object> istBracketSeries { get; set; }
    }

    public class Meta
    {
        public int version { get; set; }
        public string request { get; set; }
        public DateTime time { get; set; }
    }

    public class PlayoffPictureSeries
    {
        public string matchupType { get; set; }
        public string conference { get; set; }
        public int highSeedId { get; set; }
        public string highSeedCity { get; set; }
        public string highSeedName { get; set; }
        public string highSeedTricode { get; set; }
        public int highSeedRank { get; set; }
        public int highSeedRegSeasonWins { get; set; }
        public int highSeedRegSeasonLosses { get; set; }
        public int lowSeedId { get; set; }
        public string lowSeedCity { get; set; }
        public string lowSeedName { get; set; }
        public string lowSeedTricode { get; set; }
        public int lowSeedRank { get; set; }
        public int lowSeedRegSeasonWins { get; set; }
        public int lowSeedRegSeasonLosses { get; set; }
    }

    public class PlayInBracketSeries
    {
        public string matchupType { get; set; }
        public string conference { get; set; }
        public int highSeedId { get; set; }
        public string highSeedCity { get; set; }
        public string highSeedName { get; set; }
        public string highSeedTricode { get; set; }
        public int highSeedRegSeasonWins { get; set; }
        public int highSeedRegSeasonLosses { get; set; }
        public int highSeedRank { get; set; }
        public int lowSeedId { get; set; }
        public string lowSeedCity { get; set; }
        public string lowSeedName { get; set; }
        public string lowSeedTricode { get; set; }
        public int lowSeedRank { get; set; }
        public int lowSeedRegSeasonWins { get; set; }
        public int lowSeedRegSeasonLosses { get; set; }
        public string seriesId { get; set; }
        public string nextGameId { get; set; }
        public DateTime nextGameDateTimeEt { get; set; }
        public DateTime nextGameDateTimeUTC { get; set; }
        public int nextGameStatus { get; set; }
        public string nextGameStatusText { get; set; }
        public int nextGameBroadcasterId { get; set; }
        public string nextGameBroadcasterDisplay { get; set; }
    }

    public class PlayoffBracketSeries
    {
        public string seriesId { get; set; }
        public int roundNumber { get; set; }
        public int seriesNumber { get; set; }
        public string seriesConference { get; set; }
        public object seriesText { get; set; }
        public int seriesStatus { get; set; }
        public int seriesWinner { get; set; }
        public int highSeedId { get; set; }
        public object highSeedCity { get; set; }
        public object highSeedName { get; set; }
        public object highSeedTricode { get; set; }
        public int highSeedRank { get; set; }
        public int highSeedSeriesWins { get; set; }
        public int highSeedRegSeasonWins { get; set; }
        public int highSeedRegSeasonLosses { get; set; }
        public int lowSeedId { get; set; }
        public object lowSeedCity { get; set; }
        public object lowSeedName { get; set; }
        public object lowSeedTricode { get; set; }
        public int lowSeedRank { get; set; }
        public int lowSeedSeriesWins { get; set; }
        public int lowSeedRegSeasonWins { get; set; }
        public int lowSeedRegSeasonLosses { get; set; }
        public int displayOrderNumber { get; set; }
        public int displayTopTeam { get; set; }
        public int displayBottomTeam { get; set; }
        public object nextGameId { get; set; }
        public object nextGameNumber { get; set; }
        public object nextGameDateTimeEt { get; set; }
        public object nextGameDateTimeUTC { get; set; }
        public int nextGameStatus { get; set; }
        public object nextGameStatusText { get; set; }
        public int nextGameBroadcasterId { get; set; }
        public object nextGameBroadcasterDisplay { get; set; }
        public object lastCompletedGameId { get; set; }
    }

    public class Root
    {
        public Meta meta { get; set; }
        public Bracket bracket { get; set; }
    }
}