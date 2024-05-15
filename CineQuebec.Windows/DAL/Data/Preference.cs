using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
   public class Preference
    {
        public ObjectId Id { get; set; }
        public ObjectId IdAbonne { get; set; }
        public ObjectId IdActeur { get; set; }
        public ObjectId IdRealisateur { get; set; }
        public ObjectId IdCategorie { get; set; }
        public ObjectId IdFilm { get; set; }
        public Acteur Acteur { get; set; }
        public Realisateur Realisateur { get; set; }
        public Categorie Categorie { get; set; }
        public Film Film { get; set; }


        public override string ToString()
        {
            if (IdFilm !=ObjectId.Empty )
            {
                return $"Film : {Film.Nom}";
            }
            else if( IdRealisateur != ObjectId.Empty)
            {
                return $"Réalisateur : {Realisateur.NameRealisateur}";
            }
            else if (IdCategorie != ObjectId.Empty)
            {
                return $"Categorie : {Categorie.NameCategorie}";
            }
            else if(IdActeur != ObjectId.Empty)
            {
                return $"Acteur : {Acteur.NameActeur}";
            }
            else
            {
                return "";
            }
           
        }
    }
}
