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

namespace playoffRider
{
    public partial class playoffRider
    {
        static WebClient client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
        static SqlConnection SQL = new SqlConnection("Server=localhost;Database=myNBA;User Id=test;Password=test123;");
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
            string jsonLink = "https://stats.nba.com/stats/playoffbracket?LeagueID=00&SeasonYear=2022&State=2";
            try
            {
                string json = client.DownloadString(jsonLink);
                Root JSON = JsonConvert.DeserializeObject<Root>(json);
                CheckBracket(JSON);
            }
            catch (WebException e)
            {

            }
        }
        public static void CheckPicture(Root JSON)
        {
            //If there arent any results
            PostPicture(JSON);

        }
        public static void CheckBracket(Root JSON)
        {
            //If there arent any results
            PostBracket(JSON);
        }

        public static void UpdatePicture(Root JSON)
        {

        }
        public static void UpdateBracket(Root JSON)
        {

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
        public static void PostBracket(Root JSON)
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
                        InsertData.Parameters.AddWithValue("@series_id",                    JSON.bracket.playoffBracketSeries[i].seriesId);
                        InsertData.Parameters.AddWithValue("@conference",                   JSON.bracket.playoffBracketSeries[i].seriesConference);
                        InsertData.Parameters.AddWithValue("@roundNumber",                  JSON.bracket.playoffBracketSeries[i].roundNumber);
                        InsertData.Parameters.AddWithValue("@description",                  JSON.bracket.playoffBracketSeries[i].seriesText);
                        InsertData.Parameters.AddWithValue("@status",                       JSON.bracket.playoffBracketSeries[i].seriesStatus);
                        InsertData.Parameters.AddWithValue("@seriesWinner",                 JSON.bracket.playoffBracketSeries[i].seriesWinner);
                        InsertData.Parameters.AddWithValue("@highSeed_id",                  JSON.bracket.playoffBracketSeries[i].highSeedId);
                        InsertData.Parameters.AddWithValue("@highSeedSeriesWins",           JSON.bracket.playoffBracketSeries[i].highSeedSeriesWins);
                        InsertData.Parameters.AddWithValue("@lowSeedId",                    JSON.bracket.playoffBracketSeries[i].lowSeedId);
                        InsertData.Parameters.AddWithValue("@lowSeedSeriesWins",            JSON.bracket.playoffBracketSeries[i].lowSeedSeriesWins);
                        InsertData.Parameters.AddWithValue("@nextGame_id",                  JSON.bracket.playoffBracketSeries[i].nextGameId);
                        InsertData.Parameters.AddWithValue("@nextGameNumber",               JSON.bracket.playoffBracketSeries[i].nextGameNumber);
                        InsertData.Parameters.AddWithValue("@nextSeries_id",                0); //Need to fill placeholder
                        SQL.Open();
                        InsertData.ExecuteScalar();
                        SQL.Close();
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