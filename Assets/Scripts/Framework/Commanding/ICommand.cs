using System.Threading.Tasks;

namespace Game.Framework.Commanding
{
    public interface ICommand
    {
        Task Execute();
    }
}