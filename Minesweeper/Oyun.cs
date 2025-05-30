using System;

namespace Minesweeper
{
    internal class Oyun
    {
        private Tahta tahta;

        public void Baslat()
        {
            Console.WriteLine("Mayın Tarlası Oyununa Hoş Geldiniz!");
            tahta = new Tahta(9, 9, 10);

            while (true)
            {
                Console.Clear();
                tahta.Goster();

                Console.Write("Koordinat gir (örnek: 3 4): ");
                var input = Console.ReadLine();
                var parcalar = input.Split();

                if (parcalar.Length != 2 || !int.TryParse(parcalar[0], out int x) || !int.TryParse(parcalar[1], out int y))
                {
                    Console.WriteLine("Geçersiz giriş. Enter ile devam et.");
                    Console.ReadLine();
                    continue;
                }

                var secilenHucre = tahta.Hucreler[x, y];

                if (secilenHucre.MayinVarMi)
                {
                    Console.Clear();
                    tahta.Goster(gizli: false);
                    Console.WriteLine("💥 Mayına bastınız! Oyun bitti.");
                    break;
                }

                tahta.HucreyiAc(x, y);

                if (tahta.KazanildiMi())
                {
                    Console.Clear();
                    tahta.Goster(gizli: false);
                    Console.WriteLine("🎉 Tebrikler! Tüm mayınsız hücreleri açtınız.");
                    break;
                }
            }

            Console.WriteLine("Çıkmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
