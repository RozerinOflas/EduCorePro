using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EduCorePro
{
    public partial class Kurslar : System.Web.UI.Page
    {
        string baglanti = ConfigurationManager.ConnectionStrings["EduCoreProBaglanti"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KurslariGetir();
            }
        }
        private void KurslariGetir()
        {
            string kategori = Request.QueryString["Category"];
            string arama = Request.QueryString["q"]; 

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                string sql = @"SELECT Courses.*, Instructors.Name AS InstructorName 
                               FROM Courses 
                               INNER JOIN Instructors ON Courses.InstructorId = Instructors.InstructorId 
                               WHERE 1=1";

                if (!string.IsNullOrEmpty(kategori))
                {
                    sql += " AND Courses.Category = @cat";
                }

                if (!string.IsNullOrEmpty(arama))
                {
                    sql += " AND Courses.Title LIKE @ara";
                }

                SqlCommand cmd = new SqlCommand(sql, con);

                if (!string.IsNullOrEmpty(kategori))
                {
                    cmd.Parameters.AddWithValue("@cat", kategori);
                }
                if (!string.IsNullOrEmpty(arama))
                {
                    cmd.Parameters.AddWithValue("@ara", "%" + arama + "%");
                }

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rptKurslar.DataSource = dt;
                    rptKurslar.DataBind();
                    pnlNoData.Visible = false;
                }
                else
                {
                    rptKurslar.DataSource = null;
                    rptKurslar.DataBind();
                    pnlNoData.Visible = true;
                }
            }
        }
    }
}