using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreAPI.Repository{
    public class AccountRepository : IAccount{

        private readonly UserManager<UserModel> _usermanager;
        private readonly SignInManager<UserModel> _signmanger;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IConfiguration configuration){
            _usermanager = userManager;
            _signmanger = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> SignupAsync(SignupModel signupModel){
            var user = new UserModel(){
                firstName = signupModel.firstName,
                lastName = signupModel.lastName,
                Email = signupModel.Email,
                UserName = signupModel.userName
            };

            return await _usermanager.CreateAsync(user, signupModel.Password);
        }

        public async Task<string> SigninAsync(SignInModel siginModel){
            var result = await _signmanger.PasswordSignInAsync(siginModel.Email, siginModel.Password, false, false);
    
            if(!result.Succeeded){
                return null;
            }
            
            else{
                var authClaims = new List<Claim>{
                    new Claim(ClaimTypes.Name, ClaimTypes.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var authSignKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],  
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256Signature)
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
}