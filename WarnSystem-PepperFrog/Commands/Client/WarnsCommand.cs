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

            Warn.GetWarnsOfPlayer(player.UserId, (warns) =>
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
                    finalResponse = string.Format(MatchesResponse, Warn.GenerateWarnList(warns, true), warns.Count);
                }

                player.SendConsoleMessage(finalResponse, "green");
            });


            response = "Request sent";
            return true;
        }
    }
}