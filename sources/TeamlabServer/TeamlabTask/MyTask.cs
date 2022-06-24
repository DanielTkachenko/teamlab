using TeamlabLogger.Services;

namespace TeamlabTask
{
    public class MyTask : IRunable
    {
        LoggerService<MyTask> logger = new LoggerService<MyTask>();
        #region IRunable implementation

        public virtual async Task Run(Dictionary<string, string> arg)
        {
            Console.WriteLine("MyTask is running!");
            logger.Debug("MyTask is running!");
            foreach (var i in arg)
            {
                Console.WriteLine($"Key: {i.Key}, Value: {i.Value}");
                logger.Debug($"Key: {i.Key}, Value: {i.Value}. Key: {i.Key.GetType().Name}, Value: {i.Value.GetType().Name}");
            }
        }


        #endregion IRunable implementation
    }
}
