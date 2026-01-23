using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherPattern.Models;
using WeatherPattern.Readers;
using Xunit;
using System.IO;
using System.Xml;

namespace WeatherPatternTests.Readers;

public class XmlReaderTest
{
    [Fact]
    public async Task ReadAsync_WithValidXml_ReturnsWeatherList()
    {
        var reader = new WeatherPattern.Readers.XmlReader();
        var filePath = WeatherPattern.Utils.PathParser.GetPath("input.xml");
        var result = await reader.ReadAsync(filePath);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, w => w.Location == "New York" && w.Temperature == 22.5m && w.Humidity == 60.2m);
        Assert.Contains(result, w => w.Location == "London");
    }

    [Fact]
    public async Task ReadAsync_WithMissingFile_ThrowsException()
    {
        var reader = new WeatherPattern.Readers.XmlReader();
        // Instead of using PathParser, generate a random path that does not exist
        var filePath = Path.Combine(Path.GetTempPath(), $"notfound_{Guid.NewGuid()}.xml");
        await Assert.ThrowsAsync<FileNotFoundException>(() => reader.ReadAsync(filePath));
    }

    [Fact]
    public async Task ReadAsync_WithMalformedXml_ThrowsException()
    {
        var reader = new WeatherPattern.Readers.XmlReader();
        var tempFile = Path.GetTempFileName();
        await File.WriteAllTextAsync(tempFile, "<WeatherData><Weather><Location>Test</Location></WeatherData>"); // malformed XML
        await Assert.ThrowsAsync<XmlException>(() => reader.ReadAsync(tempFile));
        File.Delete(tempFile);
    }
}
