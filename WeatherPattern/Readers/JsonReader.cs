using Newtonsoft.Json;
using WeatherPattern.Models;

namespace WeatherPattern.Readers;

public class JsonReader : IReader<Weather>
{
    public async Task<List<Weather>> ReadAsync(string filePath)
    {
        try
        {
            var jsonString = await File.ReadAllTextAsync(filePath);
            var weatherList = JsonConvert.DeserializeObject<List<Weather>>(jsonString);
            return weatherList ?? throw new Exception("Weather data is null");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }
}