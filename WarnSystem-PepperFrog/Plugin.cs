// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WarnSystem
{
    using System;
    using System.IO;
    using Exiled.API.Features;
    using LiteDB;
    using WarnSystem.Models;

    /// <inheritdoc />
    public class Plugin : Plugin<Config, Translation>
    {
        private LiteDatabase database;

        /// <summary>
        /// Gets a static instance of the <see cref="Plugin"/> class.
        /// </summary>
        public static Plugin Instance { get; private set; }

        /// <summary>
        /// Gets the collection of warns.
        /// </summary>
        public WarnCollection WarnCollection { get; private set; }

        /// <inheritdoc/>
        public override string Author => "Build Modified by AntonioFo";

        /// <inheritdoc/>
        public override string Name => "WarnSystem";

        /// <inheritdoc/>
        public override string Prefix => "WarnSystem";

        /// <inheritdoc/>
        public override Version Version { get; } = new(1, 2, 0);

        /// <inheritdoc/>
        public override Version RequiredExiledVersion { get; } = new(9, 0, 1);

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            if (string.IsNullOrEmpty(Config.DatabasePath) || string.IsNullOrEmpty(Config.DatabaseFile))
            {
                Log.Error($"Database path or file is not set. Please set it in the config file. {nameof(Name)} will not load.");
                return;
            }

            Instance = this;
            database = new LiteDatabase(Path.Combine(Config.DatabasePath, Config.DatabaseFile));
            WarnCollection = new WarnCollection(database);
            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            database?.Checkpoint();
            WarnCollection = null;
            database = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}