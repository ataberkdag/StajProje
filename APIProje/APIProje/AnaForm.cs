using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIProje
{
    public partial class AnaForm : Form
    {
        private Sınıflar.AraKatman araKatman = new Sınıflar.AraKatman();
        public double percent = 0;
        public int counter = 0;
        public int dataCount = 0;
        public int tekrarSayısı = 0;

        public AnaForm()
        {
            InitializeComponent();
            lblDLL.Text = "DLL Sayısı: " + araKatman.DLLCounter();
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            lblDLL.Text = "DLL Sayısı: " + araKatman.DLLCounter();
            tekrarSayısı = 0;
            araKatman.AramaTuru = cbType.Text;
            araKatman.SearchAll("Search");
        }

        private void btnDurdur_Click(object sender, EventArgs e)
        {
            araKatman.SearchAll("Stop");
        }

        private void btnSeçenekler_Click(object sender, EventArgs e)
        {
            FormSettings settingsForm = new FormSettings();
            settingsForm.Show();
        }

        public void AddToGrid(DataGridView dataGridView)
        {
            dataCount++;

            LayoutEkle(dataGridView);
            ProgressBarEkle();

            lblVeri.BeginInvoke((ThreadStart)delegate ()
            {
                lblVeri.Text = "Veri Sayısı: " + dataCount;
            });
        }

        private void ProgressBarEkle()
        {
            mainProgressBar.BeginInvoke((ThreadStart)delegate ()
            {
                if (counter == 0)
                {
                    lblTekrar.Text = "Tekrar Sayısı: " + tekrarSayısı;
                    percent = 0;
                    mainProgressBar.Value = 0;
                }

                if (araKatman.AramaTuru == "Tümü")
                {
                    percent += (50.0 / araKatman.DLLCounter());
                    mainProgressBar.Value = (int)percent;
                }
                else
                {
                    percent += (100.0 / araKatman.DLLCounter());
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
    }
}
