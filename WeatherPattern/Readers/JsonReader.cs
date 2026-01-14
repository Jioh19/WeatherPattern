using WeatherPattern.Models;

namespace WeatherPattern.Readers;

public class JsonReader : IReader<Weather>
{
    public Task<Weather> ReadAsync(string filePath)
    {
        throw new NotImplementedException();
    }
}