using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Interfaces
{
    public interface IProjectionRepository
    {
        Projection ObtenirProjection(ObjectId idProjection);
        Task<List<Projection>?> GetAllProjections();

        Task<Projection> GetProjectionWithID(ObjectId pProjectionID);

        Task<List<Projection>> GetProjectionsWithIds(List<ObjectId> pIds);

	}
}
