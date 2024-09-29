using System;
using System.Windows.Forms;
using ClassLibrary1;

namespace WindowsFormsApp
{
    public partial class Anaekran : Form
    {
        public Anaekran()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            GirisEkrani form1 = new GirisEkrani();
            form1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EvEkleEkrani evEkelmeForm = new EvEkleEkrani();
            evEkelmeForm.Show();
            this.Hide();
        }
    }
}
