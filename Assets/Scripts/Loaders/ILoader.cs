namespace NE.Loaders
{
    public interface ILoader
    {
        string GameName { get; }
        void LoadGame(string path);
    }
}
