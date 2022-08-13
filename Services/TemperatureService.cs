using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using server.Models;

namespace server.Services
{
    public class TemperatureService
    {
        public async Task<Sensor> GetById(string id)
        {
            var http = new HttpClient();
            var url = $"https://temperature-sensor-service.herokuapp.com/sensor/{id}";

            var response = await http.GetAsync(url);
            var jsonString = await response.Content.ReadAsStringAsync();
            var sensorData = JsonSerializer.Deserialize<Sensor>(jsonString, new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return sensorData;
        }
    }
}