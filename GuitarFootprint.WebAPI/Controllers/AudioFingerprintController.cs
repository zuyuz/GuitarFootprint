using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GuitarFootprint.Domain.Commands;
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
            if (uploadedFile != null)
            {
                var stream = uploadedFile.OpenReadStream();

                return Ok(await CommandAsync(SaveAudioCommand.CreateInstance(uploadedFile.FileName, stream)));
            }

            return Ok();
        }

    }
}