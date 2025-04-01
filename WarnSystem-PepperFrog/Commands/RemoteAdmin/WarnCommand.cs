using System;
using System.Text;
using CommandSystem;
using NorthwoodLib.Pools;

namespace WarnSystem_PepperFrog.Commands.RemoteAdmin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class WarnCommand : ParentCommand, IUsageProvider
    {
        public WarnCommand() => LoadGeneratedCommands();

        public override string Command => "warn";

        public override string[] Aliases => Array.Empty<string>();

        public override string Description => "Manages the warns of players.";

        public string[] Usage => new string[] { "a/add g/get", "playername", "Reason And More" };

        public sealed override void LoadGeneratedCommands()
        {
            RegisterCommand(new AddWarnCommand());
            RegisterCommand(new GetWarnCommand());
            RegisterCommand(new RemoveWarnCommand());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender,
            out string response)
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine("Please specify a valid subcommand! Available:");
            foreach (ICommand command in AllCommands)
            {
                stringBuilder.AppendLine(command.Aliases is { Length: > 0 }
                    ? $"{command.Command} | Aliases: {string.Join(", ", command.Aliases)}"
                    : command.Command);
            }

            response = StringBuilderPool.Shared.ToStringReturn(stringBuilder).TrimEnd();
            return false;
        }
    }
}