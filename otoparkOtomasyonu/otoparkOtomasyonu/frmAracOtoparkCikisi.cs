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
    public partial class frmAracOtoparkCikisi : Form
    {
        public frmAracOtoparkCikisi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-PTVBV06;Initial Catalog=otopark;Integrated Security=True");


        private void frmAracOtoparkCikisi_Load(object sender, EventArgs e)
        {
            DoluYerler();   //Araç Bilgileri Kısmı için
            Plakalar();     //Plaka Bilgileri kısmı
            timer1.Enabled = true;
        }

        private void Plakalar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from aracOtoparkKaydi", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())      //sonuçlar geldiği sürece işleme devam edilecek
            {
                comboPlaka.Items.Add(read["plaka"].ToString());
            }
            baglanti.Close();
        }

        private void DoluYerler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from aracDurumu where durumu='DOLU'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())      
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        //Plaka seçildiğinde park yerinin gelmesi lazım bu sebeple park yerlerini çekeceğiz
        private void comboPlaka_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from aracOtoparkKaydi where plaka='"+comboPlaka.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())     
            {
                txtParkYeri.Text = read["parkyeri"].ToString();
            }
            baglanti.Close();
        }

        private void comboParkYeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from aracOtoparkKaydi where parkyeri='" + comboParkYeri.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtParkYeri2.Text = read["parkyeri"].ToString();
                txtTc.Text = read["tc"].ToString();
                txtAd.Text = read["ad"].ToString();
                txtSoyad.Text = read["soyad"].ToString();
                txtMarka.Text = read["marka"].ToString();
                txtSeri.Text = read["seri"].ToString();
                txtPlaka.Text = read["plaka"].ToString();
                lblGelisTarihi.Text = read["tarih"].ToString();
            }
            baglanti.Close();
            //TOPLAM TUTAR HESABI
            DateTime gelis, cikis;
            gelis = DateTime.Parse(lblGelisTarihi.Text);
            cikis = DateTime.Parse(lblCikisTarihi.Text);
            TimeSpan fark;
            fark = cikis - gelis;
            lblSure.Text = fark.TotalHours.ToString("0.00");
            lblToplamTutar.Text = (double.Parse(lblSure.Text) * (0.75)).ToString("0.00");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblCikisTarihi.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from aracOtoparkKaydi where plaka='"+txtPlaka.Text+"'", baglanti);
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update aracDurumu set durumu='BOS' where parkyeri='" + txtParkYeri2.Text + "'", baglanti);
            komut2.ExecuteNonQuery();
            SqlCommand komut3 = new SqlCommand("insert into satiss(parkyeri,plaka,gelis_tarihi,cikis_tarihi,sure,tutar) values(@parkyeri,@plaka,@gelis_tarihi,@cikis_tarihi,@sure,@tutar)", baglanti);
            komut3.Parameters.AddWithValue("@parkyeri", txtParkYeri2.Text);
            komut3.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut3.Parameters.AddWithValue("@gelis_tarihi", lblGelisTarihi.Text);
            komut3.Parameters.AddWithValue("@cikis_tarihi", lblCikisTarihi.Text);
            komut3.Parameters.AddWithValue("@sure", double.Parse(lblSure.Text));
            komut3.Parameters.AddWithValue("@tutar", double.Parse(lblToplamTutar.Text));

            komut3.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Araç Çıkışı Yapıldı");
            //TEMİZLE
            foreach(Control item in groupBox2.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                    txtParkYeri.Text = "";
                    comboParkYeri.Text = "";
                    comboPlaka.Text = "";
                }
            }
            comboPlaka.Items.Clear();
            comboParkYeri.Items.Clear();
            DoluYerler();
            Plakalar();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
