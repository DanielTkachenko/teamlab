using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamlabLogger.Services;
using ViQube.Model;


namespace ViQube.Provider
{
    public class MetaDataMethods:IMetaData
    {
        /// <summary>
        /// Домейн для обращения к серверу
        /// </summary>
        private const string AppPath = "http://192.168.21.175";

        /// <summary>
        /// Клиент для работы с Visiology API
        /// </summary>
        private readonly HttpClient _client = new AccessToken().CreateClient().Result;

        private readonly LoggerService<DatabasesMethods> _logger = new LoggerService<DatabasesMethods>();

        #region Get Methods

        /// <summary>
        /// Получить все базы данных из мета данных
        /// </summary>
        /// <returns>Возвращает <see cref="MetaDatabase"/></returns>
        public async Task<List<MetaDatabase>> GetAllMetaDataBasesAsync()
        {
            var response = await _client.GetAsync(AppPath + "/viqube/metadata/databases");
            var result = JsonConvert.DeserializeObject<List<MetaDatabase>>
                (await response.Content.ReadAsStringAsync());
            return result;
        }
        
        /// <summary>
        /// Получает конкретную базу данных по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns>Возвращает базу данных классом <see cref="MetaDatabase"/></returns>
        public async Task<MetaDatabase> GetMetaDatabaseAsync(string databaseId)
        {
            var response = await _client.GetAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}");
            var stringTest = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MetaDatabase>
                (await response.Content.ReadAsStringAsync());
            return result;
        }

        /// <summary>
        /// Получить все измерения в базе данных в пространстве MetaData
        /// </summary>
        /// <param name="databaseId">Id Базы данных</param>
        /// <returns>Возвращает измерения базы данных классом <see cref="Dimensions"/></returns>
        public async Task<List<DimensionsGet>> GetAllDimensionsDatabaseAsync(string databaseId)
        {
            var response = await _client.GetAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/dimensions");
            var result = JsonConvert.DeserializeObject<List<DimensionsGet>>
                (await response.Content.ReadAsStringAsync());
            return result;
        }
        
        /// <summary>
        /// Получить измерение по Id
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <returns>Возвращает <see cref="Dimensions"/></returns>
        public async Task<DimensionsGet> GetDimensionAsync(string databaseId, string dimId)
        {
            var response = await _client.GetAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/dimensions/{dimId}");
            var result = JsonConvert.DeserializeObject<DimensionsGet>
                (await response.Content.ReadAsStringAsync());
            return result;
        }
        
        /// <summary>
        /// Получить все атрибуты измерения
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <returns>возвращает класс <see cref="DimensionAttributes"/></returns>
        public async Task<List<DimensionAttributes>> GetAllDimensionAttributesAsync(string databaseId, string dimId)
        {
            var response = await _client.GetAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/dimensions/{dimId}/attributes");
            var result = JsonConvert.DeserializeObject<List<DimensionAttributes>>
                (await response.Content.ReadAsStringAsync());
            return result;
        }

        /// <summary>
        /// Получаем аттрибут измерения по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <param name="attributeId">Id аттрибута измерения</param>
        /// <returns>Возврашает аттрибут измерения <see cref="DimensionAttributes"/></returns>
        public async Task<DimensionAttributes> GetAttributeAsync(string databaseId, string dimId, string attributeId)
        {
            var response = await _client.GetAsync(AppPath + 
                @$"/viqube/metadata/databases/{databaseId}/dimensions/{dimId}/attributes/{attributeId}");
            var result = JsonConvert.DeserializeObject<DimensionAttributes>
                (await response.Content.ReadAsStringAsync());
            return result;
        }

        /// <summary>
        /// Получить значения атрибута измерений
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <param name="attributeId">Id аттрибута измерения</param>
        /// <returns>Возвращает список значений аттрибутов <see cref="List{T}"/></returns>
        public async Task<List<string>> GetAttributeValuesAsync(string databaseId, string dimId,
            string attributeId)
        {
            var response = await _client.GetAsync(AppPath + 
                @$"/viqube/metadata/databases/{databaseId}/dimensions/{dimId}/attributes/{attributeId}/values");
            var result = JsonConvert.DeserializeObject<List<string>>
                (await response.Content.ReadAsStringAsync());
            return result;
        }

