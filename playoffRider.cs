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