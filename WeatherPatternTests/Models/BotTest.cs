using WeatherPattern.Models;
using Xunit;

namespace WeatherPatternTests.Models;

public class BotTest
{
    [Fact]
    public void Bot_DefaultValues_AreCorrect()
    {
        var bot = new Bot();
        Assert.False(bot.Enabled);
        Assert.Null(bot.Message);
        Assert.Equal(0m, bot.Value);
        Assert.Equal(BotTypeEnum.Temperature, bot.Type); // default enum value
    }

    [Fact]
    public void Bot_SetProperties_ValuesAreSet()
    {
        var bot = new Bot
        {
            Enabled = true,
            Message = "Test message",
            Type = BotTypeEnum.Humidity
        };
        bot.Value = 42.5m;

        Assert.True(bot.Enabled);
        Assert.Equal("Test message", bot.Message);
        Assert.Equal(42.5m, bot.Value);
        Assert.Equal(BotTypeEnum.Humidity, bot.Type);
    }

    [Fact]
    public void Bot_ToString_ReturnsExpectedFormat()
    {
        var bot = new Bot
        {
            Enabled = true,
            Message = "Hello",
            Type = BotTypeEnum.Humidity
        };
        bot.Value = 99.9m;
        var expected = "Value: 99.9, Enabled: True, Message: Hello, Type: Humidity";
        Assert.Equal(expected, bot.ToString());
    }
}