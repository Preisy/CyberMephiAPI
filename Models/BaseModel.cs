using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace WebApplication3.Models;



public abstract class BaseModel {
    [BsonIgnoreIfNull]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [DefaultValue(null)]
    public string? id { get; set; }
}