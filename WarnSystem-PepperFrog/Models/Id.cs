using System;
using Exiled.API.Features;

namespace WarnSystem_PepperFrog.Models
{
    public class Id
    {
        public string UserId { get; set; }
        public string Nickname { get; set; }

        public Player Player { get; set; }

        public Id(Player player) : this(player.UserId, player.Nickname, player)
        {
        }

        public Id(string userId, string nickname, Player player)
        {
            UserId = userId;
            Nickname = nickname;
            Player = player;
        }
    }
}