using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GuitarFootprint.Data.Entities;
using GuitarFootprint.Domain.Commands;
using GuitarFootprint.Domain.Dtos;
using GuitarFootprint.Service.Abstraction.Dxos;
using LanguageExt;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.Service.Dxos
{
    public class TokenDxo : ITokenDxo
    {
        public TryAsync<TokenDto> Map(string token)
        {
            return TryAsync(() => new TokenDto(token).AsTask());
        }
    }
}
