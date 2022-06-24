using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamlabServer.Interfaces
{
    internal interface IScheduler
    {
        /// <summary>
        /// добавление экземпляра TaskMetadata в список
        /// </summary>
        /// <param name="task">экземпляр TaskMetadata для добавления</param>
        void Add(TaskMetadata task);

        /// <summary>
        /// Запуск работы расписания
        /// </summary>
        Task Run();

        /// <summary>
        /// Остановка работы расписания
        /// </summary>
        void Stop();
    }
}
