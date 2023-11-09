using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPN1
{
    class Event
    {
        public string NomEvent { get; set; }
        public string DateEvent { get; set; }
        public string HeureEvent { get; set; }
        public string Adresse { get; set; }
        public string Categorie { get; set; }
        public bool EstPublic { get; set; }
        public string UrlImage { get; set; }


        public Event(string nomEvent, string dateEvent, string heureEvent, string adresse, 
            string categorie, string urlImage, bool estPublic)
        {
            this.NomEvent = nomEvent;
            this.DateEvent = dateEvent;
            this.HeureEvent = heureEvent;
            this.Adresse = adresse;
            this.Categorie = categorie;
            this.UrlImage = urlImage;
            this.EstPublic = estPublic;
        }

    }
}
