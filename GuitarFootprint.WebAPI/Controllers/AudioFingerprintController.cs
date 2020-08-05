using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GuitarFootprint.Domain.Commands;
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

namespace GuitarFootprint.WebAPI.Controllers
{
    [Route("audio-fingerprint")]
    [ApiController]
    public class AudioFingerprintController : ApiControllerBase
    {
        public AudioFingerprintController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveFile(IFormFile uploadedFile)
        {
            try
            {
                var stream = uploadedFile.OpenReadStream();
                var task = CommandAsync(SaveAudioCommand.CreateInstance(uploadedFile.FileName, stream));
                var result = await task;

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}