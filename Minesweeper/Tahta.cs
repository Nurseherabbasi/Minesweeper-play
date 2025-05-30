using System;

namespace Minesweeper
{
    internal class Tahta
    {
        public int Satir { get; }
        public int Sutun { get; }
        public int MayinSayisi { get; }
        public Hucre[,] Hucreler { get; }

        private Random random = new Random();

        public Tahta(int satir, int sutun, int mayinSayisi)
        {
            Satir = satir;
            Sutun = sutun;
            MayinSayisi = mayinSayisi;
            Hucreler = new Hucre[Satir, Sutun];

            for (int i = 0; i < Satir; i++)
            {
                for (int j = 0; j < Sutun; j++)
                {
                    Hucreler[i, j] = new Hucre();
                }
            }

            MayinlariYerlestir();
            CevreleriHesapla();
        }

        private void MayinlariYerlestir()
        {
            int yerlestirilen = 0;
            while (yerlestirilen < MayinSayisi)
            {
                int x = random.Next(Satir);
                int y = random.Next(Sutun);

                if (!Hucreler[x, y].MayinVarMi)
                {
                    Hucreler[x, y].MayinVarMi = true;
                    yerlestirilen++;
                }
            }
        }

        private void CevreleriHesapla()
        {
            for (int i = 0; i < Satir; i++)
            {
                for (int j = 0; j < Sutun; j++)
                {
                    if (Hucreler[i, j].MayinVarMi) continue;

                    int sayac = 0;
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            int ni = i + dx;
                            int nj = j + dy;

                            if (ni >= 0 && ni < Satir && nj >= 0 && nj < Sutun && Hucreler[ni, nj].MayinVarMi)
                                sayac++;
                        }
                    }

                    Hucreler[i, j].CevreMayinSayisi = sayac;
                }
            }
        }

        public void HucreyiAc(int x, int y)
        {
            if (x < 0 || x >= Satir || y < 0 || y >= Sutun) return;
            if (Hucreler[x, y].AcildiMi || Hucreler[x, y].IsaretliMi) return;

            Hucreler[x, y].AcildiMi = true;

            if (Hucreler[x, y].CevreMayinSayisi == 0 && !Hucreler[x, y].MayinVarMi)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx != 0 || dy != 0)
                        {
                            HucreyiAc(x + dx, y + dy);
                        }
                    }
                }
            }
        }

        public bool KazanildiMi()
        {
            for (int i = 0; i < Satir; i++)
            {
                for (int j = 0; j < Sutun; j++)
                {
                    if (!Hucreler[i, j].MayinVarMi && !Hucreler[i, j].AcildiMi)
                        return false;
                }
            }
            return true;
        }

        public void Goster(bool gizli = true)
        {
            Console.Write("   ");
            for (int j = 0; j < Sutun; j++) Console.Write(j + " ");
            Console.WriteLine();

            for (int i = 0; i < Satir; i++)
            {
                Console.Write(i + " |");
                for (int j = 0; j < Sutun; j++)
                {
                    var h = Hucreler[i, j];
                    if (h.AcildiMi)
                    {
                        Console.Write(h.MayinVarMi ? "*" : (h.CevreMayinSayisi == 0 ? " " : h.CevreMayinSayisi.ToString()));
                    }
                    else if (h.IsaretliMi)
                    {
                        Console.Write("F");
                    }
                    else
                    {
                        Console.Write("#");
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}
