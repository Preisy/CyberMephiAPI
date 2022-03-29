using DataAccess.Models;

namespace DataAccess.Interfaces; 

public interface IUnitOfWork {
    IRepository<UserModelDao> user { get; }
    IRepository<LectureModelDao> lecture { get; }
}