using System;
using System.Collections.Generic;
using System.Text;
using GuitarFootprint.Domain.Dtos;
using MediatR;
using Newtonsoft.Json;

namespace GuitarFootprint.Domain.Commands
{
    public class RegisterUserCommand : IRequest<TokenDto>
    {
        [JsonConstructor]
        public RegisterUserCommand(string email, string password, string username)
        {
            Email = email;
            Password = password;
            Username = username;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
