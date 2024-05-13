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
    public class ProjectionRepository:BaseRepository, IProjectionRepository
    {
        IMongoCollection<Projection> _collection;

		public ProjectionRepository()
        {
            _collection = database.GetCollection<Projection>("Projections");
		}

        /// <summary>
        /// Obtiens une Projection associé à l'ID passé en paramètre.
        /// </summary>
        /// <param name="idProjection">L'ID de la Projection</param>
        /// <returns>Une Projection</returns>
        public virtual Projection ObtenirProjection(ObjectId idProjection)
        {
            var projection = new Projection();

            try
            {
                projection = _collection.Aggregate().ToList().FirstOrDefault(x => x.Id == idProjection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return projection;
        }

        /// <summary>
        /// Obtiens une liste de tous les Projections contenuent dans la base de donnée.
        /// </summary>
        /// <returns>Une Listes des Projections</returns>
        public virtual async Task<List<Projection>?> GetAllProjections()
        {
            try
            {
                return await _collection.Aggregate().ToListAsync();
            }catch (Exception ex)
            {
				Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
			}
            return null;
        }

		public async Task<Projection> GetProjectionWithID(ObjectId pProjectionID)
		{
            try
            {
                FilterDefinition<Projection> filter = Builders<Projection>.Filter.Eq(f => f.Id, pProjectionID);
				return await _collection.Find(filter).FirstOrDefaultAsync();


			}
			catch (Exception ex)
            {
				Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
			}
            return null;
		}

        public async Task<List<Projection>> GetProjectionsWithIds(List<ObjectId> pIds)
        {
            try
            {
                FilterDefinition<Projection> filter = Builders<Projection>.Filter.In(p => p.Id, pIds);

                return await _collection.Find(filter).ToListAsync();
            }catch(Exception ex)
            {
				Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
			}
            return null;
        }
	}
}
