namespace RG.Systems
{

    public interface IAction<in T>
    {
        void Apply(T effect);
    }

}