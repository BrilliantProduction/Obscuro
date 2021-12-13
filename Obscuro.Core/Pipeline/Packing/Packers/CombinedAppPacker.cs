using System.IO;

using Obscuro.Abstract;
using Obscuro.Abstract.Packing;
using Obscuro.Abstract.Packing.Outputs;
using Obscuro.Abstract.Transformation;
using Obscuro.Abstract.Writing;
using Obscuro.Models.Data;
using Obscuro.Pipeline.Packing.Writers;
using Obscuro.Utils;

namespace Obscuro.Pipeline.Packing.Packers
{
    class CombinedAppPacker : IObscuroPacker
    {
        private IObscuroAssemblyWriter _writer;

        public CombinedAppPacker(IObscuroTransformation transformer)
        {
            _writer = new CombinedLibraryWriter(transformer);
        }

        public void Pack(IObscuroContext context, IObscuroOutput output)
        {
            //TODO: Fix!! All is broken now
            var libraries = context.DirectoryPath.SearchFiles(context.SearchMasks);

            var buffer = new byte[8];
            buffer[7] = (byte)libraries.Length;
            output.GetOutputStream(null).Write(buffer, 0, 8);

            foreach (var library in libraries)
            {
                var assembly = new ObscuroAssembly(library);
                assembly.AssemblyData = File.ReadAllBytes(library);

                _writer.Write(assembly, output.GetOutputStream(assembly.AssemblyName));
            }

            output.Save();
        }
    }
}
