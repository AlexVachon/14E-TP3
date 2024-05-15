using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services
{
    /// <summary>
    /// Servive pour abonné
    /// </summary>
    public class AbonneService: IAbonneService
    {
        private readonly IAbonneRepository _abonneRepository;
        private readonly IReservationService _reservationService;
        private readonly IPreferenceService _preferenceService;
        private readonly IRecompenseService _recompenseService;
         

		public AbonneService (IAbonneRepository pAbonneRepo, IReservationService pReservationService, IPreferenceService pPrefService, IRecompenseService pRecompenseService )
        {
            _abonneRepository = pAbonneRepo;
            _reservationService = pReservationService;
            _preferenceService = pPrefService;
            _recompenseService = pRecompenseService;
        }

		/// <summary>
		/// Méthode qui obtient une liste de tous les sbonnés, elle fais appel a la méthode ObtenirReservation pour obtenir toutes les 
		/// réservation qu'un abonné fait, et auusi elle appelle la méthode obtenir préférence pour obtenir toutes les préférence de 
		/// cette abonné selon la catégorie du film, les acteurs et les réalistaeurs.
		/// </summary>
		/// <returns></returns>
		public List<Abonne> ObtenirAbonnes()
        {
            var abonnes = new List<Abonne>();

            try
            {
                abonnes = _abonneRepository.ObtenirAbonnes();
                foreach (var abonne in abonnes)
                {
                    abonne.Reservations = _reservationService.ObtenirReservationsAbonne(abonne.Id);
                    abonne.Preferences = _preferenceService.ObtenirPreferencesAbonne(abonne.Id);
                    abonne.Recompenses = _recompenseService.ObtenirRecompensesAbonne(abonne.Id);
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return abonnes;
        }

		public async Task<UpdateResult> AddReservation(Abonne pAbonne, Reservation pReservation)
		{
            try
            {
                return await _abonneRepository.AddReservation(pAbonne, pReservation);

            }catch(Exception  ex)
            {
                Console.WriteLine("Impossible de créer la réservation" + ex.Message, "Erreur");
            }
			return UpdateResult.Unacknowledged.Instance;
		}
	}
}
