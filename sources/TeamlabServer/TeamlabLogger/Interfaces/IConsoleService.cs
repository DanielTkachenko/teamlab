namespace TeamlabLogger.Interfaces
{
    internal interface IConsoleService
    {
        /// <summary>
        /// метод для вывода логов на консоль 
        /// </summary>
        /// <param name="message"> сообщение для вывода</param>
        /// <param name="color"> цвет текста</param>
        void WriteLog(string message, ConsoleColor color);
    }
}
