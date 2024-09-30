// -----------------------------------------------------------------------
// <copyright file="Translation.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace WarnSystem
{
    using Exiled.API.Interfaces;
    using WarnSystem.Commands.Client;
    using WarnSystem.Commands.RemoteAdmin;    

    /// <inheritdoc />
    public class Translation : ITranslation
    {
        public string MatchesResponse { get; set; } = "\n{0}\n{1} warns dans le casier.";

        public string NoMatchesResponse { get; set; } = "Aucune réponse trouvé.";

        public string PermissionDeniedResponse { get; set; } = "Vous n'avez pas la permission d'utiliser cette commande.";

        public string InvalidPlayerResponse { get; set; } = "Joueur introuvable.";        

        public string NoWarnsFound { get; set; } = "Aucun warn trouvé pour ce joueur.";

        public string ProvideArgumentResponse { get; set; } = "Veuillez donner un nom de joueur.";

        public string SuccessResponseRemove { get; set; } = "Warn suprimé:\n{0}";

        public string SuccessResponseAdd { get; set; } = "Warn ajouté:\n{0}";

        public string OfflineMatchResponse { get; set; } = "Occurence hors-ligne trouvé.\n{0}\n{1} occurences trouvé.";

        public string OnlineMatchResponse { get; set; } = "Occurence trouvé {0} ({1})\n{2}\n{3} occurences trouvé.";
    }
}