using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Models; 

public class BaseModel {
    [BsonIgnoreIfNull]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [DefaultValue(null)]
    public string? id { get; set; }
}