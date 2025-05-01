using System.ComponentModel;
using Exiled.API.Interfaces;

namespace WarnSystem_PepperFrog
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = true;
        public string APIKey { get; set; } = "ZHaksf9673gaoOioa7";

        [Description("URL of the backend must be in https://mybackend.tlp/something/handler.php")]
        public string Url { get; set; } = "https://gregtech.dedyn.io/backend_warn/requestHandler.php";
#if FRENCH
        public Models.Hint WarnedHint { get; set; } = new("Vous avez été averti\n{0}", 5, true);
#else
        public Models.Hint WarnedHint { get; set; } = new("You have been warned\n{0}", 5, true);
#endif
    }
}