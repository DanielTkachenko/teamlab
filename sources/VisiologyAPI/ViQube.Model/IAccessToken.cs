using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViQube.Model
{
    public interface IAccessToken
    {

        public Task<HttpClient> CreateClient();

        public Task<Dictionary<string, string>?> GetTokenDictionary(string userName, string password);
    }
}
