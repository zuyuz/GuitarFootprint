using System;
using System.Collections.Generic;
using System.Text;
using GuitarFootprint.Domain.Dtos;
using LanguageExt;

namespace GuitarFootprint.Service.Abstraction.Dxos
{
    public interface ITokenDxo
    {
        TryAsync<TokenDto> Map(string token);
    }
}
