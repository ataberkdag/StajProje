using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Threading;
using System.Windows.Forms;
using Quartz;
using Quartz.Impl;
using System.Data;
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

        public enum searchTypeEnum
        {
            word = 1,
            user = 2
        }

        public AnaClass()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog("C:\\Users\\ataberk.dagdelen\\Documents\\StajProje\\APIProje\\APIProje\\Eklentiler"));
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
                getDatasFromDLL(searchWord, searchUser);
            }
            else if (!string.IsNullOrEmpty(searchUser))
            {
                getDataFromDLL(searchUser, Convert.ToInt32(searchTypeEnum.user));
            }
            else if (!string.IsNullOrEmpty(searchWord))
            {
                Console.WriteLine(Convert.ToInt32(searchTypeEnum.word));
                getDataFromDLL(searchWord, Convert.ToInt32(searchTypeEnum.word));
            }
        }

        public async void getDatasFromDLL(string keyword, string user)
        {
            foreach (var dllFile in dllFiles)
            {
                Task<DataTable> taskUser;
                try
                {
                    taskUser = new Task<DataTable>(() => dllFile.Value.KullanıcıAra(user));
                }
                catch(Exception)
                {
                    lock (deadLockController)
                    {
                        taskUser = new Task<DataTable>(() => dllFile.Value.KullanıcıAra(user));
                    }
                }
                taskControl.AddToQueue(taskUser);
            }
            while (taskControl.GetRunningTaskCount() != 0) { }
            datasKeyword(keyword);
        }

        private void datasKeyword(string keyword)
        {
            taskControl.controller = 1;
            foreach (var dllFile in dllFiles)
            {
                Task<DataTable> taskKeyword;
                try
                {
                    taskKeyword = new Task<DataTable>(() => dllFile.Value.KelimeAra(keyword));
                }
                catch (Exception)
                {
                    lock (deadLockController)
                    {
                        taskKeyword = new Task<DataTable>(() => dllFile.Value.KelimeAra(keyword));
                    }
                }
                taskControl.AddToQueue(taskKeyword);
            }
        }

        public void getDataFromDLL(string keyword, int searchType)
        {
            foreach (var dllFile in dllFiles)
            {
                Task<DataTable> taskKeyword;
                try
                {
                    if (searchType == Convert.ToInt32(searchTypeEnum.word))
                        taskKeyword = new Task<DataTable>(() => dllFile.Value.KelimeAra(keyword));

                    else taskKeyword = new Task<DataTable>(() => dllFile.Value.KullanıcıAra(keyword));
                }
                catch (Exception)
                {
                    lock (deadLockController)
                    {
                        if (searchType == Convert.ToInt32(searchTypeEnum.word))
                            taskKeyword = new Task<DataTable>(() => dllFile.Value.KelimeAra(keyword));

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
            
            AnaForm MainForm = (AnaForm)dataMap.Get("mainForm");
            MainForm.percent = 0;
            MainForm.counter = 0;
            MainForm.dataCount = 0;
            MainForm.tekrarSayısı++;

            mainClassEvent += MainForm.AddToGrid; // Event eklendi.
            taskControl.taskEvent += CreateDataGridView; // Event eklendi.
            
            string searchWord = dataMap.Get("searchWord").ToString(); //
            string searchUser = dataMap.Get("searchUser").ToString(); // Kelimeler formdan alındı.

            SelectSearchType(searchWord, searchUser);
        } 
    }
}
