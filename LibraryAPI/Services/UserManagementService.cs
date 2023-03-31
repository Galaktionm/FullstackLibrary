using Microsoft.AspNetCore.Identity;

using LibraryApp.API.Data.Entities;
using LibraryApp.API.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.API.Services {

public class UserManagementService {

    private readonly UserManager<User> userManager;
    private readonly JwtHandler jwtHandler;

    public UserManagementService(UserManager<User> userManager, JwtHandler jwtHandler){
        this.userManager=userManager;
        this.jwtHandler=jwtHandler;
    }

    public async Task<User?> createUser(RegistrationRequest request){
 
        if(await userManager.FindByEmailAsync(request.email)==null) {
            User user=new User(request.username, request.email);
        IdentityResult userCreationResult=await userManager.CreateAsync(user, request.password);
        if(userCreationResult.Succeeded){
            IdentityResult addRoleResult=await userManager.AddToRoleAsync(user, "User");
            if(addRoleResult.Succeeded){
                return user;
            } else {
                return null;
            }
          } else {
             return null;
          } 
        }
        return null;
    }

    public async Task<User?> createAdmin(RegistrationRequest request){
        if(await userManager.FindByEmailAsync(request.email)==null) {
            User user=new User(request.username, request.email);
        IdentityResult userCreationResult=await userManager.CreateAsync(user, request.password);
        if(userCreationResult.Succeeded){
            IdentityResult addRoleResult=await userManager.AddToRoleAsync(user, "Admin");
            if(addRoleResult.Succeeded){
                return user;
            } else {
                return null;
            }
          } else {
             return null;
          } 
        }
        return null;
    }

    public async Task<LoginResult?> LoginUserAsync(LoginRequest request) {
        User? user=await userManager.FindByEmailAsync(request.email);
        if(user!=null && await userManager.CheckPasswordAsync(user, request.password)){
            var securityToken = await jwtHandler.GetTokenAsync(user);
            var jws= new JwtSecurityTokenHandler().WriteToken(securityToken);
            var adminCheck=false;
            var role=(await userManager.GetRolesAsync(user)).First();
            if(role.Equals("Admin")){
                adminCheck=true;
            }
            return new LoginResult {
                success=true,
                token=jws,
                userId=user.Id,
                isAdmin=adminCheck
            };
        }
        return null;
    }

    public User? findByIdAsync(String id){
        return userManager.Users.Include("checkouts").Include("checkouts.books").Include("Reviews").Where(user=>user.Id.Equals(id)).First();
    }

    public async Task<IQueryable<User>> findByEmailAsync(String email){
        return userManager.Users.Where(user=>user.Email.ToLower().Contains(email.ToLower()));
    }

}


}