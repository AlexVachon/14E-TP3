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
    public class PreferenceRepository : BaseRepository, IPreferenceRepository
    {
        IMongoCollection<Preference> _collection;

        public PreferenceRepository(IConfiguration configuration) : base(configuration)
        {
            _collection = database.GetCollection<Preference>("Preferences");
        }

        /// <summary>
        /// Obtiens la Préférence associé à l'ID passé en paramètre.
        /// </summary>
        /// <param name="idPreference">L'ID de la Préférence</param>
        /// <returns>Une Préférence</returns>
        public virtual Preference ObtenirPreference(ObjectId idPreference)
        {
            var preference = new Preference();

            try
            {
                preference = _collection.Aggregate().ToList().FirstOrDefault(x => x.Id == idPreference);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return preference;
        }
        
        /// <summary>
        /// Obtiens la liste de Préférence associé à un Abonné grâce à l'ID passé en paramètre.
        /// </summary>
        /// <param name="idAbonne">L'ID de l'Abonné</param>
        /// <returns>Une Liste des Préférences</returns>
        public virtual List<Preference> ObtenirPreferencesAbonne(ObjectId idAbonne)
        {
            var preferences = new List<Preference>();

            try
            {
                preferences = _collection.Aggregate().ToList().Where(x => x.IdAbonne == idAbonne).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return preferences;
        }

        public virtual bool AjouterPreference(Preference preference)
        {
            try
            {
                _collection.InsertOne(preference);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création du preference : {ex.Message}");
                return false;
            }
        }
    }
}
