namespace RG.Systems
{
    public interface IObjective
    {
        string Name { get; }
        bool IsCompleted { get; }
        void Initialize();
        int GetProgress();
    }
}