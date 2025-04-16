using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
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
            Plugin.Instance.Translation.ProvideArgumentResponse ?? "Please provide a player name or steamid.";

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

            string pid;

            Player ply = Player.Get(arguments.At(0));
            if (ply is null)
            {
                if (!Regex.IsMatch(arguments.At(0), @"(?:7656119\d{10}@steam)|(?:\d{17,19}@discord)"))
                {
                    response = ProvideArgumentResponse;
                    return false;
                }

                pid = arguments.At(0);
            }
            else
            {
                pid = ply.UserId;
            }

            Warn.GetWarnsOfPlayer(pid, (warns) =>
            {
                string finalResponse;

                if (warns == null)
                {
                    finalResponse = "Something went wrong. Try again later.";
                }
                else if (warns.Count == 0)
                {
                    finalResponse = NoMatchesResponse;
                }
                else
                {
                    Player player = Player.Get(arguments.At(0));
                    finalResponse = player != null
                        ? string.Format(OnlineMatchResponse, player.Nickname, player.UserId,
                            Warn.GenerateWarnList(warns, false),
                            warns.Count)
                        : string.Format(OfflineMatchResponse, Warn.GenerateWarnList(warns, false), warns.Count);
                }

                sender.Respond(finalResponse, true);
            });


            response = "Request sent";
            return true;
        }
    }
}