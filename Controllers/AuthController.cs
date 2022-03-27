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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost(Name = "logIn")]
    public async Task<ActionResult> logIn([FromBody] UserModelDTO requestUser) {
        
        Console.WriteLine("test");
        UserModelDAO user = new UserModelDAO(requestUser);
        var res = await database.findOneAsync<UserModelDAO>("email", user.email);
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
    public async Task<ActionResult> signUp([FromBody] UserModelDTO requestUser) {
        UserModelDAO user = new UserModelDAO(requestUser);
        if (await database.findOneAsync<UserModelDAO>("email", user.email) != null) {
            Console.WriteLine("This email is taken");
            return Conflict();
        }

        database.setAsync<UserModelDAO>(user); // здесь может и не нужен await?
        return Ok();
    }

    [HttpPost]
    [Authorize]
    public ActionResult logOut() {
        // SignInManager
        return Ok();
    }


    [HttpPost("test/{id}")]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(UserModelDTO))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> test([FromBody] UserModelDTO requestUser, int id) {
        Console.WriteLine(id);
        UserModelDAO user = new UserModelDAO(requestUser);
        if (await database.findOneAsync<UserModelDAO>("email", user.email) != null) {
            Console.WriteLine("This email is taken");
            return Conflict(requestUser);
        }

        // Database.setUserAsync(user); // здесь может и не нужен await?
        return Ok(requestUser);
    }

    [HttpGet]
    public List<UserModelDAO> getAllUsers() {
        return database.getAll<UserModelDAO>();
    }

    [HttpDelete]
    public int deleteAllUsers() {
        database.deleteAll<UserModelDAO>();
        return 200;
    }
}