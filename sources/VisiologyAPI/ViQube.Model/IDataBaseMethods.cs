using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViQube.Model
{
    public interface IDataBaseMethods
    {
        /// <summary>
        /// Метод для получения X-API-Version
        /// </summary>
        /// <returns>Возвращает Api-stable версию</returns>
        public Task<string> GetVersionAsync();

        /// <summary>
        /// Метод получения данных Всех Databases
        /// </summary>
        /// <returns>Возвращает все базы данных</returns>
        public Task<List<Database>> GetAllDataBasesAsync();

        /// <summary>
        /// Получить конкретную базу данных
        /// </summary>
        /// <param name="databaseName">Имя базы данных</param>
        /// <returns>Возвращает конкретную базу данных</returns>
        public Task<Database> GetDataBaseAsync(string databaseName);

        /// <summary>
        /// Показать все таблицы конкретной базы данных
        /// </summary>
        /// <param name="dataBaseName">Имя базы данных</param>
        /// <returns>Возвращает все таблицы базы данных</returns>
        public Task<List<Table>> GetAllTablesAsync(string tableName);

        /// <summary>
        /// Показать таблицу конкретную таблицу в заданой базе данных
        /// </summary>
        /// <param name="dataBaseName">Имя базы данных</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <returns>Возвращает таблицу базы данных</returns>
        public Task<Table> GetTableAsync (string databaseName, string tableName);

        /// <summary>
        /// Метод для получения список столбцов в таблице
        /// </summary>
        /// <param name="dataBaseName">Имя базы данных</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <returns>Возвращает список столбцов в таблице</returns>
        public Task<List<Column>> GetColumnsAsync (string databaseName, string tableName);

        /// <summary>
        /// Метод для получения столбца в таблице
        /// </summary>
        /// <param name="dataBaseName">Имя базы данных в которой хотим получить таблицу</param>
        /// <param name="tableName">Имя таблицы в которой хотим получить столбец</param>
        /// <param name="columnName">Имя столбца который хотим получить</param>
        /// <returns>Возвращает столбец с типом данных Column</returns>
        public Task<Column> GetColumnNameAsync(string databaseName, string tableName,string columnName);

        /// <summary>
        /// Метод для получения записей в таблице
        /// </summary>
        /// <param name="dataBaseName">Имя базы данных</param>
        /// <param name="tableName">Имя таблицы</param>
        /// <returns>Возвращает все записи в таблице</returns>
        public Task<Record> GetRecordsAsync (string databaseName, string tableName);

        /// <summary>
        /// Создание базы данных на сервере и в Metadata с присвоением ей имени 
        /// </summary>
        /// <param name="nameBase">Имя базы данных</param>
        /// <returns></returns>
        public Task PostCreateDatabaseAsync(string databaseName);

        /// <summary>
        /// Метод POST Для создания таблицы в базе данных вместе со столбцами и первичным ключов
        /// </summary>
        /// <param name="nameBase"></param>
        /// <param name="columnList">Список столбоцов для добавления в таблицу</param>
        /// <param name="nameTable">Имя таблицы</param>
        /// <param name="primaryKey">Первичный ключ в таблице</param>
        /// <returns></returns>
        public Task PostCreateTableAsync(string nameBase, List<Column> columnList,
            string nameTable, string primaryKey);

        /// <summary>
        /// Post Методя для создания столбца в таблице базы данных
        /// </summary>
        /// <param name="nameBase">Имя базы данных в которой создается столбец</param>
        /// <param name="tableName">Имя таблицы  в которой создается столбец </param>
        /// <param name="columnName">Имя столбца</param>
        /// <param name="columnType">Тип столбца</param>
        /// <returns></returns>
        public Task PostCreateColumnAsync(string nameBase, string tableName,
            string columnName, string columnType);

        /// <summary>
        /// Post метод для добавления записей в таблицу
        /// </summary>
        /// <param name="nameBase">Имя базы данных в которой создается столбец</param>
        /// <param name="tableName">Имя таблицы  в которой создается столбец </param>
        /// <param name="records">Записи которые будем вставлять в таблицы</param>
        /// <returns></returns>
        public Task PostRecordsAsync(string nameBase,
            string tableName, Record records);

        /// <summary>
        /// Переименовывает базу данных из nameBase в renameBase 
        /// </summary>
        /// <param name="nameBase">Текущее имя базы данных</param>
        /// <param name="renameBase">Новое имя базы данных</param>
        /// <returns></returns>
        public Task PutRenameDataBaseAsync(string nameBase, string renameBase);

        /// <summary>
        /// Put метод для изменения имени таблицы в конкретной базе данных
        /// </summary>
        /// <param name="nameBase">Имя базы данных в которой нужно поменять таблицу</param>
        /// <param name="tableName">Имя таблицы которую нужно поменять</param>
        /// <param name="rename">Новое имя для таблицы которую хотим поменять</param>
        /// <returns></returns>
        public Task PutRenameTableAsync(string nameBase, string tableName, string rename);

        /// <summary>
        /// Метод для переименования столбца в таблице
        /// </summary>
        /// <param name="nameBase">Имя базы данных в которой нужно поменять таблицу</param>
        /// <param name="tableName">Имя таблицы в которой нужно поменять столбец</param>
        /// <param name="columnName">Имя столбца который хотим изменить</param>
        /// <param name="rename">Новое имя для таблицы которую хотим поменять</param>
        /// <returns></returns>
        public Task PutRenameColumnAsync(string nameBase, string tableName,
            string columnName, string rename);

        /// <summary>
        /// Метод удаления всех таблиц
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <returns></returns>
        public Task DeleteAllTablesAsync(string databaseName);

        /// <summary>
        /// Метод удаления конкретной таблицы
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <param name="tableName">Имя таблицы в которую нужно удалить</param>
        /// <returns></returns>
        public Task DeleteTablesAsync(string databaseName,
            string tableName);

        /// <summary>
        /// Метод удаления конкретного столбца в таблице
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <param name="tableName">Имя таблицы в которой нужно удалить столбец</param>
        /// <param name="columnName">Имя столбца который хотим удалить</param>
        /// <returns></returns>
        public Task DeleteColumnAsync(string databaseName,
            string tableName, string columnName);

        /// <summary>
        /// Метод удаления всех столбцов в таблице
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <param name="tableName">Имя таблицы в которой нужно удалить столбцы</param>
        /// <returns></returns>
        public Task DeleteAllColumnsAsync(string databaseName,
            string tableName);

        /// <summary>
        /// Метод удаления всех записей в таблице
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <param name="tableName">Имя таблицы в которой нужно записи</param>
        /// <returns></returns>
        public Task DeleteAllRecordsAsync(string databaseName, string tableName);

        /// <summary>
        /// Метод удаления конкретной записи в таблице
        /// </summary>
        /// <param name="databaseName">Имя базы данных в которой нужно удалить</param>
        /// <param name="tableName">Имя таблицы в которой нужно удалить запись</param>
        /// <param name="record">Значения которые собираемся удалить в таблице</param>
        /// <returns></returns>
        public Task DeleteRecordAsync(string databaseName,
            string tableName, List<object> record);
    }
}
