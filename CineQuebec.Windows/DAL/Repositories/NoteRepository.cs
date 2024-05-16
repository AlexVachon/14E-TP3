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
	class NoteRepository : BaseRepository, INoteRepository
	{

		IMongoCollection<Note> _collection;

		public NoteRepository()
		{
			_collection = database.GetCollection<Note>(name: "Notes");
		}

		public async Task<bool> AddNoteToFilm(ObjectId pFilm, ObjectId pAbonne, int pNote)
		{
			try
			{
				Note note = new Note(pFilm, pAbonne, pNote);
				await _collection.InsertOneAsync(note);
				return true;

			}catch (Exception ex)
			{
				Console.WriteLine($"Impossible d'obtenir la collection {ex.Message}", "Récupération Notes");
			}
			return false;
		}

		public async Task<double> GetAVGFilm(ObjectId pFilm)
		{
			try
			{
				double avg = 0;
				int total = 0;

				FilterDefinition<Note> filter = Builders<Note>.Filter.Eq(n => n.FilmID, pFilm);

				List<Note> notes = await _collection.Find(filter).ToListAsync();

				foreach(Note note in notes)
				{
					total += note.NoteFilm;
				}

				avg = total / notes.Count;

				return avg;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Impossible d'obtenir la collection {ex.Message}", "Récupération Moyenne Film");
			}
			return 0;
		}

		public async Task<int> GetNoteAbonneForFilm(ObjectId pFilm, ObjectId pAbonne)
		{
			try
			{
				FilterDefinition<Note> filter = Builders<Note>.Filter.And(
					Builders<Note>.Filter.Eq(n => n.FilmID, pFilm),
					Builders<Note>.Filter.Eq(n => n.AbonneID, pAbonne)
				);

				Note note =  await _collection.Find(filter).FirstOrDefaultAsync();

				return note.NoteFilm;
			}catch (Exception ex)
			{
				Console.WriteLine($"Impossible d'obtenir la collection {ex.Message}", "Récupération Note Film");
			}

			return 0;
		}

		public async Task<bool> HasNotedFilm(ObjectId pFilm, ObjectId pAbonne)
		{
			try
			{
				FilterDefinition<Note> filter = Builders<Note>.Filter.And(
					Builders<Note>.Filter.Eq(n => n.FilmID, pFilm),
					Builders<Note>.Filter.Eq(n => n.AbonneID, pAbonne)
				);

				Note note = await _collection.Find(filter).FirstOrDefaultAsync();

				if (note != null)
					return true;

			}catch (Exception ex)
			{
				Console.WriteLine("Erreur lors de la vérification des Notes: " + ex.Message, "Récupération Note");
			}

			return false;
		}

		public async Task UpdateNoteToFilm(ObjectId pFilm, ObjectId pAbonne, int pNote)
		{
			try
			{
				FilterDefinition<Note> filter = Builders<Note>.Filter.And(
					Builders<Note>.Filter.Eq(n => n.FilmID, pFilm),
					Builders<Note>.Filter.Eq(n => n.AbonneID, pAbonne)
				);

				UpdateDefinition<Note> update = Builders<Note>.Update.Set(n => n.NoteFilm, pNote);

				await _collection.FindOneAndUpdateAsync(filter, update);

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Impossible d'obtenir la collection: {ex.Message}", "Récupération Notes");
			}
		}

	}
}
