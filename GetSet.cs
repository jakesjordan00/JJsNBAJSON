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

namespace GetSet
{
    public partial class GetSet
    {
        public static void BusTicket(string? ticket)
        {
            if(ticket == "First Time")
            {
                test(ticket3: "xd");
            }
            else if(ticket == "Games")
            {

            }
            else if (ticket == "Box Score")
            {

            }
            else if (ticket == "Play by Play")
            {

            }
            else if (ticket == "Playoffs")
            {

            }
        }

        public  static void test(string? ticket, string? ticket2, string? ticket1, string? ticket3)
        {

        }


        ///<<< JJ >>>                 PlayByPlay Classes         PlayByPlay Classes
        ///<<<<<<<<<<<<<<<<<<        JJ's NBA JSON Parser        >>>>>>>>>>>>>>>>>>
        ///<<< JJ >>>                 PlayByPlay Classes         PlayByPlay Classes
        public class PBPAction
        {
            public int actionNumber { get; set; }
            public string clock { get; set; }
            public DateTime timeActual { get; set; }
            public int period { get; set; }
            public string periodType { get; set; }
            public int? teamId { get; set; }
            public string teamTricode { get; set; }
            public string actionType { get; set; }
            public string subType { get; set; }
            public string descriptor { get; set; }
            public List<string> qualifiers { get; set; }
            public int personId { get; set; }
            public double? x { get; set; }
            public double? y { get; set; }
            public int possession { get; set; }
            public int scoreHome { get; set; }
            public int scoreAway { get; set; }
            public DateTime edited { get; set; }
            public int orderNumber { get; set; }
            public bool isTargetScoreLastPeriod { get; set; }
            public int? xLegacy { get; set; }
            public int? yLegacy { get; set; }
            public int isFieldGoal { get; set; }
            public string side { get; set; }
            public string description { get; set; }
            public List<int> personIdsFilter { get; set; }
            public string jumpBallRecoveredName { get; set; }
            public int? jumpBallRecoverdPersonId { get; set; }
            public string playerName { get; set; }
            public string playerNameI { get; set; }
            public string jumpBallWonPlayerName { get; set; }
            public int? jumpBallWonPersonId { get; set; }
            public string jumpBallLostPlayerName { get; set; }
            public int? jumpBallLostPersonId { get; set; }
            public string area { get; set; }
            public string areaDetail { get; set; }
            public double? shotDistance { get; set; }
            public string shotResult { get; set; }
            public string blockPlayerName { get; set; }
            public int? blockPersonId { get; set; }
            public int? stealPersonId { get; set; }
            public int? shotActionNumber { get; set; }
            public int? reboundTotal { get; set; }
            public int? reboundDefensiveTotal { get; set; }
            public int? reboundOffensiveTotal { get; set; }
            public int? pointsTotal { get; set; }
            public string assistPlayerNameInitial { get; set; }
            public int? assistPersonId { get; set; }
            public int? assistTotal { get; set; }
            public int? officialId { get; set; }
            public int? foulPersonalTotal { get; set; }
            public int? foulTechnicalTotal { get; set; }
            public string foulDrawnPlayerName { get; set; }
            public int? foulDrawnPersonId { get; set; }
        }
        public class PBPGame
        {
            public string gameId { get; set; }
            public List<PBPAction> actions { get; set; }
        }
        public class PBPMeta
        {
            public int version { get; set; }
            public int code { get; set; }
            public string request { get; set; }
            public string time { get; set; }
        }
        public class PBPRoot
        {
            public PBPMeta meta { get; set; }
            public PBPGame game { get; set; }
        }

