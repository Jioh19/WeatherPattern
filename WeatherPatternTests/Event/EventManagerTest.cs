using System;
using System.Collections.Generic;
using WeatherPattern.Event;
using WeatherPattern.Models;
using Xunit;
using System.IO;

namespace WeatherPatternTests.Event;

public class EventManagerTest
{
    private static NameBot CreateNameBot(string name, bool enabled, decimal value, BotTypeEnum type, string? message = null)
    {
        return new NameBot
        {
            Name = name,
            Bot = new Bot
            {
                Enabled = enabled,
                Value = value,
                Type = type,
                Message = message ?? $"{name} alert!"
            }
        };
    }

    [Fact]
    public void Report_ActivatesHumidityBot_WhenHumidityExceedsValue()
    {
        var weather = new Weather { Location = "TestCity", Temperature = 20, Humidity = 80 };
        var bot = CreateNameBot("HumidBot", true, 70, BotTypeEnum.Humidity, "Humidity high!");
        var manager = new EventManager(new List<Weather> { weather }, new List<NameBot> { bot });

        using var sw = new StringWriter();
        Console.SetOut(sw);
        manager.Report();
        var output = sw.ToString();
        Assert.Contains("HumidBot activated!", output);
        Assert.Contains("Humidity high!", output);
    }

    [Fact]
    public void Report_ActivatesTemperatureBot_WhenTemperatureExceedsValue()
    {
        var weather = new Weather { Location = "TestCity", Temperature = 30, Humidity = 50 };
        var bot = CreateNameBot("TempBot", true, 25, BotTypeEnum.Temperature, "Temperature high!");
        var manager = new EventManager(new List<Weather> { weather }, new List<NameBot> { bot });

        using var sw = new StringWriter();
        Console.SetOut(sw);
        manager.Report();
        var output = sw.ToString();
        Assert.Contains("TempBot activated!", output);
        Assert.Contains("Temperature high!", output);
    }

    [Fact]
    public void Report_DoesNotActivateDisabledBot()
    {
        var weather = new Weather { Location = "TestCity", Temperature = 30, Humidity = 80 };
        var bot = CreateNameBot("DisabledBot", false, 10, BotTypeEnum.Temperature);
        var manager = new EventManager(new List<Weather> { weather }, new List<NameBot> { bot });

        using var sw = new StringWriter();
        Console.SetOut(sw);
        manager.Report();
        var output = sw.ToString();
        Assert.DoesNotContain("DisabledBot activated!", output);
    }

    [Fact]
    public void Report_ActivatesTemperatureBot_WhenTemperatureBelowZeroAndLessThanValue()
    {
        var weather = new Weather { Location = "TestCity", Temperature = -5, Humidity = 50 };
        var bot = CreateNameBot("ColdBot", true, 0, BotTypeEnum.Temperature, "It's freezing!");
        var manager = new EventManager(new List<Weather> { weather }, new List<NameBot> { bot });

        using var sw = new StringWriter();
        Console.SetOut(sw);
        manager.Report();
        var output = sw.ToString();
        Assert.Contains("ColdBot activated!", output);
        Assert.Contains("It's freezing!", output);
    }
}