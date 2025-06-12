using System;

namespace Minesweeper
{
    internal class Oyun
    {
        private Tahta tahta;
        private bool oyunBitti = false;

        public void Baslat()
        {
            Console.WriteLine("Zorluk Seç (1: Kolay 9x9 10M, 2: Orta 16x16 40M, 3: Zor 16x30 99M): ");
            int secim = int.Parse(Console.ReadLine()!);
            int s = 9, t = 9, m = 10;
            if (secim == 2) { s = 16; t = 16; m = 40; }
            else if (secim == 3) { s = 16; t = 30; m = 99; }

            tahta = new Tahta(s, t, m);

            while (!oyunBitti)
            {
                tahta.Goster();
                Console.Write("Satır ve Sütun gir (örn: 3 4): ");
                string[] giris = Console.ReadLine()!.Split();
                int x = int.Parse(giris[0]);
                int y = int.Parse(giris[1]);

                if (tahta.Alan[x, y].MayinVarMi)
                {
                    oyunBitti = true;
                    Console.WriteLine("💥 Mayına bastınız! Oyun Bitti.");
                    break;
                }

                tahta.HucreAc(x, y);

                if (tahta.KazanildiMi())
                {
                    tahta.Goster();
                    Console.WriteLine("🎉 Tebrikler! Oyunu kazandınız.");
                    break;
                }
            }
        }
    }
}
