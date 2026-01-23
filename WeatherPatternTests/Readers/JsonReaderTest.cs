using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherPattern.Models;
using Xunit;
using System.IO;
using Newtonsoft.Json;
using JsonReader = WeatherPattern.Readers.JsonReader;

namespace WeatherPatternTests.Readers;

public class JsonReaderTest
{
    private readonly JsonReader _reader = new JsonReader();

    [Fact]
    public async Task ReadAsync_WithValidJson_ReturnsWeatherList()
    {
        // Ensure the test file exists in a location PathParser will find
        var testJson = "[\n  { \"Location\": \"New York\", \"Temperature\": 22.5, \"Humidity\": 60.2 },\n  { \"Location\": \"London\", \"Temperature\": 18.3, \"Humidity\": 75.0 }\n]\n";
        var inputDir = Path.Combine(AppContext.BaseDirectory, "Inputs");
        Directory.CreateDirectory(inputDir);
        var jsonPath = Path.Combine(inputDir, "input.json");
        await File.WriteAllTextAsync(jsonPath, testJson);
        var filePath = WeatherPattern.Utils.PathParser.GetPath("input.json");
        var result = await _reader.ReadAsync(filePath);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, w => w.Location == "New York" && w.Temperature == 22.5m && w.Humidity == 60.2m);
        Assert.Contains(result, w => w.Location == "London");
        // Clean up
        File.Delete(jsonPath);
    }

    [Fact]
    public async Task ReadAsync_WithMissingFile_ThrowsException()
    {
        // Instead of using PathParser, generate a random path that does not exist
        var filePath = Path.Combine(Path.GetTempPath(), $"notfound_{Guid.NewGuid()}.json");
        await Assert.ThrowsAnyAsync<Exception>(async () => await _reader.ReadAsync(filePath));
    }

    [Fact]
    public async Task ReadAsync_WithMalformedJson_ThrowsException()
    {
        // Create a temp file with invalid JSON
        var tempFile = Path.GetTempFileName();
        await File.WriteAllTextAsync(tempFile, "not a json");
        try
        {
            await Assert.ThrowsAnyAsync<Exception>(async () => await _reader.ReadAsync(tempFile));
        }
        finally
        {
            File.Delete(tempFile);
        }
    }
}
