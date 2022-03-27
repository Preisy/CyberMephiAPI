using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WebApplication3.Models;
using Microsoft.AspNetCore.Mvc; // ? 
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace WebApplication3.Controllers;

using Database;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase {
    // private readonly ILogger<TestController> _logger; // ?

    [HttpGet]
    public IActionResult test() {
        return Ok();
    }
}