using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
namespace APIProje.Sınıflar
{
    class AraKatman
    {
        private DLLCount dllCounter = new DLLCount();
        private SearchSettings searchSett = new SearchSettings();
        public string AramaTuru;
        private bool aramaDurumu = true;

        public enum appStatus
        {
            Start = 1,
            Stop = 2
        }

        public enum aramaTuruEnum
        {
            Kelime = 1,
            Kullanıcı = 2,
            Tümü = 3
        }

        public async void SearchAll(string jobType)
        {
            aramaDurumu = true;
            SetSearchSettings();

            ISchedulerFactory schFac = new StdSchedulerFactory();
            IScheduler sched = await schFac.GetScheduler();

            sched.Start();

            if (jobType.Equals(appStatus.Start.ToString()))
            {
                IJobDetail job = JobBuilder.Create<AnaClass>()
                    .WithIdentity("word", "group1")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(searchSett.sleepTime).RepeatForever())
                .Build();

                job.JobDataMap.Put("mainForm", AnaForm.ActiveForm);

                JobSettings(job);

                if (aramaDurumu == true) sched.ScheduleJob(job, trigger);
            }
            else sched.Shutdown();

        }

        private void JobSettings(IJobDetail job)
        {
            SetSearchSettings();
            string word = searchSett.searchWord;
            string user = searchSett.searchUser;

            if (AramaTuru == aramaTuruEnum.Kelime.ToString() && !string.IsNullOrEmpty(word))
            {
                job.JobDataMap.Put("searchWord", word);
                job.JobDataMap.Put("searchUser", "");
            }
            else if (AramaTuru == aramaTuruEnum.Kullanıcı.ToString() && !string.IsNullOrEmpty(user))
            {
                job.JobDataMap.Put("searchUser", user);
                job.JobDataMap.Put("searchWord", "");
            }
            else if (AramaTuru == aramaTuruEnum.Tümü.ToString() && (!string.IsNullOrEmpty(word) && !string.IsNullOrEmpty(user)))
            {
                job.JobDataMap.Put("searchWord", word);
                job.JobDataMap.Put("searchUser", user);
            }
            else
            {
                MessageBox.Show("Türe uygun veriler belirtilmemiş!");
                aramaDurumu = false;
            }
        }

        private void SetSearchSettings()
        {
            try
            {
                XmlTextReader xmlReader = new XmlTextReader("System.config");
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "SearchWord")
                    {
                        xmlReader.Read();
                        if (!string.IsNullOrWhiteSpace(xmlReader.Value)) searchSett.searchWord = xmlReader.Value;
                        else searchSett.searchWord = "";
                    }
                    else if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "SearchUser")
                    {
                        xmlReader.Read();
                        if (!string.IsNullOrWhiteSpace(xmlReader.Value)) searchSett.searchUser = xmlReader.Value;
                        else searchSett.searchUser = "";
                    }
                    else if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "SleepTime")
                    {
                        xmlReader.Read();
                        if (!string.IsNullOrWhiteSpace(xmlReader.Value))
                            searchSett.sleepTime = Convert.ToInt32(xmlReader.Value);

                        else searchSett.sleepTime = 0;
                    }
                }
                xmlReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public int DLLCounter()
        {
            return dllCounter.dllFiles.Count();
        }
    }
}
