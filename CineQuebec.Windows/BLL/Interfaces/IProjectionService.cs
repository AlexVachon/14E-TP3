using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Interfaces
{
	public interface IProjectionService
	{
		Projection ObtenirProjection(ObjectId pProjectionID);
		Task<List<Projection>?> GetAllProjections();
		Task<Projection> GetProjectionWithID(ObjectId pProjectionID);
		Task<List<Projection>> GetProjectionsWithIds(List<ObjectId> pIds);
	}
}
