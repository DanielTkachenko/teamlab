using TeamlabLogger.Interfaces;

namespace TeamlabLogger.Services
{
    /// <summary>
    /// Класс для взаимодействия логгера с консолью
    /// </summary>
    internal class ConsoleService : IConsoleService
    {
        #region IConsoleService implementation

        public void WriteLog(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        #endregion IConsoleService implementation

    }
}
