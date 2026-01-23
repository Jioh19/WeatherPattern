using WeatherPattern.Models;
using Xunit;

namespace WeatherPatternTests.Models;

public class WeatherTest
{
    [Fact]
    public void Weather_CanBeCreated_WithRequiredProperties()
    {
        var weather = new Weather
        {
            Location = "Testville",
            Temperature = 23.5m,
            Humidity = 60.2m
        };
        Assert.Equal("Testville", weather.Location);
        Assert.Equal(23.5m, weather.Temperature);
        Assert.Equal(60.2m, weather.Humidity);
    }

    [Fact]
    public void Weather_Equality_WorksForSameValues()
    {
        var w1 = new Weather { Location = "A", Temperature = 1.1m, Humidity = 2.2m };
        var w2 = new Weather { Location = "A", Temperature = 1.1m, Humidity = 2.2m };
        Assert.Equal(w1, w2);
        Assert.True(w1 == w2);
    }

    [Fact]
    public void Weather_Inequality_WorksForDifferentValues()
    {
        var w1 = new Weather { Location = "A", Temperature = 1.1m, Humidity = 2.2m };
        var w2 = new Weather { Location = "B", Temperature = 1.1m, Humidity = 2.2m };
        Assert.NotEqual(w1, w2);
        Assert.True(w1 != w2);
    }

    [Fact]
    public void Weather_WithExpression_CreatesModifiedCopy()
    {
        var original = new Weather { Location = "A", Temperature = 1.1m, Humidity = 2.2m };
        var modified = original with { Temperature = 5.5m };
        Assert.Equal("A", modified.Location);
        Assert.Equal(5.5m, modified.Temperature);
        Assert.Equal(2.2m, modified.Humidity);
        Assert.NotEqual(original, modified);
    }
}
