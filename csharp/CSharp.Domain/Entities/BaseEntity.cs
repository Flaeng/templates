using CSharp.Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace CSharp.Domain.Entities;

[PrimaryKey("Id")]
public abstract class BaseEntity : IIdentifiable, IAuditable
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; internal set; }
    public DateTimeOffset LastModifiedAt { get; internal set; }
}