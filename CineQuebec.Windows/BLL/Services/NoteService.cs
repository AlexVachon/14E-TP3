using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services
{
	public class NoteService : INoteService
	{

		private readonly INoteRepository _repository;

		public NoteService(INoteRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> AddNoteToFilm(ObjectId pFilm, ObjectId pAbonne, int pNote)
		{
			try
			{
				return await _repository.AddNoteToFilm(pFilm, pAbonne, pNote);
			}catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
			}
			return false;
		}

		public async Task<double> GetAVGFilm(ObjectId pFilm)
		{
			try
			{
				return await _repository.GetAVGFilm(pFilm);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
			}
			return 0;
		}

		public async Task<int> GetNoteAbonneForFilm(ObjectId pFilm, ObjectId pAbonne)
		{
			try
			{
				return await _repository.GetNoteAbonneForFilm(pFilm, pAbonne);
			}catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
			}
			return 0;
		}

		public async Task<bool> HasNotedFilm(ObjectId pFilm, ObjectId pAbonne)
		{
			try
			{
				return await _repository.HasNotedFilm(pFilm, pAbonne);
			}catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
			}
			return false;
		}

		public async Task UpdateNoteToFilm(ObjectId pFilm, ObjectId pAbonne, int pNote)
		{
			try
			{
				await _repository.UpdateNoteToFilm(pFilm, pAbonne, pNote);
			}catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
			}
		}
	}
}
