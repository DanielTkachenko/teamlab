using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamlabLogger.Services;

namespace TeamlabServer
{
    /// <summary>
    /// Класс для запуска программы как Windows службы
    /// </summary>
    public class MyServer : IHostedService
    {
        #region private fields

        Manager manager;
        LoggerService<MyServer> logger;

        #endregion private fields

        #region public methods

        /// <summary>
        /// Метод для запуска службы
        /// </summary>
        public void StartInternal()
        {
            #region реализация основного функционала приложения

            manager = new Manager();
            logger = new LoggerService<MyServer>();

            string dirPath = "C:\\Users\\Павел\\source\\bi_2022\\sources\\TeamlabServer\\Resources";

            if (Directory.Exists(dirPath))
            {

                string[] files = Directory.GetFiles(dirPath, "*.service.config");
                logger.Info($"Directory: {dirPath}. {files.Length} found");

                if (files.Length != 0)
                {
                    foreach (var f in files)
                    {
                        manager.Parse(f);
                        logger.Info($"File {f} was parsed");
                    }
                }
                else
                {
                    logger.Info("No files");
                }
            }
            else
            {
                logger.Info("No directory");
            }

            logger.Info("Starting scheduler");
            manager.RunScheduleAsync();

            #endregion реализация основного функционала приложения
        }

        /// <summary>
        /// Метод для остановки службы
        /// </summary>
        public void StopInternal()
        {
            logger.Info("Stopping scheduler");
            manager.StopSchedule();
        }

        /// <summary>
        /// Асинхронный метод для запуска службы
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            StartInternal();

            Task.Factory.StartNew(() =>
            {
                ReadWithStopWaiting();
                this.StopAsync(CancellationToken.None);
            });

            return Task.CompletedTask;
        }

        /// <summary>
        /// Асинхронный метод для запуска службы
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            StopInternal();

            return Task.CompletedTask;
        }

        #endregion public methods

        #region private methods

        private void ReadWithStopWaiting()
        {
            var exit = false;
            while (!exit)
            {
                while (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        exit = true;
                    }
                }
            }
        }

        #endregion private methods

    }
}
