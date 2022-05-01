using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace teamlab
{
    class Schedule
    {
        List<MyTask> onStartList;
        Dictionary<MyTask, DateTime> onTimeList;

        public Schedule()
        {
            onStartList = new List<MyTask>();
            onTimeList = new Dictionary<MyTask, DateTime>();
        }

        public void AddOnStart(MyTask task)
        {
            onStartList.Add(task);
        }

        public void AddOnTime(MyTask task, DateTime time)
        {
            onTimeList.Add(task, time);
        }

        public void Run()
        {
            foreach(MyTask t in onStartList)   //запуск сервисов с параметром OnStart
            {
                t.Run();
            }
            
            while(true)                     //запуск цикла с проверкой времени для запуска задач в определенное время
            {
                DateTime now = DateTime.Now;
                foreach(var t in onTimeList)
                {
                    if (now.Hour == t.Value.Hour && now.Minute == t.Value.Minute)
                    {
                        t.Key.Run();
                        onTimeList.Remove(t.Key);
                    }
                }
                Thread.Sleep(30000);        //проверяем расписание каждые 30 сек
            }
        }
    }
}
