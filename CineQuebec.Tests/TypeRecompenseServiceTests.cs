using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Tests
{
    public class TypeRecompenseServiceTests
    {
        [Fact]
        public void ObtenirTypeRecompenses_By_Id_TypeRecompense()
        {
            Mock<ITypeRecompenseRepository> typeRecompenseRepo =    new Mock<ITypeRecompenseRepository>();
            TypeRecompense typeRecomponse = new TypeRecompense { Id = ObjectId.GenerateNewId() };
            List<TypeRecompense> typeRecompenses = new List<TypeRecompense> { new TypeRecompense { Id = typeRecomponse.Id }, new TypeRecompense { Id = typeRecomponse.Id} };

            typeRecompenseRepo.Setup(x => x.ObtenirTypeRecompenses(typeRecomponse.Id)).Returns(typeRecomponse);
            ITypeRecompenseService typeRecompenseService = new TypeRecompenseService(typeRecompenseRepo.Object);

            TypeRecompense result = typeRecompenseService.ObtenirTypeRecompenses(typeRecomponse.Id);

            Assert.NotNull(result);
            Assert.Equivalent(result, typeRecomponse);
        }
        [Fact]
        public void ObtenirToutTypesRecompenses_Retourne_Liste_Des_TypeRecompense()
        {

            Mock<ITypeRecompenseRepository> typeRecompenseRepo = new Mock<ITypeRecompenseRepository>();
            List<TypeRecompense> typeRecompenses = new List<TypeRecompense> { new TypeRecompense(), new TypeRecompense() };
            typeRecompenseRepo.Setup(x => x.ObtenirToutTypesRecompenses()).Returns(typeRecompenses);
            ITypeRecompenseService typeRecompenseService = new TypeRecompenseService(typeRecompenseRepo.Object);


            List<TypeRecompense> resultat = typeRecompenseService.ObtenirToutTypesRecompenses();


            Assert.NotNull(resultat);
            Assert.Equal(typeRecompenses.Count, resultat.Count);
        }
    }
}
