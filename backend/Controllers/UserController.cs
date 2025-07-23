using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{


    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList()
            .Select(user => new
            {
                user.Id,
                user.Username,
                user.Email,
                user.FirstName,
                user.LastName,
                user.DateOfBirth,
                user.Role
            }).ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser([FromRoute] int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        private void CreatePass(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] Dtos.User.CreateUserDto userDto)
        {
            if (_context.Users.Any(u => u.Username == userDto.Username))
                return BadRequest("Username already exists.");

            CreatePass(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new backend.Models.User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                DateOfBirth = userDto.DateOfBirth,
                Role = "User" // or allow this to be passed if needed
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
    }

}