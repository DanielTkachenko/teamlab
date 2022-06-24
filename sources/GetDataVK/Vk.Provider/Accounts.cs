using TeamlabTask;

namespace Downloader

{
    public class Accounts : IRunable
    {
        public List<string> AccessTokens { get; set; }

        public async Task Run(Dictionary<string, string> arg)
        {
            Accounts accounts = new();
            accounts.AccessTokens = arg.Values.ToList();
        }
    }
}
