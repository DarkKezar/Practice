using MongoDB.Bson.Serialization.Attributes;

namespace Stock.Domain.Entities;

public class BaseEntity
{
    [BsonId]
    public Guid Id { get; set; }
}
