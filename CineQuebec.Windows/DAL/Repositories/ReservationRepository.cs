using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class ReservationRepository:BaseRepository, IReservationRepository
    {
        IMongoCollection<Reservation> _collection;

        public ReservationRepository(IConfiguration configuration) : base(configuration)
        {
            _collection = database.GetCollection<Reservation>("Reservations");
		}

        /// <summary>
        /// Obtiens les Réservations associés à un Abonné grâce à son ID passé en paramètre.
        /// </summary>
        /// <param name="idAbonne">L'ID de l'Abonné</param>
        /// <returns>Une Liste des Réservation</returns>
        public virtual List<Reservation> ObtenirReservationsAbonne(ObjectId idAbonne)
        {
            var reservations = new List<Reservation>();

            try
            {
                reservations = _collection.Aggregate().ToList().Where(x => x.IdAbonne == idAbonne).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return reservations;
        }

		public async Task<Reservation?> ReserverPlaceProjection(Projection pProjection, Abonne pAbonne)
		{
            try
            {
                List<Reservation> reservations = ObtenirReservationsAbonne(pAbonne.Id);

				foreach(Reservation reserv in reservations)
                {
                    if (reserv.IdProjection.Equals(pProjection.Id))
                        throw new ReservationAlreadyExistsException();
                }

                Reservation new_reservation = new Reservation(pProjection, pAbonne);

                await _collection.InsertOneAsync(new_reservation);

                return new_reservation;

            }catch(ReservationAlreadyExistsException)
            {
				Console.WriteLine("Une réservation existe déjà avec cette projection.", "Erreur");
                throw;
			}
            catch (Exception ex)
            {
				Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
			}
            return null;
		}
	}
}
