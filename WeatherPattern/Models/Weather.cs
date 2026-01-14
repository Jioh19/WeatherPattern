namespace WeatherPattern.Models;

public record Weather
{
    public required string Location { get; init; }
    public decimal Temperature { get; init; }   
    public decimal Humidity { get; init; }
}