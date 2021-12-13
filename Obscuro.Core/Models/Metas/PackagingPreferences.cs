namespace Obscuro.Models.Metas
{
    public class PackagingPreferences
    {
        public AppPackingType Packing { get; set; } = AppPackingType.Splitted;

        public string PackedAssemblyPrefix { get; set; }

        public string PackedAssemblyExtension { get; set; } = ".enc";
    }
}
