using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Film
    {
        public ObjectId Id { get; set; }
        public DateTime DateSortieFilm { get; set; }     
        public bool EstAffiche { get; set; }
        public Categorie Categorie { get; set; }
        public List<Realisateur> Realisateurs { get; set; }
        public List<Acteur> Acteurs { get; set; }
        public string Nom {  get; set; }
        public double? NoteMoyenne { get; set; } = null;

        public Film()
        {
            Id = ObjectId.GenerateNewId();
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
