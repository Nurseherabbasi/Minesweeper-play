using System;

namespace ProfessionalMinesweeper
{
    public class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int MineCount { get; private set; }
        public Cell[,] Cells { get; private set; }
        public bool GameOver { get; set; }
        public bool GameWon { get; set; }
        public int RevealedCount { get; set; }
        public int FlaggedMineCount { get; set; }

        public Board(int width, int height, int mineCount)
        {
            Width = width;
            Height = height;
            MineCount = mineCount;
            Cells = new Cell[width, height];
            GameOver = false;
            GameWon = false;
            RevealedCount = 0;
            FlaggedMineCount = 0;

            InitializeCells();
            PlaceMines();
            CalculateAdjacentMines();
        }

        private void InitializeCells()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cells[x, y] = new Cell(x, y);
                }
            }
        }

        private void PlaceMines()
        {
            Random random = new Random();
            int minesPlaced = 0;

            while (minesPlaced < MineCount)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);

                if (!Cells[x, y].IsMine)
                {
                    Cells[x, y].IsMine = true;
                    minesPlaced++;
                }
            }
        }

        private void CalculateAdjacentMines()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (!Cells[x, y].IsMine)
                    {
                        Cells[x, y].AdjacentMines = CountAdjacentMines(x, y);
                    }
                }
            }
        }

        private int CountAdjacentMines(int x, int y)
        {
            int count = 0;

            for (int i = Math.Max(0, x - 1); i <= Math.Min(Width - 1, x + 1); i++)
            {
                for (int j = Math.Max(0, y - 1); j <= Math.Min(Height - 1, y + 1); j++)
                {
                    if (Cells[i, j].IsMine)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}