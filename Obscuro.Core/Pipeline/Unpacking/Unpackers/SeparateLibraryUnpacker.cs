using System.Runtime.Serialization.Json;

using Obscuro.Abstract;
using Obscuro.Abstract.Reading;
using Obscuro.Abstract.Unpacking;
using Obscuro.Abstract.Unpacking.Inputs;
using Obscuro.Models.Data;
using Obscuro.Models.Metas;
using Obscuro.Pipeline.Unpacking.Readers;

namespace Obscuro.Pipeline.Unpacking.Unpackers
{
    class SeparateLibraryUnpacker : IObscuroUnpacker
    {
        private IObscuroAssemblyReader _reader;

        public SeparateLibraryUnpacker()
        {
            _reader = new SeparateLibraryReader();
        }

        public ObscuroApplication Unpack(IObscuroContext context, IObscuroInput input)
        {
            var appModelDesc = ReadAppDescription(context, input);
            var appModel = new ObscuroApplication { AppName = appModelDesc.AppName };

            foreach (var libraryName in appModelDesc.Assemblies)
            {
                var assemblyModel = _reader.Read(input.GetInputStream(libraryName));
                assemblyModel.AssemblyName = libraryName;
                appModel.Add(assemblyModel);
            }

            return appModel;
        }

        private static ObscuroAppMeta ReadAppDescription(IObscuroContext context, IObscuroInput input)
        {
            var inputStream = input.GetInputStream(context.StartInfo.EntryAssemblyName);

            var formatter = new DataContractJsonSerializer(typeof(ObscuroAppMeta));
            return (ObscuroAppMeta)formatter.ReadObject(inputStream);
        }
    }
}
