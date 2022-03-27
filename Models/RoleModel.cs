using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace CyberMephiAPI.Models; 

[CollectionName("Roles")]
public class RoleModel : MongoIdentityRole<Guid> {
    
}