using Newtonsoft.Json;

namespace ViQube.Model
{
    
    /// <summary>
    /// Класс Database нужен для создание базы данных в Viqube
    /// </summary>
    public class Database
    {
        [JsonProperty("name")]
        public string NameDatabase { get; set; }

        [JsonProperty("tables", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Tables { get; set; }
    }


    /// <summary>
    /// Класс для обозначения столбцов в таблице
    /// </summary>
    public class Column
    {
        [JsonConstructor]
        public Column(string name, string type)
        {
            this.NameTable = name;
            this.TableType = type;
        }

        [JsonProperty("name")]
        public string NameTable { get; set; }

        [JsonProperty("type")]
        public string TableType { get; set; }
    }
    
    /// <summary>
    /// Класс для обозначения таблицы в базе данных
    /// </summary>
    public class Table
    {
        [JsonConstructor]
        public Table(List<Column> columns, string name, string primary)
        {
            this.Columns = columns;
            this.TableName = name;
            this.Primary = primary;
        }
        [JsonProperty("columns")]
        public List<Column> Columns { get; set; }

        [JsonProperty("name")]
        public string TableName { get; set; }

        [JsonProperty("primary", NullValueHandling = NullValueHandling.Ignore)]
        public string Primary { get; set; }
    }

    /// <summary>
    /// Класс записей для занесения в таблицу
    /// </summary>
    public class Record
    {
        [JsonProperty("columns")]
        public List<string>? Columns { get; set; }

        [JsonProperty("values")]
        public List<List<object>> Values { get; set; }
    }
}