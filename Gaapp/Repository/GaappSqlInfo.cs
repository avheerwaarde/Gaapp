using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaapp.Repository
{
    public class GaappSqlInfo
    {
        public  string SqlTableName { get; set; }
        public List<string> SqlProperties { get; set; }

        public string SqlSelect { get; set; }
        public string SqlDelete { get; set; }
        public string SqlInsert { get; set; }
        public string SqlUpdate { get; set; }
    }
}
