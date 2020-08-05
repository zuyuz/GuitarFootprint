using System;
using System.Collections.Generic;
using System.Text;
using GuitarFootprint.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuitarFootprint.Data.EntityConfigurations
{
    public class AudioConfiguration : IEntityTypeConfiguration<Audio>
    {
        public void Configure(EntityTypeBuilder<Audio> builder)
        {
            builder.HasKey(audio => audio.Id);
        }
    }
}
