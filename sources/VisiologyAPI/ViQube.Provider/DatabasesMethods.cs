using System.Text;
using Newtonsoft.Json;
using ViQube.Model;
using TeamlabLogger.Services;

namespace ViQube.Provider
{
   
    /// <summary>
    /// Класс для работы с Api Databases
    /// </summary>
    public class DatabasesMethods:IDataBaseMethods
    {
        /// <summary>
        /// Домейн для обращения к серверу
        /// </summary>
        private const string AppPath = "http://192.168.21.175";

        private readonly HttpClient _client = new AccessToken().CreateClient().Result;

        private readonly LoggerService<DatabasesMethods> _logger = new LoggerService<DatabasesMethods>();

        #region GET Methods

        /// <summary>
        /// Метод для получения X-API-Version
        /// </summary>
        /// <returns>Возвращает Api-stable версию</returns>
        public async Task<string> GetVersionAsync()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(AppPath + "/viqube/version");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var xApiVersion =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                    result = xApiVersion?["apiStable"];
                    _logger.Info($"{ response.StatusCode.GetHashCode()}: { response.StatusCode}");
                    return result;
                }
                throw new ArgumentException($"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");

            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return null!;
            }
        }

        /// <summary>
        /// Метод получения данных Всех Databases
        /// </summary>
        /// <returns>Возвращает все базы данных</returns>
        public async Task<List<Database>> GetAllDataBasesAsync()
        {
            try
            {
                var response = await _client.GetAsync(AppPath + "/viqube/databases");
                if (response.IsSuccessStatusCode)
                {
                    var text = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Database>>(text);
                    _logger.Info($"{ response.StatusCode.GetHashCode()}: { response.StatusCode}");
                    return result;
                }

                throw new ArgumentException($"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return null!;
            }
            
        }

        /// <summary>
        /// Получить конкретную базу данных
        /// </summary>
        /// <param name="databaseName">Имя базы данных</param>
        /// <returns>Возвращает конкретную базу данных</returns>
        public async Task<Database> GetDataBaseAsync( string databaseName)
        {
            try
            {
                var response = await _client.GetAsync(AppPath + $"/viqube/databases/{databaseName}");
                var text = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Database>(text);
                    _logger.Info($"{ response.StatusCode.GetHashCode()}: { response.StatusCode}");
                    return result;
                }

                throw new ArgumentException($"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
                
            }
            catch (Exception e)
            {
                _logger.Error(e.Message,e);
                return null!;
            }
        }

        /// <summary>
        /// Показать все таблицы конкретной базы данных
        /// </summary>
        /// <param name="dataBaseName">Имя базы данных</param>
        /// <returns>Возвращает все таблицы базы данных</returns>
        public async Task<List<Table>> GetAllTablesAsync(string dataBaseName)
        {
            try
            {
                var response = await _client.GetAsync(AppPath + $"/viqube/databases/{dataBaseName}/tables");

                if (response.IsSuccessStatusCode)
                {
                    var text = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Table>>(text);
                    _logger.Info($"{ response.StatusCode.GetHashCode()}: { response.StatusCode}");
                    return result;
                }
                throw new ArgumentException($"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return null!;
            }
        }

        /// <summary>
        /// Показать таблицу конкретную таблицу в заданой базе данных
        /// </summary>
        /// <param name="dataBaseName">Имя базы данных</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <returns>Возвращает таблицу базы данных</returns>
        public async Task<Table> GetTableAsync( string dataBaseName,
            string tableName)
        {
            try
            {
                var response = await _client.GetAsync
                    (AppPath + $"/viqube/databases/{dataBaseName}/tables/{tableName}");
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Table>(response.Content.ReadAsStringAsync().Result);
                    _logger.Info($"{ response.StatusCode.GetHashCode()}: { response.StatusCode}");
                    return result;
                }
                throw new ArgumentException($"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return null!;
            }
            
        }

        /// <summary>
        /// Метод для получения список столбцов в таблице
        /// </summary>
        /// <param name="dataBaseName">Имя базы данных</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <returns>Возвращает список столбцов в таблице</returns>
        public async Task<List<Column>> GetColumnsAsync( string dataBaseName,
            string tableName)
        {
            try
            {
                var response = await _client.GetAsync
                    (AppPath + $"/viqube/databases/{dataBaseName}/tables/{tableName}/columns");
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<List<Column>>(await response.Content.ReadAsStringAsync());
                    _logger.Info($"{ response.StatusCode.GetHashCode()}: { response.StatusCode}");
                    return result;
                }
                throw new ArgumentException($"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return null!;
            }
        }

        /// <summary>
        /// Метод для получения столбца в таблице
        /// </summary>
        /// <param name="dataBaseName">Имя базы данных в которой хотим получить таблицу</param>
        /// <param name="tableName">Имя таблицы в которой хотим получить столбец</param>
        /// <param name="columnName">Имя столбца который хотим получить</param>
        /// <returns>Возвращает столбец с типом данных Column</returns>
        public async Task<Column> GetColumnNameAsync( string dataBaseName,
            string tableName, string columnName)
        {
            try
            {
                var response = await _client.GetAsync(AppPath +
                                                      $"/viqube/databases/{dataBaseName}/tables/{tableName}/columns/{columnName}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{ response.StatusCode.GetHashCode()}: { response.StatusCode}");
                    var result = JsonConvert.DeserializeObject<Column>(await response.Content.ReadAsStringAsync());
                    return result;
                }
                throw new ArgumentException($"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return null!;
            }
            
        }

        /// <summary>
        /// Метод для получения записей в таблице
        /// </summary>
        /// <param name="dataBaseName">Имя базы данных</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <returns>Возвращает все записи в таблице</returns>
        public async Task<Record> GetRecordsAsync(string dataBaseName,
            string tableName)
        {
            try
            {
                var response = await _client.GetAsync
                    (AppPath + $"/viqube/databases/{dataBaseName}/tables/{tableName}/records");
                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{ response.StatusCode.GetHashCode()}: { response.StatusCode}");
                    var result = JsonConvert.DeserializeObject<Record>(await response.Content.ReadAsStringAsync());
                    return result;
                }
                throw new ArgumentException($"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return null!;
            }
            
        }

        #endregion

        #region POST Methods

        /// <summary>
        /// Создание базы данных на сервере и в Metadata с присвоением ей имени 
        /// </summary>
        /// <param name="nameBase">Имя базы данных</param>
        /// <returns></returns>
        public async Task PostCreateDatabaseAsync( string nameBase)
        {
            try
            {
                //Создает базу данных в пространстве MetaData
                var database = new MetaDatabase
                {
                    Name = nameBase,
                    DatabaseName = nameBase,
                    Id = nameBase

                };
                var metaData = new MetaDataMethods();
                await metaData.CreateMetaDatabaseAsync(database);

                //Данные для базы данных
                var payload = new Database()
                {
                    NameDatabase = nameBase,
                };
                //Создает базу данных с именем
                var stringPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                var httpResponse = await _client.PostAsync($"{AppPath}/viqube/databases", content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    _logger.Info($"{ httpResponse.StatusCode.GetHashCode()}: { httpResponse.StatusCode}");
                }
                else
                {
                    throw new ArgumentException($"Ошибка {httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Метод POST Для создания таблицы в базе данных вместе со столбцами и первичным ключов
        /// </summary>
        /// <param name="nameBase"></param>
        /// <param name="columnList">Список столбоцов для добавления в таблицу</param>
        /// <param name="nameTable">Имя таблицы</param>
        /// <param name="primaryKey">Первичный ключ в таблице</param>
        /// <returns></returns>
        public async Task PostCreateTableAsync( string nameBase,
            List<Column> columnList,string nameTable,string primaryKey)
        {
            try
            {
                //Данные для базы данных
                var payload = new Table(columnList, nameTable, primaryKey);
                var stringPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                var httpResponse = await _client.PostAsync(@$"{AppPath}/viqube/databases/{nameBase}/tables", content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    _logger.Info($"{httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Post Методя для создания столбца в таблице базы данных
        /// </summary>
        /// <param name="nameBase">Имя базы данных в которой создается столбец</param>
        /// <param name="tableName">Имя таблицы  в которой создается столбец </param>
        /// <param name="columnName">Имя столбца</param>
        /// <param name="columnType">Тип столбца</param>
        /// <returns></returns>
        public async Task PostCreateColumnAsync(string nameBase,
            string tableName, string columnName, string columnType)
        {
            try
            {
                var payload = new Column(columnName, columnType);
                var stringPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                var httpResponse = await _client.PostAsync
                    (@$"{AppPath}/viqube/databases/{nameBase}/tables/{tableName}/columns", content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    _logger.Info($"{httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Post метод для добавления записей в таблицу
        /// </summary>
        /// <param name="nameBase">Имя базы данных в которой создается столбец</param>
        /// <param name="tableName">Имя таблицы  в которой создается столбец </param>
        /// <param name="records">Записи которые будем вставлять в таблицы</param>
        /// <returns></returns>
        public async Task PostRecordsAsync( string nameBase,
            string tableName, Record records)
        {
            try
            {
                var stringPayload = JsonConvert.SerializeObject(records);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                var httpResponse = await
                    _client.PostAsync(@$"{AppPath}/viqube/databases/{nameBase}/tables/{tableName}/records", content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    _logger.Info($"{httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        #endregion

        #region PUT Methods

        /// <summary>
        /// Переименовывает базу данных из nameBase в renameBase 
        /// </summary>
        /// <param name="nameBase">Текущее имя базы данных</param>
        /// <param name="renameBase">Новое имя базы данных</param>
        /// <returns></returns>
        public async Task PutRenameDataBaseAsync(string nameBase, string renameBase)
        {
            try
            {

                var database = new MetaDatabase
                {
                    Name = renameBase,
                    DatabaseName = renameBase,
                    Id = renameBase

                };

                var metaData = new MetaDataMethods().UpdateMetaDatabaseAsync(nameBase, database);

                //Данные для базы данных
                var payload = new Database()
                {
                    NameDatabase = renameBase
                };

                //Создает базу данных с именем
                var stringPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                var httpResponse = await _client.PutAsync
                    ($"{AppPath}/viqube/databases/{nameBase}", content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    _logger.Info($"{httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Put метод для изменения имени таблицы в конкретной базе данных
        /// </summary>
        /// <param name="nameBase">Имя базы данных в которой нужно поменять таблицу</param>
        /// <param name="tableName">Имя таблицы которую нужно поменять</param>
        /// <param name="rename">Новое имя для таблицы которую хотим поменять</param>
        /// <returns></returns>
        public async Task PutRenameTableAsync(string nameBase, string tableName, string rename)
        {
            try
            {
                var payload = await GetTableAsync(nameBase, tableName);
                payload.TableName = rename;

                var stringPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                var httpResponse = await _client.PutAsync
                    (@$"{AppPath}/viqube/databases/{nameBase}/tables/{tableName}", content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    _logger.Info($"{httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }


        /// <summary>
        /// Метод для переименования столбца в таблице
        /// </summary>
        /// <param name="nameBase">Имя базы данных в которой нужно поменять таблицу</param>
        /// <param name="tableName">Имя таблицы в которой нужно поменять столбец</param>
        /// <param name="columnName">Имя столбца который хотим изменить</param>
        /// <param name="rename">Новое имя для таблицы которую хотим поменять</param>
        /// <returns></returns>
        public async Task PutRenameColumnAsync(string nameBase, string tableName,
            string columnName, string rename)
        {
            try
            {
                var payload = await GetColumnNameAsync(nameBase, tableName, columnName);
                payload.NameTable = rename;

                var stringPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                var httpResponse = await _client.PutAsync
                (@$"{AppPath}/viqube/databases/{nameBase}/tables/{tableName}/columns/{columnName}",
                    content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    _logger.Info($"{httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {httpResponse.StatusCode.GetHashCode()}: {httpResponse.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        #endregion

        #region DELETE Methods

        /// <summary>
        /// Метод удаления всех таблиц
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <returns></returns>
        public async Task DeleteAllTablesAsync( string databaseName)
        {
            try
            {
                var response = await _client.DeleteAsync(AppPath + $"/viqube/databases/{databaseName}/tables");
                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Метод удаления конкретной таблицы
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <param name="tableName">Имя таблицы в которую нужно удалить</param>
        /// <returns></returns>
        public async Task DeleteTablesAsync( string databaseName,
            string tableName)
        {
            try
            {

                var response = await _client.DeleteAsync(AppPath +
                                                         $"/viqube/databases/{databaseName}/tables/{tableName}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Метод удаления конкретного столбца в таблице
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <param name="tableName">Имя таблицы в которой нужно удалить столбец</param>
        /// <param name="columnName">Имя столбца который хотим удалить</param>
        /// <returns></returns>
        public async Task DeleteColumnAsync( string databaseName,
            string tableName,string columnName)
        {
            try
            {
                var response = await _client.DeleteAsync(AppPath +
                                                         $"/viqube/databases/{databaseName}/tables/{tableName}/columns/{columnName}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Метод удаления всех столбцов в таблице
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <param name="tableName">Имя таблицы в которой нужно удалить столбцы</param>
        /// <returns></returns>
        public async Task DeleteAllColumnsAsync(string databaseName,
            string tableName)
        {
            try
            {
                var response = await _client.DeleteAsync(AppPath +
                                                         $"/viqube/databases/{databaseName}/tables/{tableName}/columns");
                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Метод удаления всех записей в таблице
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <param name="tableName">Имя таблицы в которой нужно записи</param>
        /// <returns></returns>
        public async Task DeleteAllRecordsAsync( string databaseName, string tableName)
        {
            try
            {
                var response = await _client.DeleteAsync
                    (AppPath + $"/viqube/databases/{databaseName}/tables/{tableName}/records/all");
                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Метод удаления конкретной записи в таблице
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <param name="tableName">Имя таблицы в которой нужно удалить запись</param>
        /// <param name="record">Значения которые собираемся удалить в таблице</param>
        /// <returns></returns>
        public async Task DeleteRecordAsync( string databaseName,
            string tableName, List<object> record)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete,
                    AppPath + $"/viqube/databases/{databaseName}/tables/{tableName}/records");
                request.Content = new StringContent(JsonConvert.SerializeObject(record),
                    Encoding.UTF8, "application/json");
                var response = await _client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    _logger.Info($"{response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
                else
                {
                    throw new ArgumentException(
                        $"Ошибка {response.StatusCode.GetHashCode()}: {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
        }

        #endregion

        #region Common Methods

        /// <summary>
        /// Создает таблицу
        /// </summary>
        /// <param name="columns">Список колонок для занесения в таблицу</param>
        /// <param name="nameTable">Имя таблицы</param>
        /// <param name="primaryKey">Первичный ключ</param>
        /// <returns></returns>
        public List<Table> CreateTable(List<Column> columns,string nameTable,string primaryKey)
        {
            var table = new Table(columns, nameTable, primaryKey);
            var listTable = new List<Table> { table };
            return listTable;
        }

        /// <summary>
        /// Создает колонки в таблице
        /// </summary>
        /// <param name="nameColumn">Имя колонки</param>
        /// <param name="typeColumn">Тип колонки</param>
        /// <returns></returns>
        public Column CreateColumns(string nameColumn, string typeColumn)
        {
            var columns = new Column(nameColumn,typeColumn);
            return columns;
        }
        #endregion
    }

}
