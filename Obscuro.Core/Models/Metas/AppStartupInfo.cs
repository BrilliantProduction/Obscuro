namespace Obscuro.Models.Metas
{
    public class AppStartupInfo
    {
        private string _entryAssemblyName;

        public string AppName { get; set; } = "DefaultApp";

        public string EntryAssemblyName
        {
            get => _entryAssemblyName;
            set
            {
                _entryAssemblyName = value;
                Adjust();
            }
        }

        public object[] Parameters { get; set; }

        public bool HasParameters => Parameters != null && Parameters.Length > 0;

        public void Adjust()
        {
            if (!Equals(AppName, EntryAssemblyName))
                AppName = !string.IsNullOrEmpty(EntryAssemblyName) ? EntryAssemblyName : "DefaultApp";
        }
    }
}
