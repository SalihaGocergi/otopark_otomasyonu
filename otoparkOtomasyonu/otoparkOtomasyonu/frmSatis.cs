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

namespace otoparkOtomasyonu
{
    public partial class frmSatis : Form
    {
        public frmSatis()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-PTVBV06;Initial Catalog=otopark;Integrated Security=True");
        DataSet dataSet = new DataSet();
        private void frmSatis_Load(object sender, EventArgs e)
        {
            SatislariListele();
            Hesapla();
        }

        private void SatislariListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from satiss", baglanti);
            adtr.Fill(dataSet, "satiss");
            dataGridView1.DataSource = dataSet.Tables["satiss"];
            baglanti.Close();
        }

        private void Hesapla()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(tutar) from satiss", baglanti);
            label1.Text = "Toplam Tutar= " + komut.ExecuteScalar() + " TL";
            baglanti.Close();
        }

    }
}
