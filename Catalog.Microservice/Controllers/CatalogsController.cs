using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.Microservice.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching","Indue"
        };

        private readonly ILogger<CatalogsController> _logger;
        private readonly IDistributedCache _distributedCache;

        public CatalogsController(ILogger<CatalogsController> logger, IDistributedCache distributedCache)
        {
            _logger = logger;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            _logger.LogInformation("Catalogs is called");
            var cacheresult = await _distributedCache.GetStringAsync("_catalogs");

            var expiryOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };

            if (string.IsNullOrWhiteSpace(cacheresult))
            {
                _logger.LogInformation("cache is not found");
                var rng = new Random();
                var result = Enumerable.Range(1, Summaries.Length).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

                await _distributedCache.SetStringAsync("_catalogs", JsonConvert.SerializeObject(result), expiryOptions);
                return result;
            }
            else
            {
                _logger.LogInformation("cache is not found");
                return JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(cacheresult);
            }
        }
    }
}
