using System;
using System.Threading.Tasks;
using BookStoreAPI.Models;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers{
    [ApiController]
    [Route("api/[action]")]
    public class UserController : ControllerBase{
        
        private readonly IAccount _account;
        
        public UserController(IAccount account){
            _account = account;    
        }

        [HttpPost]
        public async Task<IActionResult> signUp([FromBody]SignupModel signupModel){
            var result = await _account.SignupAsync(signupModel);
            
            if(result.Succeeded){
                return Ok();
            }
            else{
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> signIn([FromBody]SignInModel signInModel){
            
            var result = await  _account.SigninAsync(signInModel);
            if(string.IsNullOrEmpty(result)){
                return Unauthorized();
            }

            else{
                var response = new ResponseModel(){
                    msg = result
                };

                return Ok(response);
            }
        }
    }
}