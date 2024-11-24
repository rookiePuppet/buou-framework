using System.Threading.Tasks;

namespace BuouFramework.Commanding
{
    public interface ICommand
    {
        Task Execute();
    }
}