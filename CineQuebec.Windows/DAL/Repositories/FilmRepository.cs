using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MongoDB.Driver.UpdateResult;

namespace CineQuebec.Windows.DAL.Repositories
{
	public class FilmRepository:BaseRepository, IFilmRepository
	{
		IMongoCollection<Film> _collection;

		public FilmRepository()
		{
			_collection = database.GetCollection<Film>(name: "Films");
		}

		/// <summary>
		/// Obtiens la liste de tous les Films contenuent dans la base de donnée.
		/// </summary>
		/// <returns>Une Liste de Films ou null si une Exception est survenue</returns>
		public virtual async Task<List<Film>?> GetAllFilms()
		{
			try
			{
				return await _collection.Aggregate().ToListAsync<Film>().ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Impossible d'obtenir la collection {ex.Message}", "Récupération Films");
			}
			return null;
		}

		/// <summary>
		/// Crée un Film dans la base de donnée.
		/// </summary>
		/// <param name="film">L'objet Film à créer</param>
		/// <returns>True si créé sinon false</returns>
		public virtual async Task<bool> CreateFilm(Film film)
		{
			try
			{
				await _collection.InsertOneAsync(film);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors de la création du film : {ex.Message}");
				return false;
			}
		}

		/// <summary>
		/// Supprime un Film de la base de donnée.
		/// </summary>
		/// <param name="pFilm">L'objet Film à supprimer</param>
		/// <returns>Un DeleteResult ou null si une Exception est survenue</returns>
		public virtual async Task<DeleteResult?> DeleteFilm(Film pFilm)
		{
			try
			{
				FilterDefinition<Film> filter = Builders<Film>.Filter.Eq(f => f.Id, pFilm.Id);

				return await _collection.DeleteOneAsync(filter);

				
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors de la suppression du film: {ex.Message}");
			}
			return null;
		}

		/// <summary>
		/// Obtiens la liste des Films à l'affiche grâce à une liste des Projections passé en paramètre.
		/// </summary>
		/// <param name="projections">Une Liste de Projection</param>
		/// <returns>Une Liste de Films</returns>
		public virtual async Task<List<Film>?> GetAllFilmsAffiche(List<Projection> projections)
		{
			try
			{
				List<ObjectId> filmIds = projections.Where(p => p.DateProjection > DateTime.Now).Select(p => p.IdFilm).ToList();
				var filter = Builders<Film>.Filter.And(
				Builders<Film>.Filter.In(f => f.Id, filmIds)
				);

				
				return await _collection.Find(filter).ToListAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Impossible d'obtenir la collection : {ex.Message}");
			}
			return null;
		}

		/// <summary>
		/// Modifie un Film dans la base de donnée.
		/// </summary>
		/// <param name="film">L'objet Film Modifié</param>
		/// <returns>Un UpdateResult ou null si une Exception est survenue</returns>
		public virtual async Task<UpdateResult?> UpdateFilm(Film film)
		{
			try
			{
				FilterDefinition<Film> filter = Builders<Film>.Filter.Eq(f => f.Id, film.Id);
				UpdateDefinition<Film> update = Builders<Film>.Update
					.Set(f => f.Nom, film.Nom)
					.Set(f => f.DateSortieFilm, film.DateSortieFilm)
					.Set(f => f.EstAffiche, film.EstAffiche)
					.Set(f => f.Categorie, film.Categorie)
					.Set(f => f.Realisateurs, film.Realisateurs)
					.Set(f => f.Acteurs, film.Acteurs);

				return await _collection.UpdateOneAsync(filter, update);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors de la modification du film : {ex.Message}");
			}
			return null;
		}

		public async Task<Film> GetFilmWithProjection(Projection projection)
		{
			try
			{
				FilterDefinition<Film> filter = Builders<Film>.Filter.Eq(f => f.Id, projection.IdFilm);

				return await _collection.Find(filter).FirstOrDefaultAsync();
			}catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors de la modification du film : {ex.Message}");
			}
			return null;
		}

		public async Task<UpdateResult> UpdateNoteMoyenne(Film pFilm, double pNote)
		{
			try
			{
				FilterDefinition<Film> filter = Builders<Film>.Filter.Eq(f => f.Id, pFilm.Id);

				Film film = await _collection.Find(filter).FirstOrDefaultAsync();

				if (film.NoteMoyenne == null)
					film.NoteMoyenne = pNote;
				else
					film.NoteMoyenne = (film.NoteMoyenne + pNote)/2;

				return await UpdateFilm(pFilm);
			}catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors de la modification de la note moyenne du film : {ex.Message}");
			}
			return Unacknowledged.Instance;
		}

		public async Task<List<Film>> GetFilmsWithIds(List<ObjectId> pIds)
		{
			try
			{
				FilterDefinition<Film> filter = Builders<Film>.Filter.In(f => f.Id, pIds);
				return await _collection.Find(filter).ToListAsync();
			}catch (Exception ex)
			{
				Console.WriteLine($"Impossible d'obtenir les films visionnés par l'abonné : {ex.Message}");
			}
			return null;
		}
	}
}
