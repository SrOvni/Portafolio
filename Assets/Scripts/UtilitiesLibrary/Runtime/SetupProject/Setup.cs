using UnityEditor;
using static UnityEditor.AssetDatabase;


namespace UtilitiesLibrary.Setup
{
    public static class Setup
    {
        [MenuItem("Tools/Setup/Create Default Projects")]
        public static void CreateDefaultFolders()
        {
            Folders.CreateDefault("", "Scripts", "Materials", "Prefabs", "ScriptableObjects");
            Refresh();
        }
    }
}
