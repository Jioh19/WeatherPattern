using System.Xml;
using WeatherPattern.Models;

namespace WeatherPattern.Readers;

public class XmlReader : IReader<Weather>
{
   public async Task<List<Weather>> ReadAsync(string filePath)
    {
        var settings = new XmlReaderSettings { Async = true };
        var weathers = new List<Weather>();

        using var reader = System.Xml.XmlReader.Create(filePath, settings);
        while (await reader.ReadAsync())
        {
            if (reader.NodeType != XmlNodeType.Element || reader.Name != "Weather")
            {
                continue;
            }
            string? location = null;
            decimal? temperature = null;
            decimal? humidity = null;

            while (await reader.ReadAsync())
            {
                if (reader is { NodeType: XmlNodeType.EndElement, Name: "Weather" })
                {
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Location":
                            location = await reader.ReadElementContentAsStringAsync();
                            break;
                        case "Temperature":
                            temperature = reader.ReadElementContentAsDecimal();
                            break;
                        case "Humidity":
                            humidity =  reader.ReadElementContentAsDecimal();
                            break;
                    }
                }
            }

            if (location != null && temperature.HasValue && humidity.HasValue)
            {
                weathers.Add(new Weather
                {
                    Location = location,
                    Temperature = temperature.Value,
                    Humidity = humidity.Value
                });
            }
        }

        return weathers;
    }
}