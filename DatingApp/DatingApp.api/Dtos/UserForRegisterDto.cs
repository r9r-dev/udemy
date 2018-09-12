using System;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Filters;

namespace DatingApp.api.Dtos
{
    public class UserForRegisterDto
    {
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        [StringLength(24, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
        
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }

    public class UserForRegisterDtoExample : IExamplesProvider<UserForRegisterDto>
    {
        public UserForRegisterDto GetExamples()
        {
            return new UserForRegisterDto
            {
                Username = "bob",
                Password = "password",
                Gender = "male",
                KnownAs = "Bob Lennon",
                DateOfBirth = new DateTime(1982, 3, 18),
                City = "London",
                Country = "UK",
                Created = DateTime.Now,
                LastActive = DateTime.Now
            };
        }
    }
}