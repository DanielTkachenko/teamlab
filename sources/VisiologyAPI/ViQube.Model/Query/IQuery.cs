using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViQube.Model.Query
{
    public interface IQuery
    {
        public Task CreateDatabaseQueryAsync(string databaseName, QuerySelect query);

        public Task CreateMetaDataQueryAsync(MetaDataQuery query);
    }
}
