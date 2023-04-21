namespace CSharp.Domain.Interfaces;

public interface IAuditable
{
    DateTimeOffset CreatedAt { get; }
    DateTimeOffset LastModifiedAt { get; }
}