using System;
using System.ComponentModel;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using WarnSystem_PepperFrog.Models;

namespace WarnSystem_PepperFrog.Commands.RemoteAdmin
{
    public class AddWarnCommand : ICommand
    {
        public string Command { get; set; } = "add";

        public string[] Aliases { get; set; } = { "a" };

        public string Description { get; set; } = "Adds a warn to a player.";

        [Description("The response to send when the user does not send enough arguments.")]
        public string UsageResponse { get; set; } = "Usage: warn add <player> <reason>";

        [Description("The response to send when an invalid player is specified.")]
        public string InvalidPlayerResponse { get; set; } =
            Plugin.Instance.Translation.InvalidPlayerResponse ?? "Player not found.";

        [Description("The response to send when a warn is successfully added.")]
        public string SuccessResponse { get; set; } =
            Plugin.Instance.Translation.SuccessResponseAdd ?? "Warn added:\n{0}";

        [Description("The permission required to use this command.")]
        public string RequiredPermission { get; set; } = "ws.add";

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

            Player target = Player.Get(arguments.At(0));
            if (target == null)
            {
                response = InvalidPlayerResponse;
                return false;
            }

            Issuer issuer;
            try
            {
                issuer = new(Player.Get(sender));
            }
            catch (Exception)
            {
                Log.Warn("Adding a warn as the server console is not recommended");
                issuer = new("00000000000000000@steam", "SERVER CONSOLE");
            }


            string reason = string.Join(" ", arguments.Skip(1));
            Warn warn = new Warn(target, issuer, reason);
            string succ = warn.ApplyWarn();
            //todo fix
            Plugin.Instance.Config.WarnedHint?.Display(target, warn.Reason);
            response = string.Format(SuccessResponse, warn);
            return true;
        }
    }
}