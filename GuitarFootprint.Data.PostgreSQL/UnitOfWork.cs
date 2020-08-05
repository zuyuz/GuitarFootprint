using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GuitarFootprint.Data.Abstraction.Interfaces;
using GuitarFootprint.Data.Entities;
using LanguageExt;
using Microsoft.EntityFrameworkCore.Storage;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.Data.PostgreSQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;
        public IAudioRepository AudioRepository { get; }

        public IDbContextTransaction Transaction { get; }

        public UnitOfWork(ApplicationContext applicationContext,
            IAudioRepository audioRepository)
        {
            _applicationContext = applicationContext;
            AudioRepository = audioRepository;

            Transaction = applicationContext.Database.BeginTransaction();
        }

        public Task Commit()
        {
            return TryAsync(async () =>
            {
                var count = await _applicationContext.SaveChangesAsync();
                await Transaction.CommitAsync();
                return unit;
            }).Match(unit1 => unit1.AsTask(), exception => exception.AsFailedTask<Unit>());
        }

        public Task Rollback()
        {
            return Try(() =>
            {
                _applicationContext.Dispose();
                return unit;
            }).Match(unit1 => unit1.AsTask(), exception => exception.AsFailedTask<Unit>());
        }
    }
}
