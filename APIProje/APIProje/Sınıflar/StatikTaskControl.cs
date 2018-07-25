using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APIProje.Sınıflar
{
    class StatikTaskControl
    {
        public static event myDel taskEvent;
        private static int maxThread;
        private static List<Task<DataTable>> Queue = new List<Task<DataTable>>();
        private static List<Task<DataTable>> RunningList = new List<Task<DataTable>>();
        private static object lockObject = new object();

        public static void TaramayiBaslat()
        {
            Thread tarama = new Thread(ControlRunningList);
            tarama.Start();
        }

        public static void SetMaxThread(int maxThread2)
        {
            maxThread = maxThread2;
        }

        public static int GetRunningTaskCount()
        {
            return RunningList.Count();
        }
        public static void AddToQueue(Task<DataTable> task)
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

        public static void ControlRunningList()
        {
            while (true)
            {
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
