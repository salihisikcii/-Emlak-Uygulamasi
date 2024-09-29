using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class DuzenlemeEkrani : Form
    {
        string[] DuzenlenecekVeriler;
        bool kontrol;
        public DuzenlemeEkrani()
        {
            InitializeComponent();
        }
        private string[] EmlakNoIleVeriBul(string emlakNo)
        {
            string[] satilikKontrol = File.ReadAllLines("satilik.txt");
            string[] kiralikKontrol = File.ReadAllLines("kiralik.txt");

            foreach (string line in satilikKontrol)
            {
                string[] arananVeri = line.Split(',');

                if (arananVeri[0] == emlakNo)
                {
                    return arananVeri;
                }
            }
            foreach (string line in kiralikKontrol)
            {
                string[] arananVeri = line.Split(',');

                if (arananVeri[0] == emlakNo)
                {
                    return arananVeri;
                }
            }

            return new string[0];
        }
        
            private void comboBoxSehir_SelectedIndexChanged(object sender, EventArgs e)
            {
                comboBoxIlce.Items.Clear();

                OrtakBilgiler.secilenIl = comboBoxSehir.SelectedItem.ToString();

                string[] ilceler = File.ReadAllLines("ilceler.txt");
                foreach (var ilce in ilceler)
                {
                    if (ilce.StartsWith(OrtakBilgiler.secilenIl))
                    {
                        comboBoxIlce.Items.Add(ilce.Substring(OrtakBilgiler.secilenIl.Length + 1));
                    }
                }
            }
        
        private void DuzenlemeEkrani_Load(object sender, EventArgs e)
        {
            Evturleri.Items.AddRange(Enum.GetNames(typeof(Ev.evTuru)));
            
            string[] iller = File.ReadAllLines("iller.txt");
            foreach (var il in iller)
            {
                comboBoxSehir.Items.Add(il);
            }

            DuzenlenecekVeriler = EmlakNoIleVeriBul(OrtakBilgiler.emlakNo);
            textBox1.Text = DuzenlenecekVeriler[3];
            textBox2.Text = DuzenlenecekVeriler[6];
            textBox3.Text = DuzenlenecekVeriler[5];
            textBox4.Text = DuzenlenecekVeriler[7];
            string arananIfadeEvTuru = DuzenlenecekVeriler[11];
            int index = Evturleri.FindStringExact(arananIfadeEvTuru);
            Evturleri.SelectedIndex = index; 

            string arananIfadeSehir = DuzenlenecekVeriler[9];
            int index1 = comboBoxSehir.FindStringExact(arananIfadeSehir);
            comboBoxSehir.SelectedIndex = index1;

            string arananIfadeIlce = DuzenlenecekVeriler[10];
            int index2 = comboBoxIlce.FindStringExact(arananIfadeIlce);
            comboBoxIlce.SelectedIndex = index2;

            richTextBox1.Text = DuzenlenecekVeriler[12];
            if (DuzenlenecekVeriler[2]== "Aktif")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            if (DuzenlenecekVeriler[1]=="Satılık")
            {
                kontrol = true;
                radioButton4.Checked = true;
            }
            else
            {
                kontrol = false;
                radioButton3.Checked = true;
            }
            textBox5.Text = DuzenlenecekVeriler[4];
        }

        private void button1_Click(object sender, EventArgs e)

        {

            if (radioButton1.Checked)
            {
                OrtakBilgiler.aktif = true;
            }
            if (radioButton2.Checked)
            {
                OrtakBilgiler.aktif = false;
            }
            if (radioButton4.Checked)
            {
                OrtakBilgiler.satilikmikirami = true;// satılık seçili
            }
            if (radioButton3.Checked)
            {
                OrtakBilgiler.satilikmikirami = false;// kira seçili
            }
            DuzenlenecekVeriler[3] = textBox1.Text;
            DuzenlenecekVeriler[6] = textBox2.Text;
            DuzenlenecekVeriler[5] = textBox3.Text;
            DuzenlenecekVeriler[7] = textBox4.Text;
            DuzenlenecekVeriler[11] = Evturleri.SelectedItem.ToString();
            DuzenlenecekVeriler[12] = richTextBox1.Text;
            DuzenlenecekVeriler[10] = comboBoxIlce.SelectedItem.ToString();
            DuzenlenecekVeriler[9] = comboBoxSehir.SelectedItem.ToString();
            DuzenlenecekVeriler[4] = textBox5.Text;
            if (kontrol)
            {
                List<string> satirlar = File.ReadAllLines("satilik.txt").ToList();
                int i = 0;
                int j = 0;
                string guncellenmisSatir = string.Join(",", DuzenlenecekVeriler);

                foreach (string line in satirlar)
                {
                    string[] arananVeri = line.Split(',');

                    if (arananVeri[0] == DuzenlenecekVeriler[0])
                    {
                        i = j;
                    }
                    i++;
                }
                satirlar.Add(guncellenmisSatir);
                satirlar.RemoveAt(j + 1);

                File.WriteAllLines("satilik.txt", satirlar);

            }
            else
            {
                List<string> satirlar = File.ReadAllLines("kiralik.txt").ToList();
                int i = 0;
                int j = 0;
                string guncellenmisSatir = string.Join(",", DuzenlenecekVeriler);

                foreach (string line in satirlar)
                {
                    string[] arananVeri = line.Split(',');

                    if (arananVeri[0] == DuzenlenecekVeriler[0])
                    {
                        i = j;
                    }
                    i++;
                }
                satirlar.Add(guncellenmisSatir);
                satirlar.RemoveAt(j + 1);

                File.WriteAllLines("kiralik.txt", satirlar);

            }
        }

    }
}
