using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MainForm : Form
    {
        int rows = 10;
        int cols = 10;
        Button[,] buttons;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            buttons = new Button[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button btn = new Button();
                    btn.Width = 30;
                    btn.Height = 30;
                    btn.Left = j * 30;
                    btn.Top = i * 30;
                    btn.Tag = new Point(i, j);
                    btn.Click += Cell_Click;

                    buttons[i, j] = btn;
                    this.Controls.Add(btn);
                }
            }

            this.ClientSize = new Size(cols * 30 + 10, rows * 30 + 10);
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            Point position = (Point)clickedButton.Tag;
            int row = position.X;
            int col = position.Y;

            MessageBox.Show($"Tıkladığın hücre: ({row}, {col})");
        }
    }
}
