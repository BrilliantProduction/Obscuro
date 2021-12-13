using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Obscuro.Abstract;
using Obscuro.Abstract.Running;
using Obscuro.Models.Data;

namespace Obscuro.Pipeline.Running
{
    public class ObscuroAppRunner : IObscuroAppRunner
    {
        private readonly object _syncRoot = new object();

        private bool _disposing;

        private IObscuroContext _context;
        private ObscuroApplication _appModel;

        private Dictionary<string, Assembly> _loadedAssemblies;

        public ObscuroAppRunner(ObscuroApplication appModel, IObscuroContext context)
        {
            _context = context;
            _appModel = appModel;
            _loadedAssemblies = new Dictionary<string, Assembly>(0);

            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += OnResolveAssembly;
        }

        protected bool IsDisposed { get; private set; }

        public int RunApp()
        {
            int res = 0;
            try
            {
                var assembly = LoadAndCache(_context.StartInfo.EntryAssemblyName);
                ExecuteAssembly(assembly);
            }
            catch
            {
                res = 1;
            }

            Dispose();

            return res;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ObscuroAppRunner()
        {
            Dispose(false);
        }

        #region Private members

        private Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            // Retrieve assembly name
            var name = args.Name.Split(',')[0].Trim();

            if (_loadedAssemblies.TryGetValue(name, out var assembly))
                return assembly;

            if (!CanLoadAssembly(name))
                return null;

            return LoadAndCache(name);
        }

        private Assembly LoadAndCache(string assemblyName)
        {
            if (_loadedAssemblies.TryGetValue(assemblyName, out var cachedAssembly))
                return cachedAssembly;

            //TODO: make this more complex
            var assemblyModel = _appModel.Assemblies.FirstOrDefault(x => string.Equals(x.AssemblyName,
                                                                                       assemblyName,
                                                                                       StringComparison.Ordinal));
            if (Equals(_context.StartInfo.EntryAssemblyName, assemblyName) && assemblyModel == null)
            {
                var entryAssembly = Assembly.LoadFrom(_context.FileName);
                _loadedAssemblies[assemblyName] = entryAssembly;
                return entryAssembly;
            }

            var assembly = Assembly.Load(assemblyModel.AssemblyData);
            _loadedAssemblies[assemblyName] = assembly;
            return assembly;
        }

        private bool CanLoadAssembly(string assemblyName)
            => _appModel.Assemblies.Any(x => string.Equals(x.AssemblyName, assemblyName, StringComparison.Ordinal));

        private void ExecuteAssembly(Assembly assembly)
        {
            MethodInfo entryPoint = assembly.EntryPoint;

            var methodParams = entryPoint.GetParameters();

            if (methodParams.Length > 0)
            {
                var count = _context.StartInfo.HasParameters ? _context.StartInfo.Parameters.Length : 0;
                var parameters = new string[count];

                if (_context.StartInfo.HasParameters)
                {
                    int i = 0;
                    foreach (var param in _context.StartInfo.Parameters)
                    {
                        parameters[i] = _context.StartInfo.Parameters[i]?.ToString();
                        i++;
                    }
                }

                entryPoint.Invoke(null, new object[] { parameters });
            }
            else
                entryPoint.Invoke(null, null);
        }

        private void Dispose(bool disposing)
        {
            // NAIL: object was disposed (_syncRoot <= null) but finalizer calls Dispose(false)
            if (_syncRoot == null)
                return;

            lock (_syncRoot)
            {
                if (!IsDisposed && !_disposing)
                {
                    _disposing = true;

                    try
                    {
                        if (disposing)
                        {
                            AppDomain.CurrentDomain.AssemblyResolve -= OnResolveAssembly;
                            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= OnResolveAssembly;
                            _loadedAssemblies.Clear();
                        }
                    }
                    finally
                    {
                        //NOTE: this flags should be reset to avoid double-disposing
                        IsDisposed = true;
                        _disposing = false;
                    }
                }
            }
        }

        #endregion Private members
    }
}
