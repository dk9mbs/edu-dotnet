﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace De.Dk9mbs.Edu.Restclient
{
    [DataContract(Name ="location")]
    public class Location
    {
        [DataMember(Name ="locationid")]
        [Newtonsoft.Json.JsonProperty("locationid")]
        public string LocationId { get; set; }

        [DataMember(Name ="description")]
        [Newtonsoft.Json.JsonProperty("description")]
        public string Description { get; set; }
    }
}
