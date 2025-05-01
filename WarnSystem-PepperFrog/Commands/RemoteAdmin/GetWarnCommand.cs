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
        public string RequiredPermission { get; set; } = "ws.warns";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(RequiredPermission))
            {
                response = Plugin.Instance.Translation.PermissionDeniedResponse;
                return false;
            }

            if (arguments.Count == 0)
            {
                response = Plugin.Instance.Translation.ProvideArgumentResponse;
                return false;
            }

            string pid;

            Player ply = Player.Get(arguments.At(0));
            if (ply is null)
            {
                if (!Regex.IsMatch(arguments.At(0), @"(?:7656119\d{10}@steam)|(?:\d{17,19}@discord)"))
                {
                    response = Plugin.Instance.Translation.ProvideArgumentResponse;
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
                    finalResponse = Plugin.Instance.Translation.NoMatchesResponse;
                }
                else
                {
                    Player player = Player.Get(arguments.At(0));
                    finalResponse = player != null
                        ? string.Format(Plugin.Instance.Translation.OnlineMatchResponse, player.Nickname, player.UserId,
                            Warn.GenerateWarnList(warns, false),
                            warns.Count)
                        : string.Format(Plugin.Instance.Translation.OfflineMatchResponse,
                            Warn.GenerateWarnList(warns, false), warns.Count);
                }

                sender.Respond(finalResponse, true);
            });


            response = "Request sent";
            return true;
        }
    }
}