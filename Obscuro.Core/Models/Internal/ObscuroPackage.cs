using System.Linq;
using System.Runtime.InteropServices;

namespace Obscuro.Models.Internal
{
    public struct ObscuroPackage
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Signature;

        [MarshalAs(UnmanagedType.I4)]
        public int CountAssembly;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public PackedLibrary[] Assemblies;

        internal int cb => (sizeof(byte) * 4) + sizeof(int) + (Assemblies.Sum(i => i.cb));
    }
}
