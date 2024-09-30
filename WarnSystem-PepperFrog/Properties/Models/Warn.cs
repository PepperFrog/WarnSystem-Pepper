// -----------------------------------------------------------------------
// <copyright file="Warn.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WarnSystem.Models
{
    using System;
    using Exiled.API.Features;

    /// <summary>
    /// Represents a warn issued to a player by a staff member.
    /// </summary>
    [Serializable]
    public class Warn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Warn"/> class.
        /// </summary>
        public Warn()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Warn"/> class.
        /// </summary>
        /// <param name="target">The player to be warned.</param>
        /// <param name="issuer">The player issuing the warn.</param>
        /// <param name="reason">The reason for the warn.</param>
        public Warn(Player target, Player issuer, string reason)
        {
            Date = DateTime.UtcNow;
            TargetId = target.UserId;
            TargetName = target.Nickname;
            IssuerId = issuer.UserId;
            IssuerName = issuer.Nickname;
            Reason = reason;
        }

        /// <summary>
        /// Gets or sets the id of the warn.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date that the warn was issued.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the id of the user that this warn belongs to.
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user that this warn belongs to.
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// Gets or sets the id of the user that issued this warn.
        /// </summary>
        public string IssuerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user that issued this warn.
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// Gets or sets the reason for this warn.
        /// </summary>
        public string Reason { get; set; }

        /// <inheritdoc />
        public override string ToString() => $"[{Date:MM/dd/yyyy}] {TargetName} ({TargetId}) | {IssuerName} ({IssuerId}) > {Reason}";
    }
}