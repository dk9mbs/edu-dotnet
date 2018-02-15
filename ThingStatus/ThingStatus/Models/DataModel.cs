using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Mp.Thingstatus.Models
{
    public class Thing
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public String Id { get; set; }
        [Newtonsoft.Json.JsonProperty("description")]
        public String Description { get; set; }
        [Newtonsoft.Json.JsonProperty("location")]
        public Location Location { get; set; }
    }

    public class Location
    {
        [Newtonsoft.Json.JsonProperty("locationid")]
        public string LocationId { get; set; }
        [Newtonsoft.Json.JsonProperty("description")]
        public string Description { get; set; }
    }


    public class ConfigurationFile
    {
        [Newtonsoft.Json.JsonProperty("profile")]
        public string Profile { get; set; }

        [Newtonsoft.Json.JsonProperty("path")]
        public string Path { get; set; }

        [Newtonsoft.Json.JsonProperty("data")]
        public string Data { get; set; }
    }

}