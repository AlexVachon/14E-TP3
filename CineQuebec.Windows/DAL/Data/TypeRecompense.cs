using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CineQuebec.Windows.DAL.Data
{
    public class TypeRecompense
    {
        public ObjectId Id { get; set; }    
        public string NomRecompense {  get; set; }

        public override string ToString()
        {
            return $"{NomRecompense} ";
        }
    }
}
