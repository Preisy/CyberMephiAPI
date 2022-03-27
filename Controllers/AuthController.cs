using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc; // ? 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using CyberMephiAPI.Database;
using CyberMephiAPI.Models;

namespace CyberMephiAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase {
    private CyberMephiAPI.Database.Database database = new MongoDatabase();
    // [Required()]
    [HttpPost(Name = "logIn")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> logIn([FromBody] UserModelDto requestUser) {
        // Mediator

        UserModelDao user = new UserModelDao(requestUser);
        var res = await database.findOneAsync<UserModelDao>("email", user.email);
        if (res == null)
            return NotFound();
        else if (res.passwordHash != user.passwordHash)
            return Unauthorized();
        else
            return Ok();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> signUp([FromBody] UserModelDto requestUser) {
        UserModelDao user = new UserModelDao(requestUser);
        if (await database.findOneAsync<UserModelDao>("email", user.email) != null) {
            Console.WriteLine("This email is taken");
            return Conflict();
        }

        database.setAsync<UserModelDao>(user); // здесь может и не нужен await?
        return Ok();
    }

    [HttpPost]
    [Authorize]
    public ActionResult logOut() {
        // SignInManager
        return Ok();
    }
    

    [HttpGet]
    public List<UserModelDao> getAllUsers() {
        return database.getAll<UserModelDao>();
    }

    [HttpDelete]
    public int deleteAllUsers() {
        database.deleteAll<UserModelDao>();
        return 200;
    }
}