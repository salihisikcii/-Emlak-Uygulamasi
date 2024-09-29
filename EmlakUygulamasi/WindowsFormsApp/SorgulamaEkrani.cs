using ClassLibrary1;
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
    public partial class SorgulamaEkrani : Form
    {
        public SorgulamaEkrani()
        {
            InitializeComponent();
        }



        private void SorgulamaEkrani_Load(object sender, EventArgs e)
        {
            EvTurleri.Items.AddRange(Enum.GetNames(typeof(Ev.evTuru)));
            string[] iller = File.ReadAllLines("iller.txt");
            foreach (var il in iller)
            {
                comboBoxSehir.Items.Add(il);
            }
            OrtakBilgiler.DosyadanKayitlariYukle();
            VerileriListeyeEkle("satilik.txt");
            VerileriListeyeEkle("kiralik.txt");
        }
        private void comboBoxSehir_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            OrtakBilgiler.secilenIl = comboBoxSehir.SelectedItem.ToString();

            string[] ilceler = File.ReadAllLines("ilceler.txt");
            comboBoxIlce.Items.Clear();
            foreach (var ilce in ilceler)
            {
                if (ilce.StartsWith(OrtakBilgiler.secilenIl))
                {
                    comboBoxIlce.Items.Add(ilce.Substring(OrtakBilgiler.secilenIl.Length + 1));
                }
            }
        }
        private void VerileriListeyeEkle(string dosyaAdi)
        {
            if (File.Exists(dosyaAdi))
            {
                using (StreamReader reader = new StreamReader(dosyaAdi))
                {
                    while (!reader.EndOfStream)
                    {
                        string satir = reader.ReadLine();
                        string[] satirBilgileri = satir.Split(',');
                        ListViewItem item = new ListViewItem(satirBilgileri);
                        listView1.Items.Add(item);
                    }
                }
            }
        }




        private void FiltreleVeListeyeEkle()
        {

            bool aktif = true;
            bool satilikmi = false;
            bool kirami = false;
            if (radioButton1.Checked)
            {
                aktif = true;
            }
            if (radioButton2.Checked)
            {
                aktif = false;
            }
            if (checkBox3.Checked)
            {
                kirami = true;
            }
            if (checkBox4.Checked)
            {
                satilikmi = true;
            }
            string sehir = comboBoxSehir.SelectedItem != null ? comboBoxSehir.SelectedItem.ToString() : "";
            string ilce = comboBoxIlce.SelectedItem != null ? comboBoxIlce.SelectedItem.ToString() : "";
            string odaSayisi = textBox3.Text != null ? textBox3.Text : "";

            string evTuru = EvTurleri.SelectedItem != null ? EvTurleri.SelectedItem.ToString() : "";
            string emlakNo = textBox1.Text != null ? textBox1.Text : "";
            string alan = textBox4.Text != null ? textBox4.Text : "";

            listView1.Items.Clear();

            // Tüm kayıtları oku ve filtrele
            List<string> filtrelenenKayitlar = new List<string>();
            if (satilikmi)
            {
                filtrelenenKayitlar.AddRange(Filtrele("satilik.txt", emlakNo, aktif, odaSayisi, evTuru, alan, sehir, ilce));

            }
            if (kirami)
            {
                filtrelenenKayitlar.AddRange(Filtrele("kiralik.txt", emlakNo, aktif, odaSayisi, evTuru, alan, sehir, ilce));

            }

            // Filtrelenen kayıtları ListView'e ekle
            foreach (string kayit in filtrelenenKayitlar)
            {
                string[] kayitBilgileri = kayit.Split(',');
                ListViewItem item = new ListViewItem(kayitBilgileri);
                listView1.Items.Add(item);
            }
        }


        IEnumerable<string> Filtrele(string dosyaAdi, string emlakNo, bool aktif, string odaSayisi, string Evturu, string alan, string sehir, string ilce)
        {
            List<string> filtrelenenKayitlar = new List<string>();

            if (File.Exists(dosyaAdi))
            {
                using (StreamReader reader = new StreamReader(dosyaAdi))
                {
                    while (!reader.EndOfStream)
                    {
                        string satir = reader.ReadLine();
                        string[] satirBilgileri = satir.Split(',');

                        if (

                                 (string.IsNullOrEmpty(emlakNo) || satirBilgileri[0].Equals(emlakNo, StringComparison.OrdinalIgnoreCase)) &&
                            (string.IsNullOrEmpty(odaSayisi) || satirBilgileri[5].Equals(odaSayisi, StringComparison.OrdinalIgnoreCase)) &&
                            (string.IsNullOrEmpty(Evturu) || satirBilgileri[11].Equals(Evturu, StringComparison.OrdinalIgnoreCase)) &&
                           (string.IsNullOrEmpty(alan) || satirBilgileri[7].Equals(alan, StringComparison.OrdinalIgnoreCase)) &&
                            (string.IsNullOrEmpty(sehir) || satirBilgileri[9].Equals(sehir, StringComparison.OrdinalIgnoreCase)) &&
                             (string.IsNullOrEmpty(ilce) || satirBilgileri[10].Equals(ilce, StringComparison.OrdinalIgnoreCase)) &&
                            ((aktif == true && satirBilgileri[2] == "Aktif") || (aktif == false && satirBilgileri[2] == "Pasif")))

                        {
                            filtrelenenKayitlar.Add(satir);
                        }
                    }
                }
            }

            return filtrelenenKayitlar;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EvEkleEkrani evEkleform = new EvEkleEkrani();
            evEkleform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            VerileriListeyeEkle("satilik.txt");
            VerileriListeyeEkle("kiralik.txt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FiltreleVeListeyeEkle();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedRow = listView1.SelectedItems[0];
                 OrtakBilgiler.emlakNo = selectedRow.SubItems[0].Text;

                DuzenlemeEkrani editForm = new DuzenlemeEkrani();

                // Düzenleme formunu göster
                DialogResult result = editForm.ShowDialog();

               
            }
        }
    }
}
