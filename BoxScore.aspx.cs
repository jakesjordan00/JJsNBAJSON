using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static nbaJSON.About;
using System.Net;
using Newtonsoft.Json;

namespace nbaJSON
{
    public partial class Contact : Page
    {
        static int game = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get count of games
            SqlConnection sqlConnect = new SqlConnection("Server=localhost;Database=myNBA;User Id=test;Password=test123;");
            using (sqlConnect)
            {
                using (SqlCommand querySearch = new SqlCommand("jsonGames"))
                {
                    querySearch.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                    {
                        querySearch.Connection = sqlConnect;
                        sDataSearch.SelectCommand = querySearch;
                        using (DataTable dataT = new DataTable())
                        {
                            sDataSearch.Fill(dataT);
                            GamesGrid.DataSource = dataT;
                            GamesGrid.DataBind();
                        }
                    }
                }
            }
            //For each game_id, get the JSON file for the game
            foreach (GridViewRow row in GamesGrid.Rows)
            {
                game = Int32.Parse(row.Cells[0].Text);
                GetGameJson(game);
            }
        }

        static void GetGameJson(int game)
        {
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string jsonLink = "https://cdn.nba.com/static/json/liveData/boxscore/boxscore_00" + game.ToString() + ".json";
            try
            {
                string json = client.DownloadString(jsonLink);
                //DupeCheck(json);
            }
            catch (WebException e)
            {

            }
        }

        //static void DupeCheck(string json)
        //{

        //    Root JSON = JsonConvert.DeserializeObject<Root>(json);
        //    int actions = JSON.game.actions.Count();
        //    int game_id = Int32.Parse(JSON.game.gameId);
        //    SqlConnection DupeCheckConnect = new SqlConnection("Server=localhost;Database=myNBA;User Id=test;Password=test123;");
        //    {
        //        using (DupeCheckConnect)
        //        {
        //            using (SqlCommand DupeSearch = new SqlCommand("DupeCheck"))
        //            {
        //                DupeSearch.CommandType = CommandType.StoredProcedure;
        //                DupeSearch.Parameters.AddWithValue("@game_id", game_id);
        //                using (SqlDataAdapter sDupeSearch = new SqlDataAdapter())
        //                {
        //                    DupeSearch.Connection = DupeCheckConnect;
        //                    sDupeSearch.SelectCommand = DupeSearch;
        //                    DupeCheckConnect.Open();
        //                    SqlDataReader reader = DupeSearch.ExecuteReader();
        //                    if (reader.Read() || !reader.Read())
        //                    {
        //                        int oldActions = 0;
        //                        if (reader.HasRows)
        //                        {
        //                            oldActions = reader.GetInt32(1);
        //                        }
        //                        if (actions != oldActions)
        //                        {
        //                            InsertGame(JSON, game_id, oldActions, actions);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}





        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        


    }
}