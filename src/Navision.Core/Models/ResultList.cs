﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CluedIn.Crawling.Navision.Core.Models
{
    public class ResultList<T>
    {
        [JsonProperty("@odata.context")]
        public string Context { get; set; }

        [JsonProperty("value")]
        public List<T> Value { get; set; }

        [JsonProperty("@odata.nextlink")]
        public string NextLink { get; set; }

        [JsonProperty("@odata.count")]
        public string Count { get; set; }

    }
}
