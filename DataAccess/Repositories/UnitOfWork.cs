using DataAccess.Interfaces;
using DataAccess.Models;
using MongoDB.Driver.Core.Configuration;

namespace DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork {
    private string connectionString;
    private string databaseName;
    private IRepository<UserModelDao>? _userRepository;
    private IRepository<LectureModelDao>? _lectureRepository;
    
    public IRepository<UserModelDao> user {
        get {
            if (this._userRepository == null)
                this._userRepository = new MongoDbRepository<UserModelDao>(connectionString, databaseName);
            return this._userRepository;
        }
    }
    public IRepository<LectureModelDao> lecture {
        get {
            if (this._lectureRepository == null)
                this._lectureRepository = new MongoDbRepository<LectureModelDao>(connectionString, databaseName);
            return this._lectureRepository;
        }
    }

    public UnitOfWork(string connectionString, string databaseName) {
        this.connectionString = connectionString;
        this.databaseName = databaseName;
    }
}