using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using NorthwoodLib.Pools;
using WarnSystem_PepperFrog.Models;

namespace WarnSystem_PepperFrog.Commands.RemoteAdmin
{
    public class GetWarnCommand : ICommand
    {
        public string Command { get; set; } = "get";

        public string[] Aliases { get; set; } = { "g" };

        public string Description { get; set; } = "Gets the warns of a player.";

        [Description("The response to send when no player is specified.")]
        public string ProvideArgumentResponse { get; set; } =
            Plugin.Instance.Translation.ProvideArgumentResponse ?? "Please provide a player name.";

        [Description("The response to send when no matches are found.")]
        public string NoMatchesResponse { get; set; } =
            Plugin.Instance.Translation.NoMatchesResponse ?? "No matches found.";

        [Description("The response to send to the user when an offline match has been found.")]
        public string OfflineMatchResponse { get; set; } = Plugin.Instance.Translation.OfflineMatchResponse ??
                                                           "Offline matches found.\n{0}\n{1} matches found.";

        [Description(
            "The response to send to the user when an online match has been found. Available placeholders: {0}:TargetName, {1}:TargetId, {2}:WarnList, {3}:WarnCount")]
        public string OnlineMatchResponse { get; set; } = Plugin.Instance.Translation.OnlineMatchResponse ??
                                                          "Matches found for {0} ({1})\n{2}\n{3} matches found.";

        [Description("The permission required to use this command.")]
        public string RequiredPermission { get; set; } = "ws.warns";

        [Description("The response to send to the user when they lack the required permission.")]
        public string PermissionDeniedResponse { get; set; } = Plugin.Instance.Translation.PermissionDeniedResponse ??
                                                               "You do not have permission to use this command.";

        [Description(
            "The format to use when listing the warns of players. Available placeholders: {0}:Id, {1}:Date, {2}:IssuerName, {3}:IssuerId, {4}:Reason")]
        public string WarnListFormat { get; set; } = "{0}: [{1}] {2} ({3}) > {4}";

        [Description("The date format.")] public string DateFormat { get; set; } = "dd-MM-yyyy-HH:mm";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(RequiredPermission))
            {
                response = PermissionDeniedResponse;
                return false;
            }

            if (arguments.Count == 0)
            {
                response = ProvideArgumentResponse;
                return false;
            }

            List<Warn> warns = Warn.GetWarnsOfPlayer(arguments.At(0));
            if (warns.Count == 0)
            {
                response = NoMatchesResponse;
                return false;
            }

            Player player = Player.Get(arguments.At(0));

            response = Player.Get(arguments.At(0)) != null
                ? string.Format(OnlineMatchResponse, player.Nickname, player.UserId, GenerateWarnList(warns),
                    warns.Count)
                : string.Format(OfflineMatchResponse, GenerateWarnList(warns), warns.Count);

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