using System;
using Exiled.API.Features;

namespace WarnSystem_PepperFrog
{
    public class Plugin : Plugin<Config, Translation>
    {
        public static Plugin Instance { get; private set; }

        public override string Author => "Build Modified by AntonioFo";

        public override string Name => "WarnSystem";

        public override string Prefix => "WarnSystem";

        public override Version Version { get; } = new(2, 0, 0);

        public override Version RequiredExiledVersion { get; } = new(9, 5, 1);

        public override void OnEnabled()
        {
            Instance = this;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            base.OnDisabled();
        }
    }
}