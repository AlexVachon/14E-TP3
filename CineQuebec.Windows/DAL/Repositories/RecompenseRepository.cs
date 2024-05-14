using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class RecompenseRepository: BaseRepository,IRecompenseRepository
    {
        IMongoCollection<Recompense> _collection;

        public RecompenseRepository()
        {
            _collection = database.GetCollection<Recompense>("Recompences");
        }

        public bool AjouterRecompense(Recompense recompense)
        {
            try
            {
                 _collection.InsertOne(recompense);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création de la récompense : {ex.Message}");
                return false;
            }
        }

        public virtual List<Recompense> ObtenirRecompenses()
        {
            var recompenses = new List<Recompense>();

            try
            {
                recompenses = _collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return recompenses;
        }
        public virtual List<Recompense> ObtenirRecompensesAbonne(ObjectId idAbonne)
        {
            var recompenses = new List<Recompense>();

            try
            {
                recompenses = _collection.Aggregate().ToList().Where(x => x.IdAbonne == idAbonne).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return recompenses;
        }

    }

}
