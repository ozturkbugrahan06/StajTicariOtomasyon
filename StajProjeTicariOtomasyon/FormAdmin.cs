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
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        SqlBaglantisi bagla = new SqlBaglantisi();
        private void Button1_MouseHover(object sender, EventArgs e)
        {
            ButonGirisYap.BackColor = Color.Blue;
        }

        private void ButonGirisYap_Click(object sender, EventArgs e)
        {
         
            
                FormAnaSayfa fr = new FormAnaSayfa();
                fr.Show();
                this.Hide();

            
          
        }
    }
}
