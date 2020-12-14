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
    public partial class FormAyarlar : Form
    {
        public FormAyarlar()
        {
            InitializeComponent();
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            if (simpleButton1.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("insert into TabloAdmin values(@p1,@p2)", bagla.Baglanti());
                komut.Parameters.AddWithValue("@p1", textKullaniciAdi.Text);
                komut.Parameters.AddWithValue("@p2", textSifre.Text);
                komut.ExecuteNonQuery();
                bagla.Baglanti().Close();
                MessageBox.Show("Yeni Admin Sisteme Eklendi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();

            }
            if (simpleButton1.Text == "Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("update TabloAdmin set Sifre=@p2 where KullaniciAdi=@p1", bagla.Baglanti());
                komut1.Parameters.AddWithValue("@p1", textKullaniciAdi.Text);
                komut1.Parameters.AddWithValue("@p2", textSifre.Text);
                komut1.ExecuteNonQuery();
                bagla.Baglanti().Close();
                MessageBox.Show("Kayıt Güncellendi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }
       

        SqlBaglantisi bagla = new SqlBaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TabloAdmin", bagla.Baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FormAyarlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            
            if(dr!=null)
            {
                textKullaniciAdi.Text = dr["KullaniciAdi"].ToString();
                textSifre.Text = dr["Sifre"].ToString();

            }
        }

        private void TextKullaniciAdi_TextChanged(object sender, EventArgs e)
        {
            if(textKullaniciAdi.Text!="")
            {
                simpleButton1.Text = "Güncelle";

            }
            else
            {
                simpleButton1.Text = "Kaydet";
            }
        }
    }
}
