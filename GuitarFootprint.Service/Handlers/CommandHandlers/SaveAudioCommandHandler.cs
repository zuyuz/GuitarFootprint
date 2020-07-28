using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using GuitarFootprint.Domain.Commands;
using MediatR;

namespace GuitarFootprint.Service.Services
{
    public class SaveAudioCommandHandler : IRequestHandler<SaveAudioCommand, Result>
    {
        public async Task<Result> Handle(SaveAudioCommand request, CancellationToken cancellationToken)
        {

            return Result.Success();
        }
    }
}
