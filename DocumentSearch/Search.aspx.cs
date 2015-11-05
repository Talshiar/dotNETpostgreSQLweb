using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Text;
using System.Text.RegularExpressions;

namespace DocumentSearch
{
    public partial class Search : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadioListAndOr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=192.168.56.12;Port=5432;User Id=postgres;Password=reverse;Database=FilmDb;");
            conn.Open();
            LiteralResult.Text = "";
            TextBoxSQL.Text = "";
            if (TextBoxSearch.Text == "")
            {
                Response.Write("<script>alert('Please write something.');window.location='/Search.aspx';</script>");
                conn.Close();
                return;
            }
            char Operator = '|';
            string OpName = "OR";
            if (RadioListAndOr.SelectedValue == "AND")
            {
                OpName = "AND";
                Operator = '&';
            }

            var regex = new Regex(@"""(?<word>[^""]+)""|(?<word>\w+)");
            var keywords = regex.Matches(TextBoxSearch.Text).Cast<Match>().Select(m => m.Groups["word"].Value).ToArray();
            var keywordsOp = keywords.Select(s => s.Replace(" ", " & ")).ToList();
            StringBuilder SQL = new StringBuilder();
            StringBuilder keywordSQL = new StringBuilder();
            bool first = true;

            //zajednicki dio upita
            SQL.Append(@"SELECT id, ts_headline(naziv, to_tsquery('");
            foreach (string keyw in keywordsOp)
            {

                if (first)
                {
                    keywordSQL.Append("(" + keyw + ") ");
                    first = false;
                }
                else keywordSQL.Append(Operator + " (" + keyw + ") ");
            }

            SQL.Append(keywordSQL);
            SQL.Append(@"')), naziv, ts_rank(teksttsv, to_tsquery('");
            SQL.Append(keywordSQL);

            //sredjujemo keywordSQL za tablicu log i upisujemo u log zajedno sa datetime
            string query = null;
            string SQLquery = null;
            query = keywordSQL.ToString().Replace("(", "").Replace(")", "");
            var time = DateTime.Now;
            var formmatedTime = time.ToString("yyyy-MM-dd hh:mm:ss");
            SQLquery += @"INSERT INTO log VALUES ('" + query + @"', '" + formmatedTime + @"')";
            NpgsqlCommand command = new NpgsqlCommand(SQLquery, conn);
            int rows = command.ExecuteNonQuery();

            //Exact string matching
            if (RadioListType.SelectedValue == "0")
            {
                SQL.Append(@"')) rank FROM filmovi WHERE naziv LIKE ");
                first = true;
                foreach (string keyw in keywords)
                {
                    if (first) 
                    {
                        SQL.Append(@"'%" + keyw + @"%'");
                        first = false;
                    }
                    else
                        SQL.Append(" " + OpName + @" naziv LIKE '%" + keyw + "%'");
                }
                SQL.Append(" ORDER BY rank DESC");
            }

            //Dictionary matching
            else if (RadioListType.SelectedValue == "1")
            {
                SQL.Append(@"')) rank FROM filmovi WHERE teksttsv @@ to_tsquery('english',");
                first = true;
                foreach (string keyw in keywordsOp)
                {
                    if (first)
                    {
                        SQL.Append(@"'" + keyw + @"')");
                        first = false;
                    }
                    else
                        SQL.Append(" " + OpName + @" teksttsv @@ to_tsquery('english', '" + keyw + @"')");
                }
                SQL.Append(" ORDER BY rank DESC");
            }

            //Fuzzy string matching
            else if (RadioListType.SelectedValue == "2")
            {
                SQL.Append(@"')) rank FROM filmovi WHERE naziv % ");
                first = true;
                foreach (string keyw in keywords)
                {
                    if (first)
                    {
                        SQL.Append(@"'" + keyw + @"'");
                        first = false;
                    }
                    else
                        SQL.Append(" " + OpName + @" naziv % '" + keyw + @"'");
                }
                SQL.Append(" ORDER BY rank DESC");
            }

            TextBoxSQL.Text = SQL.ToString();
            command = new NpgsqlCommand(TextBoxSQL.Text, conn);
            NpgsqlDataReader row = command.ExecuteReader();
            int cnt = 0;


            //ispis
            while (row.Read())
            {
                cnt++;
                LiteralResult.Text += row[1] + "  [" + row[3] + "]<br/>";
            }
            LiteralResult.Text += "<br/>Number of documents read: " + cnt;
            conn.Close();
        }
    }
}