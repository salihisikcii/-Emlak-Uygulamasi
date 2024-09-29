using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public class OrtakBilgiler
    {
        private static HashSet<int> satilikEvEmlakNumaralari = new HashSet<int>();
        private static HashSet<int> kiralikEvEmlakNumaralari = new HashSet<int>();

        public static  List<SatilikEv> satilikEvListesi = new List<SatilikEv>();
        public static List<KiralikEv> kiralikEvListesi = new List<KiralikEv>();

        public static bool aktif;
        public static string aktifString;
        public static string emlakNo;
        public static string ilce;
        public static string secilenIl;
        public static string binaAdi;
        public static int katNumarasi;
        public static int odaSayisi;
        public static int evinAlani;
        public static string evTuru;
        public static string adres;
        public static bool satilikmikirami;
        public static string satiliKiralik;
        public static int binaYasi;
        public static int fiyat;
        public static int satilikEvEmlakNumarasi ;
        public static int kiralikEvEmlakNumarasi ;
        public static DateTime yapimTarihi;
        public static string yas;
       public static void DosyadanKayitlariYukle()
        {
            // Dosya varsa oku, yoksa yeni bir dosya oluştur
            if (File.Exists("satilik.txt"))
            {
                StreamReader satilikReader = new StreamReader("satilik.txt");
                while (!satilikReader.EndOfStream)
                {
                    string[] satir = satilikReader.ReadLine().Split(',');
                    satilikEvEmlakNumarasi = int.Parse(satir[0]);
                    satiliKiralik = (satir[1]);
                    aktifString = (satir[2]);
                    binaAdi = (satir[3]);
                    fiyat = int.Parse(satir[4]);
                    odaSayisi = int.Parse(satir[5]);
                    katNumarasi = int.Parse(satir[6]);
                    evinAlani = int.Parse(satir[7]);
                    yas = (satir[8]);
                    secilenIl = (satir[9]);
                    ilce = (satir[10]);
                    evTuru = (satir[11]);
                    adres = (satir[12]);

                    SatilikEv ev = new SatilikEv(binaAdi, odaSayisi, katNumarasi, secilenIl, ilce, adres, evinAlani, satilikEvEmlakNumarasi, aktif, fiyat,evTuru, satiliKiralik);
                    satilikEvListesi.Add(ev);
                    satilikEvEmlakNumaralari.Add(satilikEvEmlakNumarasi);

                }
                satilikReader.Close();
            }

            if (File.Exists("kiralik.txt"))
            {
                StreamReader kiralikReader = new StreamReader("kiralik.txt");
                while (!kiralikReader.EndOfStream)
                {
                    string[] satir = kiralikReader.ReadLine().Split(',');
                    kiralikEvEmlakNumarasi = int.Parse(satir[0]);
                    satiliKiralik = (satir[1]);
                    aktifString = (satir[2]);
                    binaAdi = (satir[3]);
                    fiyat = int.Parse(satir[4]);
                    odaSayisi = int.Parse(satir[5]);
                    katNumarasi = int.Parse(satir[6]);
                    evinAlani = int.Parse(satir[7]);
                    yas = (satir[8]);
                    secilenIl = (satir[9]);
                    ilce = (satir[10]);
                    evTuru = (satir[11]);
                    adres = (satir[12]);

                    KiralikEv ev = new KiralikEv(binaAdi, odaSayisi, katNumarasi, secilenIl, ilce, adres, evinAlani, kiralikEvEmlakNumarasi, aktif, fiyat,evTuru, satiliKiralik);
                    kiralikEvListesi.Add(ev);
                    kiralikEvEmlakNumaralari.Add(kiralikEvEmlakNumarasi);

                }
                kiralikReader.Close();

            }
        }
        public static int YeniSatilikEvEmlakNumarasiAl()
        {
            int yeniNumara = 1;
            while (satilikEvEmlakNumaralari.Contains(yeniNumara))
            {
                yeniNumara++;
            }
            satilikEvEmlakNumaralari.Add(yeniNumara);
            return yeniNumara;
        }

        public static int YeniKiralikEvEmlakNumarasiAl()
        {
            int yeniNumara = 500;
            while (kiralikEvEmlakNumaralari.Contains(yeniNumara))
            {
                yeniNumara++;
            }
            kiralikEvEmlakNumaralari.Add(yeniNumara);
            return yeniNumara;
        }
    }
}
