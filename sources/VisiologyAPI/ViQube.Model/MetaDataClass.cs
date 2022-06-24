using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace ViQube.Model
{
    /// <summary>
    /// Класс MetaDatabase нужен для создания базы данных в пространстве метаданных Viqube
    /// </summary> 
    public class MetaDatabase
    {
        [JsonProperty("databaseName", NullValueHandling = NullValueHandling.Ignore)]
        public string DatabaseName { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    public class DimensionsGet
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }

    public class DimensionsCreate
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)] 
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)] 
        public string Name { get; set; }

    }

    public class DimensionAttributes
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }


        [JsonProperty("sortAttributeId",NullValueHandling = NullValueHandling.Ignore)]
        public string SortAttributeId { get; set; }
    }

    public class DimensionRolesGet
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("dimid", NullValueHandling = NullValueHandling.Ignore)]
        public string DimId { get; set; }
        [JsonProperty("isPrimary")]
        public bool IsPrimary =true;
    }

    public class MeasureGroupsGet
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("tableName", NullValueHandling = NullValueHandling.Ignore)]
        public string TableName { get; set; }

        [JsonProperty("dimensionRoles", NullValueHandling = NullValueHandling.Ignore)]
        public List<DimensionRolesGet> DimensionRoles { get; set; }
    }

    public class MeasureGroups
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("tableName", NullValueHandling = NullValueHandling.Ignore)]
        public string TableName { get; set; }
    }

    public class MeasureExpression
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name",NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty("aggregator",NullValueHandling = NullValueHandling.Ignore)]
        public string Aggregator { get; set; }
        [JsonProperty("distinct",NullValueHandling = NullValueHandling.Ignore)]
        public bool Distinct { get; set; }
        [JsonProperty("type",NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        [JsonProperty("options",NullValueHandling = NullValueHandling.Ignore)]
        public Options Options { get; set; }
    }

    public class Options
    {
        [JsonProperty("columnName",NullValueHandling = NullValueHandling.Ignore)]
        public string ColumnName { get; set; }
        [JsonProperty("expression",NullValueHandling = NullValueHandling.Ignore)]
        public string Expression { get; set; }
    }

    public class Data
    {
        [JsonProperty("table",NullValueHandling = NullValueHandling.Ignore)]
        public string Table { get; set; }
        [JsonProperty("column",NullValueHandling = NullValueHandling.Ignore)]
        public string Column { get; set; }
    }

    public class Meta
    {
        [JsonProperty("dimid",NullValueHandling = NullValueHandling.Ignore)]
        public string Dimid { get; set; }
        [JsonProperty("attrid",NullValueHandling = NullValueHandling.Ignore)]
        public string AttrId { get; set; }
        [JsonProperty("mgid",NullValueHandling = NullValueHandling.Ignore)]
        public string MgId { get; set; }
        [JsonProperty("dimroleid",NullValueHandling = NullValueHandling.Ignore)]
        public string DimroleId { get; set; }
        [JsonProperty("granularity", NullValueHandling = NullValueHandling.Ignore)]
        public string Granularity { get; set; }
    }

    public class BindingDim
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }

}
