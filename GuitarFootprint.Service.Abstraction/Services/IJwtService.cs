using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GuitarFootprint.Data.Entities;

namespace GuitarFootprint.Service.Abstraction.Services
{
    public interface IJwtService
    {
        Task<string> GenerateSecurityToken(ApplicationUser user);
    }
}
