using System;
using System.IO;
using WeatherPattern.Utils;
using Xunit;

namespace WeatherPatternTests.Utils;

public class PathParserTest
{
    [Fact]
    public void GetPath_ReturnsExistingPath_WhenFileExistsInDirectPath()
    {
        var fileName = $"input_{Guid.NewGuid()}.json";
        var inputDir = Path.Combine(AppContext.BaseDirectory, "Inputs");
        Directory.CreateDirectory(inputDir);
        var expected = Path.Combine(inputDir, fileName);
        try
        {
            File.WriteAllText(expected, "test");
            var result = PathParser.GetPath(fileName);
            Assert.Equal(expected, result);
        }
        finally
        {
            if (File.Exists(expected)) File.Delete(expected);
        }
    }

    [Fact]
    public void GetPath_ReturnsExistingPath_WhenFileExistsInConsolePath()
    {
        var fileName = $"input_{Guid.NewGuid()}.json";
        var inputDir = Path.Combine(Directory.GetCurrentDirectory(), "Inputs");
        Directory.CreateDirectory(inputDir);
        var expected = Path.Combine(inputDir, fileName);
        try
        {
            File.WriteAllText(expected, "test");
            var result = PathParser.GetPath(fileName);
            Assert.Equal(expected, result);
        }
        finally
        {
            if (File.Exists(expected)) File.Delete(expected);
        }
    }

    [Fact]
    public void GetPath_ReturnsExistingPath_WhenFileExistsInRiderPath()
    {
        var fileName = $"input_{Guid.NewGuid()}.json";
        var riderDir = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName ?? string.Empty;
        var inputDir = Path.Combine(riderDir, "WeatherPattern", "Inputs");
        Directory.CreateDirectory(inputDir);
        var expected = Path.Combine(inputDir, fileName);
        try
        {
            File.WriteAllText(expected, "test");
            var result = PathParser.GetPath(fileName);
            Assert.Equal(expected, result);
        }
        finally
        {
            if (File.Exists(expected)) File.Delete(expected);
        }
    }

    [Fact]
    public void GetPath_ThrowsFileNotFoundException_WhenFileDoesNotExist()
    {
        var fileName = $"notfound_{Guid.NewGuid()}.json";
        Assert.Throws<FileNotFoundException>(() => PathParser.GetPath(fileName));
    }
}
