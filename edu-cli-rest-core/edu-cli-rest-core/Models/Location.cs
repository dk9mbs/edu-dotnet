using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;

namespace edu_cli_rest_core
{
    [DataContract(Name = "location")]
    public class Location
    {
        [DataMember(Name ="locationid")]
        [Newtonsoft.Json.JsonProperty("locationid")]
        public string LocationId { get; set; }


        [DataMember(Name = "description")]
        [Newtonsoft.Json.JsonProperty("description")]
        public string Description { get; set; }
    }

}