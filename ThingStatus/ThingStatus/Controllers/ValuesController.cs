using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Mp.Thingstatus.Models;

namespace Mp.Thingstatus.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Location> Get()
        {
            var locations = new List<Location>() { };
            using (var context = new DataContext() )
            {
                locations = (from l in context.Locations 
                    select new Location() { LocationId=l.LocationId, Description=l.Description } ).ToList<Location>()  ; 
            }

            return locations;
        }

        // GET api/values/5
        [HttpGet("{locationId}")]
        public Location Get(String locationId)
        {
            Location location = null;
            using (var context = new DataContext())
            {
                location = (from l in context.Locations
                    where l.LocationId== locationId
                            select new Location() { LocationId = l.LocationId, Description = l.Description }).Single<Location>();
            }
            return location;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
