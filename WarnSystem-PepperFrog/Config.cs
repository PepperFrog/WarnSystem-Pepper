using System.ComponentModel;
using Exiled.API.Interfaces;

namespace WarnSystem_PepperFrog
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = true;
       
        public string APIKey { get; set; } = "ZHaksf9673gaoOioa7";

        public string Url { get; set; } = "https://gregtech.dedyn.io/backend_warn/requestHandler.php";

        public Models.Hint WarnedHint { get; set; } = new("Vous avez été averti\n{0}", 5, true);
    }
}