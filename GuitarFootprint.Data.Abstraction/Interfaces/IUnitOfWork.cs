using System;
using System.Collections.Generic;
using System.Text;
using GuitarFootprint.Data.Entities;
using LanguageExt;

namespace GuitarFootprint.Data.Abstraction.Interfaces
{
    public interface IUnitOfWork
    {
        IAudioRepository AudioRepository { get; }
        TryAsync<Unit> Commit();
        Unit Rollback();
    }
}
