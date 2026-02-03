using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EduCorePro
{
    public partial class SiteMaster : MasterPage
    {
        string baglanti = ConfigurationManager.ConnectionStrings["EduCoreProBaglanti"].ConnectionString;

        public class SepetOgesi
        {
            public string Id { get; set; }
            public string Baslik { get; set; }
            public string Fiyat { get; set; }
            public string Resim { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SepetiGuncelle();
                if (Session["User"] != null)
                {
                    txtProfilEmail.Text = Session["User"].ToString();
                }
            }
        }

        protected void btnProfilGuncelle_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null) return;

            int userId = Convert.ToInt32(Session["UserId"]);
            string yeniEmail = txtProfilEmail.Text.Trim();
            string yeniSifre = txtProfilSifre.Text.Trim();

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                con.Open();
                string sql;
                SqlCommand cmd;

                if (string.IsNullOrEmpty(yeniSifre))
                {
                    sql = "UPDATE Users SET Email=@mail WHERE Id=@id";
                    cmd = new SqlCommand(sql, con);
                }
                else
                {
                    sql = "UPDATE Users SET Email=@mail, Password=@pass WHERE Id=@id";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@pass", yeniSifre);
                }

                cmd.Parameters.AddWithValue("@mail", yeniEmail);
                cmd.Parameters.AddWithValue("@id", userId);

                try
                {
                    cmd.ExecuteNonQuery();
                    Session["User"] = yeniEmail;
                    lblProfilMesaj.Text = "Bilgiler başarıyla güncellendi!";
                    lblProfilMesaj.CssClass = "text-success fw-bold d-block mt-3 text-center";
                    lblProfilMesaj.Visible = true;
                }
                catch (Exception ex)
                {
                    lblProfilMesaj.Text = "Hata: " + ex.Message;
                    lblProfilMesaj.CssClass = "text-danger fw-bold d-block mt-3 text-center";
                    lblProfilMesaj.Visible = true;
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "openProfile",
                "new bootstrap.Offcanvas(document.getElementById('profilePanel')).show();", true);
        }

        protected void btnHesapSil_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null) return;
            int userId = Convert.ToInt32(Session["UserId"]);

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                con.Open();
                SqlCommand cmdOrders = new SqlCommand("DELETE FROM Orders WHERE UserId = @id", con);
                cmdOrders.Parameters.AddWithValue("@id", userId);
                cmdOrders.ExecuteNonQuery();

                SqlCommand cmdUser = new SqlCommand("DELETE FROM Users WHERE Id = @id", con);
                cmdUser.Parameters.AddWithValue("@id", userId);
                cmdUser.ExecuteNonQuery();
            }

            Session.Abandon();

            Response.Redirect("Courses.aspx");
        }

        public void SepetiGuncelle()
        {
            List<string> sepet = Session["Sepet"] as List<string>;

            if (sepet == null || sepet.Count == 0)
            {
                lblBosSepet.Visible = true;
                rptSepet.Visible = false;
                pnlSepetOzet.Visible = false;
                lblSepetAdet.Visible = false;
                return;
            }

            lblBosSepet.Visible = false;
            rptSepet.Visible = true;
            pnlSepetOzet.Visible = true;
            lblSepetAdet.Visible = true;
            lblSepetAdet.Text = sepet.Count.ToString();

            List<SepetOgesi> urunler = new List<SepetOgesi>();
            decimal toplam = 0;

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                con.Open();
                foreach (string id in sepet)
                {
                    SqlCommand cmd = new SqlCommand("SELECT Id, Title, Price, Image FROM Courses WHERE Id=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        urunler.Add(new SepetOgesi
                        {
                            Id = dr["Id"].ToString(),
                            Baslik = dr["Title"].ToString(),
                            Fiyat = dr["Price"].ToString(),
                            Resim = dr["Image"].ToString()
                        });
                        toplam += Convert.ToDecimal(dr["Price"]);
                    }
                    dr.Close();
                }
            }

            rptSepet.DataSource = urunler;
            rptSepet.DataBind();
            lblToplamTutar.Text = toplam.ToString("N2");
        }

        protected void rptSepet_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Sil")
            {
                string idSil = e.CommandArgument.ToString();
                List<string> sepet = Session["Sepet"] as List<string>;

                if (sepet != null)
                {
                    sepet.Remove(idSil);
                    Session["Sepet"] = sepet;
                    SepetiGuncelle();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "openCart",
                        "new bootstrap.Offcanvas(document.getElementById('sepetPanel')).show();", true);
                }
            }
        }

        protected void btnCikis_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Courses.aspx");
        }
    }
}