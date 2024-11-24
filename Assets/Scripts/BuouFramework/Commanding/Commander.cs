using System.Collections.Generic;

namespace BuouFramework.Commanding
{
    public static class Commander
    {
        public static async void Execute(ICommand command)
        {
            await command.Execute();
        }

        public static async void Execute(IEnumerable<ICommand> commands)
        {
            foreach (var command in commands)
            {
                await command.Execute();
            }
        }
    }
}