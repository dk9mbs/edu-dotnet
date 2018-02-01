using System;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace edu_cli_rest_core
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            
            var locations = ProcessRepositories().Result;
            foreach (var loc in locations)
                Console.WriteLine(loc.LocationId+"=>"+loc.Description);
            
            Console.WriteLine("*** END ***");
            Console.ReadLine();
        }


        private async static Task<List<Location>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            
            var serializer = new DataContractJsonSerializer(typeof(List<Location>));
            var streamTask = client.GetStreamAsync("http://localhost:56625/api/values");
            var locations = serializer.ReadObject(await streamTask) as List<Location>;
            /*    
            foreach (var loc in locations)
                Console.WriteLine(loc.LocationId + " =>" + loc.Description);
            */
            return locations;
            
        }

    }
}
