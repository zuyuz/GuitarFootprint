using System;
using System.Collections.Generic;
using System.Text;
using GuitarFootprint.Domain.Dtos;
using MediatR;
using Newtonsoft.Json;

namespace GuitarFootprint.Domain.Commands
{
    public class SignInCommand : IRequest<TokenDto>
    {
        [JsonConstructor]
        public SignInCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
