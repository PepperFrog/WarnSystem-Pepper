// -----------------------------------------------------------------------
// <copyright file="Translation.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WarnSystem
{
    using Exiled.API.Interfaces;
    using WarnSystem.Commands.Client;
    using WarnSystem.Commands.RemoteAdmin;
    using WarnSystem.Configs;

    /// <inheritdoc />
    public class Translation : ITranslation
    {
        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="WarnsCommand"/> command.
        /// </summary>
        public WarnsCommand Warns { get; set; } = new();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="AddWarnCommand"/> command.
        /// </summary>
        public AddWarnCommand AddWarn { get; set; } = new();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="GetWarnCommand"/> command.
        /// </summary>
        public GetWarnCommand GetWarn { get; set; } = new();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="RemoveWarnCommand"/> command.
        /// </summary>
        public RemoveWarnCommand RemoveWarn { get; set; } = new();

        /// <summary>
        /// Gets or sets an instance of the <see cref="WarnCommandConfig"/> class.
        /// </summary>
        public WarnCommandConfig Warn { get; set; } = new();
    }
}