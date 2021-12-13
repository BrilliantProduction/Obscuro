using System;
using System.IO;
using System.Runtime.InteropServices;

using Obscuro.Abstract;
using Obscuro.Abstract.Reading;
using Obscuro.Abstract.Transformation;
using Obscuro.Abstract.Unpacking;
using Obscuro.Abstract.Unpacking.Inputs;
using Obscuro.Models.Data;
using Obscuro.Models.Internal;
using Obscuro.Pipeline.Unpacking.Readers;

namespace Obscuro.Pipeline.Unpacking.Unpackers
{
    class CombinedAppUnPacker : IObscuroUnpacker
    {
        private IObscuroAssemblyReader _reader;

        public CombinedAppUnPacker(IObscuroTransformation transformer)
        {
            _reader = new CombinedLibraryReader(transformer);
        }

        public ObscuroApplication Unpack(IObscuroContext context, IObscuroInput input)
        {
            //TODO: Fix!! All is broken now
            var app = new ObscuroApplication();

            int size = Marshal.SizeOf<ObscuroPackage>();
            byte[] buffer = new byte[size];

            var inputStream = input.GetInputStream(null);
            inputStream.Read(buffer, 0, size);

            int offset = 8;

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(buffer, 0, ptr, size);
            ObscuroPackage header = Marshal.PtrToStructure<ObscuroPackage>(ptr);
            Marshal.FreeHGlobal(ptr);

            header.Assemblies = new PackedLibrary[header.CountAssembly];
            inputStream.Seek(8, SeekOrigin.Begin);

            for (int i = 0; i < header.CountAssembly; i++)
            {
                app.Add(_reader.Read(inputStream));
                offset += app.Assemblies[i].RawSize;
            }

            return app;
        }
    }
}
