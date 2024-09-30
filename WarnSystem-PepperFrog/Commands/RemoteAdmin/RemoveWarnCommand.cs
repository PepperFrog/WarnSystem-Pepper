// -----------------------------------------------------------------------
// <copyright file="RemoveWarnCommand.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WarnSystem.Commands.RemoteAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;
    using WarnSystem.Models;

    /// <inheritdoc />
    public class RemoveWarnCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "remove";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = { "r" };

        /// <inheritdoc />
        public string Description { get; set; } = "Removes a warn from a player.";

        /// <summary>
        /// Gets or sets the response to send when the user does not send enough arguments.
        /// </summary>
        [Description("The response to send when the user does not send enough arguments.")]
        public string UsageResponse { get; set; } = "Usage: warn remove <userId> <warnId>";

        /// <summary>
        /// Gets or sets the response to send when an invalid warn id is specified.
        /// </summary>
        [Description("The response to send when an invalid warn id is specified.")]
        public string InvalidIdResponse { get; set; } = "Invalid warn id.";

        /// <summary>
        /// Gets or sets the response to send when no warns are found for the user specified.
        /// </summary>
        [Description("The response to send when no warns are found for the user specified.")]
        public string NoWarnsFound { get; set; } = "No warns found for the specified user.";

        /// <summary>
        /// Gets or sets the response to send when a warn is successfully deleted.
        /// </summary>
        [Description("The response to send when a warn is successfully deleted.")]
        public string SuccessResponse { get; set; } = "Deleted Warn:\n{0}";

        /// <summary>
        /// Gets or sets the permission required to use this command.
        /// </summary>
        [Description("The permission required to use this command.")]
        public string RequiredPermission { get; set; } = "ws.remove";

        /// <summary>
        /// Gets or sets the response to send to the user when they lack the <see cref="RequiredPermission"/>.
        /// </summary>
        [Description("The response to send to the user when they lack the required permission.")]
        public string PermissionDeniedResponse { get; set; } = "You do not have permission to use this command.";

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

            if (!int.TryParse(arguments.At(1), out int id))
            {
                response = InvalidIdResponse;
                return false;
            }

            List<Warn> warns = Plugin.Instance.WarnCollection.Find(arguments.At(0)).ToList();
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
            Plugin.Instance.WarnCollection.Delete(toRemove.Id);
            response = string.Format(SuccessResponse, toRemove);
            return true;
        }
    }
}