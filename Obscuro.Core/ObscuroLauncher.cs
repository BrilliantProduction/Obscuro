using System;

using Obscuro.Abstract;
using Obscuro.Abstract.Packing;
using Obscuro.Abstract.Packing.Outputs;
using Obscuro.Abstract.Unpacking;
using Obscuro.Abstract.Unpacking.Inputs;
using Obscuro.Contexts;
using Obscuro.Models.Data;
using Obscuro.Models.Metas;
using Obscuro.Pipeline.Packing;
using Obscuro.Pipeline.Packing.Outputs;
using Obscuro.Pipeline.Running;
using Obscuro.Pipeline.Unpacking;
using Obscuro.Pipeline.Unpacking.Inputs;

namespace Obscuro
{
    class ObscuroLauncher : IAppLauncher
    {
        private IObscuroUnpackerFactory _unpackerFactory;
        private IObscuroPackerFactory _packerFactory;

        private IObscuroOutputProvider _outputProvider;
        private IObscuroInputProvider _inputProvider;

        public ObscuroLauncher()
        {
            _inputProvider = new ObscuroInputProvider();
            _outputProvider = new ObscuroOutputProvider();
            _unpackerFactory = new ObscuroUnpackerFactory();
            _packerFactory = new ObscuroPackerFactory();
        }

        public void Run(string entryAssemblyName,
                        string key = null,
                        PackagingPreferences packagingSettings = null)
        {
            packagingSettings = packagingSettings ?? new PackagingPreferences();

            var ctx = new ObscuroContext
            {
                Key = key,
                Launch = ObscuroLaunchType.Read,
                StartInfo = new AppStartupInfo
                {
                    EntryAssemblyName = entryAssemblyName
                },
                Settings = packagingSettings
            };

            // TODO: check and refactor if needed
            Launch(ctx);
        }

        public void Launch(IObscuroContext context)
        {
            if (!context.IsValid)
                throw new InvalidOperationException("Bad context passed");

            switch (context.Launch)
            {
                case ObscuroLaunchType.Write:
                    IObscuroPacker packer = _packerFactory.GetPacker(context);
                    using (var output = _outputProvider.Create(context))
                        packer.Pack(context, output);

                    break;

                case ObscuroLaunchType.Read:
                    IObscuroUnpacker unPacker = _unpackerFactory.GetUnpacker(context);

                    ObscuroApplication appModel = null;
                    using (var input = _inputProvider.Create(context))
                        appModel = unPacker.Unpack(context, input);

                    var runner = new ObscuroAppRunner(appModel, context);
                    runner.RunApp();
                    break;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
