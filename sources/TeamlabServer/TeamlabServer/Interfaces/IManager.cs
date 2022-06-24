using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamlabServer.Interfaces
{
    internal interface IManager
    {
        /// <summary>
        /// Метод для парсинга config файла и добавления сервиса в Scheduler
        /// </summary>
        /// <param name="path">путь до config файла</param>
        void Parse(string path);

        /// <summary>
        /// Метод для асинхронного запуска Scheduler
        /// </summary>
        Task RunScheduleAsync();

        /// <summary>
        /// Метод для остановки работы Scheduler
        /// </summary>
        void StopSchedule();
    }
}
