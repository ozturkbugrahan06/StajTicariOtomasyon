using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StajProjeTicariOtomasyon
{
    public partial class FormAnaSayfa : Form
    {
        public FormAnaSayfa()
        {
            InitializeComponent();
        }

        FormUrunler fr;
        private void ButonUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr == null)  //Form açıkken tekrar tıklandığında yenisi açılmaması için
            {
                fr = new FormUrunler();
                fr.MdiParent = this;
                fr.Show();
            }
        }

        FormMusteriler fr2;
        private void ButonMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr2 == null)
            {
                fr2 = new FormMusteriler();
                fr2.MdiParent = this;
                fr2.Show();
            }
        }

        FormFirmalar fr3;
        private void ButonFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr3 == null)
            {
                fr3 = new FormFirmalar();
                fr3.MdiParent = this;
                fr3.Show();
            }
        }

        FormPersoneller fr4;
        private void ButonPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr4 == null)
            {
                fr4 = new FormPersoneller();
                fr4.MdiParent = this;
                fr4.Show();
            }
        }

        FormGiderler fr6;
        private void ButonGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (fr6 == null)
            {
                fr6 = new FormGiderler();
                fr6.MdiParent = this;
                fr6.Show();
            }
        }

        FormBankalar fr7;
        private void ButonBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr7 == null)
            {
                fr7 = new FormBankalar();
                fr7.MdiParent = this;
                fr7.Show();
            }
        }

        FormRehber fr5;
        private void ButonRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr5 == null)
            {
                fr5 = new FormRehber();
                fr5.MdiParent = this;
                fr5.Show();
            }
        }

        FormStoklar fr8;
        private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr8 == null)
            {
                fr8 = new FormStoklar();
                fr8.MdiParent = this;
                fr8.Show();
            }
        }

        FormAyarlar fr9;
        private void ButonAyar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {                    
            {
                if (fr9 == null)
                {
                    fr9 = new FormAyarlar();
                    fr9.Show();
                }
            }
        }
    }
}
