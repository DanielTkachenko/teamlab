using TeamlabLogger.Enums;

namespace TeamlabLogger.Interfaces
{
    internal interface ILoggerService<T>
    {
        /// <summary>
        /// Основной метод реализующий процесс логирования
        /// </summary>
        /// <param name="message">текст сообщения</param>
        /// <param name="level">уровень логирования</param>
        /// <param name="color">текст сообщения</param>
        /// <param name="ex">ошибка </param>
        /// <param name="traceId">id лога (необязательно)</param>
        void Log(string message, LoggingLevel level, ConsoleColor color, Exception exception = null, short traceId = 0);

        /// <summary>
        /// логирования на уровне Trace
        /// </summary>
        /// <param name="message">текст лога</param>
        void Trace(string message);

        /// <summary>
        /// логирования на уровне Debug
        /// </summary>
        /// <param name="message">текст лога</param>
        void Debug(string message);

        /// <summary>
        /// логирования на уровне Info
        /// </summary>
        /// <param name="message">текст лога</param>
        void Info(string message);

        /// <summary>
        /// логирования на уровне Warn
        /// </summary>
        /// <param name="message">текст лога</param>
        void Warn(string message);

        /// <summary>
        /// логирования на уровне Error
        /// </summary>
        /// <param name="message">текст лога</param>
        /// <param name="ex">ошибка</param>
        void Error(string message, Exception ex = null);

        /// <summary>
        /// логирования на уровне Critical
        /// </summary>
        /// <param name="message">текст лога</param>
        /// <param name="ex">ошибка</param>
        void Critical(string message, Exception ex = null);
    }
}
