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
        public string AramaTuru;
        private bool aramaDurumu = true;

        public async void SearchAll(string jobType)
        {
            aramaDurumu = true;
            string[] settingsList = SearchSettings();

            ISchedulerFactory schFac = new StdSchedulerFactory();
            IScheduler sched = await schFac.GetScheduler();

            sched.Start();

            if (jobType.Equals("Search"))
            {
                IJobDetail job = JobBuilder.Create<AnaClass>()
                    .WithIdentity("word", "group1")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(Convert.ToInt32(settingsList[2])).RepeatForever())
                .Build();

                job.JobDataMap.Put("mainForm", mainForm.ActiveForm);

                JobSettings(job);

                if (aramaDurumu == true) sched.ScheduleJob(job, trigger);
            }
            else sched.Shutdown();

        }

        private void JobSettings(IJobDetail job)
        {
            string[] settingsList = SearchSettings();
            string word = settingsList[0];
            string user = settingsList[1];

            if (AramaTuru == "Kelime" && !string.IsNullOrEmpty(word))
            {
                job.JobDataMap.Put("searchWord", settingsList[0]);
                job.JobDataMap.Put("searchUser", "");
            }
            else if (AramaTuru == "Kullanıcı" && !string.IsNullOrEmpty(user))
            {
                job.JobDataMap.Put("searchUser", settingsList[1]);
                job.JobDataMap.Put("searchWord", "");
            }
            else if (AramaTuru == "Tümü" && (!string.IsNullOrEmpty(word) && !string.IsNullOrEmpty(user)))
            {
                job.JobDataMap.Put("searchWord", settingsList[0]);
                job.JobDataMap.Put("searchUser", settingsList[1]);
            }
            else
            {
                MessageBox.Show("Türe uygun veriler belirtilmemiş!");
                aramaDurumu = false;
            }
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

        public int DLLCounter()
        {
            return dllCounter.dllFiles.Count();
        }
    }
}
