using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperForm.cs
{
    public partial class CustomDifficultyDialog : Form
    {
        public int SelectedWidth { get; private set; }
        public int SelectedHeight { get; private set; }
        public int SelectedMines { get; private set; }
        
        public CustomDifficultyDialog()
        {
            InitializeComponent();
            UpdateMaxMines();
        }
        private void UpdateMaxMines()
        {
            int maxMines = (int)((double)nudWidth.Value * (double)nudHeight.Value * 0.85);
            maxMines = Math.Max(1, maxMines); // En az 1 mayın olsun
            nudMines.Maximum = maxMines;

            // Eğer mevcut değer yeni maksimumu aşıyorsa ayarla
            if (nudMines.Value > maxMines)
            {
                nudMines.Value = maxMines;
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                SelectedWidth = (int)nudWidth.Value;
                SelectedHeight = (int)nudHeight.Value;
                SelectedMines = (int)nudMines.Value;
            }
            base.OnFormClosing(e);
        }


        private void CustomDifficultyDialog_Load(object sender, EventArgs e)
        {

        }

        private void lblWidth_Click(object sender, EventArgs e)
        {

        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            UpdateMaxMines();
        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            UpdateMaxMines();
        }
    }
}
