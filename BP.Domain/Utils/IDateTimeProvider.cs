namespace BP.Domain.Utils;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}