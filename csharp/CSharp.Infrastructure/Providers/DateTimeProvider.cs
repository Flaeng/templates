namespace CSharp.Infrastructure.Providers;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}