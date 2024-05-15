using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services
{
    public class PreferenceService: IPreferenceService
	{
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly IActeurService _acteurService;
        private readonly IRealisateurService _realisateurService;
        private readonly ICategorieService _categorieService;

 
        public PreferenceService(IPreferenceRepository preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;        
        }
        public PreferenceService(IPreferenceRepository preferenceRepository, IActeurService acteurService, IRealisateurService realisateurService, ICategorieService categorieService)
        {
            _preferenceRepository = preferenceRepository;
            _acteurService = acteurService;
            _realisateurService = realisateurService;
            _categorieService = categorieService;
        }

        /// <summary>
        /// Obtiens la Préférence associé à l'ID passé en paramètre.
        /// </summary>
        /// <param name="idPreference">L'ID de la Préférence</param>
        /// <returns>La Préférence</returns>
        public virtual Preference ObtenirPreference(ObjectId idPreference)
        {
            var preference = new Preference();

            try
            {
                preference = _preferenceRepository.ObtenirPreference(idPreference);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return preference;
        }

        /// <summary>
        /// Obtiens une liste des Préférences associés à un Abonné grâche à son ID.
        /// </summary>
        /// <param name="idAbonne">L'ID de l'Abonné</param>
        /// <returns>Une Liste des Préférences</returns>
        public virtual List<Preference> ObtenirPreferencesAbonne(ObjectId idAbonne)
        {
            var preferences = new List<Preference>();

            try
            {
                preferences = _preferenceRepository.ObtenirPreferencesAbonne(idAbonne);
                foreach (var preference in preferences)
                {
                    preference.Realisateur = _realisateurService.ObtenirUnRealisateur(preference.IdRealisateur);
                    preference.Acteur = _acteurService.ObtenirUnActeur(preference.IdActeur);
                    preference.Categorie = _categorieService.ObtenirCategorie(preference.IdCategorie);
                                  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return preferences;
        }

        public bool AjouterPreference(Preference preference)
        {
            try
            {
                return  _preferenceRepository.AjouterPreference(preference);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return false;
        }

    }
}
