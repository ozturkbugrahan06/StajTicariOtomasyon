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
    public partial class FormMusteriler : Form
    {
        public FormMusteriler()
        {
            InitializeComponent();
        }

        private void LabelControl3_Click(object sender, EventArgs e)
        {

        }

        private void LabelControl4_Click(object sender, EventArgs e)
        {

        }

        SqlBaglantisi bagla = new SqlBaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TabloMusteriler", bagla.Baglanti());    //TabloMusterilerin verilerini çek listele
            da.Fill(dt);        //Tabloyu doldur
            gridControl1.DataSource = dt;       //Grid kontrole ekle
        }
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

        private void FormMusteriler_Load(object sender, EventArgs e)
        {
            listele();

            SehirListesi();                                      //SehirListesi tablosunu calıstır
        }

        private void CBİl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select Ilce from TabloIlceler where Sehir=@p1", bagla.Baglanti());       //İl seçildikten sonra ilçlerinin gelmesi sağlandı.
            komut.Parameters.AddWithValue("@p1", CBİl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                CBIlce.Properties.Items.Add(dr[0]);
            }
            bagla.Baglanti().Close();
        }

        private void ButonKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TabloMusteriler(Ad,Soyad,Telefon,Telefon2,TC,Mail,Il,Ilce,VergiDaire,Adres) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bagla.Baglanti()); //Veritabanına ekle
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTel1.Text);
            komut.Parameters.AddWithValue("@p4", MskTel2.Text);                                                  //Verileri ata
            komut.Parameters.AddWithValue("@p5", MtbTC.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", CBİl.Text);
            komut.Parameters.AddWithValue("@p8", CBIlce.Text);
            komut.Parameters.AddWithValue("@p9", txtVergi.Text);
            komut.Parameters.AddWithValue("@p10",RcbAdres.Text);
            komut.ExecuteNonQuery();
            bagla.Baglanti().Close();                                                                             //Veritabanı bağlantısını kapat                        
            MessageBox.Show("Müşteri Sisteme Eklendi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);          //Mouse ile müşteriye tıklandığında verileri gride aktarma
            if (dr != null)
            {
                txtId.Text = dr["Id"].ToString();
                txtAd.Text = dr["Ad"].ToString();
                txtSoyad.Text = dr["Soyad"].ToString();
                MskTel1.Text = dr["Telefon"].ToString();
                MskTel2.Text = dr["Telefon2"].ToString();
                MtbTC.Text = dr["TC"].ToString();
                txtMail.Text = dr["Mail"].ToString();
                CBİl.Text = dr["Il"].ToString();
                CBIlce.Text = dr["Ilce"].ToString();
                txtVergi.Text = dr["VergiDaire"].ToString();
                RcbAdres.Text = dr["Adres"].ToString();
            }
        }

        private void ButonSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete From TabloMusteriler where Id=@p1", bagla.Baglanti());      //Id ye göre veri silme
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bagla.Baglanti().Close();
            MessageBox.Show("Müşteri Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            listele();
        }

        private void ButonGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TabloMusteriler set Ad=@p1,Soyad=@p2,Telefon=@p3,Telefon2=@p4,TC=@p5,Mail=@p6,Il=@p7,Ilce=@p8,VergiDaire=@p9,Adres=@p10 where Id=@p11", bagla.Baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTel1.Text);
            komut.Parameters.AddWithValue("@p4", MskTel2.Text);                                                  //Verileri ata
            komut.Parameters.AddWithValue("@p5", MtbTC.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", CBİl.Text);
            komut.Parameters.AddWithValue("@p8", CBIlce.Text);
            komut.Parameters.AddWithValue("@p9", txtVergi.Text);
            komut.Parameters.AddWithValue("@p10", RcbAdres.Text);
            komut.Parameters.AddWithValue("@p11", txtId.Text);
            komut.ExecuteNonQuery();
            bagla.Baglanti().Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }
    }
}
