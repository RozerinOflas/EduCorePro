using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace EduCorePro
{
    public partial class CourseDetails : System.Web.UI.Page
    {
        string baglanti = ConfigurationManager.ConnectionStrings["EduCoreProBaglanti"].ConnectionString;
        public class Ders
        {
            public string Baslik { get; set; }
            public string Icerik1 { get; set; }
            public string Icerik2 { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];

                if (id != null)
                {
                    VerileriGetir(id);
                }
                if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
                {
                    btnSatinAl.Visible = false;     
                    btnSepeteEkle.Visible = false;   
                    lblAdminUyari.Visible = true;    
                }
            }
        }
        private string YoutubeUrlDuzenle(string url)
        {
            if (string.IsNullOrEmpty(url)) return "";
            if (url.Contains("/embed/")) return url;

            string videoId = "";
            if (url.Contains("v="))
            {
                var p = url.Split(new string[] { "v=" }, StringSplitOptions.None);
                if (p.Length > 1) videoId = p[1].Split('&')[0];
            }
            else if (url.Contains("youtu.be/"))
            {
                var p = url.Split(new string[] { "youtu.be/" }, StringSplitOptions.None);
                if (p.Length > 1) videoId = p[1].Split('?')[0];
            }

            if (!string.IsNullOrEmpty(videoId)) return "https://www.youtube.com/embed/" + videoId;
            return url;
        }
        void VerileriGetir(string id)
        {
            using (SqlConnection con = new SqlConnection(baglanti))
            {
                con.Open();
                string sql = @"SELECT Courses.*, Instructors.Name AS InstructorName 
                               FROM Courses 
                               INNER JOIN Instructors ON Courses.InstructorId = Instructors.InstructorId 
                               WHERE Courses.Id=@id";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string kursAdi = dr["Title"].ToString();
                    lblTitle.Text = kursAdi;
                    lblPrice.Text = dr["Price"].ToString();
                    lblInstructor.Text = dr["InstructorName"].ToString();

                    if (dr["Category"] != DBNull.Value) lblCategory.Text = dr["Category"].ToString();
                    else lblCategory.Text = "Genel";

                    lblDescription.Text = "Bu kurs size kariyerinizde yükselmeniz için gerekli tüm teknik ve teorik bilgileri, uygulamalı projelerle sunmaktadır.";

                    string hamLink = "https://www.youtube.com/watch?v=zOJCCY7a_yE";
                    if (dr["VideoUrl"] != DBNull.Value && !string.IsNullOrEmpty(dr["VideoUrl"].ToString()))
                    {
                        hamLink = dr["VideoUrl"].ToString();
                    }

                    videoPlayer.Attributes["src"] = YoutubeUrlDuzenle(hamLink);
                    MufredatOlustur(kursAdi);
                }
            }
        }

        protected void btnSatinAl_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                return;
            }

            try
            {
                string kursId = Request.QueryString["id"];
                int userId = Convert.ToInt32(Session["UserId"]);

                string hamFiyat = lblPrice.Text.Replace("TL", "").Replace("₺", "").Trim();
                decimal fiyat = 0;

                if (!decimal.TryParse(hamFiyat, out fiyat))
                {
                    fiyat = 0;
                }

                using (SqlConnection con = new SqlConnection(baglanti))
                {
                    con.Open();
                    string sql = "INSERT INTO Orders (UserId, CourseId, Price, OrderDate) VALUES (@uId, @cId, @price, GETDATE())";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@uId", userId);
                    cmd.Parameters.AddWithValue("@cId", kursId);
                    cmd.Parameters.AddWithValue("@price", fiyat);

                    cmd.ExecuteNonQuery();
                }

                lblMesaj.Text = "Tebrikler! Kurs başarıyla satın alındı.";
                lblMesaj.Visible = true;
                lblMesaj.CssClass = "alert alert-success mt-3 d-block";

                btnSatinAl.Visible = false;
                btnSepeteEkle.Visible = false;
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Hata oluştu: " + ex.Message;
                lblMesaj.Visible = true;
                lblMesaj.CssClass = "alert alert-danger mt-3 d-block";
            }
        }

        protected void btnSepeteEkle_Click(object sender, EventArgs e)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                return;
            }

            string id = Request.QueryString["id"];
            if (string.IsNullOrEmpty(id)) return;

            if (Session["Sepet"] == null) Session["Sepet"] = new List<string>();
            ((List<string>)Session["Sepet"]).Add(id);

            SiteMaster masterPage = this.Master as SiteMaster;
            if (masterPage != null) masterPage.SepetiGuncelle();

            ClientScript.RegisterStartupScript(this.GetType(), "acPanel",
                "<script>try{var myOffcanvas = new bootstrap.Offcanvas(document.getElementById('sepetPanel')); myOffcanvas.show();}catch(e){}</script>");
        }

        void MufredatOlustur(string baslik)
        {
            List<Ders> dersler = new List<Ders>();
            dersler.Add(new Ders { Baslik = "Giriş ve Temeller", Icerik1 = "Tanıtım Videosu", Icerik2 = "Giriş Rehberi" });
            dersler.Add(new Ders { Baslik = "Orta Seviye", Icerik1 = "Uygulamalı Ders", Icerik2 = "Pratik Testi" });
            dersler.Add(new Ders { Baslik = "İleri Seviye", Icerik1 = "Proje / Sınav", Icerik2 = "Bitirme Sertifikası" });
            rptDersler.DataSource = dersler;
            rptDersler.DataBind();
        }
    }
}