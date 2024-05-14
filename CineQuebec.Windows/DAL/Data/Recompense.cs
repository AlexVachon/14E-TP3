using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineQuebec.Windows.DAL.Data
{
    public class Recompense
    {
        public ObjectId Id { get; set; }
        public ObjectId IdTypeRecompense { get; set; }
        public TypeRecompense TypeRecompense { get; set; }
        public ObjectId IdAbonne { get; set; }

        //public Abonne Abonne { get; set; }
        public override string ToString()
        {
            if (TypeRecompense !=null)
            {
                return $"{TypeRecompense.NomRecompense}";
            }
            else { return string.Empty; }
           
        }
    }
}
