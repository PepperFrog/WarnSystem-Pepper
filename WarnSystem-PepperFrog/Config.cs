// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WarnSystem
{
    using System.ComponentModel;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;
    using WarnSystem.Models;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;
        
        public bool Debug { get; set; }

        /// <summary>
        /// Gets or sets the path where the database file should be stored.
        /// </summary>
        [Description("The path where the database file should be stored.")]
        public string DatabasePath { get; set; } = Paths.Configs;

        /// <summary>
        /// Gets or sets the name of the database file.
        /// </summary>
        [Description("The name of the database file.")]
        public string DatabaseFile { get; set; } = "Warn.db";

        /// <summary>
        /// Gets or sets the warn to display to a player when they are warned.
        /// </summary>
        public Models.Hint WarnedHint { get; set; } = new("You have been warned\n{0}", 7, false);
        
    }
}