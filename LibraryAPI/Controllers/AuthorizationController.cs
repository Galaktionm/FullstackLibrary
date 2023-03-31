using LibraryApp.API.DTO;
using LibraryApp.API.Data.Entities;
using LibraryApp.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LibraryApp.API.Controllers {

[Route("api/authorization")]
[ApiController]
public class AuthorizationController : ControllerBase {

    private readonly UserManagementService service;

    public AuthorizationController(UserManagementService service){
        this.service=service;
    }

    [HttpGet]
    public async Task<ActionResult<IQueryable<User>>> getUserByEmail(String email){
       var user=await service.findByEmailAsync(email);
       if(user!=null){
        return Ok(user);
       } else {
        return BadRequest(new { message="User not found"});
       }
    }


    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> login(LoginRequest request){
        LoginResult? result=await service.LoginUserAsync(request);
        if(result!=null){
            return Ok(result);
        }
        return Unauthorized();
    }

    [HttpGet("account")]
    public async Task<ActionResult<User?>> getAccount(String userId){
        User? user=service.findByIdAsync(userId);
        if(user!=null){
            return Ok(user);
        } else {
            return BadRequest( new { message="User not found"});
        }
    }

    [HttpPost("register/admin")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> registerAdmin(RegistrationRequest request){
        var user=await service.createAdmin(request);
        if(user!=null){
            return Ok(user);
        } else {
            return BadRequest(new { message="There was an error"});
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> registerUser(RegistrationRequest request){
        var user=await service.createUser(request);
        if(user!=null){
            return Ok(user);
        } else {
            return BadRequest(new { message="There was an error"});
        }
    }



}

}