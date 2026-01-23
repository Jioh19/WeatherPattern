using System.Threading.Tasks;
using System.Collections.Generic;
using WeatherPattern.Event;
using WeatherPattern.Models;
using Xunit;

namespace WeatherPatternTests.Event;

public class TypeSelectorTest
{
    [Fact]
    public async Task ReadAsync_WithJsonFile_ReturnsWeatherList()
    {
        // Use a unique file name for each test run to avoid file sharing conflicts
        var fileName = $"input_{Guid.NewGuid()}.json";
        var testJson = "[\n  { \"Location\": \"New York\", \"Temperature\": 22.5, \"Humidity\": 60.2 },\n  { \"Location\": \"London\", \"Temperature\": 18.3, \"Humidity\": 75.0 }\n]\n";
        var inputDir = Path.Combine(AppContext.BaseDirectory, "Inputs");
        Directory.CreateDirectory(inputDir);
        var jsonPath = Path.Combine(inputDir, fileName);
        await File.WriteAllTextAsync(jsonPath, testJson);
        try
        {
            var file = jsonPath; // Use the direct path to avoid PathParser race
            var result = await TypeSelector.ReadAsync(file);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains(result, w => w.Location == "New York");
        }
        finally
        {
            if (File.Exists(jsonPath)) File.Delete(jsonPath);
        }
    }

    [Fact]
    public async Task ReadAsync_WithXmlFile_ReturnsWeatherList()
    {
        // Use a unique file name for each test run to avoid file sharing conflicts
        var fileName = $"input_{Guid.NewGuid()}.xml";
        var testXml = "<WeatherData>\n  <Weather>\n    <Location>London</Location>\n    <Temperature>18.3</Temperature>\n    <Humidity>75.0</Humidity>\n  </Weather>\n  <Weather>\n    <Location>New York</Location>\n    <Temperature>22.5</Temperature>\n    <Humidity>60.2</Humidity>\n  </Weather>\n</WeatherData>";
        var inputDir = Path.Combine(AppContext.BaseDirectory, "Inputs");
        Directory.CreateDirectory(inputDir);
        var xmlPath = Path.Combine(inputDir, fileName);
        await File.WriteAllTextAsync(xmlPath, testXml);
        try
        {
            var file = xmlPath; // Use the direct path to avoid PathParser race
            var result = await TypeSelector.ReadAsync(file);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains(result, w => w.Location == "London");
        }
        finally
        {
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
        }
    }

    [Fact]
    public async Task ReadAsync_WithInvalidFileType_ThrowsException()
    {
        // Create a temp file with an invalid extension to ensure the file exists
        var fileName = $"invalid_{Guid.NewGuid()}.txt";
        var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
        await File.WriteAllTextAsync(filePath, "irrelevant");
        try
        {
            await Assert.ThrowsAsync<InvalidDataException>(() => TypeSelector.ReadAsync(filePath));
        }
        finally
        {
            File.Delete(filePath);
        }
    }
}