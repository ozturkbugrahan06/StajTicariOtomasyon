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
    public partial class FormGiderler : Form
    {
        public FormGiderler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bagla = new SqlBaglantisi();
        void giderlistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TabloGiderler", bagla.Baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FormGiderler_Load(object sender, EventArgs e)
        {
            giderlistesi();
            temizle();
        }

        private void ButonKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TabloGiderler(Ay,Yıl,Elektrik,Su,Doğalgaz,Internet,Maaslar,Ekstra,Notlar) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bagla.Baglanti()); //Veritabanına ekle
            komut.Parameters.AddWithValue("@p1", CBAy.Text);
            komut.Parameters.AddWithValue("@p2", CBYil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(textElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(textSu.Text));                                                  //Verileri ata
            komut.Parameters.AddWithValue("@p5", decimal.Parse(textDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(textInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(textMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(textEkstra.Text));
            komut.Parameters.AddWithValue("@p9", RcbNotlar.Text);

            komut.ExecuteNonQuery();
            bagla.Baglanti().Close();                                                                             //Veritabanı bağlantısını kapat                        
            MessageBox.Show("Gider Lsiteye Eklendi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderlistesi();
            temizle();


        }
        void temizle()
        {
            txtId.Text = "";
            CBAy.Text = "";
            CBYil.Text = "";
            textElektrik.Text = "";
            textSu.Text = "";
            textDogalgaz.Text = "";
            textMaaslar.Text = "";
            textInternet.Text = "";
            textEkstra.Text = "";
            RcbNotlar.Text = "";
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);          //Mouse ile giderler tıklandığında verileri gride aktarma
            if (dr != null)
            {
                txtId.Text = dr["Id"].ToString();
                CBAy.Text = dr["Ay"].ToString();
                CBYil.Text = dr["Yıl"].ToString();
                textElektrik.Text = dr["Elektrik"].ToString();
                textSu.Text = dr["Su"].ToString();
                textDogalgaz.Text = dr["Doğalgaz"].ToString();
                textInternet.Text = dr["Internet"].ToString();
                textMaaslar.Text = dr["Maaslar"].ToString();
                textEkstra.Text = dr["Ekstra"].ToString();
                RcbNotlar.Text = dr["Notlar"].ToString();
                

            }
        }

        private void ButonSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete From TabloGiderler where Id=@p1", bagla.Baglanti());      //Id ye göre veri silme
            komutsil.Parameters.AddWithValue("@p1", txtId.Text);
            komutsil.ExecuteNonQuery();
            bagla.Baglanti().Close();
            MessageBox.Show("Gider Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            giderlistesi();
            temizle();
        }

        private void ButonGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TabloGiderler set Ay=@p1,Yıl=@p2,Elektrik=@p3,Su=@p4,Doğalgaz=@p5,Internet=@p6,Maaslar=@p7,Ekstra=@p8,Notlar=@p9 where Id=@p10", bagla.Baglanti());
            komut.Parameters.AddWithValue("@p1", CBAy.Text);
            komut.Parameters.AddWithValue("@p2", CBYil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(textElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(textSu.Text));                                                  //Verileri ata
            komut.Parameters.AddWithValue("@p5", decimal.Parse(textDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(textInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(textMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(textEkstra.Text));
            komut.Parameters.AddWithValue("@p9", RcbNotlar.Text);
            komut.Parameters.AddWithValue("@p10", txtId.Text);
            komut.ExecuteNonQuery();
            bagla.Baglanti().Close();
            MessageBox.Show("Gider Bilgileri Güncellendi", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            giderlistesi();
        }
    }
}
