using System.Collections.Generic;

namespace BuouFramework.Commanding
{
    public static class Commander
    {
        public static void Execute(ICommand command)
        {
            command.Execute();
        }

        public static void Execute(IEnumerable<ICommand> commands)
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }
    }
}