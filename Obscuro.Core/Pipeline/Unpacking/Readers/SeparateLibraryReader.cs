using System.IO;

using Obscuro.Abstract.Reading;
using Obscuro.Models.Data;

namespace Obscuro.Pipeline.Unpacking.Readers
{
    class SeparateLibraryReader : IObscuroAssemblyReader
    {
        public SeparateLibraryReader()
        {
        }

        public ObscuroAssembly Read(Stream stream)
        {
            var assemblyModel = new ObscuroAssembly();

            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            assemblyModel.AssemblyData = buffer;
            return assemblyModel;
        }
    }
}
