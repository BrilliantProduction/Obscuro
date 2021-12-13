using System;
using System.IO;
using System.Runtime.InteropServices;

using Obscuro.Abstract.Reading;
using Obscuro.Abstract.Transformation;
using Obscuro.Models.Data;
using Obscuro.Models.Internal;
using Obscuro.Pipeline.Transformation;

namespace Obscuro.Pipeline.Unpacking.Readers
{
    public class CombinedLibraryReader : IObscuroAssemblyReader
    {
        private IObscuroTransformation _transformer;

        public CombinedLibraryReader(IObscuroTransformation transformer)
        {
            _transformer = transformer ?? new NoopLibraryTransformation();
        }

        public ObscuroAssembly Read(Stream stream)
        {
            int dataSize = Marshal.SizeOf<PackedLibrary>();
            byte[] libDataBuffer = new byte[dataSize];

            stream.Read(libDataBuffer, 0, dataSize);
            IntPtr libDataPtr = Marshal.AllocHGlobal(dataSize);
            Marshal.Copy(libDataBuffer, 0, libDataPtr, dataSize);
            PackedLibrary data = Marshal.PtrToStructure<PackedLibrary>(libDataPtr);
            Marshal.FreeHGlobal(libDataPtr);

            stream.Seek(dataSize, SeekOrigin.Current);

            Array.Resize(ref libDataBuffer, data.cb);
            stream.Read(libDataBuffer, 0, data.cb);
            libDataPtr = Marshal.AllocHGlobal(dataSize);
            Marshal.Copy(libDataBuffer, 0, libDataPtr, dataSize);
            data = Marshal.PtrToStructure<PackedLibrary>(libDataPtr);
            Marshal.FreeHGlobal(libDataPtr);

            // TODO: Transform here

            return new ObscuroAssembly
            {
                AssemblyData = data.Data
            };
        }
    }
}
