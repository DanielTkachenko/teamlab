using TeamlabTask;

namespace TeamlabServer
{
    /// <summary>
    /// Класс для совместного хранения экземпляра класса сервиса и его параметров
    /// </summary>
    internal class TaskMetadata
    {
        public IRunable task;
        public bool onStart;
        public bool onTime;
        public List<DateTime> times;
        public Dictionary<string, string> argDict;

        public TaskMetadata()
        {
            times = new List<DateTime>();
            argDict = new Dictionary<string, string>();
            onStart = false;
            onTime = false;
        }
    }
}
