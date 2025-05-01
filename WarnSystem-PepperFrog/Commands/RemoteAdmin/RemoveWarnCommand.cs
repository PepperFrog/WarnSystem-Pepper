using System;
using System.Collections.Generic;
using System.ComponentModel;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using WarnSystem_PepperFrog.Models;

namespace WarnSystem_PepperFrog.Commands.RemoteAdmin
{
    public class RemoveWarnCommand : ICommand
    {
        public string Command { get; set; } = "remove";

        public string[] Aliases { get; set; } = { "r" };

        public string Description { get; set; } = "Removes a warn from a player.";
        public string UsageResponse { get; set; } = "Usage: warn remove <warnId>";
        public string RequiredPermission { get; set; } = "ws.remove";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(RequiredPermission))
            {
                response = Plugin.Instance.Translation.PermissionDeniedResponse;
                return false;
            }

            if (arguments.Count < 1)
            {
                response = UsageResponse;
                return false;
            }

            if (!int.TryParse(arguments.At(0), out int id))
            {
                response = Plugin.Instance.Translation.InvalidPlayerResponse;
                return false;
            }

            Warn.GetWarnsById(id, (warns) =>
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
                    finalResponse = Warn.GenerateWarnList(warns, false);
                    Warn.RemoveWarnOfPlayer(id);
                }

                sender.Respond(finalResponse, true);
            });

            response = Plugin.Instance.Translation.SuccessResponseRemove;
            return true;
        }
    }
}