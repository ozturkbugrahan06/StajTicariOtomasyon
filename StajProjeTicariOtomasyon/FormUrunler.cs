using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StajProjeTicariOtomasyon
{
    public partial class FormUrunler : Form
    {
        public FormUrunler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TabloUrunler", bgl.Baglanti());    //TabloUrunlerin verilerini çek listele
            da.Fill(dt);        //Tabloyu doldur
            gridControl1.DataSource = dt;       //Grid kontrole ekle
        }

        private void LabelControl2_Click(object sender, EventArgs e)
        {

        }

        private void FormUrunler_Load(object sender, EventArgs e)
        {















            listele();      //Listele metodunu form açılınca yükle
        }

        private void ButonKaydet_Click(object sender, EventArgs e)
        {
            //Veri Kayıt
            SqlCommand komut = new SqlCommand("insert into TabloUrunler(UrunAd,Marka,Model,Yıl,Adet,AlisFiyat,SatisFiyat,Detay) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.Baglanti()); //Veritabanına ekle
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMarka.Text);
            komut.Parameters.AddWithValue("@p3", txtModel.Text);
            komut.Parameters.AddWithValue("@p4", MskYil.Text);                                                  //Verileri ata
            komut.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtAlisFiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtSatisFiyat.Text));
            komut.Parameters.AddWithValue("@p8", RcbDetay.Text);
            komut.ExecuteNonQuery();
            bgl.Baglanti().Close();                                                                             //Veritabanı bağlantısını kapat                        
            MessageBox.Show("Ürün Sisteme Eklendi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void ButonSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete From TabloUrunler where Id=@p1", bgl.Baglanti());      //Id ye göre veri silme
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bgl.Baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();

        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);          //Mouse ile ürüne tıklandığında verileri gride aktarma
            txtId.Text = dr["Id"].ToString();
            txtAd.Text = dr["UrunAd"].ToString();
            txtMarka.Text = dr["Marka"].ToString();
            txtModel.Text = dr["Model"].ToString();
            MskYil.Text = dr["Yıl"].ToString();
            NudAdet.Text = dr["Adet"].ToString();
            txtAlisFiyat.Text = dr["AlisFİyat"].ToString();
            txtSatisFiyat.Text = dr["SatisFiyat"].ToString();
            RcbDetay.Text = dr["Detay"].ToString();

        }

        private void ButonGüncelle_Click(object sender, EventArgs e)        //Id ye göre güncelleme yapma
        {
            SqlCommand komut = new SqlCommand("update TabloUrunler set UrunAd=@p1,Marka=@p2,Model=@p3,Yıl=@p4,Adet=@p5,AlisFiyat=@p6,SatisFiyat=@p7,Detay=@p8 where Id=@p9", bgl.Baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMarka.Text);
            komut.Parameters.AddWithValue("@p3", txtModel.Text);
            komut.Parameters.AddWithValue("@p4", MskYil.Text);                                                  //Verileri ata
            komut.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtAlisFiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtSatisFiyat.Text));
            komut.Parameters.AddWithValue("@p8", RcbDetay.Text);
            komut.Parameters.Add("@p9", txtId.Text);
            komut.ExecuteNonQuery();
            bgl.Baglanti().Close();
            MessageBox.Show("Ürün Bilgileri Güncellendi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();


        }
    }
}