        /// <summary>
        /// Получить все группы показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns>Возвращает группу показателей классом <see cref="MeasureGroupsGet"/></returns>
        public async Task<List<MeasureGroupsGet>> GetAllMeasureGroupsAsync(string databaseId)
        {
            var response = await _client.GetAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/measuregroups");
            var result = JsonConvert.DeserializeObject<List<MeasureGroupsGet>>
                (await response.Content.ReadAsStringAsync());
            return result;
        }

        /// <summary>
        /// Получить группу показателей по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <returns>Возвращает группу показателей классом <see cref="MeasureGroupsGet"/></returns>
        public async Task<MeasureGroupsGet> GetMeasureGroupAsync(string databaseId, string measureGroupId)
        {
            var response = await _client.GetAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}");
            var result = JsonConvert.DeserializeObject<MeasureGroupsGet>
                (await response.Content.ReadAsStringAsync());
            return result;
        }

        /// <summary>
        /// Получить все измерения в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <returns>Возвращает класс <see cref="MeasureExpression"/></returns>
        public async Task<List<MeasureExpression>> GetAllMeasuresAsync(string databaseId, string measureGroupId)
        {
            var response = await _client.GetAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}/measures");
            var result = JsonConvert.DeserializeObject<List<MeasureExpression>>
                (await response.Content.ReadAsStringAsync());
            return result;
        }

        /// <summary>
        /// Получить измерение по Id в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="measureId">Id  измерения в группе показателей</param>
        /// <returns>Возвращает класс <see cref="MeasureExpression"/></returns>
        public async Task<MeasureExpression> GetMeasureAsync(string databaseId,
            string measureGroupId, string measureId)
        {
            var response = await _client.GetAsync(AppPath + 
                @$"/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}/measures/{measureId}");
            var result = JsonConvert.DeserializeObject<MeasureExpression>
                (await response.Content.ReadAsStringAsync());
            return result;
        }

        /// <summary>
        /// Получить все роли измерений в группе показателей
        /// </summary>
        /// <param name="databaseId"></param>
        /// <param name="measureGroupId"></param>
        /// <returns>Возвращает класс <see cref="DimensionRolesGet"/></returns>
        public async Task<List<DimensionRolesGet>> GetAllDimensionsRolesAsync(string databaseId, string measureGroupId)
        {
            var response = await _client.GetAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}/dimensionroles");
            var result = JsonConvert.DeserializeObject<List<DimensionRolesGet>>
                (await response.Content.ReadAsStringAsync());
            return result;
        }
        
        /// <summary>
        /// Получить роль измерения по ID в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="dimensionRoleId">Id роли измерений</param>
        /// <returns>Возвращает класс <see cref="DimensionRolesGet"/></returns>
        public async Task<DimensionRolesGet> GetDimensionRoleAsync(string databaseId, string measureGroupId, string dimensionRoleId)
        {
            var response = await _client.GetAsync                (AppPath +
                 @$"/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}/dimensionroles/{dimensionRoleId}");
            if (response.Content != null)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
            }
            var result = JsonConvert.DeserializeObject<DimensionRolesGet>
                (await response.Content.ReadAsStringAsync());
            return result;
        }
        
        /// <summary>
        /// Получтиь связи в базе данных
        /// </summary>
        /// <param name="databaseId">Id базы данных/param>
        ///     <returns>Возращает класс связей <see cref="BindingDim"/></returns>
        public async Task<List<BindingDim>> GetBindingsAsync(string databaseId)
        {
            var response = await _client.GetAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/bindings");
            var result = JsonConvert.DeserializeObject<List<BindingDim>>
                (await response.Content.ReadAsStringAsync());
            return result;
        }
        
        #endregion

        #region PostMethods

        /// <summary>
        /// Создает базу данных в пространстве метаданных
        /// </summary>
        /// <param name="metaDatabase">Класс данных для создания базы данных в пространстве MetaData</param>
        /// <returns></returns>
        public async Task CreateMetaDatabaseAsync(MetaDatabase metaDatabase)
        {
            try
            {
                //Создает базу данных на сервере с присвоением имени и id
                var stringPayload = JsonConvert.SerializeObject(metaDatabase);
                var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                var httpResponse = await _client.PostAsync
                    ($"{AppPath}/viqube/metadata/databases", content);
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
        /// Создает новое измерение
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimension">Новое измерение которое хотим создать</param>
        /// <returns></returns>
        public async Task CreateDimensionsDatabaseAsync(string databaseId, DimensionsCreate dimension)
        {
            var stringPayload = JsonConvert.SerializeObject(dimension);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync
                ($"{AppPath}/viqube/metadata/databases/{databaseId}/dimensions", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Создает новый атрибут измерения
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <param name="attribute">Аттрибут который хотим внести в измерение</param>
        /// <returns></returns>
        public async Task CreateDimensionAttributesAsync(string databaseId, string dimId,
            DimensionAttributes attribute)
        {
            var stringPayload = JsonConvert.SerializeObject(attribute);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync
                ($"{AppPath}/viqube/metadata/databases/{databaseId}/dimensions/{dimId}/attributes", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Создать группу показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroup"> Группы показателей базы данных</param>
        /// <returns></returns>
        public async Task CreateMeasureGroupAsync(string databaseId, MeasureGroups measureGroup)
        {
            var stringPayload = JsonConvert.SerializeObject(measureGroup);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync
                ($"{AppPath}/viqube/metadata/databases/{databaseId}/measuregroups", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Создает измерение в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="measure">Измерение в группе показателей</param>
        /// <returns></returns>
        public async Task CreateMeasuresAsync(string databaseId, string measureGroupId, MeasureExpression measure)
        {
            var stringPayload = JsonConvert.SerializeObject(measure);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync( AppPath+
                @$"/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}/measures", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Создает роль измерений в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="dimensionRole">роль измерений</param>
        /// <returns></returns>
        public async Task CreateDimensionRoleAsync(string databaseId, string measureGroupId, DimensionRolesGet dimensionRole)
        {
            var stringPayload = JsonConvert.SerializeObject(dimensionRole);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(
                @$"{AppPath}/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}/dimensionroles", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Создает связи в базе данных
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="binding">Связь в базе данных</param>
        /// <returns></returns>
        public async Task CreateBindingsAsync(string databaseId, BindingDim binding)
        {
            var stringPayload = JsonConvert.SerializeObject(binding);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync
                ($"{AppPath}/viqube/metadata/databases/{databaseId}/bindings", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        #endregion

        #region PutMethods

        /// <summary>
        /// Изменяет базу данных в пространстве MetaData
        /// </summary>
        /// <param name="previousDatabaseId">Id базы данных которую хотим изменить</param>
        /// <param name="metaDatabase">Класс содержащий параметры базы данных в MetaData</param>
        /// <returns></returns>
        public async Task UpdateMetaDatabaseAsync(string previousDatabaseId, MetaDatabase metaDatabase)
        {
            //Обновляет базу данных на сервере с присвоением имени и id
            var stringPayload = JsonConvert.SerializeObject(metaDatabase);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync
                ($"{AppPath}/viqube/metadata/databases/{previousDatabaseId}", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Изменяет измерение по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения который хотим изменить</param>
        /// <param name="dimension">Измерение которое хотим внести</param>
        /// <returns></returns>
        public async Task UpdateDimensionAsync(string databaseId, string dimId, DimensionsCreate dimension)
        {
            var stringPayload = JsonConvert.SerializeObject(dimension);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync
                ($"{AppPath}/viqube/metadata/databases/{databaseId}/dimensions/{dimId}", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Изменяет аттрибут измерения по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <param name="attributeId">Id аттрибута измерения</param>
        /// <param name="attribute">Атрибут который хотим внести</param>
        /// <returns></returns>
        public async Task UpdateDimensionAttributesAsync(string databaseId, string dimId,
            string attributeId, DimensionAttributes attribute)
        {
            var stringPayload = JsonConvert.SerializeObject(attribute);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync
                ($"{AppPath}/viqube/metadata/databases/{databaseId}/dimensions/{dimId}/attributes/{attributeId}", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Изменить группу показателей по ID группы
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="measureGroup">Группа показателей базы данных</param>
        /// <returns></returns>
        public async Task UpdateMeasureGroupAsync(string databaseId, string measureGroupId, MeasureGroups measureGroup)
        {
            var stringPayload = JsonConvert.SerializeObject(measureGroup);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync
                ($"{AppPath}/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}", content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Изменить измерение по ID в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="measureId">Id  измерения в группе показателей</param>
        /// <param name="measure">Измерение в группе показателей</param>
        /// <returns></returns>
        public async Task UpdateMeasureAsync(string databaseId, string measureGroupId,
            string measureId, MeasureExpression measure)
        {
            var stringPayload = JsonConvert.SerializeObject(measure);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(
                @$"{AppPath}/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}/measures/{measureId}",
                content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Изменить роль измерения по ID в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="dimensionRoleId">Id роли измерений</param>>
        /// <param name="dimensionRole">Роль измерений</param>
        /// <returns></returns>
        public async Task UpdateDimensionRoleAsync(string databaseId, string measureGroupId, string dimensionRoleId,
            DimensionRolesGet dimensionRole)
        {
            var stringPayload = JsonConvert.SerializeObject(dimensionRole);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync
                (@$"{AppPath}/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}/dimensionroles/{dimensionRoleId}",
                    content);

            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
        }
        #endregion

        #region DeleteMethods

        /// <summary>
        /// Удаляет базу данных из пространства MetaData
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns></returns>
        public async Task DeleteMetaDatabaseAsync(string databaseId)
        {
            var response = await _client.DeleteAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}");
            var result = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удаляет все измерения из базы данных
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns></returns>
        public async Task DeleteAllDimensionsDatabaseAsync(string databaseId)
        {
            var response = await _client.DeleteAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/dimensions");
            var result = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удаляет измерение по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения в базе данных</param>
        /// <returns></returns>
        public async Task DeleteDimensionAsync(string databaseId, string dimId)
        {
            var response = await _client.DeleteAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/dimensions/{dimId}");
            var result = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удалить атрибут измерения
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <param name="attributeId">Id аттрибута измерения</param>
        /// <returns></returns>
        public async Task DeleteDimensionAttributeAsync(string databaseId, string dimId, string attributeId)
        {
            var response = await _client.DeleteAsync
            (AppPath + 
             @$"/viqube/metadata/databases/{databaseId}/dimensions/{dimId}/attributes/{attributeId}");
            var result = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удалить все группы показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns></returns>
        public async Task DeleteAllMeasureGroupsAsync(string databaseId)
        {
            var response = await _client.DeleteAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/measuregroups");
            var result = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удалить группу показателей по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <returns></returns>
        public async Task DeleteMeasureGroupAsync(string databaseId, string measureGroupId)
        {
            var response = await _client.DeleteAsync
                (AppPath + $"/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}");
            var result = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удаляет все измерения в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <returns></returns>
        public async Task DeleteAllMeasuresAsync(string databaseId, string measureGroupId)
        {
            var response = await _client.DeleteAsync
            (AppPath + $"/viqube/metadata/databases/{databaseId}" +
             $"/measuregroups/{measureGroupId}/measures");
            var result = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удаляет измерение по ID в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="measureId">Id  измерения в группе показателей</param>
        /// <returns></returns>
        public async Task DeleteMeasureAsync(string databaseId, string measureGroupId, string measureId)
        {
            var response = await _client.DeleteAsync
            (AppPath + $"/viqube/metadata/databases/{databaseId}/measuregroups/{measureGroupId}/measures/{measureId}");
            var result = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удаляет все роли измерений в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <returns></returns>
        public async Task DeleteAllDimensionRolesAsync(string databaseId, string measureGroupId)
        {
            var response = await _client.DeleteAsync
            (AppPath + $"/viqube/metadata/databases/{databaseId}" +
             $"/measuregroups/{measureGroupId}/dimensionsroles");
            var result = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удалить роль измерения по ID в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="dimensionRoleId">Id роли измерений</param>
        /// <returns></returns>
        public async Task DeleteDimensionRoleAsync(string databaseId, string measureGroupId, string dimensionRoleId)
        {
            var response = await _client.DeleteAsync
            (AppPath + $"/viqube/metadata/databases/{databaseId}" +
             $"/measuregroups/{measureGroupId}/dimensionsroles/{dimensionRoleId}");
            var result = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удаляет все связи в базе данных
        /// </summary>
        /// <param name="databaseId">Id базы данных/param>
        /// <returns></returns>
        public async Task DeleteAllBindingsAsync(string databaseId)
        {
            var response = await _client.DeleteAsync
                (AppPath + @$"/viqube/metadata/databases/{databaseId}/bindings/all");
            var result = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Удаляет конкретную связь в базе данных
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="bindingId">Cвязи в базе данных которую хотим удалить</param>
        /// <returns></returns>
        public async Task DeleteBindingAsync(string databaseId, BindingDim bindingId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                AppPath + $"/viqube/metadata/databases/{databaseId}/bindings");
            request.Content = new StringContent(JsonConvert.SerializeObject(bindingId),
                Encoding.UTF8, "application/json");
            var response = await _client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
        }
        #endregion
    }
}
