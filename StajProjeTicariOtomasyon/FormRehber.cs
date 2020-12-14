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
    public partial class FormRehber : Form
    {
        public FormRehber()
        {
            InitializeComponent();
        }

        SqlBaglantisi bagla = new SqlBaglantisi();
        private void FormRehber_Load(object sender, EventArgs e)
        {
            //Müşterie bilgilerini çekme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Ad,Soyad,Telefon,Telefon2,Mail from TabloMusteriler", bagla.Baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //Firma Bilgilerini Çekme
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select Ad,YetkiliAdSoyad,Telefon,Telefon2,Telefon3,Mail,Fax from TabloFirmalar", bagla.Baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;
        }

        private void GridView2_DoubleClick(object sender, EventArgs e) //Kişi üzerine gelip çift tıklandğında mail adresini mail formuna taşıma
        {
            FormMail form = new FormMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if(dr!=null)
            {
                form.mail = dr["Mail"].ToString();
            }
            form.Show();
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
                FormMail form = new FormMail();
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr != null)
                {
                    form.mail = dr["Mail"].ToString();
                }
                form.Show();
            }

        }
    
}
