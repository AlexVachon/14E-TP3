using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Interfaces
{
	public interface IFilmService
	{
		Task<List<Film>?> GetAllFilms();
		Task<bool> CreateFilm(Film film);
		Task<DeleteResult?> DeleteFilm(Film pFilm);
		Task<List<Film>?> GetAllFilmsAffiche(List<Projection> projections);
		Task<UpdateResult?> UpdateFilm(Film film);
		Task<Film?> GetFilmWithProjection(Projection pProjection);
		//Task<UpdateResult> UpdateNoteMoyenne(Film pFilm, double pNote);
		Task<List<Film>> GetFilmsWithIds(List<ObjectId> pIds);
	}
}
