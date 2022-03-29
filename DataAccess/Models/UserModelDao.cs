using AspNetCore.Identity.MongoDbCore.Models;

namespace DataAccess.Models; 

public class UserModelDao : MongoIdentityUser<Guid> {
    public string email { get; set; }
    public Guid passwordHash { get; set; }
}