        ///<<< JJ >>>                  BoxScore Classes            BoxScore Classes
        ///<<<<<<<<<<<<<<<<<<        JJ's NBA JSON Parser        >>>>>>>>>>>>>>>>>>
        ///<<< JJ >>>                  BoxScore Classes            BoxScore Classes
        public class BoxArena
        {
            public int arenaId { get; set; }
            public string arenaName { get; set; }
            public string arenaCity { get; set; }
            public string arenaState { get; set; }
            public string arenaCountry { get; set; }
            public string arenaTimezone { get; set; }
        }
        public class BoxAwayTeam
        {
            public int teamId { get; set; }
            public string teamName { get; set; }
            public string teamCity { get; set; }
            public string teamTricode { get; set; }
            public int score { get; set; }
            public string inBonus { get; set; }
            public int timeoutsRemaining { get; set; }
            public List<BoxPeriod> periods { get; set; }
            public List<BoxPlayer> players { get; set; }
            public BoxStatistics statistics { get; set; }
        }

        public class BoxGame
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
            public BoxArena arena { get; set; }
            public List<BoxOfficial> officials { get; set; }
            public BoxHomeTeam homeTeam { get; set; }
            public BoxAwayTeam awayTeam { get; set; }
        }

        public class BoxHomeTeam
        {
            public int teamId { get; set; }
            public string teamName { get; set; }
            public string teamCity { get; set; }
            public string teamTricode { get; set; }
            public int score { get; set; }
            public string inBonus { get; set; }
            public int timeoutsRemaining { get; set; }
            public List<BoxPeriod> periods { get; set; }
            public List<BoxPlayer> players { get; set; }
            public BoxStatistics statistics { get; set; }
        }
        public class BoxMeta
        {
            public int version { get; set; }
            public int code { get; set; }
            public string request { get; set; }
            public string time { get; set; }
        }
        public class BoxOfficial
        {
            public int personId { get; set; }
            public string name { get; set; }
            public string nameI { get; set; }
            public string firstName { get; set; }
            public string familyName { get; set; }
            public string jerseyNum { get; set; }
            public string assignment { get; set; }
        }
        public class BoxPeriod
        {
            public int period { get; set; }
            public string periodType { get; set; }
            public int score { get; set; }
        }
        public class BoxPlayer
        {
            public string status { get; set; }
            public int order { get; set; }
            public int personId { get; set; }
            public string jerseyNum { get; set; }
            public string position { get; set; }
            public string starter { get; set; }
            public string oncourt { get; set; }
            public string played { get; set; }
            public BoxStatistics Statistics { get; set; }
            public string name { get; set; }
            public string nameI { get; set; }
            public string firstName { get; set; }
            public string familyName { get; set; }
            public string notPlayingReason { get; set; }
            public string notPlayingDescription { get; set; }
        }
        public class BoxRoot
        {
            public BoxMeta meta { get; set; }
            public BoxGame game { get; set; }
        }
        public class BoxStatistics
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

        ///<<< JJ >>>                  Playoff Classes              Playoff Classes
        ///<<<<<<<<<<<<<<<<<<        JJ's NBA JSON Parser        >>>>>>>>>>>>>>>>>>
        ///<<< JJ >>>                  Playoff Classes              Playoff Classes
        public class PostSznBracket
        {
            public string leagueId { get; set; }
            public string seasonYear { get; set; }
            public string bracketType { get; set; }
            public List<PostSznPlayoffPictureSeries> playoffPictureSeries { get; set; }
            public List<PostSznPlayInBracketSeries> playInBracketSeries { get; set; }
            public List<PostSznPlayoffBracketSeries> playoffBracketSeries { get; set; }
            public List<object> istBracketSeries { get; set; }
        }

        public class PostSznMeta
        {
            public int version { get; set; }
            public string request { get; set; }
            public DateTime time { get; set; }
        }

        public class PostSznPlayoffPictureSeries
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

        public class PostSznPlayInBracketSeries
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
        public class PostSznPlayoffBracketSeries
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

        public class PostSznRoot
        {
            public PostSznMeta meta { get; set; }
            public PostSznBracket bracket { get; set; }
        }

    }
}