using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Ev
    {
        public enum evTuru
        {
            Daire,
            Bahceli,
            Dubleks,
            Mustakil,
        }
        public string binaAdi;
        public string satilikmiKirami;

        public int odaSayisi;
        public int katNumarasi;
        public int evinAlani;
        public string sehir;
        public string ilce;
        public string adres;
        public DateTime yapimTarihi;
        public int evinYasi;
        public int emlakNumarasi;
        public bool aktif;




       

        public int OdaSayisi
        {
            get { return odaSayisi; }
            set
            {
                if (value < 0)
                {
                    odaSayisi = 0;
                    File.AppendAllText("log.txt", "Oda Sayısı Negatif Olamaz. Oda sayısı = 0 ");
                    File.AppendAllText("log.txt", DateTime.Now.ToString("dd.MM.yyy HH:mm"));
                    File.AppendAllText("log.txt", Environment.NewLine);
                }
                else
                {
                    odaSayisi = value;
                    File.AppendAllText("log.txt", $" Oda sayısı ={odaSayisi} ");
                    File.AppendAllText("log.txt", DateTime.Now.ToString("dd.MM.yyy HH:mm"));
                    File.AppendAllText("log.txt", Environment.NewLine);
                }

            }
        }
        public string EvTuru { get; set; }
        public string SatilikmiKirami { get; set; }

        public int KatNumarasi
        {
            get { return katNumarasi; }
            set
            {
                if (value < 0)
                {
                    katNumarasi = 0;
                    File.AppendAllText("log.txt", "Kat Sayısı Negatif Olamaz. kat sayısı = 0 ");
                    File.AppendAllText("log.txt", DateTime.Now.ToString("dd.MM.yyy HH:mm"));
                    File.AppendAllText("log.txt", Environment.NewLine);
                }
                else
                {
                    odaSayisi = value;
                    File.AppendAllText("log.txt", $" Kat sayısı ={katNumarasi} ");
                    File.AppendAllText("log.txt", DateTime.Now.ToString("dd.MM.yyy HH:mm"));
                    File.AppendAllText("log.txt", Environment.NewLine);
                }

            }
        }
        public int EvinAlani
        {
            get { return evinAlani; }
            set
            {
                if (value < 0)
                {
                    evinAlani = 0;
                    File.AppendAllText("log.txt", "Kat Sayısı Negatif Olamaz. kat sayısı = 0 ");
                    File.AppendAllText("log.txt", DateTime.Now.ToString("dd.MM.yyy HH:mm"));
                    File.AppendAllText("log.txt", Environment.NewLine);
                }
                else
                {
                    evinAlani = value;
                    File.AppendAllText("log.txt", $" Kat sayısı ={katNumarasi} ");
                    File.AppendAllText("log.txt", DateTime.Now.ToString("dd.MM.yyy HH:mm"));
                    File.AppendAllText("log.txt", Environment.NewLine);
                }

            }
        }
        public string EvSahibiAdi
        {
            get { return binaAdi; }
            set
            {
                binaAdi = value;

            }
        }
        public string Adres
        {
            get { return adres; }
            set
            {
                adres = value;

            }
        }
  
        public string Sehir
        {
            get { return sehir; }
            set
            {
                sehir = value;
            }
        }
        public string Ilce
        {
            get { return ilce; }
            set
            {
                ilce = value;
            }
        }
        public DateTime YapimTarihi
        {
            get { return yapimTarihi; }
            set
            {
                yapimTarihi = value;
                if (DateTime.Now < YapimTarihi)
                {
                    yapimTarihi = DateTime.Now;
                    File.AppendAllText("log.txt", "Evin Yapım Tarihi, Bu Günün Tarihinden İlerde Olamaz.");
                    File.AppendAllText("log.txt", DateTime.Now.ToString("dd.MM.yyy HH:mm"));
                    File.AppendAllText("log.txt", Environment.NewLine);
                }
            }
        }
        public int Evinyasi
        {
            get
            {
                evinYasi = DateTime.Now.Year - yapimTarihi.Year;
                return evinYasi;
            }
        }

        public int EmlakNumarasi { get => emlakNumarasi; set => emlakNumarasi = value; }

        public bool Aktif { get => aktif; set => aktif = value; }

        public Ev() { }
        public Ev(string adi, int odaSayisi, int katSayisi, string sehir, string ilce, string adres, int alan, DateTime yapimTarihi, int emlakNumarasi, bool aktif,string evTuru)
        {
            EmlakNumarasi = emlakNumarasi;
            Aktif = aktif;
            EvSahibiAdi = adi;
            OdaSayisi = odaSayisi;
            KatNumarasi = katSayisi;
            EvinAlani = alan;
            YapimTarihi = yapimTarihi;
            Sehir = sehir;
            Ilce = ilce;
            Adres = adres;
            EvTuru = evTuru;
        }



        public virtual string EVbilgileri()
        {
            string format = "Emlak Numarası: {0}, ilan durumu {1},Ev Adı: {2}, Oda sayısı: {3}, Kat sayısı: {4},Evin Alanı{5},Bina Yaşı{6},Sehir:{7},İlçe:{8},Adres:{9} ";
            string evbilgileri = string.Format(format, emlakNumarasi, (aktif ? "Aktif" : "Akif Değil"), binaAdi, odaSayisi, katNumarasi, evinAlani, evinYasi, Sehir, Ilce, Adres);
            return evbilgileri;
        }


    }

    public class KiralikEv : Ev
    {
        int kiraFiyati;
        public int KiraFiyati
        {
            get { return kiraFiyati; }
            set
            {
                kiraFiyati = value;
                if (kiraFiyati < 0)
                {
                    kiraFiyati = KiraTahmini();
                    File.AppendAllText("log.txt", "Evin Kira Fiyatı Negatif olamaz. ");
                    File.AppendAllText("log.txt", DateTime.Now.ToString("dd.MM.yyy HH:mm"));
                    File.AppendAllText("log.txt", Environment.NewLine);
                }


            }
        }

        public KiralikEv(string evadi, int odaSayisi, int katSayisi, string sehir, string ilce, string adres, int alan, DateTime yapimTarihi, int emlakNumarasi, bool aktif, int kiraFiyati, string evTuru, string satilikmikirami)
        {
            OdaSayisi = odaSayisi;
            EvSahibiAdi = evadi;
            KatNumarasi = katSayisi;
            Sehir = sehir;
            Ilce = ilce;
            EvinAlani = alan;
            Adres = adres;
            YapimTarihi = yapimTarihi;
            EmlakNumarasi = emlakNumarasi;
            Aktif = aktif;
            KiraFiyati = kiraFiyati;
            EvTuru = evTuru;
            SatilikmiKirami = satilikmikirami;
        }
        public KiralikEv(string evadi, int odaSayisi, int katSayisi, string sehir, string ilce, string adres, int alan, int emlakNumarasi, bool aktif, int kiraFiyati, string evTuru, string satilikmikirami)
        {
            OdaSayisi = odaSayisi;
            EvSahibiAdi = evadi;
            KatNumarasi = katSayisi;
            Sehir = sehir;
            Ilce = ilce;
            EvinAlani = alan;
            Adres = adres;
            EmlakNumarasi = emlakNumarasi;
            Aktif = aktif;
            KiraFiyati = kiraFiyati;
            EvTuru = evTuru;
            SatilikmiKirami = satilikmikirami;
        }
        public int KiraTahmini()
        {
            return OdaSayisi * 200;

        }
        public override string EVbilgileri()
        {
            string format = "Emlak Numarası: {0},Satılık/Kira{1} ilan durumu {2},Ev Adı: {3}, Kira bedeli:{4} Oda sayısı: {5}, Kat sayısı: {6},Evin Alanı{7},Bina Yaşı{8},Sehir:{9},İlçe:{10},Adres:{11} ";
            string evbilgileri = string.Format(format, EmlakNumarasi,SatilikmiKirami, (Aktif ? "Aktif" : "Akif Değil"), EvSahibiAdi, KiraFiyati, OdaSayisi, KatNumarasi, EvinAlani, Evinyasi, Sehir, Ilce, Adres);

            return evbilgileri;
        }
    }
    public class SatilikEv : Ev
    {
        int satilikFiyati;
        public int SatilikFiyati
        {
            get { return satilikFiyati; }
            set
            {
                satilikFiyati = value;
                if (satilikFiyati < 0)
                {
                    satilikFiyati = SatilikTahmini();
                    File.AppendAllText("log.txt", "Evin Kira Fiyatı Negatif olamaz. ");
                    File.AppendAllText("log.txt", DateTime.Now.ToString("dd.MM.yyy HH:mm"));
                    File.AppendAllText("log.txt", Environment.NewLine);
                }
            }
        }
        public SatilikEv() { }
        public SatilikEv(string evadi, int odaSayisi, int katSayisi, string sehir, string ilce, string adres, int alan, DateTime yapimTarihi, int emlakNumarasi, bool aktif, int satilikFiyati, string evTuru, string satilikmikirami)
        {
            OdaSayisi = odaSayisi;
            EmlakNumarasi = emlakNumarasi;
            EvSahibiAdi = evadi;
            KatNumarasi = katSayisi;
            Sehir = sehir;
            Ilce = ilce;
            EvinAlani = alan;
            Adres = adres;
            YapimTarihi = yapimTarihi;
            Aktif = aktif;
            SatilikFiyati = satilikFiyati;
            EvTuru = evTuru;
            SatilikmiKirami = satilikmikirami;
        }
        public SatilikEv(string evadi, int odaSayisi, int katSayisi, string sehir, string ilce, string adres, int alan, int emlakNumarasi, bool aktif, int satilikFiyati, string evTuru, string satilikmikirami)
        {
            OdaSayisi = odaSayisi;
            EmlakNumarasi = emlakNumarasi;
            EvSahibiAdi = evadi;
            KatNumarasi = katSayisi;
            Sehir = sehir;
            Ilce = ilce;
            EvinAlani = alan;
            Adres = adres;
            Aktif = aktif;
            SatilikFiyati = satilikFiyati;
            EvTuru = evTuru;
            SatilikmiKirami = satilikmikirami;
        }
        public int SatilikTahmini()
        {
            return OdaSayisi * 400000;

        }
        public override string EVbilgileri()
        {
            string format = "Emlak Numarası: {0},Satılık/Kira{1} ilan durumu {2},Ev Adı: {3}, Kira bedeli:{4} Oda sayısı: {5}, Kat sayısı: {6},Evin Alanı{7},Bina Yaşı{8},Sehir:{9},İlçe:{10},Adres:{11} ";
            string evbilgileri = string.Format(format, EmlakNumarasi, SatilikmiKirami, (Aktif ? "Aktif" : "Akif Değil"), EvSahibiAdi, SatilikFiyati, OdaSayisi, KatNumarasi, EvinAlani, Evinyasi, Sehir, Ilce, Adres);
            return evbilgileri;
        }
    }
}

