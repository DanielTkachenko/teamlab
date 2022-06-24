using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ViQube.Model.Query
{
    public class Attribute
    {
        [JsonProperty("dimroleid", NullValueHandling = NullValueHandling.Ignore)] 
        public string Dimroleid { get; set; }

        [JsonProperty("attrid")] 
        public string Attrid { get; set; }

        [JsonProperty("dimid", NullValueHandling = NullValueHandling.Ignore)]
        public string Dimid { get; set; }

        [JsonProperty("axis", NullValueHandling = NullValueHandling.Ignore)]
        public string Axis { get; set; }
    }

    public class Filter
    {
        [JsonProperty("type")] 
        public string Type { get; set; }

        [JsonProperty("value")] 
        public int Value { get; set; }

        public Attribute Attribute { get; set; }
    }

    public class From
    {
        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        [JsonProperty("offsets", NullValueHandling = NullValueHandling.Ignore)]
        public List<Offset> Offsets { get; set; }
    }

    public class Group
    {
        [JsonProperty("mgid")] 
        public string Mgid { get; set; }

        [JsonProperty("attributes", NullValueHandling = NullValueHandling.Ignore)]
        public List<Attribute> Attributes { get; set; }

        [JsonProperty("measures")]
        public List<Measure> Measures { get; set; }

        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)] 
        public List<List<Filter>> Filters { get; set; }

        [JsonProperty("runningTotal", NullValueHandling = NullValueHandling.Ignore)]
        public RunningTotal RunningTotal { get; set; }

        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)] 
        public List<Time> Time { get; set; }

        [JsonProperty("showempty", NullValueHandling = NullValueHandling.Ignore)]
        public bool Showempty { get; set; }
    }

    public class Limit
    {
        [JsonProperty("rows", NullValueHandling = NullValueHandling.Ignore)]
        public int Rows { get; set; }

        [JsonProperty("columns", NullValueHandling = NullValueHandling.Ignore)] 
        public int Columns { get; set; }
    }

    public class Measure
    {
        [JsonProperty("mid")] public string Mid { get; set; }

        [JsonProperty("alias", NullValueHandling = NullValueHandling.Ignore)]
        public string Alias { get; set; }

        [JsonProperty("expression", NullValueHandling = NullValueHandling.Ignore)]
        public string Expression { get; set; }

        [JsonProperty("aggregator", NullValueHandling = NullValueHandling.Ignore)]
        public string Aggregator { get; set; }

        [JsonProperty("distinct", NullValueHandling = NullValueHandling.Ignore)]
        public bool Distinct { get; set; }

        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public string Sort { get; set; }

        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)] 
        public List<Filter> Filters { get; set; }

        [JsonProperty("rowFilters", NullValueHandling = NullValueHandling.Ignore)]
        public List<RowFilter> RowFilters { get; set; }
    }

    public class Offset
    {
        [JsonProperty("period", NullValueHandling = NullValueHandling.Ignore)] 
        public string Period { get; set; }

        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public int Count { get; set; }
    }

    public class MetaDataQuery
    {
        [JsonProperty("database")] public string Database { get; set; }

        [JsonProperty("groups")] public List<Group> Groups { get; set; }

        [JsonProperty("limit")] public Limit Limit { get; set; }
    }

    public class RowFilter
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("value")] public int Value { get; set; }
    }

    public class RunningTotal
    {
        [JsonProperty("resetGranularity", NullValueHandling = NullValueHandling.Ignore)]
        public string ResetGranularity { get; set; }

        [JsonProperty("from")]
        public From From { get; set; }
    }

    public class Time
    {
        [JsonProperty("dimroleid", NullValueHandling = NullValueHandling.Ignore)] 
        public string Dimroleid { get; set; }

        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("to")]
        public To To { get; set; }
    }

    public class To
    {
        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        [JsonProperty("offsets", NullValueHandling = NullValueHandling.Ignore)]
        public List<Offset> Offsets { get; set; }
    }
}
