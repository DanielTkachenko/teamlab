using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViQube.Model
{
    public interface IMetaData
    {
        /// <summary>
        /// Получить все базы данных из мета данных
        /// </summary>
        /// <returns>Возвращает <see cref="MetaDatabase"/></returns>
        public Task<List<MetaDatabase>> GetAllMetaDataBasesAsync();

        /// <summary>
        /// Создает базу данных в пространстве метаданных
        /// </summary>
        /// <param name="metaDatabase">Класс данных для создания базы данных в пространстве MetaData</param>
        /// <returns></returns>
        public Task CreateMetaDatabaseAsync(MetaDatabase metaDatabase);

        /// <summary>
        /// Удаляет базу данных из пространства MetaData
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns></returns>
        public Task DeleteMetaDatabaseAsync(string databaseId);

        /// <summary>
        /// Изменяет базу данных в пространстве MetaData
        /// </summary>
        /// <param name="previousId">Предыдущий ID базы данных</param>
        /// <param name="metaDatabase"></param>
        /// <returns></returns>
        public Task UpdateMetaDatabaseAsync(string previousId, MetaDatabase metaDatabase);

        /// <summary>
        /// Получает конкретную базу данных по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns>Возвращает базу данных классом <see cref="MetaDatabase"/></returns>
        public Task<MetaDatabase> GetMetaDatabaseAsync(string databaseId);

        /// <summary>
        /// Получить все измерения в базе данных в пространстве MetaData
        /// </summary>
        /// <param name="databaseId">Id Базы данных</param>
        /// <returns>Возвращает измерения базы данных классом <see cref="Dimensions"/></returns>
        public Task<List<DimensionsGet>> GetAllDimensionsDatabaseAsync(string databaseId);

        /// <summary>
        /// Создает новое измерение
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimension">Новое измерение которое хотим создать</param>
        /// <returns></returns>
        public Task CreateDimensionsDatabaseAsync(string databaseId, DimensionsCreate dimension);

        /// <summary>
        /// Удаляет все измерения из базы данных
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns></returns>
        public Task DeleteAllDimensionsDatabaseAsync(string databaseId);

        /// <summary>
        /// Получить измерение по Id
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <returns>Возвращает <see cref="Dimensions"/></returns>
        public Task<DimensionsGet> GetDimensionAsync(string databaseId, string dimId);

        /// <summary>
        /// Изменяет измерение по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <param name="dimension">Измерение которое хотим внести</param>
        /// <returns></returns>
        public Task UpdateDimensionAsync(string databaseId, string dimId,DimensionsCreate dimension);

        /// <summary>
        /// Удаляет измерение по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения в базе данных</param>
        /// <returns></returns>
        public Task DeleteDimensionAsync(string databaseId, string dimId);

        /// <summary>
        /// Получить все атрибуты измерения
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <returns>возвращает класс <see cref="DimensionAttributes"/></returns>
        public Task<List<DimensionAttributes>> GetAllDimensionAttributesAsync(string databaseId, string dimId);

        /// <summary>
        /// Создает новый атрибут измерения
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <param name="attribute">Аттрибут который хотим внести в измерение</param>
        /// <returns></returns>
        public Task CreateDimensionAttributesAsync(string databaseId, string dimId, DimensionAttributes attribute);

        /// <summary>
        /// Получаем аттрибут измерения по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <param name="attributeId">Id аттрибута измерения</param>
        /// <returns>Возврашает аттрибут измерения <see cref="DimensionAttributes"/></returns>
        public Task<DimensionAttributes> GetAttributeAsync(string databaseId,string dimId, string attributeId);

        /// <summary>
        /// Изменяет аттрибут измерения по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <param name="attributeId">Id аттрибута измерения</param>
        /// <param name="attribute">Атрибут который хотим внести</param>
        /// <returns></returns>
        public Task UpdateDimensionAttributesAsync(string databaseId, string dimId, string attributeId,
            DimensionAttributes attribute);

        /// <summary>
        /// Удалить атрибут измерения
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <param name="attributeId">Id аттрибута измерения</param>
        /// <returns></returns>
        public Task DeleteDimensionAttributeAsync(string databaseId, string dimId, string attributeId);

        /// <summary>
        /// Получить значения атрибута измерений
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="dimId">Id измерения</param>
        /// <param name="attributeId">Id аттрибута измерения</param>
        /// <returns>Возвращает список значений аттрибутов <see cref="List{T}"/></returns>
        public Task<List<string>> GetAttributeValuesAsync(string databaseId, string dimId,string attributeId);

        /// <summary>
        /// Получить все группы показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns>Возвращает группу показателей классом <see cref="MeasureGroupsGet"/></returns>
        public Task<List<MeasureGroupsGet>> GetAllMeasureGroupsAsync(string databaseId);

        /// <summary>
        /// Создать группу показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroup"> Группы показателей базы данных</param>
        /// <returns></returns>
        public Task CreateMeasureGroupAsync(string databaseId,MeasureGroups measureGroup);

        /// <summary>
        /// Удалить все группы показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns></returns>
        public Task DeleteAllMeasureGroupsAsync(string databaseId);

        /// <summary>
        /// Получить группу показателей по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <returns>Возвращает группу показателей классом <see cref="MeasureGroupsGet"/></returns>
        public Task<MeasureGroupsGet> GetMeasureGroupAsync(string databaseId,string measureGroupId);

        /// <summary>
        /// Изменить группу показателей по ID группы
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="measureGroup">Группа показателей базы данных</param>
        /// <returns></returns>
        public Task UpdateMeasureGroupAsync(string databaseId, string measureGroupId, MeasureGroups measureGroup);

        /// <summary>
        /// Удалить группу показателей по ID
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <returns></returns>
        public Task DeleteMeasureGroupAsync(string databaseId,string measureGroupId);

        /// <summary>
        /// Получить все измерения в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <returns>Возвращает класс <see cref="MeasureExpression"/></returns>
        public Task<List<MeasureExpression>> GetAllMeasuresAsync(string databaseId,string measureGroupId);

        /// <summary>
        /// Создает измерение в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="measure">Измерение в группе показателей</param>
        /// <returns></returns>
        public Task CreateMeasuresAsync(string databaseId, string measureGroupId, MeasureExpression measure);

        /// <summary>
        /// Удаляет все измерения в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <returns></returns>
        public Task DeleteAllMeasuresAsync(string databaseId,string measureGroupId);

        /// <summary>
        /// Получить измерение по Id в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="measureId">Id  измерения в группе показателей</param>
        /// <returns>Возвращает класс <see cref="MeasureExpression"/></returns>
        public Task<MeasureExpression> GetMeasureAsync(string databaseId,string measureGroupId,string measureId);

        /// <summary>
        /// Изменить измерение по ID в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="measureId">Id  измерения в группе показателей</param>
        /// <param name="measure">Измерение в группе показателей</param>
        /// <returns></returns>
        public Task UpdateMeasureAsync(string databaseId,string measureGroupId,string measureId, MeasureExpression measure);

        /// <summary>
        /// Удаляет измерение по ID в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="measureId">Id  измерения в группе показателей</param>
        /// <returns></returns>
        public Task DeleteMeasureAsync(string databaseId,string measureGroupId,string measureId);

        /// <summary>
        /// Получить все роли измерений в группе показателей
        /// </summary>
        /// <param name="databaseId"></param>
        /// <param name="measureGroupId"></param>
        /// <returns>Возвращает класс <see cref="DimensionRolesGet"/></returns>
        public Task<List<DimensionRolesGet>> GetAllDimensionsRolesAsync(string databaseId,string measureGroupId);

        /// <summary>
        /// Создает роль измерений в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="dimensionRole">роль измерений</param>
        /// <returns></returns>
        public Task CreateDimensionRoleAsync(string databaseId,string measureGroupId,DimensionRolesGet dimensionRole);

        /// <summary>
        /// Удаляет все роли измерений в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <returns></returns>
        public Task DeleteAllDimensionRolesAsync(string databaseId,string measureGroupId);

        /// <summary>
        /// Получить роль измерения по ID в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="dimensionRoleId">Id роли измерений</param>
        /// <returns>Возвращает класс <see cref="DimensionRolesGet"/></returns>
        public Task<DimensionRolesGet> GetDimensionRoleAsync(string databaseId, string measureGroupId, string dimensionRoleId);

        /// <summary>
        /// Изменить роль измерения по ID в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="dimensionRoleId">Id роли измерений</param>>
        /// <param name="dimensionRole">Роль измерений</param>
        /// <returns></returns>
        public Task UpdateDimensionRoleAsync(string databaseId, string measureGroupId,string dimensionRoleId,DimensionRolesGet dimensionRole);

        /// <summary>
        /// Удалить роль измерения по ID в группе показателей
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="measureGroupId">Id Группы показателей</param>
        /// <param name="dimensionRoleId">Id роли измерений</param>
        /// <returns></returns>
        public Task DeleteDimensionRoleAsync(string databaseId,string measureGroupId,string dimensionRoleId);

        /// <summary>
        /// Получтиь связи в базе данных
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns>Возращает класс связей <see cref="BindingDim"/></returns>
        public Task<List<BindingDim>> GetBindingsAsync(string databaseId);

        /// <summary>
        /// Создает связи в базе данных
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <param name="binding">Связь в базе данных</param>
        /// <returns></returns>

        public Task CreateBindingsAsync(string databaseId, BindingDim binding);

        /// <summary>
        /// Удаляет все связи в базе данных
        /// </summary>
        /// <param name="databaseId">Id базы данных</param>
        /// <returns></returns>
        public Task DeleteAllBindingsAsync(string databaseId);

        /// <summary>
        /// Удаляет конкретную связь в базе данных
        /// </summary>
        /// <param name="databaseId">Id базы данных в которой удаляем связь</param>
        /// <param name="bindingId">Id связи которую хотим удалить</param>
        /// <returns></returns>
        public Task DeleteBindingAsync(string databaseId, BindingDim bindingId);
    }
}
