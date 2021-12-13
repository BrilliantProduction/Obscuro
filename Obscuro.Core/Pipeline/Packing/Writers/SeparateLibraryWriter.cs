using System.IO;
using System.Linq;

using Obscuro.Abstract.Transformation;
using Obscuro.Abstract.Writing;
using Obscuro.Models.Data;
using Obscuro.Pipeline.Transformation;

namespace Obscuro.Pipeline.Packing.Writers
{
    class SeparateLibraryWriter : IObscuroAssemblyWriter
    {
        private readonly IObscuroTransformation _transformer;

        public SeparateLibraryWriter(IObscuroTransformation transformer)
        {
            _transformer = transformer ?? new NoopLibraryTransformation();
        }

        public void Write(ObscuroAssembly assembly, Stream outputStream)
        {
            var bytes = assembly.AssemblyData.ToArray();

            if (_transformer != null)
                bytes = _transformer.GetTransfromedBytes(bytes).ToArray();

            outputStream.Write(bytes, 0, bytes.Length);
        }
    }
}
