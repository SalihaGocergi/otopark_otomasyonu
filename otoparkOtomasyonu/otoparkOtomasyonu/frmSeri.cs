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
    public partial class frmSeri : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-PTVBV06;Initial Catalog=otopark;Integrated Security=True");

        public frmSeri()
        {
            InitializeComponent();
        }

        private void frmSeri_Load(object sender, EventArgs e)
        {
            marka();
        }

        private void marka()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from markaBilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into seriBilgileri(marka,seri) values('"+comboBox1.Text+"','" + textBox1.Text + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Markaya bağlı araç serisi eklendi");
            //Ekran Temizleme
            textBox1.Clear();
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            marka();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
