using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace depo_uygulaması
{
    public partial class Gecmis : Form

    {
        int hareket;
        int Mouse_X;
        int Mouse_Y;
        public Gecmis()
        {
            InitializeComponent();
        }
        public OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=datam.accdb");
        public DataTable tablo = new DataTable();
        public OleDbDataAdapter adtr = new OleDbDataAdapter();
        public OleDbCommand kmt = new OleDbCommand();
        private void button2_Click(object sender, EventArgs e)
        {
            AnaSayfa giris = new AnaSayfa();
            giris.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void Gecmis_Load(object sender, EventArgs e)
        {
            listele();
            
        }
        public void listele()
        {
            tablo.Clear();
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("SELECT id,hareket,tarih,kullanici from hareket", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            adtr.Dispose();
            baglanti.Close();
            try
            {
                
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dataGridView1.Columns[0].HeaderText = "İD";
                dataGridView1.Columns[1].HeaderText = "İŞLEM";
                dataGridView1.Columns[2].HeaderText = "TARİH";
                dataGridView1.Columns[3].HeaderText = "KULLANICI";

                dataGridView1.Columns[0].Width = 150;
                dataGridView1.Columns[1].Width = 170;
                dataGridView1.Columns[2].Width = 170;
                dataGridView1.Columns[3].Width = 150;

            }
            catch
            {
                baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    baglanti.Open();
                    kmt.Connection = baglanti;
                    kmt.CommandText = "DELETE * from hareket";
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    baglanti.Close();
                    listele();
                }
            }
            catch
            {
                baglanti.Close();
            }
        }
    }
}
