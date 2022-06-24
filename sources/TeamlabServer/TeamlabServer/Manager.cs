using System.Reflection;
using System.Xml;
using TeamlabTask;
using TeamlabLogger.Services;
using TeamlabServer.Interfaces;

namespace TeamlabServer
{
    /// <summary>
    /// Класс для парсинга config файлов и запуска работы расписания
    /// </summary>
    internal class Manager : IManager
    {
        #region private fields

        Schedule scheduler = new Schedule();

        #endregion private fields

        #region IManager implementation

        public void Parse(string path)
        {
            LoggerService<Manager> logger = new LoggerService<Manager>();

            #region загружаем xml документ, получаем корневой элемент
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlElement xRoot = xmlDoc.DocumentElement;
            #endregion загружаем xml документ, получаем корневой элемент

            #region Если корневой элемент существует то парсим содержимое xml файла
            if (xRoot != null)
            {
                TaskMetadata new_task = new TaskMetadata();

                GetTask(xRoot, ref new_task);

                if (new_task.task != null)
                {
                    getArgDict(xRoot, ref new_task);

                    getScheduleParams(xRoot, ref new_task);

                    scheduler.Add(new_task);
                }
                else
                {
                    logger.Info($"No root in {path}");
                }
            }
            #endregion Если корневой элемент существует то парсим содержимое xml файла

        }

        public async Task RunScheduleAsync()
        {
            await Task.Run(() => RunSchedule());
        }

        public void StopSchedule()
        {
            scheduler.Stop();
        }

        #endregion IManager implementation


        #region private methods

        /// <summary>
        /// Запуск работы Scheduler
        /// </summary>
        private void RunSchedule()
        {
            scheduler.Run();
        }

        /// <summary>
        /// Метод для получения экземпляра класса, определенного в xml-документе
        /// </summary>
        /// <param name="root">корневой элемент xml-документа</param>
        /// <param name="taskMetadata">экземпляр класса TaskMetadata</param>
        private void GetTask(XmlElement root, ref TaskMetadata taskMetadata)
        {
            string assemblyName = root.Attributes.GetNamedItem("assemblyname").Value;
            string typeName = root.Attributes.GetNamedItem("classname").Value;
            taskMetadata.task = (assemblyName == Assembly.GetExecutingAssembly().GetName().Name)
                                ? ((IRunable)Assembly.GetExecutingAssembly().CreateInstance(typeName))
                                : ((IRunable)Assembly.Load(assemblyName).CreateInstance(typeName));
        }

        /// <summary>
        /// Метод для получения аргументов иp xml-документа
        /// </summary>
        /// <param name="root">корневой элемент xml-документа</param>
        /// <param name="taskMetadata">экземпляр класса TaskMetadata</param>
        private void getArgDict(XmlElement root, ref TaskMetadata taskMetadata)
        {
            foreach (XmlNode it in root.SelectNodes("arguments"))
            {
                taskMetadata.argDict.Add(it.Attributes.GetNamedItem("key").Value, it.Attributes.GetNamedItem("value").Value);
            }
        }

        /// <summary>
        /// Метод для получения параметров времени запуска (параметры onStart, onStop, время запуска сервиса)
        /// </summary>
        /// <param name="root">корневой элемент xml-документа</param>
        /// <param name="taskMetadata">экземпляр класса TaskMetadata</param>
        private void getScheduleParams(XmlElement root, ref TaskMetadata taskMetadata)
        {
            #region установка параметров времени: onStart, onTime, время запуска
            foreach (XmlNode timeNode in root.SelectSingleNode("schedule").SelectNodes("time"))
            {
                if (timeNode.Attributes.GetNamedItem("type").Value == "OnStart")
                    taskMetadata.onStart = true;
                else if (timeNode.Attributes.GetNamedItem("type").Value == "OnTime")
                {
                    taskMetadata.onTime = true;
                    taskMetadata.times.Add(DateTime.Parse(timeNode.Attributes.GetNamedItem("value").Value));
                }
            }
            #endregion установка параметров времени: onStart, onTime, время запуска
        }

        #endregion private methods

    }
}

