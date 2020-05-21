using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kitapeviApp
{
    public class Program
    {
        private static List<Kitaplik> kitapList = new List<Kitaplik>();
        private static int tableWidth = 100;

        static void Main(string[] args)
        {
            Calistir();
        }

        public static void Calistir()
        {
            try
            {
                Console.Clear();


                Console.WriteLine(
                    "\nKitap Takip Programı" +
                    "\n======== Menu ========\n" +
                    "1-)Kitap ekleme işlemi\n" +
                    "2-)Kitap Bilgileri Görüntüle" +
                    "\n\nSeçiminizi Yapınız(1/2):");


                int secim = int.Parse(Console.ReadLine());

                if (secim == 1 || secim == 2)
                {
                    Console.WriteLine("İşleminize Devam Ediliyor...");
                    switch (secim)
                    {
                        case 1:

                            Console.WriteLine(
                                "Kitap Ekleme İşlemleri\n\n" +
                                "Kaç Adet Kitap Eklenecek?");

                            int adet = int.Parse(Console.ReadLine()) + 1;

                            for (int i = 1; i < adet; i++)
                            {
                                try
                                {
                                    Console.Write("{0}.kitabın adı: ", i);
                                    string kitabinAdi = Console.ReadLine();

                                    Console.Write("{0}.Yazarın adı: ", i);
                                    string yazarinAdi = Console.ReadLine();

                                    Console.Write("{0}.Kitabın Türü: ", i);
                                    string kitabinTuru = Console.ReadLine();

                                    Console.Write("{0}.Kitabın Yılı (GÜN/AY/YIL) : ", i);
                                    DateTime kitabinYili = Convert.ToDateTime(Console.ReadLine());

                                    if (kitabinYili > DateTime.Now)
                                    {
                                        Console.WriteLine("Girdiğiniz tarih bugünden ileri bir tarih olamaz, lütfen tekrar giriniz.");

                                        Console.Write("{0}.Kitabın Yılı (GÜN/AY/YIL) : ", i);
                                        kitabinYili = Convert.ToDateTime(Console.ReadLine());
                                    }


                                    kitapList.Add(new Kitaplik()
                                    {
                                        KitapAdi = kitabinAdi,
                                        YazarAdi = yazarinAdi,
                                        KitapTuru = kitabinTuru,
                                        KitapYili = kitabinYili
                                    });
                                }
                                catch (FormatException)
                                {

                                    Console.WriteLine("Tarih Formatını GG.AA.YYYY olarak giriniz");
                                }

                            }

                            foreach (var kitap in kitapList)
                            {
                                string deger =
                                    kitap.KitapAdi +
                                    "," + kitap.YazarAdi +
                                    "," + kitap.KitapTuru +
                                    "," + kitap.KitapYili.ToString("dd/MM/yyyy") +
                                    "\n";

                                FSYazdir(deger);
                            }

                            /*
                             //Dizi test okey ! 
                            foreach (var item in kitapekle)
                            {
                                Console.WriteLine("{0}", item);
                            }*/

                            AnamenuSorusu("Kitap Ekleme İşlemi Başarıyla Tamamlandı !");

                            break;
                        //-----------------------------------------------
                        case 2:
                            FSOku();
                            AnamenuSorusu("Kitap Listeleme işlemi Başarıyla Tamamlandı !");
                            break;
                            //-----------------------------------------------
                            /*default:
                                Console.WriteLine("\nHatalı seçim.");
                                Calistir();
                                break;*/

                    }
                }
                else if (secim <= 0 || secim >= 3)
                {
                    Console.WriteLine("Lütfen 1 ya da 2 olarak yapmak istediğiniz Menu Aşamasını Seçiniz!");
                    Calistir();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Sadece Rakam Giriniz!");
                Calistir();

            }
            Console.ReadKey();
        }

        private static void AnamenuSorusu(string mesaj)
        {
            Console.WriteLine(mesaj);
            Console.WriteLine("Ana menüye devam etmek istiyor musunuz? (E/H)");
            char devammi = char.Parse(Console.ReadLine().ToLower());

            if (devammi == 'e')
            {
                Calistir();
            }
            else
            {
                Console.WriteLine("güle güle...");
            }
        }

        private static void FSYazdir(string deger)
        {
            using (FileStream fs = File.Open("dosya.txt", FileMode.Append))
            {
                byte[] degerEkle = new UTF8Encoding(true).GetBytes(deger);
                fs.Write(degerEkle, 0, degerEkle.Length);
            }
        }
        //-----------okuma/tablo bölümü-------------
        private static void FSOku()
        {
            Console.Clear();
            Console.WriteLine(" ");
            PrintRow("Sıra", "Kitap Adı", "Yazar Adı", "Kitap Türü", "Kitap Yılı");
            Console.WriteLine(new string('-', tableWidth));

            int counter = 1;
            string line;

            using (StreamReader fs = new StreamReader("dosya.txt"))
            {
                while ((line = fs.ReadLine()) != null)
                {
                    string[] lines = line.Split(',');
                    PrintRow(counter.ToString(), lines[0], lines[1], lines[2], lines[3]);
                    Console.WriteLine(new string('-', tableWidth));
                    counter++;
                }
            }
        }
        private static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }
        private static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        //------------okuma/tablo bölümü-------------


        public class Kitaplik
        {
            public string KitapAdi { get; set; }
            public string YazarAdi { get; set; }
            public string KitapTuru { get; set; }
            public DateTime KitapYili { get; set; }
        }
    }
}
