using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using GuitarFootprint.Data.Abstraction.Interfaces;
using GuitarFootprint.Data.Entities;
using LanguageExt;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.Data.PostgreSQL.Repositories
{
    public class AudioRepository : Repository<Guid, ApplicationContext, Audio>, IAudioRepository
    {
        private readonly ApplicationContext _context;

        public AudioRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        // change your signature to async so the thread can be released during the database update/insert act
        public async Task UploadStreamAsync(Guid id, Stream stream)
        {
            var conn = _context.Database.GetDbConnection(); // SqlConnection from your DbContext
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();
            var sqlConnection = conn as SqlConnection;
            var cmd = new SqlCommand("UPDATE dbo.Audio SET Content =@content WHERE Id=@name;", sqlConnection);
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@name", Value = id });
            // Size is set to -1 to indicate "MAX"
            cmd.Parameters.Add("@content", SqlDbType.Binary, -1).Value = stream;
            // Send the data to the server asynchronously
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
