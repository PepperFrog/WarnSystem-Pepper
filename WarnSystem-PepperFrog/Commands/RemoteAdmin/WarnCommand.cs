// -----------------------------------------------------------------------
// <copyright file="WarnCommand.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WarnSystem.Commands.RemoteAdmin
{
    using System;
    using System.Text;
    using CommandSystem;
    using NorthwoodLib.Pools;

    /// <inheritdoc />
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class WarnCommand : ParentCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarnCommand"/> class.
        /// </summary>
        public WarnCommand() => LoadGeneratedCommands();

        /// <inheritdoc />
        public override string Command => Plugin.Instance.Translation.Warn?.Command ?? "warn";

        /// <inheritdoc />
        public override string[] Aliases => Plugin.Instance.Translation.Warn?.Aliases ?? Array.Empty<string>();

        /// <inheritdoc />
        public override string Description => Plugin.Instance.Translation.Warn?.Description ?? "Manages the warns of players.";

        /// <inheritdoc />
        public sealed override void LoadGeneratedCommands()
        {
            RegisterCommand(Plugin.Instance.Translation.AddWarn);
            RegisterCommand(Plugin.Instance.Translation.GetWarn);
            RegisterCommand(Plugin.Instance.Translation.RemoveWarn);
        }

        /// <inheritdoc />
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
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