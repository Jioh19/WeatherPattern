namespace WeatherPattern.Readers;

public interface IReader<T>
{
    Task<T> ReadAsync(string filePath);
}