using System;
using Exiled.API.Features;

namespace WarnSystem_PepperFrog.Models
{
    [Serializable]
    public class Hint
    {
        public Hint()
        {
        }

        public Hint(string message, float duration, bool show = true)
        {
            Message = message;
            Duration = duration;
            Show = show;
        }

        public string Message { get; set; }

        public float Duration { get; set; }

        public bool Show { get; set; }

        public void Display(Player player, params object[] format)
        {
            if (Show)
                player.ShowHint(format is { Length: > 0 } ? string.Format(Message, format) : Message, Duration);
        }
    }
}