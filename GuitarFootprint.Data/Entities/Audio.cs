using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarFootprint.Data.Entities
{
    public class Audio
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public byte[] Content { get; set; }
    }
}
