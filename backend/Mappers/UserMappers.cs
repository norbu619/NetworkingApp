using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.User;

namespace backend.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this Models.User user)
        {
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Role = user.Role
            };
        }
        public static Models.User ToUser(this CreateUserDto createUserDto)
        {
            if (createUserDto == null) return null;

            return new Models.User
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                DateOfBirth = createUserDto.DateOfBirth,
                Role = createUserDto.Role
            };
        }
    }
}