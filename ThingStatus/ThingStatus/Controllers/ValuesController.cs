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
    [Route("api/test")]
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
        public void Post([FromBody]Location location)
        {
            using(var context = new DataContext()) 
            {
                location.LocationId = Guid.NewGuid().ToString();                
                context.Add<Location>(location);
                context.SaveChanges();
            }
        }

        // PUT api/values/5
        [HttpPut("{locationId}")]
        public void Put(string locationId, [FromBody]Location location)
        {
            using (var context = new DataContext())
            {
                location.LocationId = locationId;
                context.Update<Location>(location);
                context.SaveChanges();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{locationId}")]
        public void Delete(string locationId)
        {
            using (var context = new DataContext())
            {
                context.Remove<Location>(new Location() { LocationId = locationId });
                context.SaveChanges();
            }
        }
    }
}
