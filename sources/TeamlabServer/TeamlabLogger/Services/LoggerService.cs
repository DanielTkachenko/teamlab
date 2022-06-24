using System.Text.Json;
using TeamlabLogger.Enums;
using TeamlabLogger.Interfaces;

namespace TeamlabLogger.Services
{
    /// <summary>
    /// Основной класс реализующий функционал логгера
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LoggerService<T> : ILoggerService<T>
    {
        #region path properties

        // файл находится в папке Resources, которая находится в директории с sln файлом
        private const string jsonPath = "../../../../Resources/loggersettings.json";
        //сюда будут создаваться и записываться логи
        private const string logPath = "D:\\tmp\\logs\\";

        #endregion  path properties

        #region Private readonly properties

        private readonly IFileService _fileService;
        private readonly IConsoleService _consoleService;
        private readonly LoggerSettings _loggerSettings;

        private readonly string _nameSpace;

        private readonly string _currentTime = DateTime.Now.ToLongTimeString();

        #endregion Private readonly properties

        #region Constructor

        public LoggerService()
        {
            _nameSpace = typeof(T).FullName;
            _fileService = new FileService(logPath);
            _consoleService = new ConsoleService();
            if (File.Exists(jsonPath))
                _loggerSettings = JsonSerializer.Deserialize<LoggerSettings>(File.ReadAllText(jsonPath));  //парсинг json файла
            else
                _loggerSettings = new LoggerSettings(); //запуск со значениями по умолчанию

        }

        #endregion Constructor

        #region Private methods

        /// <summary>
        /// метод для создания сообщения лога
        /// </summary>
        /// <param name="message">текст сообщения</param>
        /// <param name="level">уровень логирования</param>
        /// <param name="ex">ошибка</param>
        /// <param name="traceId">id лога (необязательно)</param>
        /// <returns>текст лога</returns>
        private string CastMessage(string message, LoggingLevel level, Exception ex, short traceId)
        {
            return ex != null
                ? $"{_currentTime} | {traceId} | {_nameSpace} | {level.ToString()} | {message} {ex}"
                : $"{_currentTime} | {traceId} | {_nameSpace} | {level.ToString()} | {message}";
        }

        #endregion Private methods

        #region ILoggerService<T> implementation

        public void Log(string message, LoggingLevel level, ConsoleColor color = ConsoleColor.White, Exception ex = null, short traceId = 0)
        {
            var response = CastMessage(message, level, ex, traceId);// генерируем сообщение лога

            #region Если уровень логирования настроен на вывод в консоль или файл то записываем лог
            if (_loggerSettings.ConsoleContains(level))
                _consoleService.WriteLog(response, color);
            if (_loggerSettings.FileContains(level))
                _fileService.WriteLog(response);
            #endregion Если уровень логирования настроен на вывод в консоль или файл то записываем лог
        }

        public void Trace(string message)
        {
            Log(message, LoggingLevel.Trace);
        }

        public void Debug(string message)
        {
            Log(message, LoggingLevel.Debug);
        }

        public void Info(string message)
        {
            Log(message, LoggingLevel.Info, ConsoleColor.Green);
        }

        public void Warn(string message)
        {
            Log(message, LoggingLevel.Warning, ConsoleColor.Yellow);
        }

        public void Error(string message, Exception ex = null)
        {
            Log(message, LoggingLevel.Error, ConsoleColor.Red, ex);
        }

        public void Critical(string message, Exception ex = null)
        {
            Log(message, LoggingLevel.Critical, ConsoleColor.DarkRed, ex);
        }

        #endregion ILoggerService<T> implementation
    }
}
