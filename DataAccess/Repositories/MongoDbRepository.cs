using MongoDB.Bson;
using MongoDB.Driver;
using DataAccess.Interfaces;

namespace DataAccess.Repositories;

public class MongoDbRepository<TModel> : IRepository<TModel> {
    private readonly IMongoDatabase database;

    public MongoDbRepository(string connectionString, string databaseName) {
        var client = new MongoClient(connectionString);
        this.database = client.GetDatabase(databaseName);
    }

    private IMongoCollection<TModel> getCollection() {
        return database.GetCollection<TModel>(getCollectionName());
    }

    private string getCollectionName() {
        string name = typeof(TModel).Name;
        return name.Substring(0, name.IndexOf("Model")).ToLower();
    }   

    // public async Task<TModel?> findOneAsync<TModel>(string key, string value) where TModel : default {
    public async Task<TModel?> findOneAsync(string key, string value) {
        var collection = getCollection();
        var filter = Builders<TModel>.Filter.Eq(key, value);
        // var filter = Builders<TModel>.Filter.Eq();
        TModel? res = await collection.Find(filter).SingleOrDefaultAsync();
        return res;
    }

    public async Task<bool> setAsync(TModel user) {
        var usersCollection = getCollection();
        await usersCollection.InsertOneAsync(user);
        return true;
    }

    public List<TModel> getAll() {
        var collection = getCollection();
        var filter = new BsonDocument();
        var res = collection.Find(filter).ToList();
        return res;
    }

    public void deleteAll() {
        var usersCollection = getCollection();
        var filter = new BsonDocument();
        usersCollection.DeleteManyAsync(filter);
    }
}