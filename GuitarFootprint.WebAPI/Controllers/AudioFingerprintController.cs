using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GuitarFootprint.Domain.Commands;
using GuitarFootprint.Domain.Queries;
using LanguageExt;
using LanguageExt.SomeHelp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SoundFingerprinting;
using SoundFingerprinting.Audio;
using SoundFingerprinting.Builder;
using SoundFingerprinting.Configuration;
using SoundFingerprinting.InMemory;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.WebAPI.Controllers
{
    [Route("audio-fingerprint")]
    [ApiController]
    [Authorize]
    public class AudioFingerprintController : ApiControllerBase
    {
        public AudioFingerprintController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public Task<IActionResult> GetAllFiles()
        {
            return TryAsync(QueryAsync(GetAllFilesQuery.CreateInstance()).Map(list => (IActionResult)Ok(list))).IfFail(exception => (IActionResult)BadRequest(exception.Message));
        }

        [HttpPost]
        public async Task<IActionResult> SaveFile(IFormFile uploadedFile)
        {
            var stream = uploadedFile.OpenReadStream();
            return await TryAsync(CommandAsync(SaveAudioCommand.CreateInstance(uploadedFile.FileName, stream)).Map(list => (IActionResult)Ok(list)))
                    .IfFail(exception => (IActionResult)BadRequest(exception.Message));

        }
    }
}