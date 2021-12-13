using System;
using System.IO;

namespace Obscuro.Models.Data
{
    /// <summary>
    /// Represents an Obscuro app assembly
    /// </summary>
    /// <seealso cref="IEquatable{ObscuroAssembly}" />
    public class ObscuroAssembly : IEquatable<ObscuroAssembly>
    {
        public ObscuroAssembly() { }

        public ObscuroAssembly(string filePath)
        {
            AssemblyPath = filePath;
            AssemblyName = Path.GetFileNameWithoutExtension(filePath);
        }

        public string AssemblyPath { get; }

        public string AssemblyName { get; set; }

        public byte[] AssemblyData { get; set; }

        // NOTE: Should be same as in LibraryData
        public int RawSize => sizeof(int) * 2 + AssemblyData.Length;

        #region IEquatable implementation

        public bool Equals(ObscuroAssembly other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(other, this))
                return true;

            return Equals(AssemblyPath, other.AssemblyPath)
                && Equals(AssemblyName, other.AssemblyName);
        }

        public override bool Equals(object obj)
            => Equals(obj as ObscuroAssembly);

        public override int GetHashCode()
            => (AssemblyName?.GetHashCode() ?? 0)
            ^ (AssemblyPath?.GetHashCode() ?? 0);

        #endregion IEquatable implementation
    }
}
