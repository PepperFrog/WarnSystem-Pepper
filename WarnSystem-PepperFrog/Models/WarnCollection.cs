// -----------------------------------------------------------------------
// <copyright file="WarnCollection.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WarnSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Exiled.API.Features;
    using LiteDB;

    /// <summary>
    /// Manages an <see cref="ILiteCollection{T}"/> of <see cref="Warn"/>s.
    /// </summary>
    public class WarnCollection
    {
        private readonly ILiteCollection<Warn> warns;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarnCollection"/> class.
        /// </summary>
        /// <param name="database">The database the collection resides in.</param>
        public WarnCollection(LiteDatabase database)
        {
            warns = database.GetCollection<Warn>();
            warns.EnsureIndex(warn => warn.Id);
        }

        /// <inheritdoc cref="ILiteCollection{T}.Insert(T)"/>
        public BsonValue Insert(Warn warn) => warns.Insert(warn);

        /// <inheritdoc cref="ILiteCollection{T}.FindById(BsonValue)"/>
        public Warn Get(BsonValue id) => warns.FindById(id);

        /// <inheritdoc cref="ILiteCollection{T}.Delete(BsonValue)"/>
        public bool Delete(BsonValue id) => warns.Delete(id);

        /// <inheritdoc cref="ILiteCollection{T}.Find(Expression&lt;Func&lt;T, bool&gt;&gt;,int,int)"/>
        public IEnumerable<Warn> Find(Expression<Func<Warn, bool>> expression) => warns.Find(expression);

        /// <summary>
        /// Finds all warns that target the specified user id.
        /// </summary>
        /// <param name="targetId">The target of the warns.</param>
        /// <returns>All warns assigned to the target.</returns>
        public IEnumerable<Warn> Find(string targetId) => Find(warn => warn.TargetId == targetId);

        /// <summary>
        /// Finds all warns that target the specified user id.
        /// </summary>
        /// <param name="targetId">The target of the warns.</param>
        /// <param name="player">The player that has the specified id, or null if the player is not online.</param>
        /// <param name="isOnline">Whether the target is online.</param>
        /// <returns>All warns assigned to the target.</returns>
        public IEnumerable<Warn> Find(string targetId, out Player player, out bool isOnline)
        {
            player = Player.Get(targetId);
            if (player == null)
            {
                isOnline = false;
                return Plugin.Instance.WarnCollection.Find(warn =>
                    targetId == warn.TargetName ||
                    targetId == warn.TargetId);
            }

            isOnline = true;
            return Find(player.UserId);
        }
    }
}