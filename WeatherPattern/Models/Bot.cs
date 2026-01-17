namespace WeatherPattern.Models;

public class Bot
{
    public bool Enabled { get; set; }
    public string? Message { get; set; }
    public decimal Value;
    public BotTypeEnum Type { get; set; }

    public override string ToString()
    {
        return
            $"{nameof(Value)}: {Value}, {nameof(Enabled)}: {Enabled}, {nameof(Message)}: {Message}, {nameof(Type)}: {Type}";
    }
}