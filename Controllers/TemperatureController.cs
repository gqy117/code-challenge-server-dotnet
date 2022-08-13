using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private static IList<Product> _products() => new [] {
            new Product { Id = "1", Name = "Pilsner", MinimumTemperature = 4,  MaximumTemperature = 6 },
            new Product { Id = "2", Name = "IPA", MinimumTemperature = 5, MaximumTemperature = 6 },
            new Product { Id = "3", Name = "Lager", MinimumTemperature = 4, MaximumTemperature = 7 },
            new Product { Id = "4", Name = "Stout", MinimumTemperature = 6, MaximumTemperature = 8 },
            new Product { Id = "5", Name = "Wheat beer", MinimumTemperature = 3, MaximumTemperature = 5 },
            new Product { Id = "6", Name = "Pale Ale", MinimumTemperature = 4, MaximumTemperature = 6 },
        };

        private readonly TemperatureService _temperatureService;
        
        public TemperatureController(TemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _temperatureService.GetById(id);

            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sensorData = await Task.WhenAll(_products()
                .Select(async (product) =>
                {
                    product.Temperature = (await _temperatureService.GetById(product.Id)).Temperature;
                    return product;
                }));
            
            return Ok(sensorData);
        }
    }
}
