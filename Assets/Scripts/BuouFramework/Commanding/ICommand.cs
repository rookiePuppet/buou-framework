namespace BuouFramework.Commanding
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}