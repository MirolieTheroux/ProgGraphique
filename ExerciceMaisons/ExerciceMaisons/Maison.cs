using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciceMaisons
{
    internal class Maison
    {
        public int id { get; set; }
        public string Categorie { get; set; }
        public double Prix { get; set; }
        public string Ville { get; set; }

        public Proprietaire Proprietaire { get; set; }
    }
    internal class Proprietaire
    {
        public int id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

    }
}
