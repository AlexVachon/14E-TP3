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
    public class CategorieRepository: BaseRepository, ICategorieRepository
    {

        IMongoCollection<Categorie> _collection;

		public CategorieRepository(IConfiguration configuration) : base(configuration)
        {
            _collection = database.GetCollection<Categorie>(name: "Categories");
        } 

        /// <summary>
        /// Méthode qui retourne une catgorie existe dans la base de donnée avec un id
        /// </summary>
        /// <param name="idCategorie"></param>
        /// <returns></returns>
        public virtual Categorie ObtenirCategorie(ObjectId idCategorie)
        {
            var categorie = new Categorie();

            try
            {
                categorie = _collection.Aggregate().ToList().FirstOrDefault(x => x.Id == idCategorie);
            }
            catch (Exception ex)
            {
                
            }
            return categorie;
        }

        /// <summary>
        /// Méthode qui return la liste de touts les catégoreis dans la base de données.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<Categorie>> GetAllCategories()
        {
            try
            {
                return await _collection.Aggregate().ToListAsync().ConfigureAwait(false) ;
            }catch (Exception ex)
            {
				Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
			}
            return new List<Categorie>();
        }
    }
}
