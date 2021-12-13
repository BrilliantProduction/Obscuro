
using System;
using System.IO;

using Obscuro.Abstract;
using Obscuro.Models.Metas;
using Obscuro.Utils;

namespace Obscuro.Contexts
{
    public class ObscuroContext : IObscuroContext
    {
        public ObscuroLaunchType Launch { get; set; }

        public string Key { get; set; }

        public string FileName { get; set; }

        public string DirectoryPath { get; set; }

        public string[] SearchMasks { get; set; }

        public bool IsValid => Validate(this);

        public PackagingPreferences Settings { get; set; } = new PackagingPreferences();

        public AppStartupInfo StartInfo { get; set; } = new AppStartupInfo();

        public static IObscuroContext FromArgs(string[] args)
        {
            var path = args[0];
            var context = new ObscuroContext();

            context.DirectoryPath = !string.IsNullOrEmpty(Path.GetExtension(path)) ? Path.GetDirectoryName(path) : path;
            context.Launch = (ObscuroLaunchType)Enum.Parse(typeof(ObscuroLaunchType), args[1], true);

            // TODO: Extract from params or similar
            context.SearchMasks = new[] { "*.dll" };

            if (context.Launch == ObscuroLaunchType.Read)
            {
                context.FileName = path;
                context.StartInfo.EntryAssemblyName = args.GetItemAtOrDefault(2, Path.GetFileNameWithoutExtension(context.FileName));
            }
            else
            {
                context.FileName = args.GetItemAtOrDefault(2, "PackedLib.dll");
                context.StartInfo.EntryAssemblyName = args.GetItemAtOrDefault(3, Path.GetFileNameWithoutExtension(context.FileName));
            }

            return context;
        }

        public static bool Validate(IObscuroContext context)
        {
            return !string.IsNullOrEmpty(context.FileName);
        }

        public string GetResourceName(string resourceName)
        {
            var prefix = Settings.PackedAssemblyPrefix ?? string.Empty;
            return prefix + resourceName + Settings.PackedAssemblyExtension;
        }
    }
}
