using Obscuro.Models.Metas;

namespace Obscuro.Abstract
{
    public interface IObscuroContext : IResourceProvider
    {
        ObscuroLaunchType Launch { get; set; }

        string Key { get; set; }

        string FileName { get; set; }

        string DirectoryPath { get; set; }

        string[] SearchMasks { get; set; }

        bool IsValid { get; }

        PackagingPreferences Settings { get; set; }

        AppStartupInfo StartInfo { get; set; }
    }
}
