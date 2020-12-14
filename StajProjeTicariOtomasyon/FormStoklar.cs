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
    public partial class FormStoklar : Form
    {
        public FormStoklar()
        {
            InitializeComponent();
        }

        SqlBaglantisi bagla = new SqlBaglantisi();
        private void FormStoklar_Load(object sender, EventArgs e)
        {
            //chartControl1.Series["Series1"].Points.AddPoint("")
            SqlDataAdapter da = new SqlDataAdapter("Select UrunAd,Sum(Adet) As'Miktar' from TabloUrunler group by Urunad", bagla.Baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //Grafikte stok listeleme
            SqlCommand komut = new SqlCommand("Select UrunAd,Sum(Adet) As'Miktar' from TabloUrunler group by Urunad", bagla.Baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            bagla.Baglanti().Close();
        }
    }
}
