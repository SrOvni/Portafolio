using System.IO;
using UnityEngine;
using static System.IO.Directory;
using static System.IO.Path;

namespace UtilitiesLibrary.Setup
{
    public class Folders
    {
        public static void CreateDefault(string root, params string[] folders)
        {
            var fullPath = Combine(Application.dataPath, root);

            foreach (var folder in folders)
            {
                var path = Path.Combine(fullPath, folder);
                if (!Exists(path))
                {
                    CreateDirectory(path);
                }
            }
        }
    }
}
