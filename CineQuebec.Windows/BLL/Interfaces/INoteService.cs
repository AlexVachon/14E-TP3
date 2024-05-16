using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Interfaces
{
    public interface INoteService
    {
		Task<double> GetAVGFilm(ObjectId pFilm);
		Task<bool> HasNotedFilm(ObjectId pFilm, ObjectId pAbonne);
		Task<bool> AddNoteToFilm(ObjectId pFilm, ObjectId pAbonne, int pNote);
		Task<int> GetNoteAbonneForFilm(ObjectId pFilm, ObjectId pAbonne);
		Task UpdateNoteToFilm(ObjectId pFilm, ObjectId pAbonne, int pNote);
	}
}
