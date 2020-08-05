using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GuitarFootprint.Data.Entities;
using LanguageExt;

namespace GuitarFootprint.Data.Abstraction.Interfaces
{
    public interface IUnitOfWork
    {
        IAudioRepository AudioRepository { get; }
        Task Commit();
        Task Rollback();
    }
}
