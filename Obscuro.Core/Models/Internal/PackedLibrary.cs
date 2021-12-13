using System.Runtime.InteropServices;

namespace Obscuro.Models.Internal
{
    //[StructLayout(LayoutKind.Sequential)]
    public struct PackedLibrary
    {
        [MarshalAs(UnmanagedType.I4)]
        public int Index;

        [MarshalAs(UnmanagedType.I4)]
        public int Size;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] Data;

        internal int cb => sizeof(int) + sizeof(int) + Size;
    }
}
