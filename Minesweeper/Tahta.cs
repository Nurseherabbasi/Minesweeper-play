using System;

namespace Minesweeper
{
    internal class Tahta
    {
        private readonly int satir;
        private readonly int sutun;
        private readonly int mayinSayisi;
        public Hucre[,] Alan { get; private set; }
        private readonly Random rnd = new();

        public Tahta(int satir, int sutun, int mayinSayisi)
        {
            this.satir = satir;
            this.sutun = sutun;
            this.mayinSayisi = mayinSayisi;
            Alan = new Hucre[satir, sutun];
            AlanOlustur();
            MayinlariYerlestir();
            CevreleriHesapla();
        }

        private void AlanOlustur()
        {
            for (int i = 0; i < satir; i++)
                for (int j = 0; j < sutun; j++)
                    Alan[i, j] = new Hucre();
        }

        private void MayinlariYerlestir()
        {
            int sayac = 0;
            while (sayac < mayinSayisi)
            {
                int i = rnd.Next(satir);
                int j = rnd.Next(sutun);
                if (!Alan[i, j].MayinVarMi)
                {
                    Alan[i, j].MayinVarMi = true;
                    sayac++;
                }
            }
        }

        private void CevreleriHesapla()
        {
            for (int i = 0; i < satir; i++)
            {
                for (int j = 0; j < sutun; j++)
                {
                    if (Alan[i, j].MayinVarMi) continue;

                    int sayi = 0;
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            int ni = i + x;
                            int nj = j + y;
                            if (ni >= 0 && ni < satir && nj >= 0 && nj < sutun && Alan[ni, nj].MayinVarMi)
                                sayi++;
                        }
                    }
                    Alan[i, j].CevreMayinSayisi = sayi;
                }
            }
        }

        public void Goster()
        {
            Console.Clear();
            Console.Write("   ");
            for (int j = 0; j < sutun; j++)
                Console.Write(j + " ");
            Console.WriteLine();

            for (int i = 0; i < satir; i++)
            {
                Console.Write(i + " ");
                if (i < 10) Console.Write(" ");
                for (int j = 0; j < sutun; j++)
                    Console.Write(Alan[i, j] + " ");
                Console.WriteLine();
            }
        }

        public void HucreAc(int x, int y)
        {
            if (x < 0 || x >= satir || y < 0 || y >= sutun) return;
            var hucre = Alan[x, y];
            if (hucre.AcildiMi || hucre.IsaretliMi) return;

            hucre.AcildiMi = true;

            if (hucre.CevreMayinSayisi == 0 && !hucre.MayinVarMi)
            {
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                        HucreAc(x + dx, y + dy);
            }
        }

        public bool KazanildiMi()
        {
            for (int i = 0; i < satir; i++)
                for (int j = 0; j < sutun; j++)
                    if (!Alan[i, j].MayinVarMi && !Alan[i, j].AcildiMi)
                        return false;
            return true;
        }
    }
}
