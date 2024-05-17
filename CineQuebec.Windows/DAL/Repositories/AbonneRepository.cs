
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class AbonneRepository:BaseRepository, IAbonneRepository
    {

        IMongoCollection<Abonne> _collection;
       

        public AbonneRepository(IConfiguration configuration):base(configuration)
        {
            _collection = database.GetCollection<Abonne>("Abonnes");

        }

      
        /// <summary>
        /// Méthode qui retoune la liste des abonnés
        /// </summary>
        /// <returns></returns>
        public virtual List<Abonne> ObtenirAbonnes()
        {
            var abonnes = new List<Abonne>();

            try
            {
                abonnes = _collection.Aggregate().ToList();
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
                FilterDefinition<Abonne> filter = Builders<Abonne>.Filter.Eq(a => a.Id, pAbonne.Id);
                UpdateDefinition<Abonne> update = Builders<Abonne>.Update
                    .AddToSet(a => a.Reservations, pReservation);

                return await _collection.UpdateOneAsync(filter, update);

            }catch (Exception ex)
            {
				Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
			}

            return UpdateResult.Unacknowledged.Instance;
		}
    }
}
