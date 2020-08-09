using System.IO;
using System.Windows.Input;
using LanguageExt;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;
using MediatR;
using Newtonsoft.Json;

namespace GuitarFootprint.Domain.Commands
{
    public class SaveAudioCommand : IRequest<Unit>
    {
        [JsonConstructor]
        public SaveAudioCommand(string name, Stream stream)
        {
            Name = name;
            Stream = stream;
        }

        public static SaveAudioCommand CreateInstance(string name, Stream stream)
        {
            return new SaveAudioCommand(name, stream);
        }

        public string Name { get; set; }
        public Stream Stream { get; set; }
    }
}
