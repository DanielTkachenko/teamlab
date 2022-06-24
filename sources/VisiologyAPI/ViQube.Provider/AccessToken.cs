using System.Diagnostics;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using TeamlabLogger.Services;
using ViQube.Model;

namespace ViQube.Provider
{
    /// <summary>
    /// Класс для аутентификации токена
    /// </summary>
    public class AccessToken:IAccessToken
    {
        /// <summary>
        /// Домейн для обращения к серверу
        /// </summary>
        private const string AppPath = "http://192.168.21.175";

        private readonly LoggerService<AccessToken> _logger = new LoggerService<AccessToken>();

        /// <summary>
        /// Метод для получения аутентификатора токена
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns>Возвращает словарь пары:ключ,значение, который хранит данные токена</returns>
        public async Task<Dictionary<string, string>?> GetTokenDictionary(string userName, string password)
        {
            try
            {
                var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", $"{userName}"),
                    new KeyValuePair<string, string>("Password", $"{password}"),
                };

                var content = new FormUrlEncodedContent(pairs);
                var client = new HttpClient();
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", "cHVibGljX3JvX2NsaWVudDpAOVkjbmckXXU+SF4zajY=");
                var response = await 
                    client.PostAsync($"{AppPath}/idsrv/connect/token", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Dictionary<string, string>? tokenDictionary =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                    
                    _logger.Info($"{ response.StatusCode.GetHashCode()}: { response.StatusCode}");
                    return tokenDictionary;
                }
                throw new ArgumentException($"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                throw;
            }
        }

        /// <summary>
        /// Создание Http Клиента с версией API
        /// </summary>
        /// <returns>Создает Http клиента с Версией Api и типом app/json</returns>
        public async Task<HttpClient> CreateClient()
        {
            try
            {
                var client = new HttpClient();
                var token = await new AccessToken().GetTokenDictionary("admin", "123456");
                if (!string.IsNullOrWhiteSpace(token?["access_token"]))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(token["token_type"], token["access_token"]);
                    var response = await client.GetAsync($"{AppPath}/viqube/version");
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        var xApiVersion =
                            JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                        client.DefaultRequestHeaders.Add("X-API-VERSION", xApiVersion?["apiStable"]);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        _logger.Info($"{ response.StatusCode.GetHashCode()}: { response.StatusCode}");
                        return client;
                    }

                    throw new ArgumentException($"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }

                _logger.Info($"acess_token is: {string.IsNullOrWhiteSpace(token?["access_token"])}");
                return client;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                throw;
            }
        }

    }
}
