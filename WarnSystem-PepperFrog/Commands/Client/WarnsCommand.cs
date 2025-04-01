using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CommandSystem;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using WarnSystem_PepperFrog.Models;

namespace WarnSystem_PepperFrog.Commands.Client
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class WarnsCommand : ICommand
    {
        public string Command { get; set; } = "warns";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "Displays warns issued to you.";

        [Description(
            "The format to use when listing the warns of players. Available placeholders: {0}:Id, {1}:Date, {2}:IssuerName, {3}:IssuerId, {4}:Reason")]
        public string WarnListFormat { get; set; } = "{0}: [{1}] | {2} ({3}) > {4}";

        [Description("The date format.")] public string DateFormat { get; set; } = "dd-MM-yyyy-HH:mm";

        [Description("The response to send when no matches are found.")]
        public string NoMatchesResponse { get; set; } =
            Plugin.Instance.Translation.NoMatchesResponse ?? "No matches found.";

        [Description(
            "The response to send to the user when a match has been found. Available placeholders: {0}:WarnList, {1}:WarnCount")]
        public string MatchesResponse { get; set; } =
            Plugin.Instance.Translation.MatchesResponse ?? "\n{0}\n{1} warns on record.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player == null)
            {
                response = "You must be a player to use this command.";
                return false;
            }

            List<Warn> warns = Warn.GetWarnsOfPlayer(player.UserId);
            if (warns.Count == 0)
            {
                response = NoMatchesResponse;
                return true;
            }

            response = string.Format(MatchesResponse, GenerateWarnList(warns), warns.Count);
            return true;
        }

        private string GenerateWarnList(List<Warn> warns)
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
            for (int i = 0; i < warns.Count; i++)
            {
                Warn warn = warns[i];
                stringBuilder.AppendLine(string.Format(WarnListFormat, i + 1, warn.Date.ToString(DateFormat),
                    warn.IssuerName, warn.IssuerId, warn.Reason));
            }

            return StringBuilderPool.Shared.ToStringReturn(stringBuilder).TrimEnd('\n');
        }
    }
}