using MinesweeperForm;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MinesweeperForm.cs
{
    public partial class MinesweeperForm : Form
    {
        private Board gameBoard;
        private int timeElapsed;
        private DifficultyLevel currentDifficulty;
        private enum DifficultyLevel
        {
            Beginner,
            Intermediate,
            Expert,
            Custom
        }
        public MinesweeperForm()
        {
            InitializeComponent();
            InitializeGame(DifficultyLevel.Beginner);
        }
        private void InitializeGame(DifficultyLevel difficulty)
        {
            currentDifficulty = difficulty;

            switch (difficulty)
            {
                case DifficultyLevel.Beginner:
                    gameBoard = new Board(9, 9, 10);
                    break;
                case DifficultyLevel.Intermediate:
                    gameBoard = new Board(16, 16, 40);
                    break;
                case DifficultyLevel.Expert:
                    gameBoard = new Board(30, 16, 99);
                    break;
                case DifficultyLevel.Custom:
                    gameBoard = new Board(16, 16, 40);
                    break;
            }
            // Oyun başlatma kodları (Örnek başlangıç ayarları)
            gameBoard = new Board(9, 9, 10);
            timeElapsed = 0;
            UpdateStatusLabels();
            SetupGameBoard();
            tmrGameTimer.Start();
        }
        private void UpdateStatusLabels()
        {
            tsslMinesLeft.Text = $"Mayınlar: {gameBoard.MineCount - gameBoard.FlaggedMineCount}";
            tsslTimeElapsed.Text = $"Zaman: {timeElapsed}";
        }
            private void SetupGameBoard()
        {
            pnlGameBoard.Controls.Clear();
            pnlGameBoard.Size = new Size(gameBoard.Width * 30, gameBoard.Height * 30);

            this.ClientSize = new Size(
                Math.Max(pnlGameBoard.Width + 20, 400),
                pnlGameBoard.Height + msMainMenu.Height + ssGameStatus.Height + 40);

            pnlGameBoard.Location = new Point(
                (this.ClientSize.Width - pnlGameBoard.Width) / 2,
                msMainMenu.Height + 10);

            for (int x = 0; x < gameBoard.Width; x++)
            {
                for (int y = 0; y < gameBoard.Height; y++)
                {
                    var cell = gameBoard.Cells[x, y];
                    var button = new Button
                    {
                        Size = new Size(30, 30),
                        Location = new Point(x * 30, y * 30),
                        Tag = cell,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = SystemColors.Control,
                        Font = new Font("Arial", 10, FontStyle.Bold)
                    };

                    cell.Bounds = new Rectangle(x * 30, y * 30, 30, 30);

                    button.MouseUp += Cell_MouseUp;
                    pnlGameBoard.Controls.Add(button);
                }
            }
        }
                private void Cell_MouseUp(object sender, MouseEventArgs e)
        {
            if (gameBoard.GameOver || gameBoard.GameWon)
                return;

            var button = (Button)sender;
            var cell = (Cell)button.Tag;

            if (e.Button == MouseButtons.Left && !cell.IsFlagged)
            {
                RevealCell(cell);
            }
            else if (e.Button == MouseButtons.Right)
            {
                ToggleFlag(cell, button);
            }

            CheckGameStatus();
        }
        private void RevealCell(Cell cell)
        {
            if (cell.IsRevealed)
                return;

            cell.IsRevealed = true;
            gameBoard.RevealedCount++;

            var button = GetButtonForCell(cell);
            button.FlatAppearance.BorderSize = 1;
            button.BackColor = SystemColors.ControlLight;

            if (cell.IsMine)
            {
                button.Text = "💣";
                button.BackColor = Color.Red;
                GameOver(false);
            }
            else if (cell.AdjacentMines > 0)
            {
                button.Text = cell.AdjacentMines.ToString();
                button.ForeColor = GetNumberColor(cell.AdjacentMines);
            }
            else
            {
                RevealAdjacentCells(cell);
            }
            UpdateStatusLabels();
        }
        private void ToggleFlag(Cell cell, Button button)
        {
            if (!cell.IsRevealed)
            {
                cell.IsFlagged = !cell.IsFlagged;
                button.Text = cell.IsFlagged ? "🚩" : "";
                if (cell.IsMine) gameBoard.FlaggedMineCount += cell.IsFlagged ? 1 : -1;
                UpdateStatusLabels();
            }
        }
        private Color GetNumberColor(int number)
        {
            switch (number)
            {
                case 1: return Color.Blue;
                case 2: return Color.Green;
                case 3: return Color.Red;
                case 4: return Color.DarkBlue;
                case 5: return Color.Brown;
                case 6: return Color.Teal;
                case 7: return Color.Black;
                case 8: return Color.Gray;
                default: return Color.Black;
            }
        }

        private void RevealAdjacentCells(Cell cell)
        {
            for (int x = Math.Max(0, cell.Row - 1); x <= Math.Min(gameBoard.Width - 1, cell.Row + 1); x++)
            {
                for (int y = Math.Max(0, cell.Column - 1); y <= Math.Min(gameBoard.Height - 1, cell.Column + 1); y++)
                {
                    if (!gameBoard.Cells[x, y].IsRevealed && !gameBoard.Cells[x, y].IsFlagged)
                    {
                        RevealCell(gameBoard.Cells[x, y]);
                    }
                }
            }
        }
        

        private Button GetButtonForCell(Cell cell)
        {
            foreach (Control control in pnlGameBoard.Controls)
            {
                if (control is Button button && button.Tag == cell)
                {
                    return button;
                }
            }
            return null;
        }

        private void CheckGameStatus()
        {
            if (gameBoard.GameOver || gameBoard.GameWon)
                return;

            if (gameBoard.RevealedCount == (gameBoard.Width * gameBoard.Height - gameBoard.MineCount))
            {
                GameOver(true);
            }
        }

        private void GameOver(bool isWin)
        {
            tmrGameTimer.Stop();
            gameBoard.GameOver = true;

            if (isWin)
            {
                gameBoard.GameWon = true;
                MessageBox.Show("Tebrikler! Tüm mayınları buldunuz!", "Oyun Bitti",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                MarkAllMines();
            }
            else
            {
                MessageBox.Show("Mayına bastınız! Oyun bitti.", "Oyun Bitti",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                RevealAllMines();
            }
        }

        private void MarkAllMines()
        {
            for (int x = 0; x < gameBoard.Width; x++)
            {
                for (int y = 0; y < gameBoard.Height; y++)
                {
                    var cell = gameBoard.Cells[x, y];
                    if (cell.IsMine && !cell.IsFlagged)
                    {
                        var button = GetButtonForCell(cell);
                        button.Text = "🚩";
                        cell.IsFlagged = true;
                    }
                }
            }
        }

        private void RevealAllMines()
        {
            for (int x = 0; x < gameBoard.Width; x++)
            {
                for (int y = 0; y < gameBoard.Height; y++)
                {
                    var cell = gameBoard.Cells[x, y];
                    if (cell.IsMine && !cell.IsFlagged)
                    {
                        var button = GetButtonForCell(cell);
                        button.Text = "💣";
                        button.BackColor = Color.LightGray;
                    }
                }
            }
        }

        private void tmrGameTimer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            tsslTimeElapsed.Text = $"Zaman: {timeElapsed}";
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeGame(currentDifficulty);
        }

        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentDifficulty = DifficultyLevel.Beginner;
            InitializeGame(currentDifficulty);
        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentDifficulty = DifficultyLevel.Intermediate;
            InitializeGame(currentDifficulty);
        }

        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentDifficulty = DifficultyLevel.Expert;
            InitializeGame(currentDifficulty);
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var customDialog = new CustomDifficultyDialog())
            {
                if (customDialog.ShowDialog() == DialogResult.OK)
                {
                    gameBoard = new Board(customDialog.SelectedWidth, customDialog.SelectedHeight, customDialog.SelectedMines);
                    currentDifficulty = DifficultyLevel.Custom;
                    InitializeGame(currentDifficulty);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                            "Mayın Tarlası Nasıl Oynanır:\n\n" +
                            "1. Sol tıkla: Hücreyi aç\n" +
                            "2. Sağ tıkla: Bayrak koy/kaldır\n" +
                            "3. Amaç: Tüm mayın olmayan hücreleri açmak\n" +
                            "4. Açılan hücredeki sayı, komşu mayın sayısını gösterir\n" +
                            "5. Mayına basarsanız oyun biter!",
                            "Nasıl Oynanır", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Mayın Tarlası\n\n" +
                "Versiyon 1.0\n" +
                "© 2025 - Tüm hakları saklıdır",
                "Hakkında", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
    
    

//private void UpdateStatusLabels()
//{
    
//}

       
    


// void Form1_Load(object sender, EventArgs e)
        //{

        //}
    

