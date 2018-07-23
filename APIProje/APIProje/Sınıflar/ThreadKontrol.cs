using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIProje.Sınıflar
{
    public delegate void myDelegate(List<string> list, string str, ProgressBar progress,
        TableLayoutPanel tableLayout, DataTable dataTable, AnaClass mainClass);

    public delegate void ThreadControlDelegate(Thread complatedThread);
    /*
    class ThreadKontrol
    {
        private int maxThread;
        private List<Thread> Queue = new List<Thread>();
        private List<Thread> RunningList = new List<Thread>();
        private List<Task<List<string>>> RuningTask = new List<Task<List<string>>>();
        private object lockObject = new object();

        public event myDelegate ItemEvent;
        public event ThreadControlDelegate Complate;
        public List<string> myList;
        public string str;
        public ProgressBar mainProgressBar;
        public TableLayoutPanel tableLayout;
        public DataTable dataTable;
        public AnaClass mainClass;
        public ThreadKontrol()
        {
            Thread controlRunningListThread = new Thread(ControlRunningList);
            controlRunningListThread.Start();
        }

        public void setMaxThread(int maxThread)
        {
            this.maxThread = maxThread;
        }

        public void AddToQueue(Thread thread)
        {
            lock (lockObject)
            {
                if (RunningList.Count < maxThread)
                {
                    RunningList.Add(thread);
                    thread.Start();
                }
                else
                {
                    Queue.Add(thread);
                }
            }
        }

        public void ControlRunningList()
        {
            while (true)
            {
                for (int i = 0; i < RunningList.Count; i++)
                {
                    lock (lockObject)
                    {
                        if (RunningList[i].IsAlive == false)
                        {
                            //ItemEvent.Invoke(myList, str, mainProgressBar, tableLayout, dataTable, mainClass);
                            var returnedList = RuningTask[i].Result;
                            Complate?.Invoke(returnedList);
                            RunningList.RemoveAt(i);
                            if (Queue.Count > 0)
                            {
                                RunningList.Add(Queue[0]);
                                Queue[0].Start();
                                Queue.RemoveAt(0);
                            }
                            break;
                        }
                    }
                }
                if (RunningList.Count > 0) Console.WriteLine("Çalışan Thread Sayısı: " + RunningList.Count);
                if (Queue.Count > 0) Console.WriteLine("Kuyruktaki Thread Sayısı: " + Queue.Count);
            }
        }
    }*/
}
