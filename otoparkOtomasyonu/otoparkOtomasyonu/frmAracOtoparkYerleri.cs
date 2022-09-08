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

namespace otoparkOtomasyonu
{
    public partial class frmAracOtoparkYerleri : Form
    {
        
        public frmAracOtoparkYerleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-PTVBV06;Initial Catalog=otopark;Integrated Security=True");



        private void frmAracOtoparkYerleri_Load(object sender, EventArgs e)
        {
            BosParkYerleri();   //park alanlarına isim ekleme
            DoluParkYerleri();
            AracPlakaYazdir();
        }

        private void AracPlakaYazdir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from aracOtoparkKaydi", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == read["parkyeri"].ToString())
                        {
                            item.Text = read["plaka"].ToString();

                        }

                    }
                }
            }
            baglanti.Close();
        }

        private void DoluParkYerleri()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from aracDurumu", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == read["parkyeri"].ToString() && read["durumu"].ToString() == "DOLU")
                        {
                            item.BackColor = Color.Red;

                        }

                    }
                }
            }
            baglanti.Close();
        }

        private void BosParkYerleri()
        {
            int sayac = 1;
            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    item.Text = "P-" + sayac;
                    item.Name = "P-" + sayac;
                    sayac++;
                }
            }
        }
    }
}
