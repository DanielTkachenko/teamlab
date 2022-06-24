using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ViQube.Model.Query
{
    public class Having
    {
        [JsonProperty("column")] public string Column { get; set; }

        [JsonProperty("operator")] public string Operator { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

    public class Join
    {
        [JsonProperty("table")] public string Table { get; set; }

        [JsonProperty("keys")] public List<Key> Keys { get; set; }
    }

    public class Key
    {
        [JsonProperty("column")] public string Column { get; set; }

        [JsonProperty("joinedOn")] public string JoinedOn { get; set; }
    }

    public class OrderBy
    {
        [JsonProperty("column", NullValueHandling = NullValueHandling.Ignore)]
        public string Column { get; set; }

        [JsonProperty("function", NullValueHandling = NullValueHandling.Ignore)]
        public string Function { get; set; }

        [JsonProperty("order")] public string Order { get; set; }
    }

    public class QuerySelect
    {
        [JsonProperty("from")]
        public string From { get; set; }
        
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int Limit { get; set; }

        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int Offset { get; set; }

        [JsonProperty("join", NullValueHandling = NullValueHandling.Ignore)]
        public List<Join> Join { get; set; }

        [JsonProperty("where", NullValueHandling = NullValueHandling.Ignore)]
        public List<Where> Where { get; set; }

        [JsonProperty("groupby", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> GroupBy { get; set; }

        [JsonProperty("orderby", NullValueHandling = NullValueHandling.Ignore)]
        public List<OrderBy> OrderBy { get; set; }

        [JsonProperty("having", NullValueHandling = NullValueHandling.Ignore)]
        public List<Having> Having { get; set; }
    }

    public class Where
    {
        [JsonProperty("column")] public string Column { get; set; }

        [JsonProperty("operator")] public string Operator { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

    public class Columns
    {
        public string name { get; set; }
    }
}
