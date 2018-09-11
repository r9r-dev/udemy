using DatingApp.api.Dtos;

namespace DatingApp.api.Data
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public UserForListDto User { get; set; }
    }
}