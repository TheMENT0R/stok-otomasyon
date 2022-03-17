using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace depo_uygulaması
{
    public partial class GuvenliGiris : Form
    {
        public GuvenliGiris()
        {
            InitializeComponent();
        }

        Random rndm = new Random();
        Random rndm1 = new Random();
        
        int topla;
        int cikart;
        
        char c;
        int hareket;
        int Mouse_X;
        int Mouse_Y;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (c == '+')
                {
                    if (int.Parse(textBox1.Text) == topla)
                    {
                        AnaSayfa giris = new AnaSayfa();
                        giris.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Yanlış Giriş Yaptınız ! ");
                    }
                }
                else if (c == '-')
                {
                    if (int.Parse(textBox1.Text) == cikart)
                    {
                        AnaSayfa giris = new AnaSayfa();
                        giris.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Yanlış Giriş Yaptınız ! ");
                    }
                }
   
            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı Değer Girdiniz ! ");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int sayi1 = rndm.Next(1, 20);
            int sayi2 = rndm1.Next(1, 10);
            topla = sayi1 + sayi2;
            cikart = sayi1 - sayi2;
            
            int islem = rndm.Next(0, 2);
            if (islem == 0)
            {
                c = '+';
            }
            else if (islem == 1)
            {
                c = '-';
            }
           
            label2.Text = sayi1.ToString();
            label3.Text = c.ToString();
            label4.Text = sayi2.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = 0;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }


    }

}
