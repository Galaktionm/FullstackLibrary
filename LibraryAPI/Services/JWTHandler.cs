using LibraryApp.API.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryApp.API.Services{


    public class JwtHandler
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;

        public JwtHandler(IConfiguration configuration, UserManager<User> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<JwtSecurityToken> GetTokenAsync(User user)
        {
            var token = new JwtSecurityToken(
                issuer: configuration["JwtParameters:Issuer"],
                audience: configuration["JwtParameters:Audience"],
                claims: await GetClaimsAsync(user),
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(
                    configuration["JwtParameters:ExpirationTimeInMinutes"])),
                signingCredentials: GetSigningCredentials());
            return token;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var privateKey = Encoding.UTF8.GetBytes(configuration["JwtParameters:SecurityKey"]);
            var secret = new SymmetricSecurityKey(privateKey);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }



        private async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var x=await userManager.GetRolesAsync(user);


            foreach (var role in await userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private async Task<String> getUserRole(String tokenString){
            var token=new JwtSecurityToken(tokenString);
            foreach(var claim in token.Claims){
                if(claim.Type.Equals(ClaimTypes.Role)){
                    return claim.Value;
                }
            }
            return null;
        }
    }

}