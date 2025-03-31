using Exiled.API.Features;
using System;

namespace WarnSystem.Models
{
    [Serializable]
    public class Issuer
    {
        public string UserId { get; set; }
        public string Nickname { get; set; }
        public Issuer(string UserId, string Nickname)
        {
            this.UserId = UserId;
            this.Nickname = Nickname;
        }

        public Issuer(Player player)
        {
            this.UserId = player.UserId;
            this.Nickname = player.Nickname;
        }
    }
}
