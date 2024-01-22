using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nbaJSON
{
    public partial class Bus_Driver : System.Web.UI.Page
    {
        static int game = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void loadB_Click(object sender, EventArgs e)
        {
            earlyBird.earlyBird.FirstLoad();
        }




        protected void gameB_Click(object sender, EventArgs e)
        {
            boxRider.boxRider.GetGames();
        }




        protected void pbpB_Click(object sender, EventArgs e)
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
                pbpRider.pbpRider.GetGameJson(game);
               // boxRider.boxRider.GetGameJson(game);
            }
        }


        protected void teamB_Click(object sender, EventArgs e)
        {

        }

        
    }
}