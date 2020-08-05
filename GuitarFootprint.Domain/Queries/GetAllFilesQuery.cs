using System;
using System.Collections.Generic;
using System.Text;
using GuitarFootprint.Data.Entities;
using MediatR;

namespace GuitarFootprint.Domain.Queries
{
    public class GetAllFilesQuery : IRequest<List<Audio>>
    {
        public static GetAllFilesQuery CreateInstance()
        {
            return new GetAllFilesQuery();
        }
    }
}
