using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.View;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace CineQuebec.Windows.DAL.Repositories
{
    public class UserRepository:BaseRepository, IUserRepository
    {
        private IMongoCollection<Abonne> _collection;
       
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
            _collection = database.GetCollection<Abonne>("Abonnes");
        }

        /// <summary>
        /// Méthode qui assure la connexion avec un username et password, elle vérifie si le mot de passe saisie et haché est le même
        /// que celui haché dans la base de donnée
        /// </summary>
        /// <param name="pUsername"></param>
        /// <param name="pPassword"></param>
        /// <returns></returns>
        public virtual async Task<Abonne> ConnexionUtilisateur(string pUsername, string pPassword)
        {
            Abonne utilisateur = new Abonne();
            try
            {
                //POUR CRÉER DES ABONNÉES
                //var salt1 = CreerSalt();
                //Abonne abonne = new Abonne
                //{
                //    Id = ObjectId.Parse("66058251d38403bc569a548e"),
                //    Name = "ouiza",
                //    Email = "ouiza@gmail.com",
                //    Password = HacherMotDePasse("Admin@12", salt1),
                //    Salt = salt1,
                //    DateAdhesion = DateTime.Now,
                //    EstAdmin = false
                //};
                //var collection = database.GetCollection<User>("Abonnes");
                //collection.InsertOne(abonne);
               

                var filter = Builders<Abonne>.Filter.Eq("Name", pUsername);
                var Utilisateurs = _collection.Aggregate().ToList();
                utilisateur = await _collection.Find(filter).FirstOrDefaultAsync();

                if (utilisateur is null)
                    throw new UtilisateurNotFoundException("Le username est incorrect!");

                if (!utilisateur.Password.SequenceEqual(HacherMotDePasse(pPassword,  utilisateur.Salt))){
                    utilisateur = null;
                    throw new UtilisateurNotFoundException("Mot de passe incorrect!");
                }
            

            }
            catch (UtilisateurNotFoundException  ex){
				MessageBox.Show($"Erreur lors de la connexion: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
			}
            catch (Exception ex)
            {
                Console.WriteLine($"Impossible d'obtenir la collection {ex.Message}", "Connexion Administrateur");
            }

            return utilisateur;
        }

        /// <summary>
        /// Méthode qui hache le mot de passe 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="pSalt"></param>
        /// <returns></returns>
        public byte[] HacherMotDePasse(string password, byte[] pSalt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = pSalt,
                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 1024 * 1024
            };

            return argon2.GetBytes(16);
        }
        /// <summary>
        /// Méthode qui crée le salt pour le mot de passe 
        /// </summary>
        /// <returns></returns>
        public byte[] CreerSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }


    }
}
