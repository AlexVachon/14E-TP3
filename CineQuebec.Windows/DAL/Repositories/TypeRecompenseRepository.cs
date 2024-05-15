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
    public class TypeRecompenseRepository: BaseRepository, ITypeRecompenseRepository
    {
        IMongoCollection<TypeRecompense> _collection;

        public TypeRecompenseRepository()
        {
            _collection = database.GetCollection<TypeRecompense>("TypeRecompences");
        }

        public virtual TypeRecompense ObtenirTypeRecompenses(ObjectId idTypeRecompense)
        {
            var typeRecompense = new TypeRecompense();

            try
            {
                typeRecompense = _collection.Aggregate().ToList().First(x => x.Id == idTypeRecompense);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return typeRecompense;
        }

        public virtual List<TypeRecompense> ObtenirToutTypesRecompenses()
        {
            var typeRecompenses = new List<TypeRecompense>();

            try
            {
                typeRecompenses = _collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return typeRecompenses;
        }
    }
}
