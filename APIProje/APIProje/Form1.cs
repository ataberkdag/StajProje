using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using Npgsql;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Quartz;
using Quartz.Impl;

namespace APIProje
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }
        
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            SearchAll("Search");
        }
        public async void SearchAll(string jobType) {
            string[] settingsList = SearchSettings();
            ISchedulerFactory schFac = new StdSchedulerFactory();

            IScheduler sched = await schFac.GetScheduler();
            sched.Start();
            if (jobType.Equals("Search"))
            {
                IJobDetail job = JobBuilder.Create<AnaClass>()
                    .WithIdentity("word", "group1")
                    .Build();
                job.JobDataMap.Put("tableLayout", tableLayoutPanel2);
                job.JobDataMap.Put("progressBar1", progressBar1);
                job.JobDataMap.Put("searchWord", settingsList[0]);
                job.JobDataMap.Put("searchUser", settingsList[1]);

                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(Convert.ToInt32(settingsList[2])).RepeatForever())
                .Build();

                sched.ScheduleJob(job, trigger);
                
            }
            else
            {
                sched.Shutdown();
            }
        }
        private string[] SearchSettings() {
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
            } catch (Exception ex) {
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

        private void DeleteData()
        { // ProgressBar'ı ve TableLayout'u temizliyor.

            Invoke(new Action(() =>
            { //Form üzerindeki nesnelere erişmek için 
                progressBar1.Value = 0; // ProgressBar'ın değerini 0 yap.
                for (int delete = tableLayoutPanel2.Controls.Count - 1; delete > -1; delete--)
                {
                    tableLayoutPanel2.Controls.RemoveAt(delete); // TableLayoutPanel'i temizle.
                }
            }));
        }

        private void changeTableLayoutSettings()
        { // TableLayout eklenecek datagridler için hazırlanıyor.
            Invoke(new Action(() =>
            {
                tableLayoutPanel2.ColumnStyles.Clear();
                tableLayoutPanel2.RowStyles.Clear();
                tableLayoutPanel2.RowCount = 1; // 1 row
                tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tableLayoutPanel2.ColumnCount = 2; // 2 column
            }));
        }

        private void dataGridSettings(DataTable myDataTable)
        {
            DataGridView myDataGrid = new DataGridView();
            myDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            myDataGrid.DataSource = myDataTable; // DataGrid verileri DataTable dan alıyor.
            tableLayoutSettings(myDataGrid);
        }
        private void tableLayoutSettings(DataGridView myDataGrid) {
            Invoke(new Action(() =>
            {
                tableLayoutPanel2.Controls.Add(myDataGrid);
                tableLayoutPanel2.MaximumSize = new Size(tableLayoutPanel2.Width, tableLayoutPanel2.Height);
                tableLayoutPanel2.AutoScroll = true; 
            }));
        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            FormSettings myFormSettings = new FormSettings();
            myFormSettings.Show();

            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            SearchAll("Stop");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm MainForm = new mainForm();
            MainForm.Show();
        }
    }
}
