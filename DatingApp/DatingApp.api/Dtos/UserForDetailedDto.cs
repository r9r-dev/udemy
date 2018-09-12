using System;
using System.Collections.Generic;
using DatingApp.api.Models;
using Swashbuckle.AspNetCore.Filters;

namespace DatingApp.api.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotoForDetailedDto> Photos { get; set; }
    }

    public class UserForDetailedDtoExample : IExamplesProvider<UserForDetailedDto>
    {
        public UserForDetailedDto GetExamples()
        {
            return new UserForDetailedDto
            {
                Id = 1,
                Username = "vera",
                Gender = "female",
                Age = 27,
                KnownAs = "Vera Cruz",
                Created = DateTime.Now.AddMonths(-1),
                LastActive = DateTime.Now.AddDays(-1),
                Introduction = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce nibh.",
                LookingFor = "Lorem ipsum dolor sit amet.",
                Interests = "Nulla facilisi. Suspendisse lacinia fermentum.",
                City = "London",
                Country = "UK",
                PhotoUrl = "https://randomuser.me/api/portraits/women/82.jpg",
                Photos = new List<PhotoForDetailedDto>{
                    new PhotoForDetailedDto {
                        Id = 1,
                        Url = "https://randomuser.me/api/portraits/women/82.jpg",
                        Description = "",
                        DateAdded = DateTime.Now.AddMonths(-1),
                        IsMain = true
                    }
                }
            };
        }
    }
}