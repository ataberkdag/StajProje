using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.Upload;
using Newtonsoft.Json;
using System.Threading;
using System.Windows.Forms;
using Quartz;
using Quartz.Impl;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Xml;
using ISocialList;
namespace APIProje
{
    public delegate void mainClassDel(DataGridView objectList);
    public class AnaClass : IJob
    {
        public event mainClassDel mainClassEvent;

        static object deadLockController = new object();
        private Sınıflar.TaskControl taskControl = new Sınıflar.TaskControl();

        private CompositionContainer _container;

        [ImportMany(typeof(ListAPI))]
        public IEnumerable<Lazy<ListAPI>> dllFiles;

        public ListAPI Social { get; set; }

        public AnaClass()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog("C:\\Users\\ataberk.dagdelen\\source\\repos\\APIProje\\APIProje\\Eklentiler"));
            _container = new CompositionContainer(catalog);

            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compExcep) { }
        }
        private void SelectSearchType(string searchWord, string searchUser)
        {
            if (!string.IsNullOrEmpty(searchWord) && !string.IsNullOrEmpty(searchUser))
            {
                getDataFromDLL(searchWord, 1); // string kelime, int tür
                getDataFromDLL(searchUser, 2);
            }
            else if (!string.IsNullOrEmpty(searchUser))
            {
                getDataFromDLL(searchUser, 2);
            }
            else if (!string.IsNullOrEmpty(searchWord))
            {
                getDataFromDLL(searchWord, 1);
            }
        }

        public void getDataFromDLL(string keyword, int searchType)
        {
            foreach (var dllFile in dllFiles)
            {
                Task<DataTable> taskKeyword;
                try
                {
                    if (searchType == 1) taskKeyword = new Task<DataTable>(() => dllFile.Value.KelimeAra(keyword));
                    else taskKeyword = new Task<DataTable>(() => dllFile.Value.KullanıcıAra(keyword));
                }
                catch (Exception)
                {
                    lock (deadLockController)
                    {
                        if (searchType == 1) taskKeyword = new Task<DataTable>(() => dllFile.Value.KelimeAra(keyword));
                        else taskKeyword = new Task<DataTable>(() => dllFile.Value.KullanıcıAra(keyword));
                    }
                }
                taskControl.AddToQueue(taskKeyword);
            }
        }

        private void CreateDataGridView(DataTable dataTable)
        {
            DataGridView dataGrid = new DataGridView();
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGrid.DataSource = dataTable; // DataGrid verileri DataTable dan alıyor.

            if (mainClassEvent != null) mainClassEvent.Invoke(dataGrid);
            else MessageBox.Show("Event' in bir Subscriber'ı yok.");
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.MergedJobDataMap; 
            taskControl.SetMaxThread(6);
            
            mainForm MainForm = (mainForm)dataMap.Get("mainForm");
            MainForm.percent = 0;
            MainForm.counter = 0;
            MainForm.dataCount = 0;
            mainClassEvent += MainForm.AddToGrid; // Event eklendi.

            taskControl.taskEvent += CreateDataGridView; // Event eklendi.
            
            string searchWord = dataMap.Get("searchWord").ToString(); //
            string searchUser = dataMap.Get("searchUser").ToString(); // Kelimeler formdan alındı.

            SelectSearchType(searchWord, searchUser);
        }
        
    }
}
