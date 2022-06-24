using TeamlabServer.Interfaces;

namespace TeamlabServer
{
    /// <summary>
    /// Класс для запуска по расписанию сервисов
    /// </summary>
    internal class Schedule : IScheduler
    {
        #region private fields
        List<TaskMetadata> taskList;
        bool enabled = true;
        #endregion private fields

        #region constructor
        public Schedule()
        {
            taskList = new List<TaskMetadata>();
        }
        #endregion constructor

        #region IScheduler implementation

        public void Add(TaskMetadata task)
        {
            taskList.Add(task);
        }

        public async Task Run()
        {
            #region запускаем задачи с параметром OnStart
            foreach (TaskMetadata t in taskList)
            {
                if (t.onStart)
                    await t.task.Run(t.argDict);
            }

            #endregion запускаем задачи с параметром OnStart

            while (enabled)                     //запуск бесконечного цикла
            {
                #region периодически проходим по списку задач с параметром OnTime, если время выполнения наступило, то выполняем задачу
                DateTime now = DateTime.Now;
                foreach (var t in taskList)
                {
                    if (t.onTime)
                    {
                        foreach (DateTime time in t.times)
                        {
                            if (time.Hour == now.Hour && time.Minute == now.Minute)
                            {
                                t.task.Run(t.argDict);
                                t.times.Remove(time);
                                break;
                            }
                        }
                    }
                }
                Thread.Sleep(10000);        //проверяем расписание каждые 10 сек
                #endregion периодически проходим по списку задач с параметром OnTime, если время выполнения наступило, то выполняем задачу
            }
        }

        public void Stop()
        {
            enabled = false;
        }

        #endregion IScheduler implementation
    }
}
