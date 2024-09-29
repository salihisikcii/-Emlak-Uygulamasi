using ClassLibrary1;
using System;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApp
{
    public partial class EvEkleEkrani : Form
    {



        public EvEkleEkrani()
        {
            InitializeComponent();



        }



        private void EvEkleEkrani_Load(object sender, EventArgs e)
        {
            Evturleri.Items.AddRange(Enum.GetNames(typeof(Ev.evTuru)));
            Evturleri.SelectedIndex = 0;

            string[] iller = File.ReadAllLines("iller.txt");
            foreach (var il in iller)
            {
                comboBoxSehir.Items.Add(il);
            }
            OrtakBilgiler.DosyadanKayitlariYukle();
        }

        private void comboBoxSehir_SelectedIndexChanged(object sender, EventArgs e)
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

        private void textBoxALL(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrtakBilgiler.evTuru = Evturleri.SelectedItem.ToString();
        }



        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            OrtakBilgiler.yapimTarihi = dateTimePicker1.Value;

        }

        private void comboBoxIlce_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrtakBilgiler.ilce = comboBoxIlce.SelectedItem.ToString();

        }

        bool GirisAlanlariniDogrula()
        {

            // Gerekli alanların boş olup olmadığını veya seçilip seçilmediğini kontrol et
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                Evturleri.SelectedItem == null ||
                string.IsNullOrWhiteSpace(richTextBox1.Text) ||
                comboBoxIlce.SelectedItem == null ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                return false;
            }

            // Gerekirse daha fazla doğrulama mantığı ekleyin

            return true;
        }

        private void button1_Click(object sender, EventArgs e)

        {
            if (!GirisAlanlariniDogrula())
            {
                MessageBox.Show("Hatalı! Lütfen boş yerleri doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
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

                OrtakBilgiler.binaAdi = textBox1.Text;
                OrtakBilgiler.katNumarasi = int.Parse(textBox2.Text);
                OrtakBilgiler.odaSayisi = int.Parse(textBox3.Text);
                OrtakBilgiler.evinAlani = int.Parse(textBox4.Text);
                OrtakBilgiler.evTuru = Evturleri.SelectedItem.ToString();
                OrtakBilgiler.adres = richTextBox1.Text;
                OrtakBilgiler.ilce = comboBoxIlce.SelectedItem.ToString();
                OrtakBilgiler.yapimTarihi = dateTimePicker1.Value;
                OrtakBilgiler.fiyat = int.Parse(textBox5.Text);
                bool ayniKayitVarMi = false;

                if (OrtakBilgiler.satilikmikirami)
                {

                    if (!ayniKayitVarMi)
                    {
                        OrtakBilgiler.satilikEvEmlakNumarasi = OrtakBilgiler.YeniSatilikEvEmlakNumarasiAl();
                        OrtakBilgiler.satiliKiralik = "Satılık";
                        SatilikEv yeniSatilikEv = new SatilikEv(OrtakBilgiler.binaAdi, OrtakBilgiler.odaSayisi, OrtakBilgiler.katNumarasi, OrtakBilgiler.secilenIl, OrtakBilgiler.ilce, OrtakBilgiler.adres, OrtakBilgiler.evinAlani, OrtakBilgiler.yapimTarihi, OrtakBilgiler.satilikEvEmlakNumarasi, OrtakBilgiler.aktif, OrtakBilgiler.fiyat,OrtakBilgiler.evTuru, OrtakBilgiler.satiliKiralik);
                        OrtakBilgiler.binaYasi  =    yeniSatilikEv.Evinyasi;
                        OrtakBilgiler.satilikEvListesi.Add(yeniSatilikEv);
                        label12.Text = yeniSatilikEv.EVbilgileri();
                        VerileriKaydet();
                    }
                    else
                    {
                        MessageBox.Show("Aynı kayıttan birden fazla olamaz!");
                    }
                }
                else
                {
                    if (!ayniKayitVarMi)
                    {
                        OrtakBilgiler.kiralikEvEmlakNumarasi = OrtakBilgiler.YeniKiralikEvEmlakNumarasiAl();
                        OrtakBilgiler.satiliKiralik = "Kiralık";
                        KiralikEv yeniKiralikEv = new KiralikEv(OrtakBilgiler.binaAdi, OrtakBilgiler.odaSayisi, OrtakBilgiler.katNumarasi, OrtakBilgiler.secilenIl, OrtakBilgiler.ilce, OrtakBilgiler.adres, OrtakBilgiler.evinAlani, OrtakBilgiler.yapimTarihi, OrtakBilgiler.kiralikEvEmlakNumarasi, OrtakBilgiler.aktif, OrtakBilgiler.fiyat,OrtakBilgiler.evTuru,OrtakBilgiler.satiliKiralik);
                        OrtakBilgiler.kiralikEvListesi.Add(yeniKiralikEv);
                        label12.Text = yeniKiralikEv.EVbilgileri();
                        VerileriKaydet();
                    }
                    else
                    {
                        MessageBox.Show("Aynı kayıttan birden fazla olamaz!");
                    }
                }
            }
        }
        void VerileriKaydet()
        {
            string aktif = OrtakBilgiler.aktif ? "Aktif" : "Pasif";


            if (OrtakBilgiler.satilikmikirami)
            {
                using (StreamWriter satilikWriter = new StreamWriter("satilik.txt", true))
                {
                    satilikWriter.WriteLine($"{OrtakBilgiler.satilikEvEmlakNumarasi},{OrtakBilgiler.satiliKiralik},{aktif},{OrtakBilgiler.binaAdi},{OrtakBilgiler.fiyat},{OrtakBilgiler.odaSayisi},{OrtakBilgiler.katNumarasi},{OrtakBilgiler.evinAlani},{OrtakBilgiler.binaYasi},{OrtakBilgiler.secilenIl},{OrtakBilgiler.ilce},{OrtakBilgiler.evTuru},{OrtakBilgiler.adres}");
                }
            }
            else
            {
                using (StreamWriter kiralikWriter = new StreamWriter("kiralik.txt", true))
                {
                    kiralikWriter.WriteLine($"{OrtakBilgiler.kiralikEvEmlakNumarasi},{OrtakBilgiler.satiliKiralik},{aktif},{OrtakBilgiler.binaAdi},{OrtakBilgiler.fiyat},{OrtakBilgiler.odaSayisi},{OrtakBilgiler.katNumarasi},{OrtakBilgiler.evinAlani},{OrtakBilgiler.binaYasi},{OrtakBilgiler.secilenIl},{OrtakBilgiler.ilce},{OrtakBilgiler.evTuru},{OrtakBilgiler.adres}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Anaekran anaEkranform = new Anaekran();
            anaEkranform.Show();
            this.Hide();
        }
    }
}
