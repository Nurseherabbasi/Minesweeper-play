using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Oyun oyun = new();
            oyun.Baslat();
        }
    }
}

