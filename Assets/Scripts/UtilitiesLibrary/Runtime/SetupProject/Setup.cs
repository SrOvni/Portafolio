using UnityEditor;
using static UnityEditor.AssetDatabase;
using static UtilitiesLibrary.Setup.Folders;


namespace UtilitiesLibrary.Setup
{
    public static class Setup
    {
        [MenuItem("Tools/Setup/Create Default Projects")]
        public static void CreateDefaultFolders()
        {
            CreateDefault("Project", "Animations", "Art", "Audio", "Editor", "Level", "Particles", "Scenes", "Scripts", "Shaders");
            Refresh();
            CreateDefault("External", "");
            Refresh();
            CreateDefault("Project/Art", "Materials", "Models", "Textures", "UI");
            Refresh();
            CreateDefault("Project/Audio", "BGM", "SFX");
            Refresh();
        }
    }
}
