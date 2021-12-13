using System;
using System.Collections.Generic;
using System.Linq;

using Obscuro.Utils;

namespace Obscuro.Models.Data
{
    /// <summary>
    /// Represents an application
    /// </summary>
    /// <seealso cref="System.IEquatable{ObscuroApplication}" />
    public class ObscuroApplication : IEquatable<ObscuroApplication>
    {
        private List<ObscuroAssembly> _assemblies;

        public ObscuroApplication()
        {
            _assemblies = new List<ObscuroAssembly>(0);
        }

        public string AppName { get; set; }

        public IReadOnlyList<ObscuroAssembly> Assemblies => _assemblies;

        public void Add(ObscuroAssembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            _assemblies.Add(assembly);
        }

        #region IEquatable implementation

        public bool Equals(ObscuroApplication other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Equals(AppName, other.AppName)
                && Assemblies.SequenceEqual(other.Assemblies);
        }

        public override bool Equals(object obj)
            => Equals(obj as ObscuroApplication);

        public override int GetHashCode()
            => (AppName?.GetHashCode() ?? 0) ^ _assemblies.Xor();

        #endregion IEquatable implementation
    }
}
