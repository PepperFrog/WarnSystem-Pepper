using System.ComponentModel;
using Exiled.API.Interfaces;

namespace WarnSystem_PepperFrog
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = true;

        [Description("The api key for the backend.")]
        public string APIKey { get; set; } = "ZHaksf9673gaoOioa7";

        public string Botip { get; set; } = "127.0.0.1";

        public uint Port { get; set; } = 8080;

        public string Uri { get; set; } = "/backend_end/requestHandler.php";

        public Models.Hint WarnedHint { get; set; } = new("Vous avez été averti\n{0}", 7, false);
    }
}