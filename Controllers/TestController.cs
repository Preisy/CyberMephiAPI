using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using CyberMephiAPI.Models;
using Microsoft.AspNetCore.Mvc; // ? 
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace CyberMephiAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase {
    // private readonly ILogger<TestController> _logger; // ?

    [HttpGet]
    public IActionResult secureTest() {
        return Ok();
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult nonSecureTest() {
        return Ok();
    }
}