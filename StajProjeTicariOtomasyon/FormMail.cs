using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;


namespace StajProjeTicariOtomasyon
{
    public partial class FormMail : Form
    {
        public FormMail()
        {
            InitializeComponent();
        }

        public string mail;

        private void FormMail_Load(object sender, EventArgs e)
        {
            textMail.Text = mail;
        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void ButonGonder_Click(object sender, EventArgs e)
        {
            MailMessage Mesaj = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("Mail", "Sifre");
            istemci.Port = 587;
            istemci.Host = "smtp.live.com";
            istemci.EnableSsl = true;
            Mesaj.To.Add(rchMesaj.Text);
            Mesaj.From = new MailAddress("Mail");
            Mesaj.Subject = textKonu.Text;
            Mesaj.Body = rchMesaj.Text;
            istemci.Send(Mesaj);
        }
    }
}
