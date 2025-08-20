using UnityEditor;
using static UtilitiesLibrary.Setup.Folders;


namespace UtilitiesLibrary.Setup
{
    public static class Setup
    {
        [MenuItem("Tools/Setup/Folder preset")]
        public static void CreateDefaultFolders()
        {
            CreateDefault("Project", "Animations", "Art", "Audio", "Editor", "Input Action Assets", "Level", "Particles", "Prefabs", "Scenes", "Scripts", "Shaders");
            CreateDefault("External", "");            
            CreateDefault("Project/Art", "Materials", "Models", "Textures", "UI");            
            CreateDefault("Project/Audio", "BGM", "SFX");
            AssetDatabase.Refresh();
        }
    }
}
