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
        public string UsageResponse { get; set; } = "Usage: warn add <player> <reason>";
        public string RequiredPermission { get; set; } = "ws.add";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(RequiredPermission))
            {
                response = Plugin.Instance.Translation.PermissionDeniedResponse;
                return false;
            }

            if (arguments.Count < 2)
            {
                response = UsageResponse;
                return false;
            }

            Id target;
            Player ply = Player.Get(arguments.At(0));
            if (ply == null)
            {
                target = new Id(arguments.At(0), "???", null);
            }
            else
            {
                target = new Id(ply);
            }

            Id issuer;
            try
            {
                issuer = new(Player.Get(sender));
            }
            catch (Exception)
            {
                Log.Warn("Adding a warn as the server console is not recommended");
                issuer = new("00000000000000000@steam", "SERVER CONSOLE", null);
            }


            string reason = string.Join(" ", arguments.Skip(1));
            Warn warn = new Warn(target, issuer, reason);
            warn.ApplyWarn();
            Plugin.Instance.Config.WarnedHint?.Display(target, warn.Reason);
            response = Plugin.Instance.Translation.SuccessResponseAdd + "\n" + warn;
            return true;
        }
    }
}