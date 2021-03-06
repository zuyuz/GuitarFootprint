﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GuitarFootprint.Data.Abstraction.Interfaces;
using GuitarFootprint.Data.Entities;
using GuitarFootprint.Domain.Commands;
using GuitarFootprint.Domain.Queries;
using GuitarFootprint.Service.Abstraction.Dxos;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.Service.Services
{
    public class GetAllFilesQueryHandler : IRequestHandler<GetAllFilesQuery, List<Audio>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAudioDxo _audioDxo;

        public GetAllFilesQueryHandler(IUnitOfWork unitOfWork,
            IAudioDxo audioDxo)
        {
            _unitOfWork = unitOfWork;
            _audioDxo = audioDxo;
        }

        public Task<Unit> Handle(SaveAudioCommand request, CancellationToken cancellationToken)
        {
            return _audioDxo.Map(request)
                .Bind(audio => _unitOfWork.AudioRepository.CreateAsync(audio))
                .Match(audio => audio.AsTask(), exception => exception.AsFailedTask<Audio>())
                .Bind(audio => _unitOfWork.Commit().ToUnit());
            //.Bind(audio => _unitOfWork.AudioRepository.UploadStreamAsync(audio.Id, request.Stream).ToUnit())
            //.Bind(audio => _unitOfWork.Commit().ToUnit());
        }

        public Task<List<Audio>> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.AudioRepository.GetAll().ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
