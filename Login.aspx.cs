using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace EduCorePro
{
    public partial class Login : System.Web.UI.Page
    {
        string baglanti = ConfigurationManager.ConnectionStrings["EduCoreProBaglanti"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnGirisYap_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(baglanti))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT * FROM Users WHERE Email=@email AND Password=@pass";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@email", txtGirisEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@pass", txtGirisSifre.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        Session["UserId"] = dr["Id"].ToString();
                        Session["User"] = dr["Email"].ToString();
                        Session["Role"] = dr["Role"].ToString();

                        if (Session["Role"].ToString() == "Admin")
                        {
                            Response.Redirect("AdminPanel.aspx");
                        }
                        else
                        {
                            Response.Redirect("Courses.aspx");
                        }
                    }
                    else
                    {
                        lblMesaj.Text = "Hatalı e-posta veya şifre!";
                        lblMesaj.CssClass = "alert alert-danger d-block text-center mb-3";
                        lblMesaj.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "Hata: " + ex.Message;
                    lblMesaj.Visible = true;
                }
            }
        }

        protected void btnKayitOl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKayitEmail.Text) || string.IsNullOrEmpty(txtKayitSifre.Text))
            {
                lblMesaj.Text = "Lütfen tüm alanları doldurun.";
                lblMesaj.Visible = true;
                return;
            }

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                try
                {
                    con.Open();

                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email=@mail", con);
                    checkCmd.Parameters.AddWithValue("@mail", txtKayitEmail.Text.Trim());
                    int varMi = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (varMi > 0)
                    {
                        lblMesaj.Text = "Bu e-posta adresi zaten kayıtlı.";
                        lblMesaj.CssClass = "alert alert-warning d-block text-center mb-3";
                        lblMesaj.Visible = true;
                        return;
                    }
                    string sql = "INSERT INTO Users (Email, Password, Role) VALUES (@email, @pass, 'Student')";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@email", txtKayitEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@pass", txtKayitSifre.Text.Trim());
                    cmd.ExecuteNonQuery();

                    lblMesaj.Text = "Kayıt başarılı! Şimdi 'Giriş Yap' sekmesinden giriş yapabilirsiniz.";
                    lblMesaj.CssClass = "alert alert-success d-block text-center mb-3";
                    lblMesaj.Visible = true;

                    txtKayitEmail.Text = "";
                    txtKayitSifre.Text = "";
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "Kayıt hatası: " + ex.Message;
                    lblMesaj.CssClass = "alert alert-danger d-block text-center mb-3";
                    lblMesaj.Visible = true;
                }
            }
        }
    }
}