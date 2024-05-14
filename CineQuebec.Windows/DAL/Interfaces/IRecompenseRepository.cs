using CineQuebec.Windows.DAL.Data;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Interfaces
{
    public interface IRecompenseRepository
    {
        List<Recompense> ObtenirRecompenses();
        List<Recompense> ObtenirRecompensesAbonne(ObjectId idAbonne);
        bool AjouterRecompense(Recompense recompense);
    }
}
