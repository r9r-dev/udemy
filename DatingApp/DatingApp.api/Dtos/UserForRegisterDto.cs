using System.ComponentModel.DataAnnotations;

namespace DatingApp.api.Dtos
{
    public class UserForRegisterDto
    {
        
        [Required]
        [StringLength(24, MinimumLength = 3)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(24, MinimumLength = 6)]
        public string Password { get; set; }
    }
}