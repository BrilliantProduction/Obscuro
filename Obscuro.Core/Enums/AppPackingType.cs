namespace Obscuro
{
    /// <summary>
    /// Represents an application packing type
    /// </summary>
    public enum AppPackingType
    {
        /// <summary>
        /// The combined storage (all assemblies stored in one place)
        /// </summary>
        Combined,

        /// <summary>
        /// The splitted storage (all assemblies stored separately)
        /// </summary>
        Splitted
    }
}
