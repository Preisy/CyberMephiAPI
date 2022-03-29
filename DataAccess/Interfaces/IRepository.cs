namespace DataAccess.Interfaces; 

public interface IRepository<TModel> {
    public Task<TModel?> findOneAsync(string key, string value);

    public Task<bool> setAsync(TModel user);

    public List<TModel> getAll();

    public void deleteAll();
}