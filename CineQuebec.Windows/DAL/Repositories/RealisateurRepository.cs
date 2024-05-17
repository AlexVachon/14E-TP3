using CineQuebec.Windows.DAL.Data;
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
    public class RealisateurRepository:BaseRepository, IRealisateurRepository
    {
        IMongoCollection<Realisateur> _collection;
        public RealisateurRepository(IConfiguration configuration) : base(configuration)
        { 
            _collection = database.GetCollection<Realisateur>("Realisateurs");
		}

        /// <summary>
        /// Obtiens une liste de tous les Réalisateurs dans la base de donnée.
        /// </summary>
        /// <returns>Une Liste des Réalisateurs</returns>
        public virtual List<Realisateur> ObtenirRealisateurs()
        {
            var realisateurs = new List<Realisateur>();

            try
            {
                realisateurs = _collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return realisateurs;
        }

        /// <summary>
        /// Obtiens un Rèalisateur associé à l'ID passé en paramètre.
        /// </summary>
        /// <param name="IdRealisateur">L'ID du Réalisateur</param>
        /// <returns>Une Réalisateur</returns>
        public virtual Realisateur ObtenirUnRealisateur(ObjectId IdRealisateur)
        {
            var realisateur = new Realisateur();

            try
            {
                realisateur = _collection.Aggregate().ToList().FirstOrDefault(x => x.Id == IdRealisateur);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return realisateur;
        }

        public virtual bool CreateRealisateur(Realisateur realisateur)
        {
            try
            {
                 _collection.InsertOneAsync(realisateur);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création du realisateur : {ex.Message}");
                return false;
            }
        }
    }
}
