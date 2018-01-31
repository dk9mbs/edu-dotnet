using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De.Dk9mbs.Edu.Restclient
{
    public class Location
    {
        [Newtonsoft.Json.JsonProperty("locationid")]
        public string LocationId { get; set; }
        [Newtonsoft.Json.JsonProperty("description")]
        public string Description { get; set; }
    }
}
