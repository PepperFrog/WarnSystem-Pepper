using System;
using System.Collections.Generic;
using System.ComponentModel;
using CommandSystem;
using Exiled.Permissions.Extensions;
using WarnSystem_PepperFrog.Models;

namespace WarnSystem_PepperFrog.Commands.RemoteAdmin
{
    public class RemoveWarnCommand : ICommand
    {
        public string Command { get; set; } = "remove";

        public string[] Aliases { get; set; } = { "r" };

        public string Description { get; set; } = "Removes a warn from a player.";

        [Description("The response to send when the user does not send enough arguments.")]
        public string UsageResponse { get; set; } = "Usage: warn remove <userId> <warnId>";

        [Description("The response to send when an invalid warn id is specified.")]
        public string InvalidIdResponse { get; set; } = "Invalid warn id.";

        [Description("The response to send when no warns are found for the user specified.")]
        public string NoWarnsFound { get; set; } =
            Plugin.Instance.Translation.NoWarnsFound ?? "No warns found for the specified user.";

        [Description("The response to send when a warn is successfully deleted.")]
        public string SuccessResponse { get; set; } =
            Plugin.Instance.Translation.SuccessResponseRemove ?? "Deleted Warn:\n{0}";

        [Description("The permission required to use this command.")]
        public string RequiredPermission { get; set; } = "ws.remove";

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

            if (arguments.Count < 2)
            {
                response = UsageResponse;
                return false;
            }

            if (!int.TryParse(arguments.At(1), out int id))
            {
                response = InvalidIdResponse;
                return false;
            }

            List<Warn> warns = Warn.GetWarnsOfPlayer(arguments.At(0));
            if (warns.Count == 0)
            {
                response = NoWarnsFound;
                return false;
            }

            if (id <= 0 || id > warns.Count)
            {
                response = InvalidIdResponse;
                return false;
            }

            Warn toRemove = warns[id - 1];
            Warn.RemoveWarnOfPlayer(arguments.At(0), id);
            response = string.Format(SuccessResponse, toRemove);
            return true;
        }
    }
}