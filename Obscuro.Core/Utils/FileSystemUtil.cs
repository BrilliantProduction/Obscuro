using System.IO;
using System.Linq;

namespace Obscuro.Utils
{

    public static class FileSystemUtil
    {
        public static string[] SearchFiles(this string folderPath, params string[] searchMasks)
        {
            return searchMasks.SelectMany(x => Directory.GetFiles(folderPath, x)).Distinct().ToArray();
        }
    }
}
