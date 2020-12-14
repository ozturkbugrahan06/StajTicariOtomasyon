using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StajProjeTicariOtomasyon
{
    public partial class FormFirmalar : Form
    {
        public FormFirmalar()
        {
            InitializeComponent();
        }

        private void XtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        SqlBaglantisi bagla = new SqlBaglantisi();
        void SehirListesi()
        {
            SqlCommand komut = new SqlCommand("Select Sehir From TabloIller", bagla.Baglanti());        //SehirListesi tablosunu getir 
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CBİl.Properties.Items.Add(dr[0]);
            }
            bagla.Baglanti().Close();
        }
       
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TabloFirmalar", bagla.Baglanti());    //TabloFirmaların verilerini çek listele
            da.Fill(dt);        //Tabloyu doldur
            gridControl1.DataSource = dt;       //Grid kontrole ekle
        }

        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSektor.Text = "";
            txtGorev.Text = "";
            txtYetkili.Text = "";
            MtbTC.Text = "";
            MskTel1.Text = "";
            MskTel2.Text = "";
            MskTel3.Text = "";
            MskFax.Text = "";
            txtMail.Text = "";
            CBİl.Text = "";
            CBIlce.Text = "";
            txtVergi.Text = "";
            RcbAdres.Text = "";
        }
        private void Firmalar_Load(object sender, EventArgs e)
        {
            SehirListesi();

            listele();

            temizle();
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);          //Mouse ile firmaya tıklandığında verileri gride aktarma
            if (dr != null)
            {
                txtId.Text = dr["Id"].ToString();
                txtAd.Text = dr["Ad"].ToString();
                txtSektor.Text = dr["Sektor"].ToString();
                txtGorev.Text = dr["YetkiliStatu"].ToString();
                txtYetkili.Text = dr["YetkiliAdSoyad"].ToString();
                MtbTC.Text = dr["YetkiliTC"].ToString();                
                MskTel1.Text = dr["Telefon"].ToString();
                MskTel2.Text = dr["Telefon2"].ToString();
                MskTel3.Text = dr["Telefon3"].ToString();
                MskFax.Text= dr["Fax"].ToString();
                txtMail.Text = dr["Mail"].ToString();                
                CBİl.Text = dr["Il"].ToString();
                CBIlce.Text = dr["Ilce"].ToString();
                txtVergi.Text = dr["VergiDaire"].ToString();
                RcbAdres.Text = dr["Adres"].ToString();

            }
        }

        private void ButonKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TabloFirmalar(Ad,Sektor,YetkiliStatu,YetkiliAdSoyad,YetkiliTC,Telefon,Telefon2,Telefon3,Fax,Mail,Il,Ilce,VergiDaire,Adres) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14)", bagla.Baglanti()); //Veritabanına ekle
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSektor.Text);
            komut.Parameters.AddWithValue("@p3", txtGorev.Text);
            komut.Parameters.AddWithValue("@p4", txtYetkili.Text);                                                  //Verileri ata
            komut.Parameters.AddWithValue("@p5", MtbTC.Text);
            komut.Parameters.AddWithValue("@p6", MskTel1.Text);
            komut.Parameters.AddWithValue("@p7", MskTel2.Text);
            komut.Parameters.AddWithValue("@p8", MskTel3.Text);
            komut.Parameters.AddWithValue("@p9", MskFax.Text);
            komut.Parameters.AddWithValue("@p10", txtMail.Text);
            komut.Parameters.AddWithValue("@p11", CBİl.Text);
            komut.Parameters.AddWithValue("@p12", CBIlce.Text);
            komut.Parameters.AddWithValue("@p13", txtVergi.Text);
            komut.Parameters.AddWithValue("@p14", RcbAdres.Text);
            komut.ExecuteNonQuery();
            bagla.Baglanti().Close();                                                                             //Veritabanı bağlantısını kapat                        
            MessageBox.Show("Firma Sisteme Eklendi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void ButonSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete From TabloFirmalar where Id=@p1", bagla.Baglanti());      //Id ye göre veri silme
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bagla.Baglanti().Close();
            MessageBox.Show("Firma Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            listele();
        }

        private void ButonGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TabloFirmalar set Ad=@p1,Sektor=@p2,YetkiliStatu=@p3,YetkiliAdSoyad=@p4,YetkiliTC=@p5,Telefon=@p6,Telefon2=@p7,Telefon3=@p8,Fax=@p9,Mail=@p10,Il=@p11 ,Ilce=@p12 ,VergiDaire=@p13,Adres=@p14 where Id=@p15", bagla.Baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSektor.Text);
            komut.Parameters.AddWithValue("@p3", txtGorev.Text);
            komut.Parameters.AddWithValue("@p4", txtYetkili.Text);                                                  //Verileri ata
            komut.Parameters.AddWithValue("@p5", MtbTC.Text);
            komut.Parameters.AddWithValue("@p6", MskTel1.Text);
            komut.Parameters.AddWithValue("@p7", MskTel2.Text);
            komut.Parameters.AddWithValue("@p8", MskTel3.Text);
            komut.Parameters.AddWithValue("@p9", MskFax.Text);
            komut.Parameters.AddWithValue("@p10", txtMail.Text);
            komut.Parameters.AddWithValue("@p11", CBİl.Text);
            komut.Parameters.AddWithValue("@p12", CBIlce.Text);
            komut.Parameters.AddWithValue("@p13", txtVergi.Text);
            komut.Parameters.AddWithValue("@p14", RcbAdres.Text);
            komut.Parameters.AddWithValue("@p15", txtId.Text);
            komut.ExecuteNonQuery();
            bagla.Baglanti().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

       

        private void CBİl_SelectedIndexChanged(object sender, EventArgs e)
        {

            CBIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select Ilce from TabloIlceler where Sehir=@p1", bagla.Baglanti());       //İl seçildikten sonra ilçlerinin gelmesi sağlandı.
            komut.Parameters.AddWithValue("@p1", CBİl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CBIlce.Properties.Items.Add(dr[0]);
            }
            bagla.Baglanti().Close();
        }
    }
}
