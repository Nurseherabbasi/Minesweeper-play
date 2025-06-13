using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProfessionalMinesweeper
{
    public partial class CustomDifficultyDialog : Form
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Mines { get; private set; }

        private Label lblWidth;
        private NumericUpDown nudWidth;
        private Label lblHeight;
        private NumericUpDown nudHeight;
        private Label lblMines;
        private NumericUpDown nudMines;
        private Button btnOK;
        private Button btnCancel;

        public CustomDifficultyDialog()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Label'lar
            lblWidth = new Label
            {
                Text = "Genişlik:",
                Location = new Point(20, 20)
            };

            lblHeight = new Label
            {
                Text = "Yükseklik:",
                Location = new Point(20, 50)
            };

            lblMines = new Label
            {
                Text = "Mayın Sayısı:",
                Location = new Point(20, 80)
            };

            // NumericUpDown'lar
            nudWidth = new NumericUpDown
            {
                Location = new Point(120, 18),
                Size = new Size(60, 20),
                Minimum = 5,
                Maximum = 50,
                Value = 16
            };
            nudWidth.ValueChanged += (s, e) => UpdateMaxMines();

            nudHeight = new NumericUpDown
            {
                Location = new Point(120, 48),
                Size = new Size(60, 20),
                Minimum = 5,
                Maximum = 30,
                Value = 16
            };
            nudHeight.ValueChanged += (s, e) => UpdateMaxMines();

            nudMines = new NumericUpDown
            {
                Location = new Point(120, 78),
                Size = new Size(60, 20),
                Minimum = 1,
                Value = 40
            };

            // Butonlar
            btnOK = new Button
            {
                Text = "Tamam",
                DialogResult = DialogResult.OK,
                Location = new Point(50, 120)
            };

            btnCancel = new Button
            {
                Text = "İptal",
                DialogResult = DialogResult.Cancel,
                Location = new Point(130, 120)
            };

            // Form ayarları
            this.Text = "Özel Zorluk";
            this.ClientSize = new Size(220, 160);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Controls.AddRange(new Control[] {
                lblWidth, nudWidth,
                lblHeight, nudHeight,
                lblMines, nudMines,
                btnOK, btnCancel
            });

            UpdateMaxMines();
        }

        private void UpdateMaxMines()
        {
            int maxMines = (int)(nudWidth.Value * nudHeight.Value * 0.85);
            nudMines.Maximum = maxMines;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                Width = (int)nudWidth.Value;
                Height = (int)nudHeight.Value;
                Mines = (int)nudMines.Value;
            }

            base.OnFormClosing(e);
        }
    }
}