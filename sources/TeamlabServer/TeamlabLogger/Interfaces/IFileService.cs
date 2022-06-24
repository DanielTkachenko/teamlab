namespace TeamlabLogger.Interfaces
{
    internal interface IFileService
    {
        /// <summary>
        /// метод для записи логов в файл
        /// </summary>
        /// <param name="message">сообщение для записи</param>
        Task WriteLog(string message);

        /// <summary>
        /// Сообщение для чтения лога из файла
        /// </summary>
        Task ReadLog();
    }
}
