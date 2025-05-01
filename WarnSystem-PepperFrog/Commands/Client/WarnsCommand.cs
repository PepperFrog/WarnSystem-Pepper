using System;
using System.ComponentModel;
using CommandSystem;
using Exiled.API.Features;
using WarnSystem_PepperFrog.Models;

namespace WarnSystem_PepperFrog.Commands.Client
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class WarnsCommand : ICommand
    {
        public string Command { get; set; } = "warns";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Description { get; set; } = "Displays warns issued to you.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player == null)
            {
                response = "You must be a player to use this command.";
                return false;
            }

            Warn.GetWarnsOfPlayer(player.UserId, (warns) =>
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
                    finalResponse = string.Format(Plugin.Instance.Translation.MatchesResponse,
                        Warn.GenerateWarnList(warns, true), warns.Count);
                }

                player.SendConsoleMessage(finalResponse, "green");
            });


            response = "Request sent";
            return true;
        }
    }
}