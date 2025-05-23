using ChatApp.Models.DTOs;
using ChatApp.Models.Entities;

namespace ChatApp.Interfaces
{
    

        public interface IAuthService
        {
            Task<string> RegisterAsync(RegisterDtos registerDto);
            Task<string> LoginAsync(LoginDtos loginDto);
        }



    }


