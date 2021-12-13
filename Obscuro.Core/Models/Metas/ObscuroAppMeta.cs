using System.Runtime.Serialization;

namespace Obscuro.Models.Metas
{
    [DataContract(Name = "app-metadata", Namespace = "http://schemas.datacontract.org/2004/07/Obscuro.Meta")]
    public class ObscuroAppMeta
    {
        [DataMember(Name = "app-name", EmitDefaultValue = false, IsRequired = true)]
        public string AppName { get; set; }

        [DataMember(Name = "assemblies", EmitDefaultValue = false, IsRequired = true)]
        public string[] Assemblies { get; set; }
    }
}
