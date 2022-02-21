using System.Threading.Tasks;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStoreAPI.Repository{
    public interface IAccount{
        Task<IdentityResult> SignupAsync(SignupModel signupModel);
        Task<string> SigninAsync(SignInModel signinModel);
    }
}