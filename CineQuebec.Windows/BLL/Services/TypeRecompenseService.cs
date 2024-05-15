using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.BLL.Services
{
    public class TypeRecompenseService:ITypeRecompenseService
    {
        private readonly ITypeRecompenseRepository _typeRecompenseRepository;

        public TypeRecompenseService(ITypeRecompenseRepository typeRecompenseRepo)
        {
            _typeRecompenseRepository = typeRecompenseRepo;
        }

        public TypeRecompense ObtenirTypeRecompenses(ObjectId idTypeRecompense)
        {
            var typeRecompense = new TypeRecompense();

            try
            {
                typeRecompense = _typeRecompenseRepository.ObtenirTypeRecompenses(idTypeRecompense);
               

            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return typeRecompense;
        }

        public List<TypeRecompense> ObtenirToutTypesRecompenses()
        {
            var typeRecompenses = new List<TypeRecompense>();

            try
            {
                typeRecompenses = _typeRecompenseRepository.ObtenirToutTypesRecompenses();
               

            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible d'obtenir la collection " + ex.Message, "Erreur");
            }
            return typeRecompenses;
        }
    }
}
