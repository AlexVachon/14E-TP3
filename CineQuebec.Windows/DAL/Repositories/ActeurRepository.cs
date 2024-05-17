using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class ActeurRepository: BaseRepository,IActeurRepository
    {
        IMongoCollection<Acteur> _collection;


		public ActeurRepository(IConfiguration configuration):base(configuration)
        {
            _collection = database.GetCollection<Acteur>("Acteurs");
		}

        /// <summary>
        /// Méthode qui retourn la liste de tous les acteurs qui existent dans la base de donné
        /// </summary>
        /// <returns></returns>
        public virtual List<Acteur> ObtenirActeurs()
        {
            var acteurs = new List<Acteur>();

            try
            {
                acteurs = _collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return acteurs;
        }

        /// <summary>
        /// Méthode qui retourne un acteurs selon un id spécifique dans la base de donnée
        /// </summary>
        /// <param name="idActeur"></param>
        /// <returns></returns>
        public virtual Acteur ObtenirUnActeur(ObjectId idActeur)
        {
            var acteur = new Acteur();

            try
            {
                acteur = _collection.Aggregate().ToList().FirstOrDefault(x => x.Id == idActeur);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return acteur;
        }

    }
}
