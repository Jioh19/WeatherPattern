using WeatherPattern.Models;

namespace WeatherPattern.Readers;

public class XmlReader : IReader<Weather>
{
    public Task<Weather> ReadAsync(string filePath)
    {
        throw new NotImplementedException();
    }
}