using TeamlabLogger.Interfaces;

namespace TeamlabLogger.Services
{
    /// <summary>
    /// Класс для взаимодействия логгера с файлом лога
    /// </summary>
    internal class FileService : IFileService
    {
        #region private fields

        private string PATH;

        #endregion private fields

        #region constructor
        public FileService(string logPath)
        {
            //путь к логу задается
            PATH = logPath + DateTime.Today.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + ".log";
        }
        #endregion constructor

        #region IFileService implementation
        
        public async Task WriteLog(string message)
        {
            using (StreamWriter sw = new StreamWriter(PATH, true, System.Text.Encoding.Default))
            {
                await sw.WriteLineAsync(message);
            }
        }

        public async Task ReadLog()
        {
            using (StreamReader sr = new StreamReader(PATH))
            {
                Console.WriteLine(await sr.ReadToEndAsync());
            }
        }
        #endregion IFileService implementation
    }
}
