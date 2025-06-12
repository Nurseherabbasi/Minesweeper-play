namespace Minesweeper
{
    internal class Hucre
    {
        public bool MayinVarMi { get; set; }
        public bool AcildiMi { get; set; }
        public bool IsaretliMi { get; set; }
        public int CevreMayinSayisi { get; set; }

        public Hucre()
        {
            MayinVarMi = false;
            AcildiMi = false;
            IsaretliMi = false;
            CevreMayinSayisi = 0;
        }

        public override string ToString()
        {
            if (IsaretliMi) return "⚑";
            if (!AcildiMi) return "■";
            if (MayinVarMi) return "*";
            return CevreMayinSayisi == 0 ? " " : CevreMayinSayisi.ToString();
        }
    }
}
