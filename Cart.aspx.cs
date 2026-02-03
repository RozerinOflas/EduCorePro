using System;
using System.Collections.Generic; 
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls; 

namespace EduCorePro
{
    public partial class Cart : System.Web.UI.Page
    {
        string baglanti = ConfigurationManager.ConnectionStrings["EduCoreProBaglanti"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SepetiGetir();
            }
        }

        void SepetiGetir()
        {
            if (Session["Sepet"] == null || ((List<string>)Session["Sepet"]).Count == 0)
            {
                lblBosSepet.Visible = true;     
                rptSepet.Visible = false;      
                pnlSepetOzet.Visible = false;   
                return;
            }

            List<string> sepetListesi = (List<string>)Session["Sepet"];
            string idListesi = string.Join(",", sepetListesi);

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM Courses WHERE Id IN ({idListesi})", con);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                rptSepet.DataSource = dt;
                rptSepet.DataBind();

                decimal toplam = 0;
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    toplam += Convert.ToDecimal(row["Price"]);
                }
                lblToplamTutar.Text = toplam.ToString("N2");
            }

            lblBosSepet.Visible = false;
            rptSepet.Visible = true;
            pnlSepetOzet.Visible = true;
        }

        protected void rptSepet_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Sil")
            {
                string id = e.CommandArgument.ToString();
                List<string> sepet = (List<string>)Session["Sepet"];

                sepet.Remove(id);
                Session["Sepet"] = sepet;

                SiteMaster master = this.Master as SiteMaster;
                if (master != null) master.SepetiGuncelle();

                SepetiGetir();
            }
        }

        protected void btnSepetiOnayla_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["Sepet"] == null) return;

            int userId = Convert.ToInt32(Session["UserId"]);
            List<string> sepet = (List<string>)Session["Sepet"];

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                con.Open();
                foreach (string kursId in sepet)
                {
                    string sql = @"INSERT INTO Orders (UserId, CourseId, Price, OrderDate) 
                                   SELECT @uid, Id, Price, GETDATE() FROM Courses WHERE Id=@cid";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@cid", kursId);

                    cmd.ExecuteNonQuery();
                }
            }

            Session["Sepet"] = null;

            SiteMaster master = this.Master as SiteMaster;
            if (master != null) master.SepetiGuncelle();

            pnlSepetOzet.Visible = false;
            rptSepet.Visible = false;

            lblBosSepet.Text = "<i class='fas fa-check-circle text-success fa-3x mb-3'></i><br>Siparişiniz başarıyla alındı!<br>Keyifli dersler dileriz.";
            lblBosSepet.CssClass = "text-center fs-4 fw-bold text-success mt-5 p-5 border rounded bg-light";
            lblBosSepet.Visible = true;
        }
    }
}