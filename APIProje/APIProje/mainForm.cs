using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace APIProje
{
    public partial class mainForm : Form
    {
        private Sınıflar.DLLCount dllCounter = new Sınıflar.DLLCount();
        public double percent = 0;
        public int counter = 0;
        public int dataCount = 0;
        public int maxTask;

        public mainForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchAll("Search");
        }

        public async void SearchAll(string jobType)
        {
            string[] settingsList = SearchSettings();
            ISchedulerFactory schFac = new StdSchedulerFactory();
            IScheduler sched = await schFac.GetScheduler();

            sched.Start();

            if (jobType.Equals("Search"))
            {
                IJobDetail job = JobBuilder.Create<AnaClass> ()
                    .WithIdentity("word", "group1")
                    .Build();

                job.JobDataMap.Put("searchWord", settingsList[0]);
                job.JobDataMap.Put("searchUser", settingsList[1]);
                job.JobDataMap.Put("mainForm",mainForm.ActiveForm);

                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(Convert.ToInt32(settingsList[2])).RepeatForever())
                .Build();

                sched.ScheduleJob(job, trigger);

            }
            else sched.Shutdown();

        }

        private string[] SearchSettings()
        {
            string[] settingsList = new string[3];
            try
            {
                XmlTextReader xmlReader = new XmlTextReader("System.config");
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "SearchWord")
                    {
                        xmlReader.Read();
                        if (!string.IsNullOrWhiteSpace(xmlReader.Value)) settingsList[0] = xmlReader.Value;
                        else settingsList[0] = "";
                    }
                    else if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "SearchUser")
                    {
                        xmlReader.Read();
                        if (!string.IsNullOrWhiteSpace(xmlReader.Value)) settingsList[1] = xmlReader.Value;
                        else settingsList[1] = "";
                    }
                    else if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "SleepTime")
                    {
                        xmlReader.Read();
                        if (!string.IsNullOrWhiteSpace(xmlReader.Value)) settingsList[2] = xmlReader.Value;
                        else settingsList[2] = "";
                    }
                }
                xmlReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return settingsList;
        }

        private void AddToDatabase(string myKeyword, string myType)
        { // Duruma göre TextBox değişkeni ve tip alıyor.
            var sefact = NHibernateSettings(); //Config dosyası fonksiyondan alınıyor.
            //NHibernate başlangıç
            using (var session = sefact.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var verim = new Database
                    {
                        Keyword = myKeyword,
                        Type = myType
                    };
                    session.Save(verim); // Veritabanına kaydediliyor.
                    tx.Commit(); // İşlemler gerçekleştiriliyor.
                }
            }
            //NHibernate bitiş
        }

        private NHibernate.ISessionFactory NHibernateSettings()
        { // NHibernate bağlantısı için gerekli olan dosyayı return ediyor.
            //NHibernate için cfg tanımlandı.
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=127.0.0.1;Port=5432;Database=Calisma;User Id=postgres;Password=1";
                x.Driver<NpgsqlDriver>();
                x.Dialect<PostgreSQLDialect>();
            });

            cfg.AddAssembly(Assembly.GetExecutingAssembly());
            var sefact = cfg.BuildSessionFactory();
            return sefact;
            //NHibernate ayarları sonu
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            SearchAll("Stop");
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
            string[] searchSettings = SearchSettings();
            string searchWord = searchSettings[0];
            string searchUser = searchSettings[1];

            LayoutEkle(dataGridView);
            ProgressBarEkle(searchWord,searchUser);
            
            lblDataCount.BeginInvoke((ThreadStart)delegate ()
            {
                lblDataCount.Text = "Veri Sayısı: " + dataCount;
            });
        }

        private void ProgressBarEkle(string searchWord,string searchUser)
        {
            mainProgressBar.BeginInvoke((ThreadStart)delegate ()
            {
                if (counter == 0)
                {
                    percent = 0;
                    mainProgressBar.Value = 0;
                }

                if (!string.IsNullOrEmpty(searchWord) && !string.IsNullOrEmpty(searchUser))
                {
                    percent += (50.0 / dllCounter.dllFiles.Count());
                    mainProgressBar.Value = (int)percent;
                }
                else
                {
                    percent += (100.0 / (dllCounter.dllFiles.Count()));
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
            
            lblDll.Text = "DLL Sayısı: " + dllCounter.dllFiles.Count();
        }

        private void bilgilerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
