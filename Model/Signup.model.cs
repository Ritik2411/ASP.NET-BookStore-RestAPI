using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models{
    public class SignupModel{
        [Required]
        public string userName { get;set; }

        [Required]
        public string firstName { get;set; }
        
        [Required]
        public string lastName { get;set; }
        
        [Required]
        public string Email { get;set; }
        
        [Required]
        [Compare("confirmPassword")]
        public string Password { get;set; }
        
        [Required]
        public string confirmPassword { get;set; }
    }
}