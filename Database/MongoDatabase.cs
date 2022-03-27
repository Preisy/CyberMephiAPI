using CyberMephiAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CyberMephiAPI.Database; 

public class MongoDatabase : CyberMephiAPI.Database.Database {
    public MongoDatabase() {
        var client = new MongoClient(Config.MONGO_URL);
        this.database = client.GetDatabase(Config.MONGO_DATABASE_NAME);
    }
    
    // private readonly MongoClient client;
    private readonly IMongoDatabase database;

    public IMongoCollection<TModel> getCollection<TModel>() {
        return database.GetCollection<TModel>(getCollectionName<TModel>());
    }

    private string getCollectionName<TModel>() {
        string name = typeof(TModel).Name;
        return name.Substring(0, name.IndexOf("Model")).ToLower();
    }

    // public async Task<UserModelDAO?> findOneAsync2(string key, string value) {
    //     var usersCollection = getCollection<UserModelDAO>();
    //     var filter = Builders<UserModelDAO>.Filter.Eq(key, value);
    //     var res = await usersCollection.Find(filter).SingleOrDefaultAsync();
    //     // var res1 = usersCollection.Find(filter);
    //     return res;
    // }

    // public override async Task<TModel?> findOneAsync<TModel>(string key, string value) {
    public override async Task<TModel?> findOneAsync<TModel>(string key, string value) where TModel : default {
        var collection = getCollection<TModel>();
        var filter = Builders<TModel>.Filter.Eq(key, value);
        // var filter = Builders<TModel>.Filter.Eq();
        TModel? res = await collection.Find(filter).SingleOrDefaultAsync();
        return res;
    }

    public override async Task<bool> setAsync<TModel>(TModel user) {
        var usersCollection = getCollection<TModel>();
        await usersCollection.InsertOneAsync(user);
        return true;
    }

    public override List<TModel> getAll<TModel>() {
        var collection = getCollection<TModel>();
        var filter = new BsonDocument();
        var res = collection.Find(filter).ToList();
        return res;
    }

    public override void deleteAll<TModel>() {
        var usersCollection = getCollection<TModel>();
        var filter = new BsonDocument();
        usersCollection.DeleteManyAsync(filter);
    }
}