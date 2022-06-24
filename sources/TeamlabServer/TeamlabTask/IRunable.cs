namespace TeamlabTask
{
    public interface IRunable
    {
        Task Run(Dictionary<string, string> arg);
    }
}
