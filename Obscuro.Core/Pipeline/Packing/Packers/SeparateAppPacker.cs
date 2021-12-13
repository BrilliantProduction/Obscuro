using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

using Obscuro.Abstract;
using Obscuro.Abstract.Packing;
using Obscuro.Abstract.Packing.Outputs;
using Obscuro.Abstract.Transformation;
using Obscuro.Abstract.Writing;
using Obscuro.Models.Data;
using Obscuro.Models.Metas;
using Obscuro.Pipeline.Packing.Writers;
using Obscuro.Utils;

namespace Obscuro.Pipeline.Packing.Packers
{
    class SeparateAppPacker : IObscuroPacker
    {
        private IObscuroAssemblyWriter _writer;

        public SeparateAppPacker(IObscuroTransformation transformer)
        {
            _writer = new SeparateLibraryWriter(transformer);
        }

        public void Pack(IObscuroContext context, IObscuroOutput output)
        {
            var libraries = context.DirectoryPath.SearchFiles(context.SearchMasks);
            WriteAppDescription(context, libraries, output);

            foreach (var library in libraries)
            {
                var assembly = new ObscuroAssembly(library);
                assembly.AssemblyData = File.ReadAllBytes(library);

                _writer.Write(assembly, output.GetOutputStream(assembly.AssemblyName));
            }

            output.Save();
        }

        private static void WriteAppDescription(IObscuroContext context, string[] libraries, IObscuroOutput output)
        {
            var appDescription = new ObscuroAppMeta
            {
                AppName = context.StartInfo.AppName,
                Assemblies = libraries.Select(Path.GetFileNameWithoutExtension).ToArray()
            };

            var outStream = output.GetOutputStream(appDescription.AppName);

            var formatter = new DataContractJsonSerializer(typeof(ObscuroAppMeta));
            formatter.WriteObject(outStream, appDescription);
        }
    }
}
