using System;
using System.Collections.Generic;
using System.Text;
using GuitarFootprint.Data.Entities;

namespace GuitarFootprint.Data.Abstraction.Interfaces
{
    public interface IAudioRepository : IRepository<Guid, Audio>
    {
    }
}
