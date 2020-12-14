using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StajProjeTicariOtomasyon
{
    class SqlBaglantisi
    {
        public SqlConnection Baglanti()
        {
            SqlConnection Baglan = new SqlConnection(@"Data Source=DESKTOP-IHBU4CB;Initial Catalog=DboTicariOtomasyon;Integrated Security=True");
            Baglan.Open();
            return Baglan;
        }
    }
}
