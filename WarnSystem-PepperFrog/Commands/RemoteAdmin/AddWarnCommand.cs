// -----------------------------------------------------------------------
// <copyright file="AddWarnCommand.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WarnSystem.Commands.RemoteAdmin
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using WarnSystem.Models;

    /// <inheritdoc />
    public class AddWarnCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "add";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = { "a" };

        /// <inheritdoc />
        public string Description { get; set; } = "Adds a warn to a player.";

        /// <summary>
        /// Gets or sets the response to send when the user does not send enough arguments.
        /// </summary>
        [Description("The response to send when the user does not send enough arguments.")]
        public string UsageResponse { get; set; } = "Usage: warn add <player> <reason>";

        /// <summary>
        /// Gets or sets the response to send when an invalid player is specified.
        /// </summary>
        [Description("The response to send when an invalid player is specified.")]
        public string InvalidPlayerResponse { get; set; } = Plugin.Instance.Translation.InvalidPlayerResponse ?? "Player not found.";

        /// <summary>
        /// Gets or sets the response to send when a warn is successfully added.
        /// </summary>
        [Description("The response to send when a warn is successfully added.")]
        public string SuccessResponse { get; set; } = Plugin.Instance.Translation.SuccessResponseAdd?? "Warn added:\n{0}";

        /// <summary>
        /// Gets or sets the permission required to use this command.
        /// </summary>
        [Description("The permission required to use this command.")]
        public string RequiredPermission { get; set; } = "ws.add";

        /// <summary>
        /// Gets or sets the response to send to the user when they lack the <see cref="RequiredPermission"/>.
        /// </summary>
        [Description("The response to send to the user when they lack the required permission.")]
        public string PermissionDeniedResponse { get; set; } = Plugin.Instance.Translation.PermissionDeniedResponse ?? "You do not have permission to use this command.";

        /// <inheritdoc />
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

            string reason = string.Join(" ", arguments.Skip(1));
            Warn warn = new Warn(target, Player.Get(sender), reason);
            Plugin.Instance.WarnCollection.Insert(warn);
            Plugin.Instance.Config.WarnedHint?.Display(target, warn.Reason);
            response = string.Format(SuccessResponse, warn);
            return true;
        }
    }
}