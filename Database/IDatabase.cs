namespace CyberMephiAPI.Database;

// interface IDatabase { // или так лучше?
public abstract class Database { 
    public abstract Task<TModel?> findOneAsync<TModel>(string key, string value);

    public abstract Task<bool> setAsync<TModel>(TModel user);

    public abstract List<TModel> getAll<TModel>();

    public abstract void deleteAll<TModel>();
}