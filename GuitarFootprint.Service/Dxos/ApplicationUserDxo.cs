using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GuitarFootprint.Data.Entities;
using GuitarFootprint.Domain.Commands;
using GuitarFootprint.Service.Abstraction.Dxos;
using LanguageExt;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.Service.Dxos
{
    public class ApplicationUserDxo : IApplicationUserDxo
    {
        public TryAsync<ApplicationUser> Map(RegisterUserCommand command)
        {
            return TryAsync(() =>
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterUserCommand, ApplicationUser>(); });

                var mapper = config.CreateMapper();

                return mapper.Map<ApplicationUser>(command).AsTask();
            });
        }
    }
}
