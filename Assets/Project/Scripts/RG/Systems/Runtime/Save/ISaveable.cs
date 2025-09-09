using LitJson;

namespace RG.Systems
{
    public interface ISaveable
    {
        string SaveID { get; }
        JsonData SavedData { get; }
        void LoadFromData(JsonData data);
    }
}
