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
        public string UsageResponse { get; set; } = "Usage: warn remove <warnId>";

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

            if (arguments.Count < 1)
            {
                response = UsageResponse;
                return false;
            }

            if (!int.TryParse(arguments.At(0), out int id))
            {
                response = InvalidIdResponse;
                return false;
            }
            
            //TODO GET THE WARN BEFORE REMOVING IT
            
            Warn.RemoveWarnOfPlayer(id);
            response = SuccessResponse;
            return true;
        }
    }
}