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
    public partial class FormPersoneller : Form
    {
        public FormPersoneller()
        {
            InitializeComponent();
        }

        SqlBaglantisi bagla = new SqlBaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TabloPersoneller", bagla.Baglanti());    //TabloMusterilerin verilerini çek listele
            da.Fill(dt);        //Tabloyu doldur
            gridControl1.DataSource = dt;       //Grid kontrole ekle
        }
        void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            MskTel1.Text = "";
            MtbTC.Text = "";
            txtMail.Text = "";
            CBİl.Text = "";
            CBIlce.Text = "";
            RcbAdres.Text = "";
            txtGorev.Text = "";

        }
        private void FormPersoneller_Load(object sender, EventArgs e)
        {
            SehirListesi();            
            listele();
            temizle();
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
       

        private void ButonKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TabloPersoneller(Ad,Soyad,Telefon,TC,Mail,Il,Ilce,Adres,Gorev) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bagla.Baglanti()); //Veritabanına ekle
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTel1.Text);
            komut.Parameters.AddWithValue("@p4", MtbTC.Text);                                                  //Verileri ata
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6", CBİl.Text);
            komut.Parameters.AddWithValue("@p7", CBIlce.Text);
            komut.Parameters.AddWithValue("@p8", RcbAdres.Text);
            komut.Parameters.AddWithValue("@p9", txtGorev.Text);
            
            komut.ExecuteNonQuery();
            bagla.Baglanti().Close();                                                                             //Veritabanı bağlantısını kapat                        
            MessageBox.Show("Personel Sisteme Eklendi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            
        }
       
        private void CBİl_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void GridView1_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);          //Mouse ile personele tıklandığında verileri gride aktarma
            if (dr != null)
            {
                txtId.Text = dr["Id"].ToString();
                txtAd.Text = dr["Ad"].ToString();
                txtSoyad.Text = dr["Soyad"].ToString();
                MskTel1.Text = dr["Telefon"].ToString();
                MtbTC.Text = dr["TC"].ToString();
                txtMail.Text = dr["Mail"].ToString();
                CBİl.Text = dr["Il"].ToString();
                CBIlce.Text = dr["Ilce"].ToString();
                RcbAdres.Text = dr["Adres"].ToString();
                txtGorev.Text = dr["Gorev"].ToString();
            }
           
        }

        private void ButonTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void ButonSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete From TabloPersoneller where Id=@p1", bagla.Baglanti());      //Id ye göre veri silme
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bagla.Baglanti().Close();
            MessageBox.Show("Personel Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            listele();
        }

        private void ButonGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TabloPersoneller set Ad=@p1,Soyad=@p2,Telefon=@p3,TC=@p4,Mail=@p5,Il=@p6,Ilce=@p7,Adres=@p8,Gorev=@p9 where Id=@p10", bagla.Baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTel1.Text);
            komut.Parameters.AddWithValue("@p4", MtbTC.Text);                                                  //Verileri ata
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6", CBİl.Text);
            komut.Parameters.AddWithValue("@p7", CBIlce.Text);
            komut.Parameters.AddWithValue("@p8", RcbAdres.Text);
            komut.Parameters.AddWithValue("@p9", txtGorev.Text);
            komut.Parameters.AddWithValue("@p10",txtId.Text);

            komut.ExecuteNonQuery();
            bagla.Baglanti().Close();
            MessageBox.Show("Personel Bilgileri Güncellendi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }
    }
}
