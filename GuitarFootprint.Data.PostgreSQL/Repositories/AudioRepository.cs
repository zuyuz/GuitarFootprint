using System;
using System.Collections.Generic;
using System.Text;
using GuitarFootprint.Data.Abstraction.Interfaces;
using GuitarFootprint.Data.Entities;

namespace GuitarFootprint.Data.PostgreSQL.Repositories
{
    public class AudioRepository : Repository<Guid, ApplicationContext, Audio>, IAudioRepository
    {
        public AudioRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
