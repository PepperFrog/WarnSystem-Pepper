using Exiled.API.Interfaces;

namespace WarnSystem_PepperFrog
{
    public class Translation : ITranslation
    {
#if FRENCH
        public string MatchesResponse { get; set; } = "\n{0}\n{1} warns dans le casier.";

        public string NoMatchesResponse { get; set; } = "Aucune réponse trouvé.";

        public string PermissionDeniedResponse { get; set; } =
            "Vous n'avez pas la permission d'utiliser cette commande.";

        public string InvalidPlayerResponse { get; set; } = "Joueur introuvable.";

        public string ProvideArgumentResponse { get; set; } = "Veuillez donner un nom de joueur ou steamid.";

        public string SuccessResponseRemove { get; set; } = "Warn suprimé:";

        public string SuccessResponseAdd { get; set; } = "Warn ajouté:";

        public string OfflineMatchResponse { get; set; } = "Occurence hors-ligne trouvé.\n{0}\n{1} occurences trouvé.";

        public string OnlineMatchResponse { get; set; } = "Occurence trouvé {0} ({1})\n{2}\n{3} occurences trouvé.";
#else
        public string MatchesResponse { get; set; } = "\n{0}\n{1} warns found.";

        public string NoMatchesResponse { get; set; } = "No matches found.";

        public string PermissionDeniedResponse { get; set; } = "You don't have the permission to use this command.";

        public string InvalidPlayerResponse { get; set; } = "Invalid player.";

        public string ProvideArgumentResponse { get; set; } = "Provide a valid player name or steamid/discordid.";

        public string SuccessResponseRemove { get; set; } = "Warn removed:";

        public string SuccessResponseAdd { get; set; } = "Warn added:";

        public string OfflineMatchResponse { get; set; } = "Offline match found.\n{0}\n{1} match found.";

        public string OnlineMatchResponse { get; set; } = "Match found {0} ({1})\n{2}\n{3} match found.";
#endif
    }
}