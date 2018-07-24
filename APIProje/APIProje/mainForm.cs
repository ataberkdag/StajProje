using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace APIProje
{
    public partial class mainForm : Form
    {
        private Sınıflar.AraKatman araKatman = new Sınıflar.AraKatman();
        public double percent = 0;
        public int counter = 0;
        public int dataCount = 0;
        public int maxTask;
        public int dllCount;
        public mainForm()
        {
            InitializeComponent();
            dllCount = araKatman.DLLCounter();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            araKatman.AramaTuru = cbType.Text;
            araKatman.SearchAll("Search");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            araKatman.SearchAll("Stop");
        }

        private void menuItemSecenekler_Click(object sender, EventArgs e)
        {
            FormSettings settingsForm = new FormSettings();
            settingsForm.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public void AddToGrid(DataGridView dataGridView)
        {
            dataCount++;

            LayoutEkle(dataGridView);
            ProgressBarEkle();
            
            lblDataCount.BeginInvoke((ThreadStart)delegate ()
            {
                lblDataCount.Text = "Veri Sayısı: " + dataCount;
            });
        }

        private void ProgressBarEkle()
        {
            mainProgressBar.BeginInvoke((ThreadStart)delegate ()
            {
                if (counter == 0)
                {
                    percent = 0;
                    mainProgressBar.Value = 0;
                }

                if (araKatman.AramaTuru == "Tümü")
                {
                    percent += (50.0 / dllCount);
                    mainProgressBar.Value = (int)percent;
                }
                else
                {
                    percent += (100.0 / dllCount);
                    mainProgressBar.Value = (int)percent;
                }
                counter++;
            });
        }
        private void LayoutEkle(DataGridView dataGridView)
        {
            mainLayout.BeginInvoke((ThreadStart)delegate ()
            {
                if (counter == 0)
                {
                    LayoutTemizle();
                }

                mainLayout.Controls.Add(dataGridView);
                mainLayout.MaximumSize = new Size(mainLayout.Width, mainLayout.Height);
                mainLayout.AutoScroll = true;
            });
            
        }

        private void LayoutTemizle()
        {
            for (int delete = mainLayout.Controls.Count - 1; delete > -1; delete--)
            {
                mainLayout.Controls.RemoveAt(delete); // TableLayoutPanel'i temizle.
            }

            mainLayout.ColumnStyles.Clear();
            mainLayout.RowStyles.Clear();
            mainLayout.RowCount = 1; // 1 row
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.ColumnCount = 2; // 2 column
        }


        private void mainForm_Load(object sender, EventArgs e)
        {
            lblDll.Text = "DLL Sayısı: " + dllCount;
        }

        private void bilgilerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
