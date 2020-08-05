using System;
using System.Collections.Generic;
using System.IO;
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
    public class AudioDxo : IAudioDxo
    {
        public TryAsync<Audio> Map(SaveAudioCommand audioCommand)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SaveAudioCommand, Audio>()
                    .ForMember(audio => audio.Content, expression => expression.MapFrom(command => ReadToEnd(command.Stream)))
                    .ForMember(audio => audio.CreatedOn, expression => expression.MapFrom(command => DateTimeOffset.Now));
            });

            var mapper = config.CreateMapper();

            return TryAsync(() => mapper.Map<Audio>(audioCommand).AsTask());
        }

        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}
