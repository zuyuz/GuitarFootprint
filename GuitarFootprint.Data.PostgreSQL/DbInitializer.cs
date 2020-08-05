using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarFootprint.Data.PostgreSQL
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext ctx)
        {
            ctx.Database.EnsureCreated();
        }
    }
}
