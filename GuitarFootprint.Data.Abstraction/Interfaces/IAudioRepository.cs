using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using GuitarFootprint.Data.Entities;
using LanguageExt;

namespace GuitarFootprint.Data.Abstraction.Interfaces
{
    public interface IAudioRepository : IRepository<Guid, Audio>
    {
        Task UploadStreamAsync(Guid id, Stream stream);
    }
}
