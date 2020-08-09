using System;
using System.Collections.Generic;
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
using static LanguageExt.Prelude;

namespace GuitarFootprint.Service.Handlers.CommandHandlers
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, TokenDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserDxo _applicationUserDxo;
        private readonly IJwtService _jwtService;
        private readonly ITokenDxo _tokenDxo;

        public SignInCommandHandler(UserManager<ApplicationUser> userManager,
            IApplicationUserDxo applicationUserDxo,
            IJwtService jwtService,
            ITokenDxo tokenDxo)
        {
            _userManager = userManager;
            _applicationUserDxo = applicationUserDxo;
            _jwtService = jwtService;
            _tokenDxo = tokenDxo;
        }

        public Task<TokenDto> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            return TryAsync(async () =>
                {
                    var user = await _userManager.FindByEmailAsync(request.Email);
                    var identityResult = await _userManager.CheckPasswordAsync(user, request.Password);
                    return (user, identityResult);
                }).Map(tuple => tuple.identityResult
                    ? EitherAsync<bool, ApplicationUser>.Right(tuple.user)
                    : EitherAsync<bool, ApplicationUser>.Left(tuple.identityResult))
                .MapAsyncT(user => _jwtService.GenerateSecurityToken(user))
                .MapT(s => _tokenDxo.Map(s))
                .Match(async => async.Match(
                        dto =>
                        {
                            return dto.Match(tokenDto => tokenDto.AsTask(),
                                exception => exception.AsFailedTask<TokenDto>());
                        },
                        result => new ArgumentException("Bad password")
                            .AsFailedTask<TokenDto>()),
                    exception => exception.AsFailedTask<TokenDto>())
                .Flatten();
        }
    }
}
