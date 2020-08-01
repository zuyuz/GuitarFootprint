using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GuitarFootprint.Data.Abstraction.Interfaces;
using GuitarFootprint.Domain.Commands;
using LanguageExt;
using MediatR;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.Service.Services
{
    public class SaveAudioCommandHandler : IRequestHandler<SaveAudioCommand, TryAsync<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaveAudioCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<TryAsync<Unit>> Handle(SaveAudioCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(TryAsync(unit));
        }
    }
}
