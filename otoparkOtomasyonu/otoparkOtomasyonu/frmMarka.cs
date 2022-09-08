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
    public partial class frmMarka : Form
    {
        public frmMarka()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-PTVBV06;Initial Catalog=otopark;Integrated Security=True");


        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            //Marka eklendi
            SqlCommand komut = new SqlCommand("insert into markaBilgileri(marka) values('"+textBox1.Text+"')",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Marka eklendi");
            textBox1.Clear();
        }

        private void frmMarka_Load(object sender, EventArgs e)
        {

        }
    }
}
