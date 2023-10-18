using MongoDB.Bson.Serialization.Attributes;

namespace Cafe.Domain.Entities;

public class BaseEntity
{
    [BsonId]
    public Guid Id { get; set; }
}
