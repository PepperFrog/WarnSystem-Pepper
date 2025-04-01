using System;
using Exiled.API.Features;

namespace WarnSystem_PepperFrog.Models
{
    [Serializable]
    public class Issuer(string userId, string nickname)
    {
        public string UserId { get; set; } = userId;
        public string Nickname { get; set; } = nickname;

        public Issuer(Player player) : this(player.UserId, player.Nickname)
        {
        }
    }
}