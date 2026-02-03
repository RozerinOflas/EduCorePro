using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EduCorePro
{
    public partial class AdminPanel : System.Web.UI.Page
    {
        string baglanti = ConfigurationManager.ConnectionStrings["EduCoreProBaglanti"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("Courses.aspx");
            }

            if (!IsPostBack)
            {
                KurslariListele();
                KullanicilariListele();
                EgitmenleriGetir();
            }
        }
        private string EmbedLinkeCevir(string normalLink)
        {
            if (string.IsNullOrEmpty(normalLink)) return "";
            if (normalLink.Contains("embed")) return normalLink;

            if (normalLink.Contains("watch?v="))
            {
                var parts = normalLink.Split(new string[] { "v=" }, StringSplitOptions.None);
                if (parts.Length > 1) return "https://www.youtube.com/embed/" + parts[1].Split('&')[0];
            }
            else if (normalLink.Contains("youtu.be/"))
            {
                var parts = normalLink.Split(new string[] { "youtu.be/" }, StringSplitOptions.None);
                if (parts.Length > 1) return "https://www.youtube.com/embed/" + parts[1].Split('?')[0];
            }
            return normalLink;
        }

        void KurslariListele()
        {
            using (SqlConnection con = new SqlConnection(baglanti))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Courses", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridCourses.DataSource = dt;
                gridCourses.DataBind();
            }
        }

        void KullanicilariListele()
        {
            using (SqlConnection con = new SqlConnection(baglanti))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users", con); 
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridUsers.DataSource = dt;
                gridUsers.DataBind();
            }
        }

        void EgitmenleriGetir()
        {
            using (SqlConnection con = new SqlConnection(baglanti))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT InstructorId, Name FROM Instructors", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlEgitmen.DataSource = dt;
                ddlEgitmen.DataTextField = "Name";
                ddlEgitmen.DataValueField = "InstructorId";
                ddlEgitmen.DataBind();
                ddlEgitmen.Items.Insert(0, new ListItem("Eğitmen Seçiniz", "0"));
            }
        }
        protected void btnKursEkle_Click(object sender, EventArgs e)
        {
            if (ddlEgitmen.SelectedValue == "0") return;
            string videoUrl = EmbedLinkeCevir(txtVideoUrl.Text);

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                con.Open();
                try
                {
                    string sql = "INSERT INTO Courses (Title, Price, Image, Category, InstructorId, VideoUrl) VALUES (@baslik, @fiyat, @resim, @kat, @insId, @vid)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@baslik", txtBaslik.Text);
                    cmd.Parameters.AddWithValue("@fiyat", Convert.ToDecimal(txtFiyat.Text));
                    cmd.Parameters.AddWithValue("@resim", txtResim.Text);
                    cmd.Parameters.AddWithValue("@kat", ddlKategori.SelectedValue);
                    cmd.Parameters.AddWithValue("@insId", ddlEgitmen.SelectedValue);
                    cmd.Parameters.AddWithValue("@vid", videoUrl);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    string sql2 = "INSERT INTO Courses (Title, Price, Image, Category, InstructorId) VALUES (@baslik, @fiyat, @resim, @kat, @insId)";
                    SqlCommand cmd2 = new SqlCommand(sql2, con);
                    cmd2.Parameters.AddWithValue("@baslik", txtBaslik.Text);
                    cmd2.Parameters.AddWithValue("@fiyat", Convert.ToDecimal(txtFiyat.Text));
                    cmd2.Parameters.AddWithValue("@resim", txtResim.Text);
                    cmd2.Parameters.AddWithValue("@kat", ddlKategori.SelectedValue);
                    cmd2.Parameters.AddWithValue("@insId", ddlEgitmen.SelectedValue);
                    cmd2.ExecuteNonQuery();
                }
            }
            FormuTemizle();
            KurslariListele();
            MesajGoster("Kurs başarıyla eklendi.", "success");
        }

        protected void gridCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gridCourses.SelectedDataKey.Value.ToString();
            hfKursId.Value = id;

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Courses WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtBaslik.Text = dr["Title"].ToString();
                    txtFiyat.Text = dr["Price"].ToString();
                    txtResim.Text = dr["Image"].ToString();
                    if (dr["Category"] != DBNull.Value) ddlKategori.SelectedValue = dr["Category"].ToString();
                    if (dr["InstructorId"] != DBNull.Value) ddlEgitmen.SelectedValue = dr["InstructorId"].ToString();
                    try { if (dr["VideoUrl"] != DBNull.Value) txtVideoUrl.Text = dr["VideoUrl"].ToString(); } catch { }
                }
            }
            btnKursEkle.Visible = false;
            btnGuncelle.Visible = true;
            btnVazgec.Visible = true;
            MesajGoster("Kurs düzenleniyor...", "warning");
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            string videoUrl = EmbedLinkeCevir(txtVideoUrl.Text);
            using (SqlConnection con = new SqlConnection(baglanti))
            {
                con.Open();
                try
                {
                    string sql = "UPDATE Courses SET Title=@baslik, Price=@fiyat, Image=@resim, Category=@kat, InstructorId=@insId, VideoUrl=@vid WHERE Id=@id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@vid", videoUrl);
                    cmd.Parameters.AddWithValue("@baslik", txtBaslik.Text);
                    cmd.Parameters.AddWithValue("@fiyat", Convert.ToDecimal(txtFiyat.Text));
                    cmd.Parameters.AddWithValue("@resim", txtResim.Text);
                    cmd.Parameters.AddWithValue("@kat", ddlKategori.SelectedValue);
                    cmd.Parameters.AddWithValue("@insId", ddlEgitmen.SelectedValue);
                    cmd.Parameters.AddWithValue("@id", hfKursId.Value);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    string sql2 = "UPDATE Courses SET Title=@baslik, Price=@fiyat, Image=@resim, Category=@kat, InstructorId=@insId WHERE Id=@id";
                    SqlCommand cmd = new SqlCommand(sql2, con);
                    cmd.Parameters.AddWithValue("@baslik", txtBaslik.Text);
                    cmd.Parameters.AddWithValue("@fiyat", Convert.ToDecimal(txtFiyat.Text));
                    cmd.Parameters.AddWithValue("@resim", txtResim.Text);
                    cmd.Parameters.AddWithValue("@kat", ddlKategori.SelectedValue);
                    cmd.Parameters.AddWithValue("@insId", ddlEgitmen.SelectedValue);
                    cmd.Parameters.AddWithValue("@id", hfKursId.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            FormuTemizle();
            KurslariListele();
            MesajGoster("Kurs güncellendi.", "info");
        }

        protected void btnVazgec_Click(object sender, EventArgs e)
        {
            FormuTemizle();
            lblMesaj.Visible = false;
        }

        void FormuTemizle()
        {
            txtBaslik.Text = ""; txtFiyat.Text = ""; txtResim.Text = ""; txtVideoUrl.Text = "";
            btnKursEkle.Visible = true; btnGuncelle.Visible = false; btnVazgec.Visible = false; hfKursId.Value = "";
            gridCourses.SelectedIndex = -1;
        }

        void MesajGoster(string mesaj, string tip)
        {
            lblMesaj.Text = mesaj; lblMesaj.CssClass = "alert alert-" + tip; lblMesaj.Visible = true;
        }
        protected void gridCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gridCourses.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                con.Open();
                SqlCommand cmdOrders = new SqlCommand("DELETE FROM Orders WHERE CourseId = @id", con);
                cmdOrders.Parameters.AddWithValue("@id", id);
                cmdOrders.ExecuteNonQuery();

                SqlCommand cmdCourse = new SqlCommand("DELETE FROM Courses WHERE Id = @id", con);
                cmdCourse.Parameters.AddWithValue("@id", id);
                cmdCourse.ExecuteNonQuery();
            }
            KurslariListele();
            MesajGoster("Kurs ve ilişkili siparişler silindi.", "danger");
        }

        protected void gridUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gridUsers.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                con.Open();

                SqlCommand cmdOrders = new SqlCommand("DELETE FROM Orders WHERE UserId = @id", con);
                cmdOrders.Parameters.AddWithValue("@id", id);
                cmdOrders.ExecuteNonQuery();
                SqlCommand cmdUser = new SqlCommand("DELETE FROM Users WHERE Id = @id", con);
                cmdUser.Parameters.AddWithValue("@id", id);
                cmdUser.ExecuteNonQuery();
            }
            KullanicilariListele();
            MesajGoster("Kullanıcı silindi.", "danger");
        }

        protected void btnRaporGetir_Click(object sender, EventArgs e)
        {
            string secilenKategori = ddlRaporKategori.SelectedValue;
            string kategoriAdi = ddlRaporKategori.SelectedItem.Text;

            string karmasikSql = @"
                SELECT * FROM Users 
                WHERE Id IN (
                    SELECT UserId FROM Orders 
                    WHERE CourseId IN (
                        SELECT Id FROM Courses WHERE Category = @kat
                    )
                )";

            using (SqlConnection con = new SqlConnection(baglanti))
            {
                SqlDataAdapter da = new SqlDataAdapter(karmasikSql, con);
                da.SelectCommand.Parameters.AddWithValue("@kat", secilenKategori);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gridReport.DataSource = dt;
                gridReport.DataBind();
                gridReport.Visible = true;
            }
            lblMesaj.Text = $"Rapor Sonucu: '{kategoriAdi}' kategorisinden kurs alan öğrenciler.";
            lblMesaj.CssClass = "alert alert-info d-block";
            lblMesaj.Visible = true;
            btnTumunuGetir.Visible = true;
        }

        protected void btnTumunuGetir_Click(object sender, EventArgs e)
        {
            gridReport.Visible = false;
            btnTumunuGetir.Visible = false;
            lblMesaj.Visible = false;
        }

        protected void btnInnerJoin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(baglanti))
            {
                string sql = @"SELECT 
                                Courses.Id, 
                                Courses.Title AS [Kurs Adı], 
                                Courses.Price AS [Fiyat], 
                                Instructors.Name AS [Eğitmen Adı],
                                Instructors.Phone AS [Eğitmen Telefon]
                               FROM Courses 
                               INNER JOIN Instructors ON Courses.InstructorId = Instructors.InstructorId";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gridInnerJoin.DataSource = dt;
                gridInnerJoin.DataBind();
                gridInnerJoin.Visible = true;

                MesajGoster("Inner Join işlemi başarılı.", "success");
            }
        }
    }
}