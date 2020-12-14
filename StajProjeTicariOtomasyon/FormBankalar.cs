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
    public partial class FormBankalar : Form
    {
        public FormBankalar()
        {
            InitializeComponent();
        }

        private void GroupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
        SqlBaglantisi bagla = new SqlBaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri", bagla.Baglanti());    //Banka Bilgileri procedure kullanımı
            da.Fill(dt);        //Tabloyu doldur
            gridControl1.DataSource = dt;       //Grid kontrole ekle
        }

        void FirmaListesi()     //Firma Listesinin gelmesini sağlama(Id yerine firma adının gözükmesi)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Id,Ad from TabloFirmalar", bagla.Baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.NullText = "Lütfen Bir Firma Seçiniz...";
            lookUpEdit1.Properties.ValueMember = "Id";
            lookUpEdit1.Properties.DisplayMember = "Ad";
            lookUpEdit1.Properties.DataSource = dt;
        }
        private void FormBankalar_Load(object sender, EventArgs e)
        {
            FirmaListesi();
            SehirListesi();
            listele();
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
            SqlCommand komut = new SqlCommand("insert into TabloBankalar(BankaAdi,Il,Ilce,Sube,IBAN,HesapNo,Yetkili,Tarih,HesapTuru,FirmaId) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bagla.Baglanti()); //Veritabanına ekle
            komut.Parameters.AddWithValue("@p1", txtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", CBİl.Text);
            komut.Parameters.AddWithValue("@p3", CBIlce.Text);
            komut.Parameters.AddWithValue("@p4", txtSube.Text);                                                  //Verileri ata
            komut.Parameters.AddWithValue("@p5", textIBAN.Text);
            komut.Parameters.AddWithValue("@p6", textHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", textYetkili.Text);
            komut.Parameters.AddWithValue("@p8", textTarih.Text);
            komut.Parameters.AddWithValue("@p9", textHesapTuru.Text);
           // komut.Parameters.AddWithValue("@p10", lookUpEdit1.Text);
   
            komut.ExecuteNonQuery();
            bagla.Baglanti().Close();                                                                             //Veritabanı bağlantısını kapat                        
            MessageBox.Show("Banka Bilgileri Eklendi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
