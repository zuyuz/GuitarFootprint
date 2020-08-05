using System;
using System.Collections.Generic;
using System.Text;
using GuitarFootprint.Data.Entities;
using GuitarFootprint.Domain.Commands;
using LanguageExt;

namespace GuitarFootprint.Service.Abstraction.Dxos
{
    public interface IAudioDxo
    {
        TryAsync<Audio> Map(SaveAudioCommand audioCommand);
    }
}
