// -----------------------------------------------------------------------
// <copyright file="WarnCommandConfig.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WarnSystem.Configs
{
    using CommandSystem;
    using WarnSystem.Commands.RemoteAdmin;

    /// <summary>
    /// Handles configurable options for the <see cref="WarnCommand"/>.
    /// </summary>
    public class WarnCommandConfig
    {
        /// <summary>
        /// Gets or sets the translation for <see cref="ICommand.Command"/>.
        /// </summary>
        public string Command { get; set; } = "warn";

        /// <summary>
        /// Gets or sets the translation for <see cref="ICommand.Aliases"/>.
        /// </summary>
        /// 
        public string[] Aliases { get; set; } = { "warning" };

        /// <summary>
        /// Gets or sets the translation for <see cref="ICommand.Description"/>.
        /// </summary>
        public string Description { get; set; } = "Manages the warns of players.";
    }
}