using System.IO;
using System.Reflection;

using Obscuro.Abstract;
using Obscuro.Abstract.Unpacking.Inputs;

namespace Obscuro.Pipeline.Unpacking.Inputs
{
    class AssemblyStreamInput : IObscuroInput
    {
        private readonly Assembly _assembly;
        private readonly IObscuroContext _context;

        public AssemblyStreamInput(IObscuroContext context)
        {
            // TODO: verify and fix
            if (File.Exists(context.FileName))
                _assembly = Assembly.LoadFrom(context.FileName);
            else
                _assembly = Assembly.GetCallingAssembly();

            _context = context;
        }

        public Stream GetInputStream(string name)
            => _assembly.GetManifestResourceStream(_context.GetResourceName(name));

        public void Dispose()
        {
            // DO NOTHING HERE
        }
    }
}
