using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GuitarFootprint.Data.Entities;
using GuitarFootprint.Domain.Commands;
using GuitarFootprint.Domain.Dtos;
using GuitarFootprint.Service.Abstraction.Dxos;
using GuitarFootprint.Service.Abstraction.Services;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GuitarFootprint.Service.Handlers.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserDxo _applicationUserDxo;
        private readonly IJwtService _jwtService;
        private readonly ITokenDxo _tokenDxo;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager,
            IApplicationUserDxo applicationUserDxo,
            IJwtService jwtService,
            ITokenDxo tokenDxo)
        {
            _userManager = userManager;
            _applicationUserDxo = applicationUserDxo;
            _jwtService = jwtService;
            _tokenDxo = tokenDxo;
        }

        public Task<TokenDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return _applicationUserDxo.Map(request)
                .MapAsync(async user =>
                {
                    var identityResult = await _userManager.CreateAsync(user, request.Password);
                    return (user, identityResult);
                })
                .Map(tuple => tuple.identityResult.Succeeded
                    ? EitherAsync<IdentityResult, ApplicationUser>.Right(tuple.user)
                    : EitherAsync<IdentityResult, ApplicationUser>.Left(tuple.identityResult))
                .MapAsyncT(user => _jwtService.GenerateSecurityToken(user))
                .MapT(s => _tokenDxo.Map(s))
                .Match(async => async.Match(
                    dto =>
                    {
                        return dto.Match(tokenDto => tokenDto.AsTask(),
                            exception => exception.AsFailedTask<TokenDto>());
                    }, 
                    result =>
                    {
                        return new ArgumentException(string.Join(",", result.Errors.Select(error => error.Description)))
                            .AsFailedTask<TokenDto>();
                    }), 
                    exception => exception.AsFailedTask<TokenDto>())
                .Flatten();
        }
    }
}
