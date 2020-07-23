using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CluedIn.Crawling.Navision.Core.Models
{
    public class Result
    {
        public Result(SqlDataReader reader)
        {
            Value = reader["Value"].ToString();
        }
        public string Value { get; set; }
    }
}
