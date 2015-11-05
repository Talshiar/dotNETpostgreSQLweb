using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Text;
using System.Data;

namespace DocumentSearch
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLog_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=192.168.56.12;Port=5432;User Id=postgres;Password=reverse;Database=FilmDb;");
            conn.Open();
            if (TextBoxDateFrom.Text == "" || TextBoxDateTo.Text == "")
            {
                Response.Write("<script>alert('Please write a starting and ending date.');window.location='/Log.aspx';</script>");
                conn.Close();
                return;
            }

            StringBuilder SQL = new StringBuilder();
            if (RadioListGranulation.SelectedValue == "Hours")
            {
                int danOd = int.Parse(TextBoxDateFrom.Text.Split('-')[2]);
                int danDo = int.Parse(TextBoxDateTo.Text.Split('-')[2]);

                SQL.Append(@"SELECT * FROM crosstab ('SELECT CAST(query AS character(120)) AS upit, ");
                SQL.Append(@"CAST(EXTRACT(HOUR FROM date) AS int) AS sat, CAST(COUNT(*) AS int) as cnt ");
                SQL.Append(@"FROM log ");
                SQL.Append(@"WHERE CAST(EXTRACT(DAY FROM date) AS int) = ''" + danOd + @"'' ");
                for (int i = danOd + 1; i <= danDo; i++)
                {
                    SQL.Append(@"OR CAST(EXTRACT(DAY FROM date) AS int) = ''" + i + @"'' ");
                }

                SQL.Append(@" GROUP BY upit, sat ORDER BY upit, sat', 'SELECT hour from temphours ORDER BY hour') ");
                SQL.Append(@"AS pivotTable (upit character(120), h00 INT, h01 INT, h02 INT, h03 INT, h04 INT, h05 INT, h06 INT, h07 INT, h08 INT, h09 INT, h10 INT, ");
                SQL.Append(@"h11 INT, h12 INT, h13 INT, h14 INT, h15 INT, h16 INT, h17 INT, h18 INT, h19 INT, h20 INT, h21 INT, h22 INT, h23 INT) ORDER BY upit");
                conn.Close();
            } else if (RadioListGranulation.SelectedValue == "Days")
            {
                int danOd = int.Parse(TextBoxDateFrom.Text.Split('-')[2]);
                int danDo = int.Parse(TextBoxDateTo.Text.Split('-')[2]);

                SQL.Append(@"SELECT * FROM crosstab ('SELECT CAST(query AS character(120)) AS upit, ");
                SQL.Append(@"CAST(EXTRACT(DAY FROM date) AS int) AS dan, CAST(COUNT(*) AS int) AS cnt FROM log ");
                SQL.Append(@"WHERE CAST(EXTRACT(DAY FROM date) AS int) = ''" + danOd + @"'' ");
                for (int i = danOd + 1; i <= danDo; i++ )
                {
                    SQL.Append(@"OR CAST(EXTRACT(DAY FROM date) AS int) = ''" + i + @"'' ");
                }

                SQL.Append(@"GROUP BY upit, dan ORDER BY upit, dan', 'SELECT rbrdan from tempdays WHERE rbrdan = ''" + danOd + @"'' ");
                for (int i = danOd + 1; i <= danDo; i++)
                {
                    SQL.Append(@"OR rbrdan = ''" + i + @"'' ");
                }

                SQL.Append(@"ORDER BY rbrdan') AS pivotTable (upit character(120), d0" + danOd + @"m11 INT");

                for (int i = danOd + 1; i <= danDo; i++)
                {
                    SQL.Append(@", d0" + i + @"m11 INT");
                }
                SQL.Append(@") ORDER BY upit");
            }


            var table = new DataTable();
            DataSet DS = new DataSet("DS1");
            NpgsqlDataAdapter DA = new NpgsqlDataAdapter();
            DA.SelectCommand = new NpgsqlCommand(SQL.ToString(), conn);
            DA.Fill(DS, "DS1");
            GridViewLog.DataSource = DS;
            GridViewLog.DataBind();
            conn.Close();


        }
    }
}