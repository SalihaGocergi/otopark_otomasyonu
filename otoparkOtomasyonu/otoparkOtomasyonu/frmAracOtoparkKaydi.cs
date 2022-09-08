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
    public partial class frmAracOtoparkKaydi : Form
    {
        public frmAracOtoparkKaydi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-PTVBV06;Initial Catalog=otopark;Integrated Security=True");


        private void frmAracOtoparkKaydi_Load(object sender, EventArgs e)
        {
            //ParkYeri combobox'a listeleme
            BosAraclar();
            //ComboMarka kısmına veritabanında kayıtlı olan markaları getirir
            Marka();
            

        }

        private void Marka()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from markaBilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void BosAraclar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from aracdurumu WHERE durumu='BOS'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        //KAyıt İşlemi
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into aracOtoparkKaydi(tc,ad,soyad,telefon,email,plaka,marka,seri,renk,parkyeri,tarih)values(@tc,@ad,@soyad,@telefon,@email,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)",baglanti);
            komut.Parameters.AddWithValue("@tc",txtTc.Text);
            komut.Parameters.AddWithValue("@ad",txtAd.Text);
            komut.Parameters.AddWithValue("@soyad",txtSoyad.Text);
            komut.Parameters.AddWithValue("@telefon",txtTelefon.Text);
            komut.Parameters.AddWithValue("@email",txtEmail.Text);
            komut.Parameters.AddWithValue("@plaka",txtPlaka.Text);
            komut.Parameters.AddWithValue("@marka",comboMarka.Text);
            komut.Parameters.AddWithValue("@seri",comboSeri.Text);
            komut.Parameters.AddWithValue("@renk",txtRenk.Text);
            komut.Parameters.AddWithValue("@parkyeri",comboParkYeri.Text);
            komut.Parameters.AddWithValue("@tarih",DateTime.Now.ToString());
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update aracDurumu set durumu='DOLU' where parkyeri='"+comboParkYeri.SelectedItem+"'",baglanti);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Araç Kaydı Oluşturuldu.","Kayıt");
            comboParkYeri.Items.Clear();
            BosAraclar();
            comboMarka.Items.Clear();
            Marka();
            comboSeri.Items.Clear();
            foreach(Control item in grupKişi.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in grupAraç.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in grupAraç.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }
        //Marka Ekleme
        private void btnMarka_Click(object sender, EventArgs e)
        {
            frmMarka marka = new frmMarka();
            marka.ShowDialog();
        }
        //Seri Ekleme
        private void btnSeri_Click(object sender, EventArgs e)
        {
            frmSeri seri = new frmSeri();
            seri.ShowDialog();

        }

        //Seçilen markaya göre serileri listeleme
        private void comboMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboSeri.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka,seri from seriBilgileri where marka='"+comboMarka.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboSeri.Items.Add(read["seri"].ToString());
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void grupAraç_Enter(object sender, EventArgs e)
        {

        }
    }
}