using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ViQube.Model;
using ViQube.Model.Query;

namespace ViQube.Provider
{
    public class QueryMethods:IQuery
    {
        /// <summary>
        /// Домейн для обращения к серверу
        /// </summary>
        private const string AppPath = "http://192.168.21.175";

        /// <summary>
        /// Клиент для работы с Visiology API
        /// </summary>
        private readonly HttpClient _client = new AccessToken().CreateClient().Result;


        public async Task CreateDatabaseQueryAsync(string databaseName,QuerySelect query )
        {
            var stringPayload = JsonConvert.SerializeObject(query);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(
                @$"{AppPath}/viqube/databases/{databaseName}/query", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        public async Task CreateMetaDataQueryAsync(MetaDataQuery query)
        {
            var stringPayload = JsonConvert.SerializeObject(query);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(
                @$"{AppPath}/viqube/metadata/query", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }
    }
}
