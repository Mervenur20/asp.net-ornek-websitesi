using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StajProjesi
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DuyurulariGetir();

        }

        private void DuyurulariGetir()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);

            string sorgu = "Select * from Duyurular order by Tarih desc";

            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            cnn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            lstDuyuru.DataSource = dr;
            lstDuyuru.DataBind();

            cnn.Close();
        }

        protected void btnKayit_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            string sifre = txtSifre.Text;

            if (kullaniciAdi.Contains(" ") == true | sifre.Contains(" ") == true)
            {
                lblSonuc.Text = "Kullanıcı adı veya şifre içinde boşluk harfi olamaz";
            }
            else
            {
                if (txtKullaniciAdi.Text != "" && txtSifre.Text != "")
                {
                    SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);

                    string sorgu = "Insert into Kullanicilar (KullaniciAdi, Sifre) Values (@kullaniciadi, @sifre)";

                    SqlCommand cmd = new SqlCommand(sorgu, cnn);
                    cnn.Open();

                    try
                    {
                        cmd.Parameters.AddWithValue("@kullaniciadi", txtKullaniciAdi.Text);
                        cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);

                        cmd.ExecuteNonQuery();
                        cnn.Close();

                        lblSonuc.Text = "Başarıyla kayıt yapılmıştır.";
                    }
                    catch (Exception)
                    {
                        lblSonuc.Text = "Kaydınız yapılamamıştır.";
                    }

                }
                else
                {
                    lblSonuc.Text = "Boş alanları doldurmanız gerekir.";
                }

            }


        }
    }
}