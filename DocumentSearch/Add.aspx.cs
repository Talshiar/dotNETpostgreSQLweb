using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
namespace DocumentSearch
{
    public partial class Add : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection("Server=192.168.56.12;Port=5432;User Id=postgres;Password=reverse;Database=FilmDb;");
                conn.Open();
                if (TextBoxAdd.Text == "")
                {
                    Response.Write("<script>alert('The title of the movie cannot be empty.');window.location='/Add.aspx';</script>");
                    conn.Close();
                    return;
                }
                string SQL = "INSERT INTO filmovi VALUES (\'" + TextBoxAdd.Text + "\')";
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);
                int rowsaffected = command.ExecuteNonQuery();
                conn.Close();
                Response.Write("<script>alert('You have successfully added the movie!');window.location='/Add.aspx';</script>");
            }
            catch (Exception)
            {
                Response.Write("<script>alert('There was an error.');window.location='/Add.aspx';</script>");
            }
        }
    }
}