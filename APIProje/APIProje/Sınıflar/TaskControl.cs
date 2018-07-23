using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APIProje.Sınıflar
{
    public delegate void myDel(DataTable dataTable);
    class TaskControl
    {
        private int controller = 1;
        public event myDel taskEvent;
        private int maxThread;
        private List<Task<DataTable>> Queue = new List<Task<DataTable>>();
        private List<Task<DataTable>> RunningList = new List<Task<DataTable>>();
        private object lockObject = new object();

        
        public TaskControl()
        {
            Thread controlThread = new Thread(ControlRunningList);
            controlThread.Start();
        }

        public void SetMaxThread(int maxThread)
        {
            this.maxThread = maxThread;
        }

        public void AddToQueue(Task<DataTable> task)
        {
            lock (lockObject)
            {
                if (RunningList.Count < maxThread)
                {
                    RunningList.Add(task);
                    task.Start();
                    Console.WriteLine("Task RunningList'e alındı.");
                }
                else
                {
                    Queue.Add(task);
                    Console.WriteLine("Task Kuruğa alındı.");
                }
            }
        }

        public void ControlRunningList()
        {
            while (true)
            {
                if (RunningList.Count + Queue.Count > 0) controller = 2;
                else if (controller == 2 && RunningList.Count + Queue.Count < 1) Thread.Sleep(900000000);

                for (int i = 0; i < RunningList.Count; i++)
                {
                    lock (lockObject)
                    {
                        if (RunningList[i].IsCompleted == true)
                        {
                            var dataTable = RunningList[i].Result;
                            RunningList.RemoveAt(i);

                            taskEvent.Invoke(dataTable);

                            Console.WriteLine("Task bitti.");
                            if (Queue.Count > 0)
                            {
                                RunningList.Add(Queue[0]);
                                Queue[0].Start();
                                Queue.RemoveAt(0);
                                Console.WriteLine("Task RunningList'e alındı.");
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
