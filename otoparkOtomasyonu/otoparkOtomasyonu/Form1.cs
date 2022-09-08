namespace otoparkOtomasyonu
{
    public partial class frmAnasayfa : Form
    {
        public frmAnasayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAracOtoparkKaydi kayit = new frmAracOtoparkKaydi();
            kayit.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAracOtoparkYerleri yer = new frmAracOtoparkYerleri();
            yer.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAracOtoparkCikisi cikis = new frmAracOtoparkCikisi();
            cikis.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmSatis satis = new frmSatis();
            satis.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmAnasayfa_Load(object sender, EventArgs e)
        {

        }

    }
}