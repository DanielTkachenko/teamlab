using System.Text.Json.Serialization;
using TeamlabLogger.Enums;

namespace TeamlabLogger.Services
{
    /// <summary>
    /// Класс для настройки уровней логирования
    /// </summary>
    internal class LoggerSettings
    {
        #region public properties

        [JsonPropertyName("Console")]
        public string[] ConsoleSettings { get; set; } = { "Trace", "Debug", "Info", "Warning", "Error" };

        [JsonPropertyName("File")]
        public string[] FileSettings { get; set; } = { "Debug", "Info", "Warning", "Error", "Critical" };

        #endregion public properties


        /// <summary>
        /// проверка того, настроен ли вывод данного уровня в консоль 
        /// </summary>
        /// <param name="level">уровень логирования</param>
        /// <returns>True - да, False - нет</returns>
        public bool ConsoleContains(LoggingLevel level)
        {
            foreach (string i in ConsoleSettings)
            {
                if (i == level.ToString())
                    return true;
            }
            return false;
        }

        /// <summary>
        /// проверка того, настроен ли вывод данного уровня в файл 
        /// </summary>
        /// <param name="level">уровень логирования</param>
        /// <returns>True - да, False - нет</returns>
        public bool FileContains(LoggingLevel level)
        {
            foreach (string i in FileSettings)
            {
                if (i == level.ToString())
                    return true;
            }
            return false;
        }
    }
}
