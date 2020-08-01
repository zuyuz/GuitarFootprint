using System;
using System.Collections.Generic;
using System.Text;
using GuitarFootprint.Data.Abstraction.Interfaces;
using GuitarFootprint.Data.Entities;
using LanguageExt;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.Data.PostgreSQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;
        public IAudioRepository AudioRepository { get; }

        public UnitOfWork(ApplicationContext applicationContext,
            IAudioRepository audioRepository)
        {
            _applicationContext = applicationContext;
            AudioRepository = audioRepository;
        }

        public TryAsync<Unit> Commit()
        {
            return TryAsync(async () =>
            {
                await _applicationContext.SaveChangesAsync();
                return unit;
            });
        }

        public Unit Rollback()
        {
            _applicationContext.Dispose();
            return unit;
        }
    }
}
