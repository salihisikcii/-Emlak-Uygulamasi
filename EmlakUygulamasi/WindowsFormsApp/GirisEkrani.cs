using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private bool ValidateCredentials(string username, string password)
        {
            // Kullanıcı adı ve şifre kontrolü burada yapılır
            string filePath = "users.txt";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && parts[0] == username && parts[1] == password)
                    {
                        return true; 
                    }
                }
            }

            return false; 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = girisKullanicAdi.Text;
            string password = girisSifre.Text;

            if (ValidateCredentials(username, password))
            {
                SorgulamaEkrani anaForm = new SorgulamaEkrani();
                anaForm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KayitEkrani emlakkayit = new KayitEkrani();
            this.Hide();
            emlakkayit.ShowDialog();
            this.Show();
        }
    }
}
