using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.BLL.Services;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Repositories;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Tests
{
    public class ReservationServiceTests
    {
    
        [Fact]
        public void ObtenirReservationsAbonne_By_Id_Abonne()
        {
            Mock<IReservationRepository> mockRepo = new Mock<IReservationRepository>();
            Mock<IProjectionService> mockProjection = new Mock<IProjectionService>();  
            

            Abonne abonne = new Abonne { Id = ObjectId.GenerateNewId() };
            Projection projection = new Projection { Id = ObjectId.GenerateNewId()};

            List<Reservation> reservations = new List<Reservation> { new Reservation { IdAbonne = abonne.Id, IdProjection = projection.Id }, new Reservation { IdAbonne = abonne.Id, IdProjection = projection.Id } };

            mockRepo.Setup(x => x.ObtenirReservationsAbonne(abonne.Id)).Returns(reservations);
            mockProjection.Setup(y => y.ObtenirProjection(projection.Id)).Returns(projection);
            IReservationService reservationService = new ReservationService(mockRepo.Object, mockProjection.Object);

           
            List<Reservation> resultat = reservationService.ObtenirReservationsAbonne(abonne.Id);


            Assert.NotNull(resultat);
            Assert.Equal(reservations.Count, resultat.Count);
            Assert.Equal(reservations[0].IdProjection, resultat[0].IdProjection);
        }
    }
}
