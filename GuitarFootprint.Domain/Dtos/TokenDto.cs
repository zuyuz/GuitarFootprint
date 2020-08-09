using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarFootprint.Domain.Dtos
{
    public class TokenDto
    {
        public TokenDto(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
