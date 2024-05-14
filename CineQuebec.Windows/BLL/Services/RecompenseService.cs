using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services
{
    public class RecompenseService :IRecompenseService
    {
        private readonly IRecompenseRepository _recompenseRepository;
        private readonly ITypeRecompenseService _typeRecompenseService;
        private IRecompenseRepository @object;

        public RecompenseService(IRecompenseRepository pRecompenseRepository, ITypeRecompenseService pTypeRecompenseService)
        {
            _recompenseRepository = pRecompenseRepository;
            _typeRecompenseService = pTypeRecompenseService;
        }

        public RecompenseService(IRecompenseRepository recompenseRepository)
        {
            _recompenseRepository = recompenseRepository;
        }

        public virtual List<Recompense> ObtenirRecompensesAbonne(ObjectId idAbonne)
         {
            var recompenses = new List<Recompense>();

            try
            {
                recompenses = _recompenseRepository.ObtenirRecompensesAbonne(idAbonne);
                foreach (var recompense in recompenses)
                {
                    recompense.TypeRecompense = _typeRecompenseService.ObtenirTypeRecompenses(recompense.IdTypeRecompense);
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return recompenses;
        }

        public bool AjouterRecompense(Recompense recompense)
        {
            try
            {
                return _recompenseRepository.AjouterRecompense(recompense);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return false;
        }
       
    }
}
