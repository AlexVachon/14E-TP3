using CineQuebec.Windows.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Repositories
{
    /// <summary>
    /// Un repository de base qu'on appel pour les autres repositories au besoin, c'est fais pour éviter de dupliquer du code 
    /// dans chaque méthode. en Gros elle fais la conncexion a la base de donnée.
    /// </summary>
    public class BaseRepository 
    {
        private IConfiguration _configuration;
        private IMongoClient mongoDBClient;
        protected IMongoDatabase database;
        private string connexionString = "";

        public BaseRepository(IConfiguration pConfiguration, IMongoClient client = null)
        {
            _configuration = pConfiguration;
            connexionString = _configuration["ConnectionString"]!;
            mongoDBClient = client ?? OuvrirConnexion();
            database = ConnectDatabase();
           
           
        }

        /// <summary>
        /// Méthode qui assure et ouvre la connexion, elle utilise la chaine de caractères connectionString pour pouvoir accéder 
        /// a la base de donnée
        /// </summary>
        /// <returns></returns>
        public IMongoClient OuvrirConnexion()
        {
            MongoClient dbClient = null;
            try
            {
                dbClient = new MongoClient(connexionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de se connecter à la base de données " + ex.Message, "Erreur");
            }
            return dbClient;
        }
        /// <summary>
        /// Méthode assure la connexion a la base de donnée
        /// </summary>
        /// <returns></returns>
        private IMongoDatabase ConnectDatabase()
        {
            IMongoDatabase db = null;
            try
            {
                db = mongoDBClient.GetDatabase("TP3_conception");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de se connecter à la base de données " + ex.Message, "Erreur");
            }
            return db;
        }
    }
}
