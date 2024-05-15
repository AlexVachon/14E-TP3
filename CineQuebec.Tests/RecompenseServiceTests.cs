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
    public class RecompenseServiceTests
    {
        [Fact]
        public void ObtenirRecompensesAbonne_By_Id_Abonne()
        {
            Mock<IRecompenseRepository> mockRepoRecomepnse = new Mock<IRecompenseRepository>();
            Mock<ITypeRecompenseService> mockTypeRecompenseService = new Mock<ITypeRecompenseService>();

            Abonne abonne = new Abonne { Id = ObjectId.GenerateNewId() };
            TypeRecompense typeRecompense = new TypeRecompense {  Id = ObjectId.GenerateNewId() };
            List<Recompense> recompenses = new List<Recompense> { new Recompense { IdAbonne = abonne.Id, IdTypeRecompense = typeRecompense.Id }, new Recompense { IdAbonne = abonne.Id, IdTypeRecompense = typeRecompense.Id } };

            mockRepoRecomepnse.Setup(x => x.ObtenirRecompensesAbonne(abonne.Id)).Returns(recompenses);
            mockTypeRecompenseService.Setup(y => y.ObtenirTypeRecompenses(typeRecompense.Id)).Returns(typeRecompense);
            IRecompenseService recompenseService = new RecompenseService(mockRepoRecomepnse.Object, mockTypeRecompenseService.Object);
            List<Recompense> resultat = recompenseService.ObtenirRecompensesAbonne(abonne.Id);

            Assert.NotNull(resultat);
            Assert.Equal(recompenses.Count, resultat.Count);
            Assert.Equal(recompenses[0].IdTypeRecompense, resultat[0].IdTypeRecompense);
        }
        [Fact]
        public void Ajouter_Recompense_De_Chaque_abonne_A_La_Liste_Recompenses()
        {

            Mock<IRecompenseRepository> recompenseRepo = new Mock<IRecompenseRepository>();
            Recompense recompense = new Recompense();
            List<Recompense> preferences = new List<Recompense>();
            recompenseRepo.Setup(x => x.AjouterRecompense(recompense)).Returns(true);
            IRecompenseService recompenseService = new RecompenseService(recompenseRepo.Object);

            bool result = recompenseService.AjouterRecompense(recompense);

            Assert.Equal(result, true);
        }
    }
}
