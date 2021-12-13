using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

using Obscuro.Abstract.Transformation;
using Obscuro.Abstract.Writing;
using Obscuro.Models.Data;
using Obscuro.Models.Internal;
using Obscuro.Pipeline.Transformation;

namespace Obscuro.Pipeline.Packing.Writers
{
    public class CombinedLibraryWriter : IObscuroAssemblyWriter
    {
        private readonly IObscuroTransformation _transformer;

        public CombinedLibraryWriter(IObscuroTransformation transformer)
        {
            _transformer = transformer ?? new NoopLibraryTransformation();
        }

        public void Write(ObscuroAssembly assembly, Stream outputStream)
        {
            var bytes = assembly.AssemblyData.ToArray();

            if (_transformer != null)
                bytes = _transformer.GetTransfromedBytes(bytes).ToArray();

            PackedLibrary lib = new PackedLibrary();
            lib.Index = 0;
            lib.Size = bytes.Length;
            lib.Data = new byte[bytes.Length];
            bytes.CopyTo(lib.Data, 0);

            IntPtr ptr = Marshal.AllocHGlobal(lib.cb);
            Marshal.StructureToPtr(lib, ptr, false);

            var buffer = new byte[lib.cb];
            Marshal.Copy(ptr, buffer, 0, lib.cb);
            Marshal.FreeHGlobal(ptr);
            outputStream.Write(buffer, 0, lib.cb);
        }
    }
}