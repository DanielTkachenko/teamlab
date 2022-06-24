using TeamlabLogger.Services;

namespace TeamlabTask
{
    public class MyTaskOne : MyTask
    {
        LoggerService<MyTask> logger = new LoggerService<MyTask>();
        public override async Task Run(Dictionary<string, string> arg)
        {
            Console.WriteLine("Task 1 is running!");
            logger.Debug("Task 1 is running!");
            foreach (var i in arg)
            {
                Console.WriteLine($"Key: {i.Key}, Value: {i.Value}");
                logger.Debug($"Key: {i.Key}, Value: {i.Value}");
            }
        }

        
    }
}
