using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace WebApplication3.Models;

public class UserModelDAO : BaseModel {
    public UserModelDAO(UserModelDTO user) {
        this.email = user.email;
        this.passwordHash = this.getHashPassword(user.password);
    }

    public string email { get; set; }
    public Guid passwordHash { get; set; }

    private Guid getHashPassword(string s) {
        byte[] bytes = Encoding.Unicode.GetBytes(s);
        MD5CryptoServiceProvider CSP =
            new MD5CryptoServiceProvider();
        byte[] byteHash = CSP.ComputeHash(bytes);
        string hash = string.Empty;
        foreach (byte b in byteHash)
            hash += string.Format("{0:x2}", b);
        return new Guid(hash);
    }
}